import styles from './page.module.css'
import {Promosection} from "@/components";
import {useEffect, useState} from "react";
export default function AuthLayout({ children } : { readonly children: React.ReactNode; })
{
    return (
        <main className={styles.login}>
            {children}
            <Promosection/>
        </main>
    );
}