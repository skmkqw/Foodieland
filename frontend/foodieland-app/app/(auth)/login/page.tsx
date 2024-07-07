'use client'

import styles from '../page.module.css';
import Link from "next/link";
import { Logo, LoginForm, Promosection, Button } from '@/components'
import { useEffect, useState } from "react";
export default function Login()
{
    const [mobile, setMobile] = useState(true)

    useEffect(() => {
        const updateMobile = () => {
            setMobile(window.innerWidth > 900)
        }

        updateMobile()
        window.addEventListener('resize', updateMobile)
        return () => {
            window.removeEventListener('resize', updateMobile)
        }
    }, [])

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
            {mobile ? <div className={styles.loginRight}>
                    <div className={styles.loginRightContainer}>
                        <Promosection/>
                    </div>
                </div>
                : <></>}
        </main>
    );
}