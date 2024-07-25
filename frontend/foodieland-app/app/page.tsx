import { getUserData } from "@/lib/authorization";
import { Button } from "@/components";
import { logout } from "@/actions/auth";
import Link from "next/link";

export default async function Home() {
    const userData = await getUserData();
    return (
        <main>
            <div className="container">
                <h1>FoodieLand</h1>
                {userData ? (
                    <div>
                        <p>{userData?.unique_name}</p>
                        <p>{userData?.email}</p>
                        <form action={logout}>
                            <Button type="submit" text="Log out" />
                        </form>
                    </div>
                ) : (
                    <div>
                        <p>You are not logged in.</p>
                        <Link href="http://localhost:3000/login">
                            <Button type="button" text="Log in" />
                        </Link>
                    </div>
                )}
            </div>
        </main>
    );
}
