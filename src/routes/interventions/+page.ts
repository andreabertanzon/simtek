import type { intervention } from "src/models/intervention";

export async function load() {
  const interventions: intervention[] = [
    { date: "12-10-2022", description: "questa e' una descrizione di cosa ho fatto", spentHours: 6, completed: false },
    { date: "12-11-2022", description: "sostituiti i radiatori al piano terra", spentHours: 24, completed: false },
    { date: "10-10-2022", description: "messi i nuovi condizionatori", spentHours: 12, completed: false },
    { date: "12-10-2022", description: "cambiati comandi dei cancelli della struttura", spentHours: 12, completed: true },
    { date: "12-10-2022", description: "modificato comando cancello principale", spentHours: 5, completed: true },
    { date: "12-10-2022", description: "iniziato passaggio fili cancello esterno", spentHours: 8, completed: true },
    { date: "12-10-2022", description: "cambiate le guarnizioni ai termo", spentHours: 2, completed: false },
    { date: "12-10-2022", description: "sostituiti i rubinetti nella parte nuova", spentHours: 44, completed: false },
    { date: "12-10-2022", description: "cambiate le lampade nelle camere", spentHours: 1, completed: true },
  ].sort((a, b) => a.date < b.date ? 1 : -1)
  return { interventions: interventions };
}
