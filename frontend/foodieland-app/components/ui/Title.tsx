export default function Title({ text, className }: { text: string; className?: string }) {
    return (
        <h1 className={`font-semibold text-5xl ${className}`}>{text}</h1>
    );
}