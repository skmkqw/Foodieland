export default function InputGroup({ type, errorMessage } : { type: "email" | "username" | "password", errorMessage?: string }) {
    let inputGroup;

    switch (type) {
        case "email":
            inputGroup = (
                <div className="inputGroup">
                    <label htmlFor="email">E-mail</label>
                    <input
                        type="email"
                        id="email"
                        name="email"
                        placeholder="example@email.com"
                    />
                </div>
            );
            break;
        case "password":
            inputGroup = (
                <div className="inputGroup">
                    <label htmlFor="password">Password</label>
                    <input
                        type="password"
                        id="password"
                        name="password"
                        placeholder="********"
                    />
                </div>
            );
            break;
        case "username":
            inputGroup = (
                <div className="inputGroup">
                    <label htmlFor="username">Full Name</label>
                    <input
                        type="text"
                        id="username"
                        name="username"
                        placeholder="John Doe"
                    />
                </div>
            );
            break;
        default:
            inputGroup = null;
            break;
    }

    return (
        <div>
            {inputGroup}
            {errorMessage && <p className="errorMessage mt-2">{errorMessage}</p>}
        </div>
    );
}