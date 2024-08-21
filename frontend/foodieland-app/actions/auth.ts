"use server";

import { redirect } from "next/navigation";
import { axiosInstance } from "@/lib/axios";
import { createSession, deleteSession } from "@/lib/session";
import { loginSchema, signupSchema } from "@/schemas/auth";
import { decodeToken } from "@/lib/authorization";

interface PrevStateErrors {
    errors: {
        fullName?: string[];
        email?: string[];
        password?: string[];
        general?: string[];
    };
}

export async function login(prevState: PrevStateErrors | undefined, formData: FormData) {
    const parsedData = loginSchema.safeParse({
        email: formData.get("email"),
        password: formData.get("password")
    });

    if (!parsedData.success) {
        return {
            errors: parsedData.error.flatten().fieldErrors
        };
    }

    try {
        const response = await axiosInstance.post("/account/login", parsedData.data);

        await createSession(response.data.token);

        const userData = decodeToken(response.data.token);

        if (userData.roles.includes("admin")) {
            redirect("/admin");
        } else {
            redirect("/");
        }

    } catch (error: any) {
        if (error.response && error.response.status === 400) {
            return {
                errors: {
                    general: "Invalid email or password"
                }
            };
        } else {
            return {
                errors: {
                    general: "An unexpected error occurred. Please try again later."
                }
            };
        }
    }
}


export async function signup(prevState: PrevStateErrors | undefined, formData: FormData) {
    const parsedData = signupSchema.safeParse({
        fullName: formData.get("username"),
        email: formData.get("email"),
        password: formData.get("password")
    });

    if (!parsedData.success) {
        return {
            errors: parsedData.error.flatten().fieldErrors
        };
    }
    let response;
    try {
        const [firstName, ...lastNameParts] = parsedData.data.fullName.trim().split(" ");
        if (lastNameParts.length === 0) {
            return {
                errors: {
                    fullName: ["Full name must include both first name and last name"]
                }
            };
        }

        const lastName = lastNameParts.join(" ");
        const data = {
            firstName: firstName,
            lastName: lastName ? lastName : "",
            email: parsedData.data.email,
            password: parsedData.data.password
        };
        response = await axiosInstance.post("account/register", data);

        await createSession(response.data.token);
    } catch (error: any) {
        if (error.response && error.response.status === 400) {
            const message = error.response.data["message"];
            return {
                errors: {
                    general: message
                }
            };
        } else {
            return {
                errors: {
                    general: "An unexpected error occurred. Please try again later."
                }
            };
        }
    }

    redirect("/");
}

export async function logout() {
    await deleteSession();
    redirect("/login");
}