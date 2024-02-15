package data

import "github.com/andreabertanzon/simtek/models"

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
