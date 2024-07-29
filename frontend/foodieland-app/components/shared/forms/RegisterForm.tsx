"use client";

import { Button, InputGroup } from "@/components";
import { useFormState } from "react-dom";
import { signup } from "@/actions/auth";

export default function RegisterForm() {
    const [errorMessage, formAction, isPending] = useFormState(signup, undefined);

    const formatErrors = (errors: string[] | undefined) => {
        if (!errors) return null;
        return (
            <ul className="list-none text-red-500 pl-0">
                {errors.map((error, index) => (
                    <li key={index} className="errorMessage">- {error}</li>
                ))}
            </ul>
        );
    };

    return (
        <form action={formAction}>
            <InputGroup type="username" errorMessage={errorMessage?.errors.fullName} />
            <InputGroup type="email" errorMessage={errorMessage?.errors.email} />
            <InputGroup type="password"
                        errorMessage={errorMessage?.errors.password && formatErrors(errorMessage.errors.password)} />
            {errorMessage?.errors.general && (
                <p className="errorMessage">{errorMessage.errors.general}</p>
            )}
            <Button type={"submit"} additionalStyles="w-full mt-5" text={"Sign up"} />
        </form>
    );
}