import styles from './form.module.css'
import { Button } from "@/components";
import { login } from "@/actions/auth";

export default function LoginForm()
{
    async function Login(formData: FormData)
    {
        const email = formData.get('email').toString();
        const password = formData.get('password').toString();
        const token = await login(email, password)
        console.log(token)
    }
    return (
        <form action={Login}>
            <div className={styles.inputGroup}>
                <label htmlFor="email">E-mail</label>
                <input
                    type="email"
                    id="email"
                    name="email"
                    placeholder="example@email.com"
                    required
                />
            </div>
            <div className={styles.inputGroup}>
                <label htmlFor="password">Password</label>
                <input
                    type="password"
                    id="password"
                    name="password"
                    placeholder="********"
                    required
                />
            </div>
            <Button type={'submit'} additionalStyles={styles.submitButton} text={'Log in'}/>
        </form>
    );
}