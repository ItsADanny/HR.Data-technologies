import { useState, useEffect, useCallback } from 'react';

const SESSION_STORAGE_KEY = 'pcweb_session_token';

export const useAuth = () => {
    const [sessionToken, setSessionToken] = useState<string | null>(null);

    // Load session token from localStorage on mount
    useEffect(() => {
        const savedToken = localStorage.getItem(SESSION_STORAGE_KEY);
        if (savedToken) {
            setSessionToken(savedToken);
        }
    }, []);

    const login = useCallback((token: string) => {
        localStorage.setItem(SESSION_STORAGE_KEY, token);
        setSessionToken(token);
    }, []);

    const logout = useCallback(async () => {
        const token = sessionToken ?? localStorage.getItem(SESSION_STORAGE_KEY);

        if (token) {
            try {
                await fetch(`http://localhost:5221/api/User/logout?sessionToken=${encodeURIComponent(token)}`, {
                    method: 'PUT',
                });
            } catch (err) {
                console.error('Failed to log out on server:', err);
            }
        }

        localStorage.removeItem(SESSION_STORAGE_KEY);
        setSessionToken(null);
    }, [sessionToken]);

    return {
        sessionToken,
        isLoggedIn: sessionToken !== null,
        login,
        logout,
    };
};
