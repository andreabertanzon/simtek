package main

import (
	"context"
	"log"
	"time"

	"github.com/andreabertanzon/simtek/templates"
	"github.com/labstack/echo/v4"
	"github.com/labstack/echo/v4/middleware"
)

type Intervention struct {
	Site         string   `form:"site"`
	Intervention string   `form:"intervention"`
	Materials    []string `form:"materials[]"`
	Workers      []string `form:"workers[]"`
}

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

	e.GET("/new-material-input", func(c echo.Context) error {
		component := templates.MaterialInput()
		component.Render(context.Background(), c.Response().Writer)
		return nil
	})

	e.GET("/new-worker-input", func(c echo.Context) error {
		component := templates.WorkerInput()
		component.Render(context.Background(), c.Response().Writer)
		return nil
	})

	e.GET("/new-intervention", func(c echo.Context) error {
		component := templates.AddInterventionForm()
		component.Render(context.Background(), c.Response().Writer)
		return nil
	})

	e.POST("/new-intervention", func(c echo.Context) error {
		intervention := new(Intervention)
		if err := c.Bind(intervention); err != nil {
			log.Println(err)
			return err
		}
		return c.JSON(200, intervention)
	})

	e.Static("/css", "css")
	e.Logger.Fatal(e.Start(":6006"))
}
