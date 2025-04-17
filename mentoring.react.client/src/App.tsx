import { useEffect, useState } from "react";
import { useMsal, AuthenticatedTemplate, UnauthenticatedTemplate } from "@azure/msal-react";
import SignInButton from "./SignInButton";
import BookForm from "./components/BookForm";
import BookList from "./components/BookList";
import apiServer from "./services/apiServer";

export interface Book {
    id: number;
    title: string;
    description: string;
    author: string;
}

const App = () => {
    const { accounts } = useMsal();
    const [books, setBooks] = useState<Book[]>([]);
    const [editingBook, setEditingBook] = useState<Book | null>(null);
    const [error, setError] = useState<string | null>(null);

    const fetchBooks = async () => {
        try {
            const books = await apiServer.getBooks();
            setBooks(books);
        } catch (err) {
            console.error("Błąd pobierania książek:", err);
            setError("Nie udało się pobrać książek.");
        }
    };

    useEffect(() => {
        if (accounts.length > 0) {
            fetchBooks();
        }
    }, [accounts]);

    const handleAddOrUpdateBook = async (book: Omit<Book, "id"> | Book) => {
        try {
            if ("id" in book) {
                await apiServer.updateBook(book.id, book);
            } else {
                await apiServer.addBook(book);
            }
            setEditingBook(null);
            fetchBooks();
        } catch (err) {
            console.error("Błąd zapisu książki:", err);
            setError("Nie udało się zapisać książki.");
        }
    };

    const handleDelete = async (id: number) => {
        try {
            await apiServer.deleteBook(id);
            fetchBooks();
        } catch (err) {
            console.error("Błąd usuwania książki:", err);
            setError("Nie udało się usunąć książki.");
        }
    };

    const handleEdit = (book: Book) => {
        setEditingBook(book);
    };

    const handleCancelEdit = () => {
        setEditingBook(null);
    };

    return (
        <>
            <AuthenticatedTemplate>
                <div className="app-container">
                    <h1>📚 Lista Książek</h1>
                    {error && <p style={{ color: "red" }}>{error}</p>}
                    <BookForm onSubmit={handleAddOrUpdateBook} initialBook={editingBook} cancelEdit={handleCancelEdit} />
                    <BookList books={books} onEdit={handleEdit} onDelete={handleDelete} />
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
