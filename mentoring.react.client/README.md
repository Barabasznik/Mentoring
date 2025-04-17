# 📚 Lista Książek – Aplikacja React + Azure AD

## 🧩 Opis projektu

Aplikacja umożliwia zalogowanym użytkownikom:
- przeglądanie listy książek,
- dodawanie nowych książek,
- edycję istniejących,
- usuwanie książek.

Uwierzytelnianie odbywa się przez **Azure AD** przy użyciu biblioteki `@azure/msal-react`. Komunikacja z API zabezpieczona jest tokenem JWT.

---

## 🔧 Struktura katalogów

```
📁 src
├── 📁 components
│   ├── BookForm.tsx
│   └── BookList.tsx
├── 📁 services
│   ├── apiClient.ts
│   ├── apiServer.ts
│   └── authconfig.ts
├── 📁 types
│   └── Book.ts
├── App.tsx
├── SignInButton.tsx
└── index.tsx
```

---

## 📦 Technologie

- **React 18**
- **TypeScript**
- **Axios**
- **MSAL** (Microsoft Authentication Library)
- **Azure AD**

---

## ✅ Główne komponenty

### `App.tsx`
Zarządza logiką pobierania danych i stanem aplikacji.

### `BookForm.tsx`
Formularz dodawania i edytowania książki.

### `BookList.tsx`
Lista książek z możliwością edycji i usuwania.

---

## 🌐 Serwisy

### `apiClient.ts`
Axios + interceptor do tokenów.

### `apiServer.ts`
Abstrakcja dla zapytań HTTP: `getBooks`, `addBook`, `updateBook`, `deleteBook`.

### `authconfig.ts`
Konfiguracja MSAL i obsługa kont użytkowników.

---

## 🔐 Uwierzytelnianie

Użytkownik loguje się przez Azure AD. Token pozyskiwany jest za pomocą:

- `acquireTokenSilent()`
- `acquireTokenPopup()` (jeśli potrzebna interakcja)

---

## 📌 Wymagania

- Rejestracja aplikacji w Azure AD
- Scope `api://{clientId}/All.ReadWrite`
- Udzielone uprawnienia

---

## 💡 Możliwe rozszerzenia

- Walidacja formularzy (Formik / Yup)
- Paginacja i filtrowanie
- Obsługa spinnerów i loadingów
- Testy jednostkowe

---

## 🧑‍💻 Autorzy

Projekt stworzony w ramach mentoringu.