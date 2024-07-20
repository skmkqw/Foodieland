import { NextRequest, NextResponse } from "next/server";
import {updateSession} from "@/lib/session";

export function middleware(request: NextRequest) {
    const response = NextResponse.next();
    updateSession();
    return response;
}
export const config = {
    matcher: ['/((?!api|_next/static|favicon.ico).*)'],
};