import { useEffect, useState } from "react";
import { Book } from "../types/Book";

interface Props {
    onSubmit: (book: Book | Omit<Book, "id">) => void;
    initialBook: Book | null;
    cancelEdit: () => void;
}

const BookForm = ({ onSubmit, initialBook, cancelEdit }: Props) => {
    const [title, setTitle] = useState("");
    const [description, setDescription] = useState("");
    const [author, setAuthor] = useState("");

    useEffect(() => {
        if (initialBook) {
            setTitle(initialBook.title);
            setDescription(initialBook.description);
            setAuthor(initialBook.author);
        } else {
            setTitle("");
            setDescription("");
            setAuthor("");
        }
    }, [initialBook]);

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();

        const book: Book | Omit<Book, "id"> = initialBook
            ? { id: initialBook.id, title, description, author }
            : { title, description, author };

        onSubmit(book);
        setTitle("");
        setDescription("");
        setAuthor("");
    };

    return (
        <form onSubmit={handleSubmit} className="book-form">
            <input
                type="text"
                placeholder="Tytuł"
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                required
            />
            <input
                type="text"
                placeholder="Opis"
                value={description}
                onChange={(e) => setDescription(e.target.value)}
                required
            />
            <input
                type="text"
                placeholder="Autor"
                value={author}
                onChange={(e) => setAuthor(e.target.value)}
                required
            />
            <button type="submit">
                {initialBook ? "Zaktualizuj książkę" : "Dodaj książkę"}
            </button>
            {initialBook && (
                <button onClick={cancelEdit} type="button">
                    Anuluj
                </button>
            )}
        </form>
    );
};

export default BookForm;
