import Image from "next/image";

export default function RecipeCardSkeleton() {
    return (
        <div className="p-4 flex flex-col gap-7 bg-primary rounded-3xl">
            <Image src="/white-bg.png" alt="Recipe image" width={370} height={250} className="rounded-3xl" />
            <div className="flex flex-col gap-5 px-3">
                <div className="rounded-2xl w-60 h-10 bg-gray-400" />
                <div className="flex items-center gap-5 pb-9">
                    <div className="flex items-center gap-3">
                        <div className="w-6 h-6 bg-gray-400 rounded-full" />
                        <div className="w-10 h-5 bg-gray-400 rounded-full" />
                    </div>
                    <div className="flex items-center gap-3">
                        <div className="w-6 h-6 bg-gray-400 rounded-full" />
                        <div className="w-10 h-5 bg-gray-400 rounded-full" />
                    </div>
                </div>
            </div>
        </div>
    );
}