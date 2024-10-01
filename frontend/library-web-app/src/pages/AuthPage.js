import React, { useState } from 'react';
import authService from '../services/authService';
import { useNavigate } from 'react-router-dom';

const AuthPage = () => {
    const [isLogin, setIsLogin] = useState(true);
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (isLogin) {
            const response = await authService.login(username, password);
            if (response.data.token) {
                localStorage.setItem('token', response.data.token);
                navigate('/books');
            }
        } else {
            if (password === confirmPassword) {
                await authService.register(username, password, confirmPassword);
                setIsLogin(true);
            } else {
                alert("Passwords don't match");
            }
        }
    };

    return (
        <div>
            <h1>{isLogin ? 'Login' : 'Register'}</h1>
            <form onSubmit={handleSubmit}>
                <input type="text" placeholder="Username" value={username} onChange={(e) => setUsername(e.target.value)} required />
                <input type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)} required />
                {!isLogin && (
                    <input type="password" placeholder="Confirm Password" value={confirmPassword} onChange={(e) => setConfirmPassword(e.target.value)} required />
                )}
                <button type="submit">{isLogin ? 'Login' : 'Register'}</button>
            </form>
            <button onClick={() => setIsLogin(!isLogin)}>
                {isLogin ? 'Go to Register' : 'Go to Login'}
            </button>
        </div>
    );
};

export default AuthPage;
