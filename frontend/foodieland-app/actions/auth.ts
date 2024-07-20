'use server'

import { z } from 'zod';
import { cookies } from "next/headers";
import { redirect } from "next/navigation";
import { axiosInstance } from "@/lib/axios";

const signupSchema = z.object({
    email: z.string().email({ message: 'Invalid email address' }),
    password: z.string().min(8, { message: 'Password must be at least 8 characters long' })
        .regex(/(?=.*[A-Z])/, { message: 'Password must contain at least one capital letter' })
        .regex(/(?=.*[!@#$%^&*])/, { message: 'Password must contain at least one special character' }),
});

const loginSchema = z.object({
   email: z.string().email({ message: 'Invalid email address' }),
   password: z.string().min(8, { message: 'Password must be at least 8 characters long' })
});

export async function login (prevState: { errors: { email?: string[]; password?: string[]; general?: string }} | undefined, formData: FormData) {
    const parsedData = loginSchema.safeParse({
            email: formData.get('email'),
            password: formData.get('password')
    });

    if (!parsedData.success)
    {
        return {
            errors: parsedData.error.flatten().fieldErrors,
        }
    }

    try {
        const response = await axiosInstance.post('/account/login', parsedData.data);

        const data = await response.data;
        const expires = new Date(Date.now() + 10 * 1000);
        cookies().set("session", data.token, { httpOnly: true, secure: true, expires: expires });
    } catch (error) {
        if (error.response && error.response.status === 400) {
            return {
                errors: {
                    general: 'Invalid email or password'
                }
            };
        } else {
            return {
                errors: {
                    general: 'An unexpected error occurred. Please try again later.'
                }
            };
        }
    }

    redirect('/');
}