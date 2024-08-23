import { Footer, Header } from "@/components";

export default function RootLayout({ children }: Readonly<{ children: React.ReactNode; }>) {
    return (
        <div>
            <Header />
            {children}
            <Footer />
        </div>
    );
}
