import { z } from 'zod'

const materialScheme = z.object({
  Name: z.string().trim(),
  Company: z.string().trim(),
  Unit: z.string().max(4),
  Price: z.number().positive().default(0.00)
})

export type Material = z.infer<typeof materialScheme>

export const ChosenMaterialScheme = materialScheme.extend({
  Checked: z.boolean().default(false).optional(),
  Pieces: z.number().default(0)
})

export type ChosenMaterial = z.infer<typeof ChosenMaterialScheme>
