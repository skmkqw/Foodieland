import styles from './form.module.css'
export default function RegisterForm()
{
    return (
        <form>
            <div className={styles.inputGroup}>
                <label htmlFor="fullName">Full name</label>
                <input
                    type="text"
                    id="fullName"
                    placeholder="John Doe"
                    required
                />
            </div>
            <div className={styles.inputGroup}>
                <label htmlFor="email">E-mail</label>
                <input
                    type="email"
                    id="email"
                    placeholder="example@email.com"
                    required
                />
            </div>
            <div className={styles.inputGroup}>
                <label htmlFor="password">Password</label>
                <input
                    type="password"
                    id="password"
                    placeholder="********"
                    required
                />
            </div>
            <button type="submit" className={`${styles.submitButton} btnMain`}>Sign Up</button>
        </form>
    );
}