'use client'
export default function Button({text, onClick})
{
    return (
        <button type={"button"} onClick={onClick} className={"btnMain"}>
            {text}
        </button>
    );
}