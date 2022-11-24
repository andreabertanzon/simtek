import { z } from "zod";
import { ChosenMaterialScheme } from "./material";
import { WorkerInterventionSchema } from "./Worker";

export const InterventionStruct = z.object({
  date: z.string(),
  title: z.string(),
  spentHours: z.number().default(0).optional(),
  materialCost: z.number().default(0).optional(),
  workCost: z.number().default(0).optional(),
  completed: z.boolean().default(false).optional(),
  numberOfWorkers: z.number().default(1).optional(),
  clientName: z.string()
})

export type Intervention = z.infer<typeof InterventionStruct>

export const CurrentInterventionStruct = InterventionStruct.extend({
  descriptions: z.array(z.string()).default([]).optional(),
  workers: z.array(WorkerInterventionSchema).default([]).optional(),
  notes: z.array(z.string()),
  materials: z.array(ChosenMaterialScheme)
})

export type CurrentIntervention = z.infer<typeof CurrentInterventionStruct>

