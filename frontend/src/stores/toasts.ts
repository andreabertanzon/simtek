import { writable } from "svelte/store";
import { z } from "zod";

let toastStruct = z.object({
  id: z.number().optional(),
  type: z.string().default("info"),
  dismissable: z.boolean().default(true),
  timeout: z.number().default(3000),
  message: z.string().optional()
})
export type Toast = z.infer<typeof toastStruct>
export const toasts = writable<Toast[]>([])

export const addToast = (toast: Toast) => {
  // Create a unique ID so we can easily find/remove it
  // if it is dismissible/has a timeout.
  const id = Math.floor(Math.random() * 10000);
  toast.id = id
  // Push the toast to the top of the list of toasts
  toasts.update((all: Toast[]) => [toast, ...all]);

  // If toast is dismissible, dismiss it after "timeout" amount of time.
  if (toast.timeout) setTimeout(() => dismissToast(id), toast.timeout);
};

export const dismissToast = (id: number) => {
  toasts.update((all) => all.filter((t) => t.id !== id));
};
