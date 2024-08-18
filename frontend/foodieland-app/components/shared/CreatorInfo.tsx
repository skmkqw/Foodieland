import Image from "next/image";
import { RecipeCreator } from "@/types";

interface CreatorInfoProps {
    creator: RecipeCreator,
    creationDate: string,
    className?: string
}

export default function CreatorInfo({ creator, creationDate, className }: CreatorInfoProps) {
    return (
        <div className={`${className} flex gap-3 items-center`}>
            <Image
                src={creator.userImage ? `data:image/jpeg;base64,${creator.userImage}` : "/user-placeholder.jpg"}
                alt="Portrait"
                height={1000}
                width={1000}
                className="rounded-[100%] w-[50px] h-[50px]"
            />
            <div className="flex flex-col gap-2">
                <b className="text-lg">{creator.creatorName}</b>
                <p className="text-black text-opacity-60">{creationDate}</p>
            </div>
        </div>
    );
}