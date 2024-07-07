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
        <button type={type} onClick={handleClick} className={`btnMain ${additionalStyles}`}>
            {text}
        </button>
    );
}