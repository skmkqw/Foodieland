import Link from "next/link";

export default function NavLinks() {
    return (
        <ul className="flex gap-10">
            <li className="navLink">
                <Link href="/">Home</Link>
            </li>
            <li className="navLink">
                <Link href="/">Recipes</Link>
            </li>
            <li className="navLink">
                <Link href="/">Blog</Link>
            </li>
            <li className="navLink">
                <Link href="/">Contact</Link>
            </li>
            <li className="navLink">
                <Link href="/">About us</Link>
            </li>
        </ul>
    );
}