import { getUserData } from "@/lib/authorization";
import { NavBar } from "@/components";

export default async function Header() {
    const userData = await getUserData();

    return <NavBar userName={userData?.unique_name} />;
}