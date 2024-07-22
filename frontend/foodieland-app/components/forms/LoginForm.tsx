'use client';

import { Button } from "@/components";
import { login } from "@/actions/auth";
import  { useFormState } from "react-dom";
export default function LoginForm() {
    const [errorMessage, formAction, isPending] = useFormState(login, undefined,);

    return (
        <form action={formAction}>
            <div className="inputGroup">
                <label htmlFor="email">E-mail</label>
                <input
                    type="email"
                    id="email"
                    name="email"
                    placeholder="example@email.com"
                    required
                />
                {errorMessage?.errors.email && (
                    <p className="errorMessage">{errorMessage.errors.email}</p>
                )}
            </div>

            <div className="inputGroup">
                <label htmlFor="password">Password</label>
                <input
                    type="password"
                    id="password"
                    name="password"
                    placeholder="********"
                    required
                />
                {errorMessage?.errors.password && (
                    <p className="errorMessage">{errorMessage.errors.password}</p>
                )}
                {errorMessage?.errors.general && (
                    <p className="errorMessage">{errorMessage.errors.general}</p>
                )}
            </div>

            <Button type={'submit'} additionalStyles="w-full" text={'Log in'}/>
        </form>
    );
}