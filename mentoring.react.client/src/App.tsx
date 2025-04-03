import { useEffect, useState } from 'react';
import './App.css';
import { AuthenticatedTemplate, MsalProvider, UnauthenticatedTemplate, useMsal } from '@azure/msal-react';
import { IPublicClientApplication } from "@azure/msal-browser";

interface Book {
    id: number;
    title: string;
    description: string;
    author: string;
}

function App({ app }: { app: IPublicClientApplication }) {
    const [books, setBooks] = useState<Book[]>([]);
    const [error, setError] = useState<string | null>(null);

    function SignInButton() {
        const { instance } = useMsal();

        const handleLogin = () => {
            instance.loginRedirect({
                scopes: ["user.read"]
            });

        };
        return (
            <button onClick={handleLogin} style={{ padding: "10px 20px", background: "#0078d4", color: "white" }}>
                Zaloguj się kontem Microsoft
            </button>
        );
    }


    useEffect(() => {
        fetch("https://localhost:7051/api/books", {
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "Accept": "application/json"
            }
        })
            .then(response => response.text())
            .then(text => {
                console.log("📜 Otrzymany surowy tekst z API:", text);
                return JSON.parse(text);
            })
            .then(data => {
                console.log("📦 Otrzymane dane JSON:", data);
                setBooks(data);
            })
            .catch(error => {
                console.error("❌ Błąd API:", error.message);
                setError(error.message);
            });

    }, []);

    return (
        <MsalProvider instance={app}>
            <AuthenticatedTemplate>
                <div style={{ textAlign: "center", padding: "20px" }}>
                    <h1>Lista Książek</h1> { }
                    {error && <p style={{ color: "red" }}>{error}</p>}
                    {books.length > 0 ? (
                        <ul>
                            {books.map(book => (
                                <li key={book.id}>
                                    <strong>{book.title}</strong> - {book.author}
                                </li>
                            ))}
                        </ul>
                    ) : (
                        <p>Brak książek do wyświetlenia</p>
                    )}
                </div>
            </AuthenticatedTemplate>


            <UnauthenticatedTemplate>
                <div>
                    <h1>Nie jesteś zalogowany</h1>
                    <SignInButton />
                </div>
            </UnauthenticatedTemplate>
        </MsalProvider>

    );
}

export default App;
