import { Container } from "@/components";

export default function FeaturedRecipeSkeleton() {
    return (
        <Container className="w-full">
            <div
                className="flex flex-col-reverse lg:flex-row rounded-3xl bg-primary lg:h-[640px] h-full cursor-pointer animate-pulse">
                <div className="flex-1/2 flex flex-col justify-between gap-12 md:gap-24 py-6 md:py-12 px-4 sm:px-10">
                    <div
                        className="flex flex-col gap-6 md:gap-12 items-center text-center base:text-start base:items-start">
                        <div className="bg-white rounded-full h-10 w-32"></div>
                        <div className="flex flex-col gap-6 md:gap-10">
                            <div className="w-full h-12 bg-black opacity-5 rounded-full"></div>
                            <div className="leading-7 text-lg h-8 w-3/4 bg-black opacity-5 rounded-full"></div>
                            <div className="flex flex-col xs:flex-row justify-center base:justify-start gap-4">
                                <div
                                    className="bg-black bg-opacity-5 py-3 px-5 rounded-full flex gap-4 items-center justify-center xs:justify-start h-8 w-20"></div>
                                <div
                                    className="bg-black bg-opacity-5 p-3 rounded-full flex gap-4 items-center justify-center xs:justify-start h-8 w-20"></div>
                            </div>
                        </div>
                    </div>
                    <div className="flex flex-col gap-6 base:flex-row base:justify-between items-center w-full">
                        <div className="flex gap-3 items-center">
                            <div className="h-12 w-12 rounded-full bg-white"></div>
                            <div>
                                <div className="h-4 w-36 bg-white mb-1"></div>
                                <div className="h-4 w-20 bg-white"></div>
                            </div>
                        </div>
                        <div className="rounded-full h-10 w-32 bg-white mt-2"></div>
                    </div>
                </div>
                <div className="bg-primary lg:flex-1/2 rounded-3xl lg:rounded-r-3xl lg:rounded-l-none">
                    <div className="p-4 lg:p-10 rounded-3xl lg:rounded-r-3xl lg:rounded-l-none h-full">
                        <div className="bg-white w-full h-full rounded-2xl min-h-80 sm:min-h-96 md:min-h-[430px]"></div>
                    </div>
                </div>
            </div>
        </Container>
    );
}