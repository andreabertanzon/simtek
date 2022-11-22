export async function load() {
  const interventions: any[] = [
    { date: "12-10-2022", description: "questa e' una descrizione di cosa ho fatto", spentHours: 6 },
    { date: "12-11-2022", description: "sostituiti i radiatori al piano terra", spentHours: 24 },
    { date: "10-10-2022", description: "messi i nuovi condizionatori", spentHours: 12 },
    { date: "12-10-2022", description: "cambiati comandi dei cancelli della struttura", spentHours: 12 },
    { date: "12-10-2022", description: "modificato comando cancello principale", spentHours: 5 },
    { date: "12-10-2022", description: "iniziato passaggio fili cancello esterno", spentHours: 8 },
    { date: "12-10-2022", description: "cambiate le guarnizioni ai termo", spentHours: 2 },
    { date: "12-10-2022", description: "sostituiti i rubinetti nella parte nuova", spentHours: 44 },
    { date: "12-10-2022", description: "cambiate le lampade nelle camere", spentHours: 1 },
  ].sort((a, b) => a.date < b.date ? 1 : -1)
  return { interventions: interventions };
}
