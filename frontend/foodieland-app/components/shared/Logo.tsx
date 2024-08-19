import { Lobster } from "next/font/google";
import Link from "next/link";

const lobster = Lobster({
    weight: "400",
    subsets: ["latin"]
});

export default function Logo({ fontsize }: { fontsize: number | string }) {
    const textStyle = {
        fontSize: fontsize
    };

    const dotStyle = {
        color: "orange"
    };

    return (
        <Link href="/">
            <h3 className={lobster.className} style={textStyle}>
                Foodieland<span style={dotStyle}>.</span>
            </h3>
        </Link>
    );
}
