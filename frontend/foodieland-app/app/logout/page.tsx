import { Button } from "@/components";
import { logout } from "@/actions/auth";

export default function LogoutPage() {
    return (
        <form action={logout}>
            <Button type="submit" text="Log out" />
        </form>
    );
}