import Link from "next/link";
import { useState } from "react";
import { UserInfo } from "@/components";

const MENU_ITEMS = [
    {
        title: "Home",
        href: "/"
    },
    {
        title: "Recipes",
        href: "/"
    },
    {
        title: "Blog",
        href: "/"
    },
    {
        title: "Contact",
        href: "/"
    },
    {
        title: "About us",
        href: "/"
    }
];

export default function MobileDrawer({ isOpen, closeDrawer }: { isOpen: boolean, closeDrawer: () => void }) {
    const [activeIndex, setActiveIndex] = useState(0);


    return (
        <div
            className={`fixed flex flex-col justify-center items-center z-10 top-0 right-0 h-full w-full bg-white text-black transition-transform duration-300 transform ${
                isOpen ? "translate-x-0" : "translate-x-full"
            }`}
        >
            <ul className="flex flex-col justify-center items-center gap-8">
                {
                    MENU_ITEMS.map((item, idx) => (
                        <div onClick={() => {
                            closeDrawer();
                            setActiveIndex(idx);
                        }} key={item.title}>
                            <li className={`navLinkMobile ${activeIndex === idx ? "navLinkMobileActive" : ""}`}>
                                <Link href={item.href}>{item.title}</Link>
                            </li>
                        </div>
                    ))
                }
                <UserInfo userName={userName} />
            </ul>
        </div>
    );
}