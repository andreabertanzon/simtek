package data

type InterventionInput struct {
	Guid         string
	Site         string   `form:"site"`
	Intervention string   `form:"intervention"`
	Materials    []string `form:"materials[]"`
	Workers      []string `form:"workers[]"`
	Notes        string   `form:"notes"`
	Timestamp    string
}
