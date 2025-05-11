import { Book } from "../App";

interface Props {
    books: Book[];
    onEdit?: (book: Book) => void;
    onDelete?: (id: number) => void;
}

const BookList = ({ books, onEdit, onDelete }: Props) => {
    if (books.length === 0) {
        return <p>Brak książek do wyświetlenia</p>;
    }

    return (
        <ul className="book-list">
            {books.map((book) => (
                <li key={book.id} className="book-item">
                    <div>
                        <strong>{book.title}</strong> – {book.author}
                        <p>{book.description}</p>
                    </div>

                    {(onEdit || onDelete) && (
                        <div className="book-actions">
                            {onEdit && <button onClick={() => onEdit(book)}>Edytuj</button>}
                            {onDelete && <button onClick={() => onDelete(book.id)}>Usuń</button>}
                        </div>
                    )}


                </li>
            ))}
        </ul>
    );
};

export default BookList;
