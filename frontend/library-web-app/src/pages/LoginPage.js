import React, { useState } from 'react';
import AuthService from '../services/authService';
import { TextField, Button, Container, Typography, Box } from '@mui/material';
import { useNavigate } from 'react-router-dom';

const LoginPage = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const navigate = useNavigate();

    const handleLogin = async () => {
        try {
            await AuthService.login(username, password);
            navigate('/Books')
        } catch (err) {
            setError('Invalid login credentials');
        }
    };

    return (
        <Container maxWidth="sm">
            <Box display="flex" flexDirection="column" alignItems="center" justifyContent="center" height="100vh">
                <Typography variant="h4" gutterBottom>Login</Typography>
                <TextField
                    label="Username"
                    variant="outlined"
                    fullWidth
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                    margin="normal"
                />
                <TextField
                    label="Password"
                    type="password"
                    variant="outlined"
                    fullWidth
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    margin="normal"
                />
                {error && <Typography color="error">{error}</Typography>}
                <Button variant="contained" color="primary" fullWidth onClick={handleLogin}>Login</Button>
                <Button fullWidth href='/Register'>Registration</Button>
            </Box>
        </Container>
    );
};

export default LoginPage;
