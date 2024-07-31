"use server";

import { cookies } from "next/headers";
import { NextRequest, NextResponse } from "next/server";

const SESSION_DURATION_MS = 600 * 1000;

export async function getSession() {
    return cookies().get("session")?.value;
}

export async function createSession(token: string) {
    const expires = new Date(Date.now() + SESSION_DURATION_MS);
    cookies().set("session", token, { httpOnly: true, secure: true, expires: expires });
}

export async function updateSession(request: NextRequest) {
    const session = request.cookies.get("session")?.value;
    if (!session) return;

    const expires = new Date(Date.now() + SESSION_DURATION_MS);
    const response = NextResponse.next();
    response.cookies.set("session", session, { httpOnly: true, secure: true, expires: expires });

    return response;
}

export async function deleteSession() {
    cookies().set("session", "", { expires: new Date(0) });
}
