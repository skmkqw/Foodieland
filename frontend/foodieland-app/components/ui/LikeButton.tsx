import { Heart } from "lucide-react";

interface LikeButtonProps {
    isLiked: boolean;
    onToggle: (e: React.MouseEvent) => void;
    className?: string;
}

export default function LikeButton({ isLiked, onToggle, className }: LikeButtonProps) {
    return (
        <button
            onClick={onToggle}
            className={`p-2 rounded-full bg-white shadow-md ${className} z-10`}
        >
            <Heart
                size={24}
                color={isLiked ? "#FF6363" : "#DBE2E5"}
                fill={isLiked ? "#FF6363" : "#DBE2E5"}
                className="transition-all duration-500"
            />
        </button>
    );
}