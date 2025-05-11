import apiClient from "./apiClient";
import { Book } from "../types/Book";

export const getBooks = async (): Promise<Book[]> => {
    const response = await apiClient.get("/api/books");
    return response.data;
};

export const addBook = async (book: Omit<Book, "id">) => {
    return apiClient.post("/api/books", book);
};

export const updateBook = async (book: Book) => {
    return apiClient.put(`/api/books/${book.id}`, book);
};

export const deleteBook = async (id: number) => {
    return apiClient.delete(`/api/books/${id}`);
};
