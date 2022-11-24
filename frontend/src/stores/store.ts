// import type { Site } from '../models/site'
import { writable } from 'svelte/store'
import type { Client } from '../models/client'
import { CurrentInterventionStruct, InterventionStruct, type CurrentIntervention, type Intervention } from '../models/intervention'
import type { Worker } from '../models/Worker'
import type { Material } from '../models/material'
import { z } from 'zod'

interface rStore {
        interventions: Intervention[],
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
        interventions: [],
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
        addIntervention: (intv: CurrentIntervention) =>
                _rStore.update((self: rStore) => {
                        const value: Intervention = InterventionStruct.parse(intv)
                        self.interventions.push(value);
                        return self
                }),
}

