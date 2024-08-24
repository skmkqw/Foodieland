export default function Description({ text, className }: { text: string; className?: string }) {
    return (
        <p className={`text-gray-500 ${className || "text-base"}`}>{text}</p>
    );
}