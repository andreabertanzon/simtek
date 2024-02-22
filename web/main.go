package main

import (
	"context"
	"fmt"
	"log"
	"time"

	"github.com/andreabertanzon/simtek/data"
	"github.com/andreabertanzon/simtek/models"
	"github.com/andreabertanzon/simtek/templates"
	"github.com/labstack/echo/v4"
	"github.com/labstack/echo/v4/middleware"
)

var repo data.InterventionRepository = data.NewSqliteInterventionRepository()

func main() {
	e := echo.New()

	e.Use(middleware.CORSWithConfig(middleware.CORSConfig{
		AllowOrigins: []string{"*"},
		AllowMethods: []string{"*"}}))

	e.GET("/", func(c echo.Context) error {
		// get the current date with the format DD-MM-YYYY
		today := time.Now().Format("02-01-2006")
		interventions, err := repo.GetInterventions()

		if err != nil {
			log.Println(err)
			return err
		}
		helloComp := templates.IndexCopy(today, interventions)
		helloComp.Render(context.Background(), c.Response().Writer)
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
		interventionInput := new(models.InterventionInput)
		if err := c.Bind(interventionInput); err != nil {
			log.Println(err)
			return err
		}
		err := repo.AddIntervention(interventionInput.ToDomainModel(""))
		if err != nil {
			log.Println(err)
			return err
		}
		interventions, err := repo.GetInterventions()
		if err != nil {
			log.Println(err)
			return err
		}

		component := templates.InterventionContainer(interventions)
		component.Render(context.Background(), c.Response().Writer)
		return nil
	})

	e.GET("/modify-intervention/:timestamp", func(c echo.Context) error {
		timestamp := c.Param("timestamp")
		fmt.Println("Timestamp To look for:", timestamp)
		intervention, err := repo.GetIntervention(timestamp)
		if err != nil {
			log.Println(err)
			return err
		}

		component := templates.ModifyIntervention(intervention)
		component.Render(context.Background(), c.Response().Writer)
		return nil
	})

	e.PUT("/intervention/:timestamp", func(c echo.Context) error {
		timestamp := c.Param("timestamp")
		fmt.Println("Timestamp To look for:", timestamp)

		interventionInput := new(models.InterventionInput)
		if err := c.Bind(interventionInput); err != nil {
			log.Println(err)
			return err
		}

		err := repo.UpdateIntervention(timestamp, interventionInput.ToDomainModel(timestamp))
		if err != nil {
			log.Println(err)
			return err
		}

		interventions, err := repo.GetInterventions()
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
