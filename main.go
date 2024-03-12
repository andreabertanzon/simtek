package main

import (
	"context"
	"fmt"
	"log"

	"github.com/andreabertanzon/simtek/data"
	"github.com/andreabertanzon/simtek/templates"
	"github.com/google/uuid"
	"github.com/labstack/echo/v4"
	"github.com/labstack/echo/v4/middleware"
)

var repo data.InterventionRepository = data.NewSqliteInterventionRepository()

func main() {
	// stateRepo := data.NewStateRepository()
	// err := stateRepo.UpsertState(time.Now().Format("2006-01-02"))
	// if err != nil {
	// 	log.Fatal(err)
	// }
	e := echo.New()

	e.Use(middleware.CORSWithConfig(middleware.CORSConfig{
		AllowOrigins: []string{"*"},
		AllowMethods: []string{"*"}}))

	e.GET("/", func(c echo.Context) error {
		// get the current date with the format DD-MM-YYYY

		// upsert state
		stateRepo := data.NewStateRepository()
		state, err := stateRepo.GetState()
		if err != nil {
			log.Println(err)
			return err
		}

		interventions, err := repo.GetInterventions(state)

		if err != nil {
			log.Println(err)
			return err
		}
		helloComp := templates.IndexCopy(state, interventions)
		helloComp.Render(context.Background(), c.Response().Writer)
		return nil
	})

	e.PUT("/date", func(c echo.Context) error {
		stateRepo := data.NewStateRepository()
		date := c.FormValue("date")
		err := stateRepo.UpsertState(date)
		if err != nil {
			log.Println(err)
			return err
		}

		interventionRepo := data.NewSqliteInterventionRepository()
		interventions, err := interventionRepo.GetInterventions(date)
		if err != nil {
			log.Println(err)
			return err
		}

		template := templates.IndexCopy(date, interventions)
		template.Render(context.Background(), c.Response().Writer)
		return nil
	})

	e.GET("/dynamic-input", func(c echo.Context) error {
		formType := c.QueryParam("type")
		if formType == "" {
			return c.Redirect(302, "/")
		}
		component := templates.DynamicFormInput(formType)
		component.Render(context.Background(), c.Response().Writer)
		return nil
	})

	e.GET("/new-intervention", func(c echo.Context) error {
		component := templates.AddInterventionForm()
		component.Render(context.Background(), c.Response().Writer)
		return nil
	})

	e.POST("/new-intervention", func(c echo.Context) error {
		interventionInput := new(data.InterventionInput)
		if err := c.Bind(interventionInput); err != nil {
			log.Println(err)
			return err
		}

		log.Printf("Intervention Input (After Binding): %+v", interventionInput)

		guid := uuid.New().String()
		interventionInput.Guid = guid

		err := repo.AddIntervention(interventionInput.ToDomainModel())
		if err != nil {
			log.Println(err)
			return err
		}

		stateRepo := data.NewStateRepository()
		state, err := stateRepo.GetState()
		if err != nil {
			log.Println(err)
			return err
		}

		interventions, err := repo.GetInterventions(state)
		if err != nil {
			log.Println(err)
			return err
		}

		component := templates.InterventionContainer(interventions)
		component.Render(context.Background(), c.Response().Writer)
		return nil
	})

	e.GET("/modify-intervention/:guid", func(c echo.Context) error {
		guid := c.Param("guid")
		fmt.Println("Started Modifying intervention:", guid)
		intervention, err := repo.GetIntervention(guid)
		if err != nil {
			log.Println(err)
			return err
		}

		component := templates.ModifyIntervention(intervention)
		component.Render(context.Background(), c.Response().Writer)
		return nil
	})

	e.PUT("/intervention/:guid", func(c echo.Context) error {
		guid := c.Param("guid")
		fmt.Println("UPDATING intervention:", guid)

		interventionInput := new(data.InterventionInput)
		if err := c.Bind(interventionInput); err != nil {
			log.Println(err)
			return err
		}

		interventionInput.Guid = guid

		err := repo.UpdateIntervention(guid, interventionInput.ToDomainModel())
		if err != nil {
			log.Println(err)
			return err
		}

		stateRepo := data.NewStateRepository()
		state, err := stateRepo.GetState()
		if err != nil {
			log.Println(err)
			return err
		}

		interventions, err := repo.GetInterventions(state)
		if err != nil {
			log.Println(err)
			return err
		}

		component := templates.InterventionContainer(interventions)
		component.Render(context.Background(), c.Response().Writer)
		return nil
	})

	e.Static("/css", "css")
	e.Logger.Fatal(e.Start(":6006"))
}
