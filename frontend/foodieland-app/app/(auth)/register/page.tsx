"use client";

import { LinkButton, Logo, RegisterForm } from "@/components";

export default function Register() {
    return (
        <div className="flex-[0_1_100%] md:flex-[0_1_50%] flex flex-col justify-between">
            <div className="p-5">
                <Logo fontsize={24} />
            </div>
            <div className="flex justify-center">
                <div className="flex flex-col items-start">
                    <h1>Sign Up</h1>
                    <RegisterForm />
                </div>
            </div>
            <div className="flex items-center justify-between p-5">
                <LinkButton url="/login" buttonText="Log in" />
                <LinkButton url="/" buttonText="Skip" />
            </div>
        </div>
    );
}