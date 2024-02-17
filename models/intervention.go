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
}

type Intervention struct {
	Site         string           `json:"site"`
	Descriptions []string         `json:"descriptions"`
	Materials    []string         `json:"materials"`
	Workers      []map[string]int `json:"workers"`
	Timestamp    string           `json:"timestamp"`
}

func (interventionInput *InterventionInput) ToDomainModel() Intervention {
	var intervention Intervention
	intervention.Site = interventionInput.Site
	descriptions := strings.Split(interventionInput.Intervention, "\n")
	intervention.Descriptions = descriptions

	intervention.Materials = interventionInput.Materials
	workers := make([]map[string]int, 0)
	for _, worker := range interventionInput.Workers {
		splittedWorker := strings.Split(worker, " ")
		if len(splittedWorker) > 1 {
			workerName := splittedWorker[0]
			workerHours, err := strconv.Atoi(splittedWorker[1])
			if err != nil {
				workerHours = 0
			}

			workers = append(workers, map[string]int{workerName: workerHours})
		} else if len(splittedWorker) == 1 {
			workerName := splittedWorker[0]
			workers = append(workers, map[string]int{workerName: 0})
		} else {
			workers = append(workers, map[string]int{"Simone": 0})
		}
	}

	intervention.Workers = workers
	intervention.Timestamp = time.Now().Format("02-01-2006:15:04:05")

	return intervention
}

func (intervention *Intervention) TotalHours() int {
	totalHours := 0
	for _, worker := range intervention.Workers {
		for _, hours := range worker {
			totalHours += hours
		}
	}
	return totalHours
}
