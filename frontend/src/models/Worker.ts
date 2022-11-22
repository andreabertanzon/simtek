import { z } from "Zod"

export const workerSchema = z.object({
  Name: z.string(),
  PricePerHour: z.number().positive().default(30.00),
})

export type Worker = z.infer<typeof workerSchema>
