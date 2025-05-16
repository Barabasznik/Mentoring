import { useEffect, useState } from "react";
import BookList from "../components/BookList";
import { Book } from "../types/Book";
import apiServer from "../services/apiServer";
import "../styles/Dashboard.css";
import SignOutButton from "../SignOutButton";

const MemberPage = () => {
    const [books, setBooks] = useState<Book[]>([]);

    useEffect(() => {
        apiServer.getBooks().then(setBooks);
    }, []);

    return (
        <div className="dashboard-container">
            <h1>📖 Panel Czytelnika</h1>
            <BookList books={books} />
            <SignOutButton />
        </div>
    );
};

export default MemberPage;
