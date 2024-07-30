import { z } from "zod";

export const signupSchema = z.object({
    fullName: z.string()
        .trim()
        .min(1, { message: "Full name is required and cannot be empty" })
        .regex(/^\S+\s+\S+.*$/, { message: "Full name must include both first name and last name" }),

    email: z.string()
        .email({ message: "Invalid email address" }),

    password: z.string()
        .min(6, { message: "Password must be at least 6 characters long" })
        .regex(/(?=.*[A-Z])/, { message: "Password must contain at least one capital letter" })
        .regex(/(?=.*\d)/, { message: "Password must contain at least one digit" })
        .regex(/(?=.*[!@#$%^&*])/, { message: "Password must contain at least one special character" })
});

export const loginSchema = z.object({
    email: z.string().email({ message: "Invalid email address" }),

    password: z.string().min(6, { message: "Password must be at least 6 characters long" })
});