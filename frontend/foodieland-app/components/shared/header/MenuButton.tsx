export default function MenuButton({ onClick, isOpen }: { onClick: () => void, isOpen: boolean }) {
    return (
        <button onClick={onClick}
                className="flex flex-col justify-center items-center z-20">
            <span className={`bg-black block transition-all duration-300 ease-out 
                            h-[2px] w-6 rounded-sm ${isOpen ?
                "rotate-45 translate-y-[4px]" : "-translate-y-0.5"
            }`} />
            <span className={`bg-black block transition-all duration-300 ease-out 
                    h-[2px] w-6 my-0.5 rounded-sm ${isOpen ?
                "opacity-0" : "opacity-100"
            }`} />
            <span className={`bg-black block transition-all duration-300 ease-out
                        h-[2px] w-6 rounded-sm ${isOpen ?
                "-rotate-45 -translate-y-[4px]" : "translate-y-0.5"
            }`} />
        </button>
    );
}