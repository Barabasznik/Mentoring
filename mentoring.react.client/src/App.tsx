import { useEffect, useState } from 'react';
import './App.css';
import {
    AuthenticatedTemplate,
    UnauthenticatedTemplate,
    useMsal,
    MsalProvider
} from '@azure/msal-react';
import { IPublicClientApplication, InteractionRequiredAuthError } from '@azure/msal-browser';
import axios from 'axios';
import SignInButton from './SignInButton';

interface Book {
    id: number;
    title: string;
    author: string;
}

function BookList() {
    const { instance, accounts } = useMsal();
    const [books, setBooks] = useState<Book[]>([]);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchBooks = async () => {
            if (accounts.length === 0) return;

            try {
                const response = await instance.acquireTokenSilent({
                    scopes: ["api://11354273-dc58-487d-975c-653135c76928/All.ReadWrite"],
                    account: accounts[0]
                });

                const token = response.accessToken;

                const result = await axios.get("https://localhost:7051/api/books", {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                });

                setBooks(result.data);
            } catch (error: any) {
                if (error instanceof InteractionRequiredAuthError) {
                    try {
                        const response = await instance.acquireTokenPopup({
                            scopes: ["api://11354273-dc58-487d-975c-653135c76928/All.ReadWrite"]
                        });

                        const token = response.accessToken;

                        const result = await axios.get("https://localhost:7051/api/books", {
                            headers: {
                                Authorization: `Bearer ${token}`
                            }
                        });

                        setBooks(result.data);
                    } catch (popupError) {
                        console.error("Błąd podczas acquireTokenPopup:", popupError);
                        setError("Nie udało się uzyskać tokenu przez popup.");
                    }
                } else {
                    console.error("Błąd API:", error);
                    setError("Nie udało się pobrać książek.");
                }
            }
        };

        fetchBooks();
    }, [instance, accounts]);

    return (
        <div style={{ textAlign: "center", padding: "20px" }}>
            <h1>📚 Lista książek</h1>
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
    );
}

function App({ app }: { app: IPublicClientApplication }) {
    return (
        <MsalProvider instance={app}>
            <AuthenticatedTemplate>
                <BookList />
            </AuthenticatedTemplate>

            <UnauthenticatedTemplate>
                <div className="unauthenticated-container">
                    <h1 className="unauthenticated-title">Nie jesteś zalogowany</h1>
                    <SignInButton />
                </div>
            </UnauthenticatedTemplate>
        </MsalProvider>
    );
}

export default App;
