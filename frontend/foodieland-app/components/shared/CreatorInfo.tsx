import Image from "next/image";

interface CreatorInfoProps {
    image: string | null;
    name: string;
    creationDate: string,
    className?: string
}

export default function CreatorInfo({ image, name, creationDate, className }: CreatorInfoProps) {
    return (
        <div className={`${className} flex gap-3 items-center`}>
            <Image
                src={image ? `data:image/jpeg;base64,${image}` : "/user-placeholder.jpg"}
                alt="Portrait"
                height={1000}
                width={1000}
                className="rounded-[100%] w-[50px] h-[50px]"
            />
            <div className="flex flex-col gap-2">
                <b className="text-lg">{name}</b>
                <p className="text-black text-opacity-60">{creationDate}</p>
            </div>
        </div>
    );
}