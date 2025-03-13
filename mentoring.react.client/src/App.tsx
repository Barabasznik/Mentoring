import { useEffect, useState } from 'react';
import './App.css';

interface Book {
    id: number;
    title: string;
    description: string;
    author: string;
}

function App() {
    const [books, setBooks] = useState<Book[]>([]);
    const [error, setError] = useState<string | null>(null);
   

    

    useEffect(() => {
        fetch("/api/books", {
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
    );
}

export default App;
