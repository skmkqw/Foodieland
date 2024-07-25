import { Logo, UserInfo } from "@/components";
import Link from "next/link";

export default function Header() {
    return (
        <header className="py-6 border-b-gray-200 border-b">
            <div className="container">
                <div className="flex items-center justify-between">
                    <Logo fontsize={24} />
                    <div className="flex items-center gap-10">
                        <Link className="navLink" href="#" >Home</Link>
                        <Link className="navLink" href="#">Recipes</Link>
                        <Link className="navLink" href="#">Blog</Link>
                        <Link className="navLink" href="#">Contact</Link>
                        <Link className="navLink" href="#">About us</Link>
                    </div>
                    <UserInfo />
                </div>
            </div>
        </header>
    );
}