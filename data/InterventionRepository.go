package data

import "github.com/andreabertanzon/simtek/models"

type InterventionRepository interface {
	// AddIntervention adds a new intervention to the repository
	AddIntervention(intervention models.Intervention) error
	// GetInterventions returns all the interventions in the repository
	GetInterventions() ([]models.Intervention, error)
	GetIntervention(timestamp string) (models.Intervention, error)
}
