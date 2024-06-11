package data

type SqliteInterventionRepository struct {
}

func NewSqliteInterventionRepository() *SqliteInterventionRepository {
	return &SqliteInterventionRepository{}
}

func (ir *SqliteInterventionRepository) AddIntervention(intervention Intervention) error {
	db, err := NewDatabase()
	if err != nil {
		return err
	}

	return db.AddIntervention(intervention)
}

func (ir *SqliteInterventionRepository) GetInterventions(guid string) ([]Intervention, error) {
	db, err := NewDatabase()
	if err != nil {
		return nil, err
	}

	return db.GetInterventions(guid)
}

func (ir *SqliteInterventionRepository) GetIntervention(guid string) (Intervention, error) {
	db, err := NewDatabase()
	if err != nil {
		return Intervention{}, err
	}

	return db.GetIntervention(guid)
}

func (ir *SqliteInterventionRepository) UpdateIntervention(guid string, intervention Intervention) error {
	db, err := NewDatabase()
	if err != nil {
		return err
	}

	return db.UpdateIntervention(guid, intervention)
}
