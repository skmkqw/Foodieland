import Link from "next/link";

const MENU_ITEMS = [
    { title: "Home", href: "/" },
    { title: "Recipes", href: "/recipes" },
    { title: "Favourite", href: "/recipes/favourite" },
    { title: "About us", href: "/about" }
];

export default function NavLinks() {
    return (
        <ul className="flex flex-col text-center sm:flex-row gap-10">
            {MENU_ITEMS.map((item) => (
                <li className="navLink" key={item.title}>
                    <Link href={item.href}>{item.title}</Link>
                </li>
            ))}
        </ul>
    );
}