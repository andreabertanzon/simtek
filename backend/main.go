package main

import (
	"fmt"
	"net/http"
)

type Test string

func (t Test) ServeHTTP(w http.ResponseWriter, r *http.Request) {
	fmt.Fprint(w, "Hello, from server!")
}

func main() {
	fmt.Println("listening at 8080")
	// multiplexer
	mux := http.NewServeMux()
	var t Test
	// handle
	mux.Handle("/", t)

	mux.HandleFunc("/test", func(w http.ResponseWriter, r *http.Request) {
		fmt.Fprintf(w, "Test Endpoint")
	})

	// server
	server := http.Server{
		Addr:    ":8080",
		Handler: mux,
	}
	server.ListenAndServe()
}
