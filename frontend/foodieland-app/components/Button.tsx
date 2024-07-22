'use client'

import {MouseEventHandler} from "react";

interface ButtonProps {
    type: "button" | "submit" | "reset",
    text: string,
    additionalStyles?: string,
    handleClick?: MouseEventHandler<HTMLButtonElement>
}
export default function Button({type, text, additionalStyles, handleClick} : ButtonProps)
{
    return (
        <button type={type} onClick={handleClick} className={`${additionalStyles} bg-black text-white font-medium text-lg transition-[background-color] duration-[0.3s] ease-[ease] cursor-pointer px-10 py-[15px] rounded-2xl hover:bg-[rgba(0,0,0,0.8)]`}>
            {text}
        </button>
    );
}