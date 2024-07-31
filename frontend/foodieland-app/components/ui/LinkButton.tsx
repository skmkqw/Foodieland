import Link from "next/link";
import { Button } from "@/components";

export default function LinkButton({ url, buttonText, additionalStyles, children }: { url: string, buttonText: string, additionalStyles?: string, children?: React.ReactNode }) {
    return (
        <Link href={url}>
            <Button type={"button"} text={buttonText} additionalStyles={additionalStyles} children={children}/>
        </Link>
    );
}