import { jwtDecode } from "jwt-decode";
import { getSession } from "@/lib/session";

export function decodeToken(token: string): { unique_name: string; email: string; roles: string[] } {
    const decoded: { unique_name: string; email: string; roles: string | string[] } = jwtDecode(token);

    const roles = Array.isArray(decoded.roles) ? decoded.roles : [decoded.roles];

    return {
        unique_name: decoded.unique_name,
        email: decoded.email,
        roles: roles
    };
}
export async function getUserData() {
    const session = await getSession();
    if (session) return decodeToken(session);
    return null;
}