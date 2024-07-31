import Image from "next/image";

export default function CategoryCard({ imagePath, name, gradientColors }: { imagePath: string, name: string, gradientColors: string }) {
    return (
        <div className={`rounded-2xl flex flex-col items-center justify-center gap-6 py-8 px-2 cursor-pointer transition-all duration-200 hover:scale-110 bg-gradient-to-b ${gradientColors}`}>
            <Image alt={name} src={imagePath} width={120} height={120} className="ml-8"/>
            <p className="font-semibold text-2xl md:text-xl lg:text-lg">{name}</p>
        </div>
    );
}