import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookService, Book } from '../services/book.service';

@Component({
  selector: 'app-book-list',
  standalone: true, 
  imports: [CommonModule], 
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})
export class BookListComponent implements OnInit {
  books: Book[] = [];
  isLoading: boolean = true; 
  errorMessage: string = ''; 

  constructor(private bookService: BookService) { }

  ngOnInit(): void {
    this.bookService.getBooks().subscribe(
      (data) => {
        this.books = data;
        this.isLoading = false;
      },
      (error) => {
        this.errorMessage = 'Błąd podczas pobierania książek!';
        this.isLoading = false;
      }
    );
  }
}
