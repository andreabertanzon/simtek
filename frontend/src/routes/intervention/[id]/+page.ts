import type { Client } from "src/models/client"
import type { CurrentIntervention } from "../../../models/intervention"

/** @type {import('./$types').pageload} */
export function load({ params }) {
  console.log(params.id)
  // let currentintervention: intervention
  // if (params.id === -1) {
  //   currentintervention = new intervention("", 0, "", false)
  // } else {
  //   currentintervention = new intervention("", 0, "", false)
  // }
  // const clients: client[] = [
  //   { name: "gino", surname: "peppino" },
  //   { name: "susie", surname: "storm" },
  //   { name: "luca", surname: "marenzio" },
  //   { name: "andrea", surname: "bertanzon" },
  //   { name: "simone", surname: "bonfante" },
  //   { name: "valentina", surname: "bertanzon" },
  //   { name: "francesco", surname: "bertanzon" },
  // ]
  let currentIntervention: CurrentIntervention = {
    date: "",
    title: "",
    spentHours: 0,
    materialCost: 0,
    workCost: 0,
    completed: false,
    numberOfWorkers: 0,
    clientName: "",
    descriptions: [],
    workers: [],
    notes: [],
    materials: []
  }
  return {
    currentintervention: currentIntervention,
  }
}
