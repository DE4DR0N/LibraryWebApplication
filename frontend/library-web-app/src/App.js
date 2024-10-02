import React, { useState, useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { Container, CssBaseline } from '@mui/material';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import BookList from './pages/BookListPage';
import AddEditBookPage from './pages/AddEditBookPage';
import BookDetailsPage from './pages/BookDetailsPage';
import UserProfilePage from './pages/UserProfilePage';
import Navbar from './components/Navbar';
import authService from './services/authService';

const theme = createTheme({
  palette: {
    mode: 'light',
    primary: {
      main: '#3f51b5',
    },
    secondary: {
      main: '#f50057',
    },
  },
});

const App = () => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [isAdmin, setIsAdmin] = useState(false);

  useEffect(() => {
    const user = authService.getCurrentUser();
    if (user) {
      setIsAuthenticated(true);
      // Assuming there's a way to check if the user is an admin
      setIsAdmin(user.username === 'admin'); // Example condition, adapt it as needed
    }
  }, []);

  const PrivateRoute = ({ children }) => {
    return isAuthenticated ? children : <Navigate to="/login" />;
  };

  const AdminRoute = ({ children }) => {
    return isAuthenticated && isAdmin ? children : <Navigate to="/login" />;
  };

  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <Router>
        <Navbar />
        <Container>
          <Routes>
            <Route path="/login" element={<LoginPage />} />
            <Route path="/register" element={<RegisterPage />} />
            <Route
              path="/books"
              element={
                <PrivateRoute>
                  <BookList />
                </PrivateRoute>
              }
            />
            <Route
              path="/books/add"
              element={
                <AdminRoute>
                  <AddEditBookPage />
                </AdminRoute>
              }
            />
            <Route
              path="/books/updateBook/:id"
              element={
                <AdminRoute>
                  <AddEditBookPage />
                </AdminRoute>
              }
            />
            <Route
              path="/books/:id"
              element={
                <PrivateRoute>
                  <BookDetailsPage isAdmin={isAdmin} />
                </PrivateRoute>
              }
            />
            <Route
              path="/user/:id"
              element={
                <PrivateRoute>
                  <UserProfilePage />
                </PrivateRoute>
              }
            />
            <Route path="*" element={<Navigate to="/books" />} />
          </Routes>
        </Container>
      </Router>
    </ThemeProvider>
  );
};

export default App;
