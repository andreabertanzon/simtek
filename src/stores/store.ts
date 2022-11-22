// import type { Site } from '../models/site'
import { writable } from 'svelte/store'
import type { Client } from '../models/client'
import type { intervention } from '../models/intervention'
import type { Worker } from '../models/Worker'
import type { Material } from '../models/material'

interface rStore {
  interventions: intervention[],
  clients: Client[],
  workers: Worker[],
  materials: Material[]
}
const _rStore = writable<rStore>({
  clients: [
    { name: "Gino", surname: "Peppino", address: "", city: "", cap: "", uniqueCode: "", vat: "", email: "", phone: "", sites: [{ Name: "Via Ernestina", Address: "Via Ernestina 67", City: "Villafranca di Verona", Cap: "37069" }] },
    { name: "Susie", surname: "Storm", address: "", city: "", cap: "", uniqueCode: "", vat: "", email: "", phone: "", sites: [] },
    { name: "Luca", surname: "Marenzio", address: "", city: "", cap: "", uniqueCode: "", vat: "", email: "", phone: "", sites: [] },
    { name: "Luca", surname: "Ambrosi", address: "", city: "", cap: "", uniqueCode: "", vat: "", email: "", phone: "", sites: [] },
    { name: "Luca", surname: "Schiappa", address: "", city: "", cap: "", uniqueCode: "", vat: "", email: "", phone: "", sites: [] },
    { name: "Riccardo", surname: "Tarallucci", address: "", city: "", cap: "", uniqueCode: "", vat: "", email: "", phone: "", sites: [] },
    { name: "Paolo", surname: "Piubello", address: "", city: "", cap: "", uniqueCode: "", vat: "", email: "", phone: "", sites: [] },
    { name: "Stefania", surname: "Marenzio", address: "", city: "", cap: "", uniqueCode: "", vat: "", email: "", phone: "", sites: [] },
    { name: "Piergiorgio", surname: "Merola", address: "", city: "", cap: "", uniqueCode: "", vat: "", email: "", phone: "", sites: [] },
    { name: "Fabrizio", surname: "Merola", address: "", city: "", cap: "", uniqueCode: "", vat: "", email: "", phone: "", sites: [] },
    { name: "Andrea", surname: "Sfigato", address: "", city: "", cap: "", uniqueCode: "", vat: "", email: "", phone: "", sites: [] },
    { name: "Pierpaolo", surname: "Pasolini", address: "", city: "", cap: "", uniqueCode: "", vat: "", email: "", phone: "", sites: [] },
    { name: "Francesco", surname: "Burione", address: "", city: "", cap: "", uniqueCode: "", vat: "", email: "", phone: "", sites: [] },
    { name: "Luca", surname: "Foffa", address: "", city: "", cap: "", uniqueCode: "", vat: "", email: "", phone: "", sites: [] },
    { name: "Simona", surname: "Topazio", address: "", city: "", cap: "", uniqueCode: "", vat: "", email: "", phone: "", sites: [] },

  ],
  interventions: [
    { date: "12-10-2022", description: "questa e' una descrizione di cosa ho fatto", spentHours: 6, completed: false },
    { date: "12-11-2022", description: "sostituiti i radiatori al piano terra", spentHours: 24, completed: false },
    { date: "10-10-2022", description: "messi i nuovi condizionatori", spentHours: 12, completed: false },
    { date: "12-10-2022", description: "cambiati comandi dei cancelli della struttura", spentHours: 12, completed: true },
    { date: "12-10-2022", description: "modificato comando cancello principale", spentHours: 5, completed: true },
    { date: "12-10-2022", description: "iniziato passaggio fili cancello esterno", spentHours: 8, completed: true },
    { date: "12-10-2022", description: "cambiate le guarnizioni ai termo", spentHours: 2, completed: false },
    { date: "12-10-2022", description: "sostituiti i rubinetti nella parte nuova", spentHours: 44, completed: false },
    { date: "12-10-2022", description: "cambiate le lampade nelle camere", spentHours: 1, completed: true },
  ],
  workers: [
    { Name: 'Simone Bonfante', PricePerHour: 30 },
    { Name: 'Cristian Bonfante', PricePerHour: 30 }
  ],
  materials: [
    { Name: "Plana: interruttore", Company: "Vimar", Unit: 'pz', Price: 2.00 },
    { Name: "Arke: interruttore", Company: "Vimar", Unit: 'pz', Price: 2.00 },
    { Name: "Idea: interruttore", Company: "Vimar", Unit: 'pz', Price: 2.00 },
    { Name: "Eikon: interruttore", Company: "Vimar", Unit: 'pz', Price: 2.00 },
    { Name: "Linea: interruttore", Company: "Vimar", Unit: 'pz', Price: 2.00 },
  ]
})
export const rootStore = {
  subscribe: _rStore.subscribe,
  set: _rStore.set,
  update: _rStore.update,
  addClient: (client: Client) =>
    _rStore.update((self: rStore) => {
      self.clients.push(client)
      return self
    }),
  addIntervention: (intv: intervention) =>
    _rStore.update((self: rStore) => {
      self.interventions.push(intv);
      return self
    }),
}

