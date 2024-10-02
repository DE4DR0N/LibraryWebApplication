import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { Container, Typography, Button, Card, CardContent, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, CircularProgress, Box } from '@mui/material';
import bookService from '../services/bookService';
import authService from '../services/authService';
import dayjs from 'dayjs';

function BookDetailsPage({ isAdmin }) {
    const { id } = useParams();
    const navigate = useNavigate();

    const [book, setBook] = useState(null);
    const [open, setOpen] = useState(false);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchBook = async () => {
            setLoading(true);
            try {
                const response = await bookService.getBookById(id);
                setBook(response.data);
            } catch (err) {
                console.error('Error fetching book:', err);
            } finally {
                setLoading(false);
            }
        };
        fetchBook();
    }, [id]);

    const handleBorrow = async () => {
        const currentUser = authService.getCurrentUser();
        const userId = currentUser ? currentUser.userId : null;
        const returnDate = dayjs().add(7, 'day').format('YYYY-MM-DD');

        if (userId) {
            try {
                const response = await bookService.issueBookToUser(id, userId, returnDate);
                if (response.data.success) {
                    alert('Book borrowed successfully!');
                    navigate('/books');
                }
            } catch (err) {
                console.error('Error borrowing book:', err);
            }
        } else {
            alert('User not authenticated');
        }
    };

    const handleReturn = async () => {
        try {
            await bookService.returnBook(id);
            alert('Book returned successfully!');
            navigate('/books');
        } catch (err) {
            console.error('Error returning book:', err);
        }
    };

    const handleDelete = async () => {
        try {
            await bookService.deleteBook(id);
            alert('Book deleted successfully!');
            navigate('/books');
        } catch (err) {
            console.error('Error deleting book:', err);
        }
    };

    if (loading) {
        return (
            <Box display="flex" justifyContent="center" alignItems="center" height="100vh">
                <CircularProgress />
            </Box>
        );
    }

    if (!book) {
        return <Typography>No book found</Typography>;
    }

    return (
        <Container>
            <Card>
                <CardContent>
                    <Typography variant="h4">{book.title}</Typography>
                    <Typography variant="body1" gutterBottom>{book.description}</Typography>
                    <Typography variant="subtitle1">Genre: {book.genre}</Typography>
                    <Typography variant="subtitle1">Author: {book.author ? `${book.author.firstName} ${book.author.lastName}` : 'Unknown'}</Typography>

                    {book.isAvailable ? (
                        <Button variant="contained" color="primary" onClick={handleBorrow}>Borrow</Button>
                    ) : (
                        <Button variant="contained" color="secondary" onClick={handleReturn}>Return</Button>
                    )}

                    {isAdmin && (
                        <>
                            <Button variant="outlined" href={`/books/updateBook/${book.id}`} style={{ margin: '10px' }}>Edit</Button>
                            <Button variant="outlined" color="error" onClick={() => setOpen(true)}>Delete</Button>

                            <Dialog open={open} onClose={() => setOpen(false)}>
                                <DialogTitle>Confirm Delete</DialogTitle>
                                <DialogContent>
                                    <DialogContentText>Are you sure you want to delete this book?</DialogContentText>
                                </DialogContent>
                                <DialogActions>
                                    <Button onClick={() => setOpen(false)} color="primary">Cancel</Button>
                                    <Button onClick={handleDelete} color="error">Delete</Button>
                                </DialogActions>
                            </Dialog>
                        </>
                    )}
                </CardContent>
            </Card>
        </Container>
    );
}

export default BookDetailsPage;
