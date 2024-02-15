package models

type Intervention struct {
	Site         string   `form:"site"`
	Intervention string   `form:"intervention"`
	Materials    []string `form:"materials[]"`
	Workers      []string `form:"workers[]"`
}
