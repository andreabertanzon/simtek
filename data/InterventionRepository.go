package data

type InterventionRepository interface {
	AddIntervention(intervention Intervention) error
	GetInterventions(timestamp string) ([]Intervention, error)
	GetIntervention(guid string) (Intervention, error)
	UpdateIntervention(guid string, intervention Intervention) error
}
