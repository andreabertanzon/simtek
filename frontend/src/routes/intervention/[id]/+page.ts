import type { Client } from "src/models/client"
import { intervention } from "../../../models/intervention"

/** @type {import('./$types').PageLoad} */
export function load({ params }) {
  let currentIntervention: intervention
  if (params.id === -1) {
    currentIntervention = new intervention("", 0, "", false)
  } else {
    currentIntervention = new intervention("", 0, "", false)
  }
  const clients: Client[] = [
    { name: "Gino", surname: "Peppino" },
    { name: "Susie", surname: "Storm" },
    { name: "Luca", surname: "Marenzio" },
    { name: "Andrea", surname: "Bertanzon" },
    { name: "Simone", surname: "Bonfante" },
    { name: "Valentina", surname: "Bertanzon" },
    { name: "Francesco", surname: "Bertanzon" },
  ]

  return {
    currentIntervention: currentIntervention,
    clients: clients
  }
}
