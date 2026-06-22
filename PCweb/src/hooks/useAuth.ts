import { useState, useEffect, useCallback } from 'react';

const SESSION_STORAGE_KEY = 'pcweb_session_token';

export const useAuth = () => {
    // Initialize synchronously from localStorage so the first render already
    // reflects a logged-in session (avoids a redirect race on direct page loads)
    const [sessionToken, setSessionToken] = useState<string | null>(() => localStorage.getItem(SESSION_STORAGE_KEY));
    const [roleID, setRoleID] = useState<number | null>(null);
    const [userID, setUserID] = useState<number | null>(null);
    const [roleLoading, setRoleLoading] = useState<boolean>(() => localStorage.getItem(SESSION_STORAGE_KEY) !== null);

    // Whenever the session token changes, look up which role the user has
    useEffect(() => {
        if (!sessionToken) {
            setRoleID(null);
            setUserID(null);
            return;
        }

        let cancelled = false;

        const fetchRole = async () => {
            setRoleLoading(true);
            try {
                const sessionRes = await fetch(`http://localhost:5221/api/UserSession/session/sessiontoken/${encodeURIComponent(sessionToken)}`);
                if (!sessionRes.ok) throw new Error("Session not found");
                const session = await sessionRes.json();

                const userRes = await fetch(`http://localhost:5221/api/User/userid/${session.userID}`);
                if (!userRes.ok) throw new Error("User not found");
                const user = await userRes.json();

                if (!cancelled) {
                    setRoleID(user.role_ID ?? null);
                    setUserID(session.userID ?? null);
                }
            } catch {
                if (!cancelled) {
                    setRoleID(null);
                    setUserID(null);
                }
            } finally {
                if (!cancelled) setRoleLoading(false);
            }
        };

        fetchRole();

        return () => { cancelled = true; };
    }, [sessionToken]);

    const login = useCallback((token: string) => {
        localStorage.setItem(SESSION_STORAGE_KEY, token);
        setSessionToken(token);
        setRoleLoading(true);
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
        setRoleID(null);
        setUserID(null);
    }, [sessionToken]);

    return {
        sessionToken,
        isLoggedIn: sessionToken !== null,
        roleID,
        userID,
        roleLoading,
        isAdmin: roleID === 1,
        login,
        logout,
    };
};
