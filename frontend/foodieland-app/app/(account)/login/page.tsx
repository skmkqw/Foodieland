'use client'

import styles from '../page.module.css';
import Logo from "@/app/ui/Logo";
import Link from "next/link";
import LoginForm from "@/app/(account)/login/LoginForm";
import Promosection from "@/app/(account)/Promosection";
import Button from "@/app/ui/Button";

export default function Login()
{
    return (
        <main className={styles.login}>
            <div className={styles.loginLeft}>
                <div className={styles.loginHeader}>
                    <Logo fontsize={24}/>
                </div>
                <div className={styles.loginContent}>
                    <div>
                        <h1>Log In</h1>
                        <LoginForm/>
                    </div>
                </div>
                <div className={styles.loginFooter}>
                    <div>
                        <Link href={'/register'}>
                            <Button onClick={() => console.log("Button clicked!")} text={'Sign up'}/>
                        </Link>
                    </div>
                    <div>
                        <Link href={'/'}>
                            <Button onClick={() => console.log("Button clicked!")} text={'Skip'}/>
                        </Link>
                    </div>
                </div>
            </div>
            <div className={styles.loginRight}>
            <div className={styles.loginRightContainer}>
                    <Promosection/>
                </div>
            </div>
        </main>
    );
}