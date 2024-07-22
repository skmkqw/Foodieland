'use client'

import {Button} from "@/components";
import {useFormState} from "react-dom";
import {signup} from "@/actions/auth";
export default function RegisterForm()
{
    const [errorMessage, formAction, isPending] = useFormState(signup, undefined,);

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
            <div className="inputGroup">
                <label htmlFor="firstName">Full name</label>
                <input
                    type="text"
                    id="firstName"
                    name="firstName"
                    placeholder="John"
                    required
                />
                {errorMessage?.errors.firstName && (
                    <p className="errorMessage">{errorMessage.errors.firstName}</p>
                )}
            </div>
            <div className="inputGroup">
                <label htmlFor="lastName">Full name</label>
                <input
                    type="text"
                    id="lastName"
                    name="lastName"
                    placeholder="Doe"
                    required
                />
                {errorMessage?.errors.lastName && (
                    <p className="errorMessage">{errorMessage.errors.lastName}</p>
                )}
            </div>
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
                {errorMessage?.errors.password && formatErrors(errorMessage.errors.password)}
                {errorMessage?.errors.general && (
                    <p className="errorMessage">{errorMessage.errors.general}</p>
                )}
            </div>
            <Button type={'submit'} additionalStyles="w-full mt-5" text={'Sign up'}/>
        </form>
    );
}