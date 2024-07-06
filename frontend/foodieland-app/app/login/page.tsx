import styles from './page.module.css';
import Logo from "@/app/ui/Logo";
import Link from "next/link";
import LoginForm from "@/app/login/LoginForm";

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
                <p>Don't have and account? </p>
                    <Link href={'/register'}>Sign up</Link>
                </div>
            </div>
            <div className={styles.loginRight}>
            </div>
        </main>
    );
}