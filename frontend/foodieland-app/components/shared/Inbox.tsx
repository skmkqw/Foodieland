import { Button, Container, Title } from "@/components";
import Image from "next/image";

export default function Inbox () {
    return (
        <Container className="w-full">
            <div className="relative flex flex-col items-center justify-center p-12 gap-10 bg-primary rounded-3xl text-center">
                <Title text="Deliciousness to your inbox" className="text-4xl sm:text-5xl"/>
                <p className="text-base text-gray-500 max-w-2xl">Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ad aut enim libero neque non quas repellat repellendus tenetur ullam voluptates?</p>
                <form className="flex flex-col base:flex-row items-stretch justify-center gap-5 p-4 bg-white mt-4 rounded-3xl mb-10 z-10">
                    <input type="email" placeholder="Your email address..." className="border-none"/>
                    <Button type="submit" text="Subscribe"/>
                </form>
                <Image src="/inbox-left.png" alt="Food" height={300} width={300} className="absolute bottom-0 left-0 w-80"/>
                <Image src="/inbox-right.png" alt="Food" height={250} width={250} className="absolute bottom-0 right-0 w-70"/>
            </div>
        </Container>
    );
}