package main

import (
	"context"
	"log"
	"time"

	"github.com/andreabertanzon/simtek/data"
	"github.com/andreabertanzon/simtek/models"
	"github.com/andreabertanzon/simtek/templates"
	"github.com/labstack/echo/v4"
	"github.com/labstack/echo/v4/middleware"
)

func main() {
	e := echo.New()

	e.Use(middleware.CORSWithConfig(middleware.CORSConfig{
		AllowOrigins: []string{"*"},
		AllowMethods: []string{"*"}}))

	e.GET("/", func(c echo.Context) error {
		// get the current date with the format DD-MM-YYYY
		today := time.Now().Format("02-01-2006")
		helloComp := templates.Index(today)
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
		intervention := new(models.Intervention)
		if err := c.Bind(intervention); err != nil {
			log.Println(err)
			return err
		}
		interventionRepository := data.NewInterventionRepository()
		err := interventionRepository.AddIntervention(*intervention)
		if err != nil {
			log.Println(err)
			return err
		}
		interventions, err := interventionRepository.GetInterventions()
		if err != nil {
			log.Println(err)
			return err
		}
		return c.JSON(200, interventions)
	})

	e.Static("/css", "css")
	e.Logger.Fatal(e.Start(":6006"))
}
