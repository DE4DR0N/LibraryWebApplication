import React from 'react';
import { useParams } from 'react-router-dom';
import { Typography, Paper } from '@mui/material';

const BookDetail = ({ books }) => {
  const { id } = useParams();
  const book = books.find(book => book.id === parseInt(id));

  if (!book) {
    return <Typography variant="h6">Book not found</Typography>;
  }

  return (
    <Paper style={{ padding: 16 }}>
      <Typography variant="h4" gutterBottom>{book.title}</Typography>
      <Typography variant="body1" paragraph>{book.description}</Typography>
      <Typography variant="subtitle1">Author: {book.author}</Typography>
    </Paper>
  );
};

export default BookDetail;
