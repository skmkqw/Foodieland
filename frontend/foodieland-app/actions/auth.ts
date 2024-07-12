'use server'
import {z} from 'zod';
import {cookies} from "next/headers";
import {redirect} from "next/navigation";


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

    const response = await fetch("http://localhost:5148/account/login", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json; charset=utf-8',
        },
        body: JSON.stringify(parsedData.data),
    });

    if (!response.ok) {
        return {
            errors: {
                general: 'Invalid email or password'
            }
        }
    }

    const data = await response.json();
    const oneDay = 24 * 60 * 60 * 1000;
    cookies().set("token", data.token, {httpOnly: true, secure: true, expires: Date.now() + oneDay});
    redirect('/');
}