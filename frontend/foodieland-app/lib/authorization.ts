import { jwtDecode } from "jwt-decode";
import { getSession } from "@/lib/session";

export function decodeToken(token: string): { unique_name: string, email: string } {
    return jwtDecode(token);
}

export async function getUserData() {
    const session = await getSession();
    if (session) return decodeToken(session);
    return null;
}