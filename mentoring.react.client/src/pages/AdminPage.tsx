import BookForm from "../components/BookForm";
import BookList from "../components/BookList";
import { useState, useEffect } from "react";
import { Book } from "../types/Book";
import apiServer from "../services/apiServer";
import SignOutButton from "../SignOutButton";



const AdminPage = () => {
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

    const handleDelete = async (id: number) => {
        await apiServer.deleteBook(id);
        fetchBooks();
    };

    return (
        <div className="dashboard-container">
            <h1>📚 Panel Administratora</h1>
            <BookForm
                onSubmit={handleAddOrUpdateBook}
                initialBook={editingBook}
                cancelEdit={() => setEditingBook(null)}
            />
            <BookList books={books} onEdit={setEditingBook} onDelete={handleDelete} />
            <SignOutButton />
        </div>
    );
};

export default AdminPage;
