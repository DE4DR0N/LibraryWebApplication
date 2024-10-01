import React from 'react';
import { useParams } from 'react-router-dom';
import { Typography, Paper } from '@mui/material';

const AuthorDetail = ({ authors }) => {
  const { id } = useParams();
  const author = authors.find(author => author.id === parseInt(id));

  if (!author) {
    return <Typography variant="h6">Author not found</Typography>;
  }

  return (
    <Paper style={{ padding: 16 }}>
      <Typography variant="h4" gutterBottom>{author.name}</Typography>
      <Typography variant="body1" paragraph>{author.bio}</Typography>
    </Paper>
  );
};

export default AuthorDetail;
