import { LinkButton } from "@/components";
import React from "react";
import { LogOut } from "lucide-react";
import { logout } from "@/actions/auth";

export default function UserInfo({ userName }: { userName?: string }) {

    const handleLogout = async () => {
        try {
            await logout();
        } catch (error) {
            console.error("Failed to log out:", error);
        }
    };

    return (
        <div>
            {userName ? (
                <div className="flex items-center gap-3 rounded-lg bg-primary py-2 px-3 cursor-pointer">
                    <p className="text-lg font-medium">{userName}</p>
                    <LogOut size={20} onClick={handleLogout} />
                </div>
            ) : (
                <LinkButton url="/login" buttonText="Log in" additionalStyles="!py-3" />
            )}
        </div>
    );
}