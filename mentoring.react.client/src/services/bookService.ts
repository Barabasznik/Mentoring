import apiClient from "./apiClient";
import { Book } from "../types/Book";

export const getBooks = async (token: string): Promise<Book[]> => {
    const response = await apiClient.get("/api/books", {
        headers: { Authorization: `Bearer ${token}` }
    });
    return response.data;
};

export const addBook = async (book: Omit<Book, "id">, token: string) => {
    return apiClient.post("/api/books", book, {
        headers: { Authorization: `Bearer ${token}` }
    });
};

export const updateBook = async (book: Book, token: string) => {
    return apiClient.put(`/api/books/${book.id}`, book, {
        headers: { Authorization: `Bearer ${token}` }
    });
};

export const deleteBook = async (id: number, token: string) => {
    return apiClient.delete(`/api/books/${id}`, {
        headers: { Authorization: `Bearer ${token}` }
    });
};
