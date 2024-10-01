import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';

function App() {
    return (
        <Router>
            <AppBar position="static">
                <Toolbar>
                    <Typography variant="h6" component="div">
                        Library Application
                    </Typography>
                </Toolbar>
            </AppBar>
            <Routes>
                <Route path="/login" element={<LoginPage />} />
                <Route path="/register" element={<RegisterPage />} />
                <Route path="/books" element={<BooksListPage />} />
                <Route path="/books/:id" element={<BookDetailsPage />} />
                <Route path="/books/add" element={<BookFormPage />} />
                <Route path="/books/edit/:id" element={<BookFormPage />} />
                <Route path="/authors" element={<AuthorsListPage />} />
                <Route path="/authors/:id" element={<AuthorDetailsPage />} />
                <Route path="/user/:id" element={<UserBooksPage />} />
            </Routes>
        </Router>
    );
}

export default App;
