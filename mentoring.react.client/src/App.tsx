import { useState } from 'react';
import './App.css';
import axios from 'axios';
import { useMsal, AuthenticatedTemplate, UnauthenticatedTemplate } from '@azure/msal-react';
import { InteractionRequiredAuthError } from '@azure/msal-browser';
import SignInButton from './SignInButton';

interface Book {
    id: number;
    title: string;
    description: string;
    author: string;
}

const App = () => {
    const { instance, accounts } = useMsal();
    const [books, setBooks] = useState<Book[]>([]);
    const [error, setError] = useState<string | null>(null);

    const fetchBooks = async () => {
        if (accounts.length === 0) return;

        const request = {
            scopes: ["api://11354273-dc58-487d-975c-653135c76928/All.ReadWrite"],
            account: accounts[0],
        };

        try {
            const response = await instance.acquireTokenSilent(request);
            const result = await axios.get("https://localhost:7051/api/books", {
                headers: {
                    Authorization: `Bearer ${response.accessToken}`,
                }
            });
            setBooks(result.data);
        } catch (error: any) {
            console.warn("Silent token error", error);

            // Jeśli potrzebna zgoda → acquireTokenPopup
            if (error instanceof InteractionRequiredAuthError) {
                try {
                    const popupResponse = await instance.acquireTokenPopup(request);
                    const result = await axios.get("https://localhost:7051/api/books", {
                        headers: {
                            Authorization: `Bearer ${popupResponse.accessToken}`,
                        }
                    });
                    setBooks(result.data);
                } catch (popupError) {
                    console.error("Błąd podczas acquireTokenPopup:", popupError);
                    setError("Nie udało się pobrać książek.");
                }
            } else {
                setError("Błąd pobierania tokenu.");
            }
        }
    };

    return (
        <>
            <AuthenticatedTemplate>
                <div style={{ textAlign: "center", padding: "20px" }}>
                    <h1>📚 Lista Książek</h1>
                    <button onClick={fetchBooks}>Załaduj książki</button>
                    {error && <p style={{ color: "red" }}>{error}</p>}
                    {books.length > 0 ? (
                        <ul>
                            {books.map((book) => (
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
                <div className="unauthenticated-container">
                    <h1>Nie jesteś zalogowany</h1>
                    <SignInButton />
                </div>
            </UnauthenticatedTemplate>
        </>
    );
};

export default App;
