import Link from "next/link";
import { Button } from "@/components/index";

export default function LinkButton({ url, buttonText }: { url: string, buttonText: string }) {
    return (
        <Link href={url}>
            <Button type={"button"} text={buttonText} />
        </Link>
    );
}