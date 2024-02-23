package data

import (
	"database/sql"
	"encoding/json"
	"time"

	_ "github.com/mattn/go-sqlite3"
)

type Database struct {
}

func NewDatabase() (*Database, error) {
	obj := &Database{}
	db, err := sql.Open("sqlite3", "simtek.db")
	if err != nil {
		return nil, err
	}
	defer db.Close()

	sqlStmt := `
	create table if not exists interventions (
		id integer not null primary key autoincrement,
		guid text not null unique,
		timestamp text,
		details text
	);
	`
	_, err = db.Exec(sqlStmt)
	if err != nil {
		return nil, err
	}

	createStateTable := `
	create table if not exists state (
		id integer not null primary key autoincrement,
		timestamp text,
		last_updated text
	);
	`
	db.Exec(createStateTable)
	if err != nil {
		return nil, err
	}
	return obj, nil
}

func (d *Database) AddIntervention(intervention Intervention) error {
	db, err := sql.Open("sqlite3", "simtek.db")
	if err != nil {
		return err
	}
	defer db.Close()

	details, err := json.Marshal(intervention)
	if err != nil {
		return err
	}

	insertStmt := `
	INSERT INTO interventions (guid, timestamp, details) VALUES (?, ?, ?)
	`

	_, err = db.Exec(insertStmt, intervention.Guid, intervention.Timestamp, string(details))
	if err != nil {
		return err
	}

	return nil
}

func (d *Database) GetInterventions(timestamp string) ([]Intervention, error) {
	var today string
	if timestamp == "" {
		today = time.Now().Format("02-01-2006")
	} else {
		today = timestamp
	}

	db, err := sql.Open("sqlite3", "simtek.db")
	if err != nil {
		return nil, err
	}
	defer db.Close()

	rows, err := db.Query("SELECT * FROM interventions WHERE timestamp = ?", today)
	if err != nil {
		return nil, err
	}

	var interventions []Intervention
	for rows.Next() {
		var id int
		var guid string
		var timestamp string
		var details string
		err = rows.Scan(&id, &guid, &timestamp, &details)
		if err != nil {
			return nil, err
		}
		intervention := Intervention{}
		err = json.Unmarshal([]byte(details), &intervention)
		if err != nil {
			return nil, err
		}

		interventions = append(interventions, intervention)

	}

	return interventions, nil
}

func (d *Database) GetIntervention(guid string) (Intervention, error) {
	db, err := sql.Open("sqlite3", "simtek.db")
	if err != nil {
		return Intervention{}, err
	}
	defer db.Close()

	var id int
	var details string
	var timestamp string
	err = db.QueryRow("SELECT * FROM interventions WHERE guid = ?", guid).Scan(&id, &guid, &timestamp, &details)
	if err != nil {
		return Intervention{}, err
	}

	intervention := Intervention{}
	err = json.Unmarshal([]byte(details), &intervention)
	if err != nil {
		return Intervention{}, err
	}

	return intervention, nil
}

func (d *Database) UpdateIntervention(guid string, intervention Intervention) error {
	db, err := sql.Open("sqlite3", "simtek.db")
	if err != nil {
		return err
	}
	defer db.Close()

	details, err := json.Marshal(intervention)
	if err != nil {
		return err
	}

	updateStmt := `
	UPDATE interventions SET details = ? WHERE guid = ?
	`
	_, err = db.Exec(updateStmt, string(details), guid)
	if err != nil {
		return err
	}

	return nil
}

func (*Database) UpsertState(timestamp string) error {
	selectSTMT := "SELECT id FROM state WHERE id = 1"
	db, err := sql.Open("sqlite3", "simtek.db")
	if err != nil {
		return err
	}
	defer db.Close()

	row, err := db.Query(selectSTMT)
	if err != nil {
		return err
	}

	var id int
	for row.Next() {
		err = row.Scan(&id)
		if err != nil {
			return err
		}
	}

	if id == 0 {
		_, err = db.Exec("INSERT INTO state (timestamp, last_updated) VALUES (?, ?)", timestamp, time.Now().Format("02-01-2006 15:04:05"))
		return err
	} else {
		_, err = db.Exec("UPDATE state SET timestamp = ?, last_updated = ? WHERE id = 1", timestamp, time.Now().Format("02-01-2006 15:04:05"))
		return err
	}
}

func (*Database) GetState() (string, error) {
	db, err := sql.Open("sqlite3", "simtek.db")
	if err != nil {
		return "", err
	}
	defer db.Close()

	row, err := db.Query("SELECT timestamp FROM state WHERE id = 1")
	if err != nil {
		return "", err
	}
	var state string
	for row.Next() {
		err = row.Scan(&state)
		if err != nil {
			return "", err
		}
	}

	return state, nil
}
