"use server";

import { z } from "zod";
import { redirect } from "next/navigation";
import { axiosInstance } from "@/lib/axios";
import { createSession, deleteSession } from "@/lib/session";


const signupSchema = z.object({
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

const loginSchema = z.object({
    email: z.string().email({ message: "Invalid email address" }),

    password: z.string().min(6, { message: "Password must be at least 6 characters long" })
});

export async function login(prevState: {
    errors: { firstName?: string[]; lastName?: string[]; email?: string[]; password?: string[]; general?: string[] }
} | undefined, formData: FormData) {
    const parsedData = loginSchema.safeParse({
        email: formData.get("email"),
        password: formData.get("password")
    });

    if (!parsedData.success) {
        return {
            errors: parsedData.error.flatten().fieldErrors
        };
    }

    try {
        const response = await axiosInstance.post("/account/login", parsedData.data);

        await createSession(response.data.token);
    } catch (error) {
        if (error.response && error.response.status === 400) {
            return {
                errors: {
                    general: "Invalid email or password"
                }
            };
        } else {
            return {
                errors: {
                    general: "An unexpected error occurred. Please try again later."
                }
            };
        }
    }

    redirect("/");
}


export async function signup(prevState: {
    errors: { fullName?: string[], email?: string[]; password?: string[]; general?: string[] }
} | undefined, formData: FormData) {
    const parsedData = signupSchema.safeParse({
        fullName: formData.get("username"),
        email: formData.get("email"),
        password: formData.get("password")
    });

    if (!parsedData.success) {
        return {
            errors: parsedData.error.flatten().fieldErrors
        };
    }
    let response;
    try {
        const [firstName, ...lastNameParts] = parsedData.data.fullName.trim().split(" ");
        if (lastNameParts.length === 0) {
            return {
                errors: {
                    fullName: ["Full name must include both first name and last name"]
                }
            };
        }

        const lastName = lastNameParts.join(" ");
        const data = {
            firstName: firstName,
            lastName: lastName ? lastName : "",
            email: parsedData.data.email,
            password: parsedData.data.password
        };
        response = await axiosInstance.post("account/register", data);

        await createSession(response.data.token);
    } catch (error) {
        if (error.response && error.response.status === 400) {
            const message = error.response.data["message"];
            return {
                errors: {
                    general: message
                }
            };
        } else {
            return {
                errors: {
                    general: "An unexpected error occurred. Please try again later."
                }
            };
        }
    }

    redirect("/");
}

export async function logout() {
    await deleteSession();
    redirect("/login");
}