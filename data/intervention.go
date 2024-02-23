package data

import "fmt"

type Intervention struct {
	Guid         string           `json:"guid"`
	Site         string           `json:"site"`
	Descriptions []string         `json:"descriptions"`
	Materials    []string         `json:"materials"`
	Workers      []map[string]int `json:"workers"`
	Notes        string           `json:"notes"`
	Timestamp    string           `json:"timestamp"`
}

// Returns the total hours of the intervention
func (intervention *Intervention) TotalHours() int {
	totalHours := 0
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

	interventionInput.Materials = append(interventionInput.Materials, intervention.Materials...)

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
