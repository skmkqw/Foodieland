import { Container, Description, Inbox, Title } from "@/components";

export default function AboutPage() {
    return (
        <Container className="w-full flex flex-col items-center gap-20 py-40">
            <Title text="What is Foodieland?" />
            <Description text="Foodieland is a recipes sharing platform with is created to proove that cooking is easy and enjoable. Find your favourite recipes, save them and come back any minute! All recipes are certified by best chefs of the world! Enjoy cooking with Foodieland!" />
            <Inbox />
        </Container>
    );
}