import { z } from "zod"

export const siteSchema = z.object({
  Name: z.string().min(1),
  Address: z.string().optional(),
  City: z.string().optional(),
  Cap: z.string().optional()
})
export type Site = z.infer<typeof siteSchema> 
