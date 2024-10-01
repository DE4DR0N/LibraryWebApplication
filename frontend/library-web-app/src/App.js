import React, { useState, useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import BookList from './components/BookList/BookList';
import BookDetail from './components/BookDetail/BookDetail';
import AuthorList from './components/AuthorList/AuthorList';
import AuthorDetail from './components/AuthorDetail/AuthorDetail';
import { Container } from '@mui/material';

const App = () => {
  const [books, setBooks] = useState([
    { id: 1, title: 'Book 1', description: 'Description 1', author: 'Author 1' },
    { id: 2, title: 'Book 2', description: 'Description 2', author: 'Author 2' },
  ]);

  const [authors, setAuthors] = useState([
    { id: 1, name: 'Author 1', bio: 'Bio 1' },
    { id: 2, name: 'Author 2', bio: 'Bio 2' },
  ]);

  useEffect(() => {
    // Пример вызова setBooks
    setBooks(prevBooks => [
      ...prevBooks,
      { id: 3, title: 'Book 3', description: 'Description 3', author: 'Author 3' }
    ]);

    // Пример вызова setAuthors
    setAuthors(prevAuthors => [
      ...prevAuthors,
      { id: 3, name: 'Author 3', bio: 'Bio 3' }
    ]);
  }, []);

  return (
    <Router>
      <Container>
        <Routes>
          <Route path="/" element={<Navigate to="/books" />} />
          <Route path="/books" element={<BookList books={books} />}/>
          <Route path="/books/:id" element={<BookDetail books={books} />} />
          <Route path="/authors" element={<AuthorList authors={authors} />} />
          <Route path="/authors/:id" element={<AuthorDetail authors={authors} />} />
        </Routes>
      </Container>
    </Router>
  );
};

export default App;
