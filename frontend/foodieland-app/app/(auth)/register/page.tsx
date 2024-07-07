'use client'

import styles from '../page.module.css';
import Link from "next/link";
import { Logo, RegisterForm, Button } from '@/components'

export default function Register()
{
    return (
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
                        <Button type={'button'} text={'Log in'}/>
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