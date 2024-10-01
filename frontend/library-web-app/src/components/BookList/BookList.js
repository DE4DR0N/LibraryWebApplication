import React from 'react';
import { Link } from 'react-router-dom';
import { List, ListItem, ListItemText, Typography } from '@mui/material';

const BookList = ({ books }) => {
  return (
    <div>
      <Typography variant="h4" gutterBottom>Books</Typography>
      <List>
        {books.map(book => (
          <ListItem key={book.id} button component={Link} to={`/books/${book.id}`}>
            <ListItemText primary={book.title} />
          </ListItem>
        ))}
      </List>
    </div>
  );
};

export default BookList;
