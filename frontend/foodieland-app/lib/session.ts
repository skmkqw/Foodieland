'use server'

import { cookies } from "next/headers";

const SESSION_DURATION_MS = 10 * 1000;

export async function createSession(token: string) {
    const expires = new Date(Date.now() + SESSION_DURATION_MS);
    cookies().set("session", token, { httpOnly: true, secure: true, expires: expires });
}

export async function updateSession() {
    const session = cookies().get("session")?.value;
    if (session) {
        const expires = new Date(Date.now() + SESSION_DURATION_MS);
        cookies().set('session', session, { httpOnly: true, secure: true, expires: expires });
    }
}
