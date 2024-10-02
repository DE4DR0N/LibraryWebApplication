import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { Container, TextField, Button, Typography, Box, CircularProgress, MenuItem, Select, InputLabel, FormControl } from '@mui/material';
import bookService from '../services/bookService';
import authorService from '../services/authorService';

const AddEditBookPage = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const isEditMode = Boolean(id);

    const [book, setBook] = useState({
        title: '',
        isbn: '',
        description: '',
        genre: '',
        authorId: ''
    });
    const [authors, setAuthors] = useState([]);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        const fetchAuthors = async () => {
            try {
                const response = await authorService.getAllAuthors();
                console.log(response.data)
                setAuthors(response.data);
            } catch (err) {
                console.error('Error fetching authors:', err);
            }
        };
        fetchAuthors();
    }, []);

    useEffect(() => {
        if (isEditMode) {
            const fetchBook = async () => {
                setLoading(true);
                try {
                    const response = await bookService.getBookById(id);
                    setBook({
                        title: response.data.title,
                        isbn: response.data.isbn,
                        description: response.data.description,
                        genre: response.data.genre,
                        authorId: response.data.author.id
                    });
                } catch (err) {
                    console.error('Error fetching book:', err);
                } finally {
                    setLoading(false);
                }
            };
            fetchBook();
        }
    }, [id, isEditMode]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setBook((prevBook) => ({
            ...prevBook,
            [name]: value
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);
        try {
            if (isEditMode) {
                await bookService.updateBook(id, book);
                alert('Book updated successfully!');
            } else {
                await bookService.addBook(book);
                alert('Book added successfully!');
            }
            navigate('/books');
        } catch (err) {
            console.error('Error saving book:', err);
        } finally {
            setLoading(false);
        }
    };

    if (loading) {
        return (
            <Box display="flex" justifyContent="center" alignItems="center" height="100vh">
                <CircularProgress />
            </Box>
        );
    }

    return (
        <Container>
            <Typography variant="h4" gutterBottom>
                {isEditMode ? 'Edit Book' : 'Add Book'}
            </Typography>
            <form onSubmit={handleSubmit}>
                <TextField
                    label="Title"
                    name="title"
                    value={book.title}
                    onChange={handleChange}
                    fullWidth
                    margin="normal"
                    required
                />
                <TextField
                    label="ISBN"
                    name="isbn"
                    value={book.isbn}
                    onChange={handleChange}
                    fullWidth
                    margin="normal"
                    required
                />
                <TextField
                    label="Description"
                    name="description"
                    value={book.description}
                    onChange={handleChange}
                    fullWidth
                    margin="normal"
                    required
                />
                <TextField
                    label="Genre"
                    name="genre"
                    value={book.genre}
                    onChange={handleChange}
                    fullWidth
                    margin="normal"
                    required
                />
                
                <FormControl fullWidth margin="normal">
                    <InputLabel id="author-label">Author</InputLabel>
                    <Select
                        labelId="author-label"
                        name="authorId"
                        value={book.authorId}
                        onChange={handleChange}
                        required
                    >
                        {authors.map((author) => (
                            <MenuItem key={author.id} value={author.id}>
                                {author.firstName} {author.lastName}
                            </MenuItem>
                        ))}
                    </Select>
                </FormControl>

                <Box mt={2}>
                    <Button type="submit" variant="contained" color="primary">
                        {isEditMode ? 'Update Book' : 'Add Book'}
                    </Button>
                </Box>
            </form>
        </Container>
    );
};

export default AddEditBookPage;
