import Link from "next/link";

export default function NavLinks() {
    return (
        <ul className="flex gap-10">
            <li className="navLink">
                <Link href="/foodieland-app/public">Home</Link>
            </li>
            <li className="navLink">
                <Link href="/foodieland-app/public">Recipes</Link>
            </li>
            <li className="navLink">
                <Link href="/foodieland-app/public">Blog</Link>
            </li>
            <li className="navLink">
                <Link href="/foodieland-app/public">Contact</Link>
            </li>
            <li className="navLink">
                <Link href="/foodieland-app/public">About us</Link>
            </li>
        </ul>
    );
}