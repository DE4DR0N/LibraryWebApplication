import React from 'react';
import { Link } from 'react-router-dom';
import { List, ListItem, ListItemText, Typography } from '@mui/material';

const AuthorList = ({ authors }) => {
  return (
    <div>
      <Typography variant="h4" gutterBottom>Authors</Typography>
      <List>
        {authors.map(author => (
          <ListItem key={author.id} button component={Link} to={`/authors/${author.id}`}>
            <ListItemText primary={author.name} />
          </ListItem>
        ))}
      </List>
    </div>
  );
};

export default AuthorList;
