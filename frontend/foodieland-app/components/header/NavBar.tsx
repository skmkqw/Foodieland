"use client";

import { useState } from "react";
import Link from "next/link";
import { Logo, MenuButton, NavLinks, SocialLinks } from "@/components";
import MobileDrawer from "@/components/header/MobileDrawer";

export default function NavBar() {
    const [isDrawerOpen, setIsDrawerOpen] = useState(false);

    const handleDrawerToggle = () => {
        setIsDrawerOpen(!isDrawerOpen);
    };

    const closeDrawer = () => {
        setIsDrawerOpen(false);
    };

    return (
        <>
            <div className="border-b-gray-200 border-b">
                <div className="hidden relative py-6 md:flex justify-between items-center gap-6 container">
                    <Link href="/foodieland-app/public">
                        <Logo fontsize={24} />
                    </Link>
                    <NavLinks />
                    <SocialLinks />
                </div>
            </div>
            <div className="border-b-gray-200 border-b">
                <div className="md:hidden relative flex items-center justify-between flex-row my-4 container">
                    <Link href="/foodieland-app/public">
                        <Logo fontsize={24} />
                    </Link>
                    <MenuButton onClick={handleDrawerToggle} isOpen={isDrawerOpen} />
                    <MobileDrawer isOpen={isDrawerOpen} closeDrawer={closeDrawer} />
                </div>
            </div>
        </>
    );
}