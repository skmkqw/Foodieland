'use client'

import styles from '../page.module.css';
import Link from "next/link";
import { Logo, LoginForm, Button } from '@/components'
export default function Login()
{
    return (
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
                        <Button type={'button'} text={'Sign up'}/>
                    </Link>
                </div>
                <div>
                    <Link href={'/'}>
                        <Button type={'button'} text={'Skip'}/>
                    </Link>
                </div>
            </div>
        </div>
    );
}