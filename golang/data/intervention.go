package data

import (
	"fmt"
	"strconv"
	"strings"
)

type Intervention struct {
	Guid         string               `json:"guid"`
	Site         string               `json:"site"`
	Descriptions []string             `json:"descriptions"`
	Materials    []string             `json:"materials"`
	Workers      []map[string]float32 `json:"workers"`
	Notes        string               `json:"notes"`
	Timestamp    string               `json:"timestamp"`
}

// Returns the total hours of the intervention
func (intervention *Intervention) TotalHours() float32 {
	var totalHours float32 = 0
	for _, worker := range intervention.Workers {
		for _, hours := range worker {
			totalHours += hours
		}
	}
	return totalHours
}

func (intervention *Intervention) ToViewModel() InterventionInput {
	var interventionInput InterventionInput
	interventionInput.Site = intervention.Site

	for _, description := range intervention.Descriptions {
		interventionInput.Intervention += description + "\n"
	}

	for _, material := range intervention.Materials {
		materialParts := strings.Split(material, "|")
		if len(materialParts) == 3 {
			qty, err := strconv.ParseFloat(materialParts[2], 32)
			if err != nil {
				qty = 0
			}
			interventionInput.Quantity = append(interventionInput.Quantity, float32(qty))
			interventionInput.Umeasure = append(interventionInput.Umeasure, materialParts[1])
			interventionInput.Materials = append(interventionInput.Materials, materialParts[0])
		}
	}

	for _, worker := range intervention.Workers {
		for name, hours := range worker {
			interventionInput.Workers = append(interventionInput.Workers, name+" "+fmt.Sprintf("%v", hours))
		}
	}

	interventionInput.Notes = intervention.Notes

	interventionInput.Timestamp = intervention.Timestamp
	interventionInput.Guid = intervention.Guid

	return interventionInput
}
