'use client'

import styles from './form.module.css'
import {Button} from "@/components";
import {useFormState} from "react-dom";
import {signup} from "@/actions/auth";
export default function RegisterForm()
{
    const [errorMessage, formAction, isPending] = useFormState(signup, undefined,);

    const formatErrors = (errors: string[] | undefined) => {
        if (!errors) return null;
        return (
            <ul className={styles.errorList}>
                {errors.map((error, index) => (
                    <li key={index} className={styles.errorMessage}>- {error}</li>
                ))}
            </ul>
        );
    };

    return (
        <form action={formAction}>
            <div className={styles.inputGroup}>
                <label htmlFor="firstName">Full name</label>
                <input
                    type="text"
                    id="firstName"
                    name="firstName"
                    placeholder="John"
                    required
                />
                {errorMessage?.errors.firstName && (
                    <p className={styles.errorMessage}>{errorMessage.errors.firstName}</p>
                )}
            </div>
            <div className={styles.inputGroup}>
                <label htmlFor="lastName">Full name</label>
                <input
                    type="text"
                    id="lastName"
                    name="lastName"
                    placeholder="Doe"
                    required
                />
                {errorMessage?.errors.lastName && (
                    <p className={styles.errorMessage}>{errorMessage.errors.lastName}</p>
                )}
            </div>
            <div className={styles.inputGroup}>
                <label htmlFor="email">E-mail</label>
                <input
                    type="email"
                    id="email"
                    name="email"
                    placeholder="example@email.com"
                    required
                />
                {errorMessage?.errors.email && (
                    <p className={styles.errorMessage}>{errorMessage.errors.email}</p>
                )}
            </div>
            <div className={styles.inputGroup}>
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
                    <p className={styles.errorMessage}>{errorMessage.errors.general}</p>
                )}
            </div>
            <Button type={'submit'} additionalStyles={styles.submitButton} text={'Sign up'}/>
        </form>
    );
}