package models

import (
	"strconv"
	"strings"
	"time"
)

type InterventionInput struct {
	Site         string   `form:"site"`
	Intervention string   `form:"intervention"`
	Materials    []string `form:"materials[]"`
	Workers      []string `form:"workers[]"`
	Notes        string   `form:"notes"`
	Timestamp    string
}

// Converts the InterventionInput to a Intervention domain model
func (interventionInput *InterventionInput) ToDomainModel(timestamp string) Intervention {
	var intervention Intervention
	intervention.Site = interventionInput.Site
	descriptions := strings.Split(interventionInput.Intervention, "\n")
	intervention.Descriptions = descriptions

	for _, material := range interventionInput.Materials {
		if material == "" {
			continue
		}
		intervention.Materials = append(intervention.Materials, material)
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

	intervention.Workers = workers
	if timestamp != "" {
		intervention.Timestamp = timestamp
	} else {
		intervention.Timestamp = time.Now().Format("02-01-2006:15:04:05")
	}

	intervention.Notes = interventionInput.Notes

	return intervention
}
