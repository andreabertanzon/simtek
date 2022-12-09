import { z } from "zod"

export const workerSchema = z.object({
  Name: z.string(),
  PricePerHour: z.number().positive().default(30.00),
})

export type Worker = z.infer<typeof workerSchema>


export const WorkerInterventionSchema = workerSchema.extend({
  WorkedHours: z.number().positive().default(0).optional(),
  Checked: z.boolean().default(false).optional()
})

export type WorkerIntervention = z.infer<typeof WorkerInterventionSchema>
