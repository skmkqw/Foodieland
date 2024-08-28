import { Container, Description, Error, Inbox, RecipeCard, Title } from "@/components";
import { fetchRecipes } from "@/actions/recipes";

const articleContent = [
    {
        title: "Our Mission",
        description: "Welcome to Foodieland, your go-to destination for discovering, saving, and sharing your favorite recipes! Whether you're a seasoned chef or just starting your culinary journey, Foodieland is here to show you that cooking can be easy, enjoyable, and endlessly rewarding. Our mission is to inspire a love for cooking by providing you with a diverse collection of recipes that are simple to follow and guaranteed to delight your taste buds.",
    },
    {
        title: "Explore & Save Recipes",
        description: "At Foodieland, we believe that great cooking is for everyone. That’s why we’ve partnered with some of the world’s most renowned chefs to bring you recipes that are not only delicious but also accessible to all skill levels. Each recipe on our platform is carefully crafted and certified by top culinary experts, ensuring that you can create restaurant-quality meals in the comfort of your own kitchen.",
    },
    {
        title: "Certified by World-Class Chefs",
        description: "Explore our vast library of recipes, from quick weeknight dinners to gourmet dishes that will impress your guests. Save your favorites, create personalized collections, and come back whenever you need a dose of culinary inspiration. Whether you’re cooking for yourself, your family, or a special occasion, Foodieland has something for everyone.",
    },
    {
        title: "Join the Foodieland Community",
        description: "Join our vibrant community of food lovers, share your own recipes, and embark on a culinary adventure like no other. With Foodieland, cooking is not just a necessity—it’s a passion. Let’s make every meal a masterpiece, one recipe at a time.",
    },
];

export default async function AboutPage() {
    const data = await fetchRecipes(1, 3);
    if (!data) {
        return <Error errorMessage="Failed to fetch recipes." />;
    }

    const { totalAmount, recipes } = data;

    return (
        <Container className="w-full flex flex-col items-center gap-20 py-10 text-center">
            <div className="flex flex-col items-center gap-10">
                <Title text="What is Foodieland?" className="text-5xl md:text-6xl" />
                {articleContent.map((item, idx) => (
                    <div className="flex flex-col items-center gap-6 max-w-6xl border-primary border-4 rounded-3xl p-10 shadow-[0_3px_10px_rgb(0,0,0,0.2)]" key={idx}>
                        <Title text={item.title} className="text-3xl" />
                        <Description text={item.description} className="text-xl" />
                    </div>
                ))}
            </div>
            <div className="w-full">
                <Title text="Check out some delicious recipes" className="text-4xl" />
                {recipes && recipes.length != 0 ?
                    <div className="w-full grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-10 mt-10">
                        {recipes.map((recipe, idx) => (
                            <RecipeCard recipe={recipe} key={idx} />
                        ))}
                    </div>
                    : <Error errorMessage="Oops! An unexpected error occurred while fetching recipes. Please try again later." className="mt-10"/>
                }
            </div>
            <Inbox />
        </Container>
    );
}