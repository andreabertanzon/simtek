package data

import (
	"errors"

	"github.com/andreabertanzon/simtek/models"
)

var interventions []models.Intervention

type InterventionRepositoryImpl struct {
}

func NewInterventionRepository() InterventionRepository {
	return &InterventionRepositoryImpl{}
}

func (ir *InterventionRepositoryImpl) AddIntervention(intervention models.Intervention) error {
	interventions = append(interventions, intervention)
	return nil
}

func (ir *InterventionRepositoryImpl) GetInterventions() ([]models.Intervention, error) {
	return interventions, nil
}

func (ir *InterventionRepositoryImpl) GetIntervention(timestamp string) (models.Intervention, error) {
	for _, intervention := range interventions {
		if intervention.Timestamp == timestamp {
			return intervention, nil
		}
	}
	return models.Intervention{}, errors.New("intervention not found")
}
