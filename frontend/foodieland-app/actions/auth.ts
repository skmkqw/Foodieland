'use server'

import { z } from 'zod';
import { redirect } from "next/navigation";
import { axiosInstance } from "@/lib/axios";
import { createSession } from "@/lib/session";


const signupSchema = z.object({
    firstName: z.string()
        .trim()  // Remove leading and trailing whitespace
        .min(1, { message: 'First name is required and cannot be empty' }),

    lastName: z.string()
        .trim()  // Remove leading and trailing whitespace
        .min(1, { message: 'Last name is required and cannot be empty' }),

    email: z.string()
        .email({ message: 'Invalid email address' }),

    password: z.string()
        .min(8, { message: 'Password must be at least 8 characters long' })
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

        createSession(response.data.token);
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


export async function signup(prevState: { errors: { email?: string[]; password?: string[]; general?: string }} | undefined, formData: FormData) {
    const parsedData = signupSchema.safeParse({
        email: formData.get('email'),
        password: formData.get('password')
    });

    if (!parsedData.success)
    {
        return {
            errors: parsedData.error.flatten().fieldErrors
        }
    }

    try {
        const response = await axiosInstance.post('account/register');

        createSession(response.data.token);
    } catch (error) {
        if (error.response && error.response.status === 400) {
            return {
                errors: {
                    general: 'This email address is already in use.'
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