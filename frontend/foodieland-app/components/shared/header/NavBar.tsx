"use client";

import { useState } from "react";
import Link from "next/link";
import { Container, Logo, MenuButton, NavLinks, SocialLinks } from "@/components";
import MobileDrawer from "@/components/shared/header/MobileDrawer";

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
                <Container>
                    <div className="hidden relative py-6 md:flex justify-between items-center gap-6">
                        <Link href="/foodieland-app/public">
                            <Logo fontsize={24} />
                        </Link>
                        <NavLinks />
                        <SocialLinks />
                    </div>
                </Container>
            </div>
            <div className="border-b-gray-200 border-b">
                <Container>
                    <div className="md:hidden relative flex items-center justify-between flex-row my-4">
                        <Link href="/foodieland-app/public">
                            <Logo fontsize={24} />
                        </Link>
                        <MenuButton onClick={handleDrawerToggle} isOpen={isDrawerOpen} />
                        <MobileDrawer isOpen={isDrawerOpen} closeDrawer={closeDrawer} />
                    </div>
                </Container>
            </div>
        </>
    );
}