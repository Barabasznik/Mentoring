import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';

export interface Book {
  id: number;
  title: string;
  description: string;
  author: string;
}

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private apiUrl = 'https://localhost:7051/api/books'; // <-- Popraw adres API

  constructor(private http: HttpClient) { }

  getBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(this.apiUrl).pipe(
      catchError((error) => {
        console.error('Błąd w serwisie:', error);
        return throwError(() => new Error('Nie udało się pobrać danych z API'));
      })
    );
  }
}
