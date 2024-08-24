import { Container, Description, Inbox, Title } from "@/components";

export default function AboutPage() {
    return (
        <Container className="w-full flex flex-col items-center gap-20 py-10 text-center">
            <div className="flex flex-col items-center gap-10 max-w-6xl">
                <Title text="What is Foodieland?" className="text-6xl" />
                <Description
                    text="Welcome to Foodieland, your go-to destination for discovering, saving, and sharing your favorite recipes! Whether you're a seasoned chef or just starting your culinary journey, Foodieland is here to show you that cooking can be easy, enjoyable, and endlessly rewarding. Our mission is to inspire a love for cooking by providing you with a diverse collection of recipes that are simple to follow and guaranteed to delight your taste buds."
                    className="text-xl"
                />
                <Description
                    text="At Foodieland, we believe that great cooking is for everyone. That’s why we’ve partnered with some of the world’s most renowned chefs to bring you recipes that are not only delicious but also accessible to all skill levels. Each recipe on our platform is carefully crafted and certified by top culinary experts, ensuring that you can create restaurant-quality meals in the comfort of your own kitchen."
                    className="text-xl"
                />
                <Description
                    text="Explore our vast library of recipes, from quick weeknight dinners to gourmet dishes that will impress your guests. Save your favorites, create personalized collections, and come back whenever you need a dose of culinary inspiration. Whether you’re cooking for yourself, your family, or a special occasion, Foodieland has something for everyone."
                    className="text-xl"
                />
                <Description
                    text="Join our vibrant community of food lovers, share your own recipes, and embark on a culinary adventure like no other. With Foodieland, cooking is not just a necessity—it’s a passion. Let’s make every meal a masterpiece, one recipe at a time."
                    className="text-xl"
                />
            </div>
            <Inbox />
        </Container>
    );
}