'use client'

import styles from './promo.module.css';
import Logo from "@/components/Logo";
import {useEffect, useState} from "react";
export default function Promosection()
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
        mobile ? <div className={styles.loginRight}>
            <div className={styles.loginRightContainer}>
                <div className={styles.container}>
                    <div className={styles.slogan}>
                        <h1>Welcome to </h1>
                        <Logo fontsize={34}/>
                    </div>
                    <p className={styles.tagline}>Discover the best recipes, all in one place.</p>
                    <ul className={styles.features}>
                        <li>🍔 Easy-to-follow recipes</li>
                        <li>🌟 User reviews and ratings</li>
                        <li>📱 Access from any device</li>
                    </ul>
                    <blockquote className={styles.testimonial}>
                        "Foodieland has changed the way I cook. It's my go-to source for new recipes!" - Happy User
                    </blockquote>
                </div>
            </div>
        </div> : <></>
    );
}