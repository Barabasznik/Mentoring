import SignInButton from "../SignInButton";
import "../styles/FirstPageLogin.css"

const FirstPageLogin = () => {
    return (
        <div className="landing-container">

            <h1>📚 Witamy w MentoringBook</h1>
            <p>
                MentoringBook to aplikacja do zarządzania książkami i wspierania rozwoju przez czytanie.
                Jako Admin, Bibliotekarz lub Członek – masz dostęp do odpowiednich funkcji systemu.
            </p>
            <SignInButton />
        </div>
    )
}

export default FirstPageLogin;