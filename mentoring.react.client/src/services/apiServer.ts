import axios from "./apiClient";
import { Book } from "../App";

const getBooks = async (): Promise<Book[]> => {
    const response = await axios.get("/api/books");
    return response.data;
};

const addBook = async (book: Omit<Book, "id">): Promise<Book> => {
    const response = await axios.post("/api/books", book);
    return response.data;
};

const updateBook = async (id: number, book: Omit<Book, "id">): Promise<Book> => {
    const response = await axios.put(`/api/books/${id}`, book);
    return response.data;
};

const deleteBook = async (id: number): Promise<void> => {
    await axios.delete(`/api/books/${id}`);
};

const apiServer = {
    getBooks,
    addBook,
    updateBook,
    deleteBook,
};

export default apiServer;
