package data

import (
	"fmt"
	"strconv"
	"strings"
	"time"

	"github.com/google/uuid"
)

type InterventionInput struct {
	Guid         string
	Site         string    `form:"site"`
	Intervention string    `form:"intervention"`
	Materials    []string  `form:"materials[]"`
	Quantity     []float32 `form:"quantity[]"`
	Umeasure     []string  `form:"umeasure[]"`
	Workers      []string  `form:"workers[]"`
	Notes        string    `form:"notes"`
	Timestamp    string
}

// Converts the InterventionInput to a Intervention domain model
func (interventionInput *InterventionInput) ToDomainModel() Intervention {
	var intervention Intervention
	intervention.Site = interventionInput.Site
	descriptions := strings.Split(interventionInput.Intervention, "\n")
	var cleanedDescriptions []string
	for _, description := range descriptions {
		if description == "" || description == " " || description == "\n" {
			continue
		}
		cleanedDescriptions = append(cleanedDescriptions, description)
	}

	intervention.Descriptions = cleanedDescriptions

	for i, material := range interventionInput.Materials {
		if material == "" {
			continue
		}
		umeasure := interventionInput.Umeasure[i]
		quantity := interventionInput.Quantity[i]
		materialAndQuantityStr := material + "|" + umeasure + "|" + strconv.FormatFloat(float64(quantity), 'f', -1, 32)
		intervention.Materials = append(intervention.Materials, materialAndQuantityStr)
	}

	workers := make([]map[string]float32, 0)
	for _, worker := range interventionInput.Workers {
		splittedWorker := strings.Split(worker, " ")
		if len(splittedWorker) > 1 {
			workerName := splittedWorker[0]
			if workerName == "" {
				workerName = "Simone"
			}
			hoursString := strings.Replace(".", ".", splittedWorker[1], -1)
			workerHoursFloat64, err := strconv.ParseFloat(hoursString, 32)
			if err != nil {
				fmt.Println(err)
				workerHoursFloat64 = 0
			}
			workerHours := float32(workerHoursFloat64)

			workers = append(workers, map[string]float32{workerName: workerHours})
		} else if len(splittedWorker) == 1 {
			workerName := splittedWorker[0]
			if workerName == "" {
				workerName = "Simone"
			}
			workers = append(workers, map[string]float32{workerName: 0})
		} else {
			workers = append(workers, map[string]float32{"Simone": 0})
		}
	}

	stateRepository := NewStateRepository()
	timestamp, err := stateRepository.GetState()
	if err != nil {
		timestamp = time.Now().Format("2006-01-02")
	}

	intervention.Timestamp = timestamp
	fmt.Printf("Adding %s to db with Timestamp: %s", intervention.Guid, intervention.Timestamp)

	intervention.Workers = workers

	if interventionInput.Guid == "" {
		intervention.Guid = uuid.New().String()
	} else {
		intervention.Guid = interventionInput.Guid
	}

	intervention.Notes = interventionInput.Notes

	return intervention
}
