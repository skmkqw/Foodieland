export default function Title({ text, className }: { text: string; className?: string }) {
    return (
        <h1 className={`font-semibold ${className || "text-5xl"}`}>{text}</h1>
    );
}