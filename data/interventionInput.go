package data

import (
	"strconv"
	"strings"
	"time"

	"github.com/google/uuid"
)

type InterventionInput struct {
	Guid         string
	Site         string   `form:"site"`
	Intervention string   `form:"intervention"`
	Materials    []string `form:"materials[]"`
	Quantity     []int    `form:"quantity[]"`
	Umeasure     []string `form:"umeasure[]"`
	Workers      []string `form:"workers[]"`
	Notes        string   `form:"notes"`
	Timestamp    string
}

// Converts the InterventionInput to a Intervention domain model
func (interventionInput *InterventionInput) ToDomainModel() Intervention {
	var intervention Intervention
	intervention.Site = interventionInput.Site
	descriptions := strings.Split(interventionInput.Intervention, "\n")
	intervention.Descriptions = descriptions

	for i, material := range interventionInput.Materials {
		if material == "" {
			continue
		}
		umeasure := interventionInput.Umeasure[i]
		quantity := interventionInput.Quantity[i]
		materialAndQuantityStr := material + "|" + umeasure + "|" + strconv.Itoa(quantity)
		intervention.Materials = append(intervention.Materials, materialAndQuantityStr)
	}

	workers := make([]map[string]int, 0)
	for _, worker := range interventionInput.Workers {
		splittedWorker := strings.Split(worker, " ")
		if len(splittedWorker) > 1 {
			workerName := splittedWorker[0]
			if workerName == "" {
				workerName = "Simone"
			}
			workerHours, err := strconv.Atoi(splittedWorker[1])
			if err != nil {
				workerHours = 0
			}

			workers = append(workers, map[string]int{workerName: workerHours})
		} else if len(splittedWorker) == 1 {
			workerName := splittedWorker[0]
			if workerName == "" {
				workerName = "Simone"
			}
			workers = append(workers, map[string]int{workerName: 0})
		} else {
			workers = append(workers, map[string]int{"Simone": 0})
		}
	}

	stateRepository := NewStateRepository()
	timestamp, err := stateRepository.GetState()
	if err != nil {
		timestamp = time.Now().Format("2006-01-02")
	}

	intervention.Timestamp = timestamp

	intervention.Workers = workers

	if interventionInput.Guid == "" {
		intervention.Guid = uuid.New().String()
	} else {
		intervention.Guid = interventionInput.Guid
	}

	intervention.Notes = interventionInput.Notes

	return intervention
}
