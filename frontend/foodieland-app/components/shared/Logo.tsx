import { Lobster } from "next/font/google";

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
        <h3 className={lobster.className} style={textStyle}>
            Foodieland<span style={dotStyle}>.</span>
        </h3>
    );
}
