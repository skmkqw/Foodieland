import { NextRequest, NextResponse } from "next/server";

export function middleware(request: NextRequest) {
    const response = NextResponse.next();
    const session = request.cookies.get("session")?.value;
    if (session) {
        const expires = new Date(Date.now() + 10 * 1000);
        response.cookies.set('session', session, { httpOnly: true, secure: true, expires: expires });
    }
    return response;
}
export const config = {
    matcher: ['/((?!api|_next/static|favicon.ico).*)'],
};