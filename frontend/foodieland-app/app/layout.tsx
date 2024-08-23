import { Metadata } from "next";
import { Inter } from "next/font/google";
import "./globals.css";
import { Toaster } from "sonner";

const inter = Inter({ subsets: ["latin"] });
export const metadata: Metadata = {
    title: "Foodieland",
    description: "Explore hundreds of mind-blowing recipes",
    icons: "/icon.png",
    keywords: "recipes, cooking, food, cuisine, easy recipes, gourmet",
    openGraph: {
        title: "Foodieland",
        description: "Explore hundreds of mind-blowing recipes",
        url: "https://www.foodieland.com",
        type: "website"
    }
};

export default function RootLayout({ children }: Readonly<{ children: React.ReactNode; }>) {
    return (
        <html lang="en">
        <body className={inter.className}>
        {children}
        <Toaster
            position="top-right"
            toastOptions={{
            classNames: {
                error: "bg-white",
                success: "bg-primary",
            }
        }} />
        </body>
        </html>
    );
}
