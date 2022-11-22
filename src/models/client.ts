import { siteSchema } from "./site"
import { z } from "zod"

export const ClientSchema = z.object({
  name: z.string(),
  surname: z.string(),
  address: z.string().optional(),
  city: z.string().optional(),
  cap: z.string().optional(),
  vat: z.string().optional(),
  uniqueCode: z.string().optional(),
  email: z.string().email(),
  phone: z.string().optional(),
  sites: z.array(siteSchema)
})

export type Client = z.infer<typeof ClientSchema>;
