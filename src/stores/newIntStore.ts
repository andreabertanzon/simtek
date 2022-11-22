import type { intervention } from "../models/intervention"
import type { Client } from "../models/client";
import { writable } from "svelte/store";
import type { Site } from "../models/site";

interface NewInterventionState {
  step: 1 | 2 | 3 | 4,
  currentIntervention?: intervention | null,
  choosenClient?: Client | null,
  choosenSite?: Site | null
}
const _newInterventionStore = writable<NewInterventionState>({
  step: 1,
  currentIntervention: null,
  choosenClient: null,
  choosenSite: null,
})

export const newInterventionStore = {
  subscribe: _newInterventionStore.subscribe,
  set: _newInterventionStore.set,
  update: _newInterventionStore.update,
  clear: () =>
    _newInterventionStore.update((self: NewInterventionState) => {
      self.step = 1;
      self.currentIntervention = null;
      self.choosenClient = null;
      return self
    }),
  addClient: (client: Client) =>
    _newInterventionStore.update((self: NewInterventionState) => {
      self.choosenClient = client;
      return self
    }),
  updateStep: (s: 1 | 2 | 3 | 4) =>
    _newInterventionStore.update((self: NewInterventionState) => {
      self.step = s;
      return self
    }),
  updateSite: (site: Site) =>
    _newInterventionStore.update((self: NewInterventionState) => {
      self.choosenSite = site;
      return self
    })
}
