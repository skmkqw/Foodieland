import { Container, FilterSidebar, Title } from "@/components";

const filterGroups = [
    {
        groupName: "Categories",
        filterNames: ["Luch", "Dinner", "Vegan", "Seafood"]
    },
    {
        groupName: "Cooking time",
        filterNames: ["< 5 min", "5-15 min", "15-30 min", "45+ min"]
    }
];

export default function FavouritePage() {
    return (
        <Container className="w-full py-10">
            <Title text="Recipes that bring happiness to your table ❤️" className="text-4xl" />
            <div className="grid grid-cols-5 mt-10">
                <FilterSidebar className="col-span-1" groups={filterGroups}/>
                <div className="col-span-4">

                </div>
            </div>
        </Container>
    );
}