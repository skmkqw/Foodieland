import {Promosection} from "@/components";
export default function AuthLayout({ children } : { readonly children: React.ReactNode; })
{
    return (
        <main className="flex h-full">
            {children}
            <Promosection/>
        </main>
    );
}