import { useEffect, useState } from "react";
import BookForm from "../components/BookForm";
import BookList from "../components/BookList";
import { Book } from "../types/Book";
import apiServer from "../services/apiServer";
import "../styles/Dashboard.css";
import SignOutButton from "../SignOutButton";

const LibrarianPage = () => {
    const [books, setBooks] = useState<Book[]>([]);
    const [editingBook, setEditingBook] = useState<Book | null>(null);

    const fetchBooks = async () => {
        const books = await apiServer.getBooks();
        setBooks(books);
    };

    useEffect(() => {
        fetchBooks();
    }, []);

    const handleAddOrUpdateBook = async (book: Book | Omit<Book, "id">) => {
        if ("id" in book) {
            await apiServer.updateBook(book.id, book);
        } else {
            await apiServer.addBook(book);
        }
        setEditingBook(null);
        fetchBooks();
    };

    return (
        <div className="dashboard-container">
            <h1>📘 Panel Bibliotekarza</h1>
            <BookForm
                onSubmit={handleAddOrUpdateBook}
                initialBook={editingBook}
                cancelEdit={() => setEditingBook(null)}
            />
            <BookList books={books} onEdit={setEditingBook} />
            <SignOutButton />
        </div>
    );
};

export default LibrarianPage;
