export default function Error({ errorMessage, className }: { errorMessage: string, className?: string }) {
    return (
        <div className={`${className} rounded-3xl bg-primary flex items-center justify-center p-10 text-center`}>
            <p className="font-medium text-2xl">{errorMessage}</p>
        </div>
    );
}