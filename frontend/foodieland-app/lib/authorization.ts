import { jwtDecode } from "jwt-decode";

export function decodeToken(token: string) : { fullName: string, email: string } {
    return jwtDecode(token);
}