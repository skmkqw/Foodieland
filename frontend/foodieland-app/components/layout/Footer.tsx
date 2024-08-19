import { Container, Logo, NavLinks } from "@/components";
import Link from "next/link";

export default function Footer() {
    const currentYear = new Date().getFullYear();
    return (
        <Container className="w-full">
            <footer className="flex flex-col mt-20">
                <div className="flex flex-col justify-center gap-6 md:flex-row items-center md:justify-between py-7">
                    <Logo fontsize={24} />
                    <NavLinks />
                </div>
                <span className="block w-full h-[2px] bg-gray-200"></span>
                <div className="flex items-center justify-center py-7 gap-1">
                    <p className="text-gray-500 text-base">&copy;{currentYear} Developed by</p>
                    <Link href="https://github.com/skmkqw" className="text-amber-600 text-base">skmkqw</Link>
                </div>
            </footer>
        </Container>
    );
}