import styles from './form.module.css'
import {Button} from "@/components";
export default function LoginForm()
{
    return (
        <form>
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
            <Button type={'submit'} additionalStyles={styles.submitButton} text={'Log in'}/>
        </form>
    );
}