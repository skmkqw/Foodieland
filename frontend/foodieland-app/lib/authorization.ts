import { jwtDecode } from "jwt-decode";
import { getSession } from "@/lib/session";

export function decodeToken(token: string): { unique_name: string; email: string; roles: string[] | string } {
    const decoded: { unique_name: string; email: string; role: string | string[] } = jwtDecode(token);

    return {
        unique_name: decoded.unique_name,
        email: decoded.email,
        roles: decoded.role
    };
}
export async function getUserData() {
    const session = await getSession();
    if (session) return decodeToken(session);
    return null;
}