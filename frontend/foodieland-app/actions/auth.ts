'use server'
import {cookies} from "next/headers";
import {revalidatePath} from "next/cache";
import {redirect} from "next/navigation";

export const login = async (email: string, password: string) => {
    const response = await fetch('http://localhost:5148/account/login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ email, password }),
    });

    if (!response.ok) {
        throw new Error('Failed to login');
    }

    const data = await response.json();
    const oneDay = 24 * 60 * 60 * 1000;
    cookies().set("token", data.token, {httpOnly: true, secure: true, expires: Date.now() + oneDay});
    redirect('/');
};