import { LinkButton } from "@/components";
import { getUserData } from "@/lib/authorization";
import Link from "next/link";

export default async function UserInfo() {
    const userData = await getUserData();

    return (
        <div>
            {userData ? (
                <div className="flex rounded-lg bg-primary transition duration-200 ease-in-out py-2 px-3 cursor-pointer">
                    <Link href="/logout">
                        <p className="text-lg font-medium">{userData?.unique_name}</p>
                    </Link>
                </div>
            ) : (
                <LinkButton url="/login" buttonText="Log in" additionalStyles="!py-3" />
            )}
        </div>
    );
}