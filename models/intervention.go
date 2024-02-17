package models

type Intervention struct {
	Site         string           `json:"site"`
	Descriptions []string         `json:"descriptions"`
	Materials    []string         `json:"materials"`
	Workers      []map[string]int `json:"workers"`
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
