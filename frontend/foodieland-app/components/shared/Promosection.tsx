"use client";

import Logo from "@/components/shared/Logo";
import { useEffect, useState } from "react";

export default function Promosection() {
    const [mobile, setMobile] = useState(false);
    const [mobileHorizontal, setMobileHorizontal] = useState(false);

    useEffect(() => {
        const updateDimensions = () => {
            setMobile(window.innerWidth > 768);
            setMobileHorizontal(window.innerHeight < 650);
        };

        updateDimensions();

        window.addEventListener("resize", updateDimensions);
        return () => {
            window.removeEventListener("resize", updateDimensions);
        };
    }, []);

    return (
        mobile ? <div className="flex-[0_1_50%] p-4">
            <div
                className={`bg-primary flex items-center justify-end p-8 rounded-3xl ${!mobileHorizontal ? "h-full" : ""}`}>
                <div className="flex flex-col font-bold text-center lg:text-start gap-7 p-5">
                    <div className="flex-col items-center lg:flex-row lg:items-baseline gap-2.5 mb-2.5">
                        <h1 className="text-4xl">Welcome to </h1>
                        <Logo fontsize={36} />
                    </div>
                    <p className="text-2xl">Discover the best recipes, all in one place.</p>
                    <ul className="flex flex-col items-center lg:items-start gap-5 mb-5 p-0 list-none">
                        <li className="text-xl bg-[rgba(0,0,0,0.05)] mb-2.5 p-4 rounded-full">🍔 Easy-to-follow recipes
                        </li>
                        <li className="text-xl bg-[rgba(0,0,0,0.05)] mb-2.5 p-4 rounded-full">🌟 User reviews and
                            ratings
                        </li>
                        <li className="text-xl bg-[rgba(0,0,0,0.05)] mb-2.5 p-4 rounded-full">📱 Access from any device
                        </li>
                    </ul>
                    <blockquote className="italic text-lg text-[#555]">
                        "Foodieland has changed the way I cook. It's my go-to source for new recipes!" - Happy User
                    </blockquote>
                </div>
            </div>
        </div> : <></>
    );
}