'use client';

import {InputGroup, Button} from "@/components";
import { login } from "@/actions/auth";
import  { useFormState } from "react-dom";
export default function LoginForm() {
    const [errorMessage, formAction, isPending] = useFormState(login, undefined,);

    return (
        <form action={formAction}>
            <InputGroup type="email" errorMessage={errorMessage?.errors.email} />
            <InputGroup type="password" errorMessage={errorMessage?.errors.password} />
            {errorMessage?.errors.general && (
                <p className="errorMessage mt-2">{errorMessage.errors.general}</p>
            )}
            <Button type={'submit'} additionalStyles="w-full mt-5" text={'Log in'}/>
        </form>
    );
}