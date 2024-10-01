import React, { useEffect, useState } from 'react';
import bookService from '../services/bookService';
import { Container, Typography, Button, Box, CircularProgress, Card, CardContent } from '@mui/material';

const BookList = () => {
    const [books, setBooks] = useState([]);
    const [loading, setLoading] = useState(true);
    const [page, setPage] = useState(1);
    const [pageSize] = useState(10);

    useEffect(() => {
        const fetchBooks = async () => {
            setLoading(true);
            const response = await bookService.getAllBooks(page, pageSize);
            setBooks(response.data.items);
            setLoading(false);
        };
        fetchBooks();
    }, [page, pageSize]);

    if (loading) {
        return (
            <Box display="flex" justifyContent="center" alignItems="center" height="100vh">
                <CircularProgress />
            </Box>
        );
    }

    if (books.length === 0) {
        return <Typography>No books available at the moment</Typography>;
    }

    return (
        <Container>
            {books.map(book => (
                <Card key={book.id} style={{ marginBottom: '20px' }}>
                    <CardContent>
                        <Typography variant="h5">{book.title}</Typography>
                        <Typography variant="body2">{book.description}</Typography>
                    </CardContent>
                </Card>
            ))}
            <Box display="flex" justifyContent="space-between" marginTop="20px">
                <Button variant="contained" color="primary" onClick={() => setPage(page - 1)} disabled={page === 1}>
                    Previous
                </Button>
                <Button variant="contained" color="primary" onClick={() => setPage(page + 1)}>
                    Next
                </Button>
            </Box>
        </Container>
    );
};

export default BookList;