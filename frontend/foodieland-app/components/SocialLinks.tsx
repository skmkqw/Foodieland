import Link from "next/link";
import Image from "next/image";

export default function SocialLinks() {
    return (
        <div className="mt-8 md:mt-0 flex items-center gap-9">
            <Link href="#">
                <Image src="/facebook.svg" alt="logo" height={20} width={9} />
            </Link>
            <Link href="#">
                <Image src="/twitter.svg" alt="logo" height={22} width={22} />
            </Link>
            <Link href="#">
                <Image src="/instagram.svg" alt="logo" height={22} width={22} />
            </Link>
        </div>
    );
}