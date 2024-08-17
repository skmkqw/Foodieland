"use client";

import { useState } from "react";
import { Container, Logo, MenuButton, NavLinks, UserInfo } from "@/components";
import MobileDrawer from "@/components/shared/header/MobileDrawer";

export default function NavBar({ userName }: { userName?: string }) {
    const [isDrawerOpen, setIsDrawerOpen] = useState(false);

    const openDrawer = () => {
        document.body.style.overflow = 'hidden';
        setIsDrawerOpen(true);
    };

    const closeDrawer = () => {
        document.body.style.overflow = 'auto';
        setIsDrawerOpen(false);
    }

    return (
        <>
            <div className="border-b-gray-200 border-b">
                <Container className="hidden relative py-6 md:flex justify-between items-center gap-6">
                    <Logo fontsize={24} />
                    <NavLinks />
                    <UserInfo userName={userName} />
                </Container>
            </div>
            <div className="border-b-gray-200 border-b">
                <Container className="md:hidden relative flex items-center justify-between flex-row my-4">
                    <Logo fontsize={24} />
                    <MenuButton onClick={isDrawerOpen ? closeDrawer : openDrawer} isActive={isDrawerOpen} />
                    <MobileDrawer isOpen={isDrawerOpen} closeDrawer={closeDrawer} userName={userName} />
                </Container>
            </div>
        </>
    );
}