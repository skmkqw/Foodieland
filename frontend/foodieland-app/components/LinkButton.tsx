import Link from "next/link";
import { Button } from "@/components/index";

export default function LinkButton({ url, buttonText, additionalStyles }: { url: string, buttonText: string, additionalStyles?: string }) {
    return (
        <Link href={url}>
            <Button type={"button"} text={buttonText} additionalStyles={additionalStyles}/>
        </Link>
    );
}