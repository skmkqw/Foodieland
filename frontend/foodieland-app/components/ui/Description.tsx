export default function Description({ text, className }: { text: string; className?: string }) {
    return (
        <p className={`text-base text-gray-500 ${className}`}>{text}</p>
    );
}