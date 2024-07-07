'use client'

import styles from '../page.module.css';
import Link from "next/link";
import { Logo, RegisterForm, Promosection, Button } from '@/components'

export default function Register()
{
    return (
        <main className={styles.login}>
            <div className={styles.loginLeft}>
                <div className={styles.loginHeader}>
                    <Logo fontsize={24}/>
                </div>
                <div className={styles.loginContent}>
                    <div>
                        <h1>Sign Up</h1>
                        <RegisterForm/>
                    </div>
                </div>
                <div className={styles.loginFooter}>
                    <div>
                        <Link href={'/login'}>
                            <Button onClick={() => console.log("Button clicked!")} text={'Log in'}/>
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