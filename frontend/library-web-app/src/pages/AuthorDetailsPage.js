import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { Container, Typography, Button, Card, CardContent, CircularProgress, Box } from '@mui/material';
import authorService from '../services/authorService';

const AuthorPageDetails = () => {
    const { id } = useParams();
    const navigate = useNavigate();

    const [author, setAuthor] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchAuthor = async () => {
            setLoading(true);
            try {
                const response = await authorService.getAuthorById(id);
                setAuthor(response.data);
            } catch (error) {
                console.error('Error fetching author:', error);
            } finally {
                setLoading(false);
            }
        };
        fetchAuthor();
    }, [id]);

    const handleDelete = async () => {
        try {
            await authorService.deleteAuthor(id);
            alert('Author deleted successfully!');
            navigate('/authors');
        } catch (error) {
            console.error('Error deleting author:', error);
        }
    };

    if (loading) {
        return (
            <Box display="flex" justifyContent="center" alignItems="center" height="100vh">
                <CircularProgress />
            </Box>
        );
    }

    if (!author) {
        return <Typography>No author found</Typography>;
    }

    return (
        <Container>
            <Card>
                <CardContent>
                    <Typography variant="h4">{author.firstName} {author.lastName}</Typography>
                    <Typography variant="body1" gutterBottom>{author.biography}</Typography>
                    <Button variant="outlined" href={`/authors/edit/${author.id}`} style={{ margin: '10px' }}>Edit</Button>
                    <Button variant="outlined" color="error" onClick={handleDelete}>Delete</Button>
                </CardContent>
            </Card>
        </Container>
    );
};

export default AuthorPageDetails;
