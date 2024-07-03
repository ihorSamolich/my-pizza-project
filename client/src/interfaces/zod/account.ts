import { z } from "zod";

export const LoginSchema = z.object({
  email: z
    .string()
    .email({ message: "Invalid email address" })
    .min(3, { message: "Email must be at least 3 characters long" })
    .max(100, { message: "Email must be at most 100 characters long" }),

  password: z
    .string()
    .min(8, { message: "Password must be at least 8 characters long" })
    .max(100, { message: "Password must be at most 100 characters long" }),
});

export type LoginSchemaType = z.infer<typeof LoginSchema>;

export const RegisterSchema = z
  .object({
    firstName: z.string().min(1, { message: "First name is required" }),

    lastName: z.string().min(1, { message: "Last name is required" }),

    email: z.string().email({ message: "Invalid email address" }),

    password: z.string().min(8, { message: "Password must be at least 8 characters long" }),

    confirmPassword: z.string().min(8, { message: "Password must be at least 8 characters long" }),

    image: z
      .any()
      .nullable()
      .transform((files) => (files ? Array.from(files) : []))
      .refine((files: any[]) => files.length === 0 || files.length === 1, "You can only upload one image.")
      .refine(
        (files: any[]) => files.length === 0 || files.every((file) => file.size <= 5 * 1024 * 1024), // 5MB max file size
        "Max file size is 5MB.",
      )
      .refine(
        (files: any[]) =>
          files.length === 0 ||
          files.every((file) => ["image/jpeg", "image/jpg", "image/png", "image/webp"].includes(file.type.toLowerCase())),
        "Only .jpg, .jpeg, .png, and .webp files are accepted.",
      ),
  })
  .refine((data) => data.password === data.confirmPassword, {
    message: "Passwords do not match",
    path: ["confirmPassword"],
  });

export type RegisterSchemaType = z.infer<typeof RegisterSchema>;
