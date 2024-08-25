import { NextRequest, NextResponse } from "next/server";
import { getSession, updateSession } from "@/lib/session";


export async function middleware(request: NextRequest) {
    const session = await getSession();

    const protectedRoutes = ["/recipes/favourite"]

    if (protectedRoutes.includes(request.nextUrl.pathname)) {
        if (!session) {
            return NextResponse.redirect(new URL("/login", request.url));
        }
    }

    return await updateSession(request);
}

export const config = {
    matcher: ["/((?!api|_next/static|favicon.ico).*)"]
};