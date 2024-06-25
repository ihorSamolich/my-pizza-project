import { ACCEPTED_IMAGE_MIME_TYPES, MAX_FILE_SIZE } from "constants/index.ts";
import { z } from "zod";

export const PizzaCreateSchema = z.object({
  image: z
    .any()
    .transform((files) => (files ? Array.from(files) : []))
    .refine((files: any[]) => files.length > 0, `Min photo count is 1.`)
    .refine((files: any[]) => files.length <= 1, `Max photo count is 1.`)
    .refine((files: any[]) => files.length === 0 || files.every((file) => file.size <= MAX_FILE_SIZE), `Max file size is 5MB.`)
    .refine(
      (files: any[]) => files.length === 0 || files.every((file) => ACCEPTED_IMAGE_MIME_TYPES.includes(file.type)),
      "Only .jpg, .jpeg, .png and .webp files are accepted.",
    ),
});

export type PizzaCreateSchemaType = z.infer<typeof PizzaCreateSchema>;
