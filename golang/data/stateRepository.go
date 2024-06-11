package data

type StateRepository struct {
}

func NewStateRepository() *StateRepository {
	return &StateRepository{}
}

func (sr *StateRepository) GetState() (string, error) {
	db, err := NewDatabase()
	if err != nil {
		return "", err
	}
	state, err := db.GetState()
	if err != nil {
		return "", err
	}
	return state, nil
}

func (sr *StateRepository) UpsertState(state string) error {
	db, err := NewDatabase()
	if err != nil {
		return err
	}
	return db.UpsertState(state)
}
