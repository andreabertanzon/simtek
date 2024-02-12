package main

import (
	"context"

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
		helloComp := templates.Index()
		helloComp.Render(context.Background(), c.Response().Writer)
		return nil
	})

	e.Static("/css", "css")
	e.Logger.Fatal(e.Start(":8083"))

}
