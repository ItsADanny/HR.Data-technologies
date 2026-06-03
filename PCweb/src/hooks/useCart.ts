import { useState, useEffect, useCallback } from 'react';

export interface CartItem {
    id: number;
    name: string;
    price: number;
    quantity: number;
}

const CART_STORAGE_KEY = 'pcweb_cart';

export const useCart = () => {
    const [items, setItems] = useState<CartItem[]>([]);

    // Load cart from localStorage on mount
    useEffect(() => {
        const savedCart = localStorage.getItem(CART_STORAGE_KEY);
        if (savedCart) {
            try {
                setItems(JSON.parse(savedCart));
            } catch (err) {
                console.error('Failed to load cart from localStorage:', err);
            }
        }
    }, []);

    // Save cart to localStorage whenever items change
    useEffect(() => {
        localStorage.setItem(CART_STORAGE_KEY, JSON.stringify(items));
    }, [items]);

    const addItem = useCallback((item: CartItem) => {
        setItems((prevItems) => {
            const existingItem = prevItems.find(i => i.id === item.id);
            if (existingItem) {
                return prevItems.map(i =>
                    i.id === item.id ? { ...i, quantity: i.quantity + item.quantity } : i
                );
            }
            return [...prevItems, item];
        });
    }, []);

    const removeItem = useCallback((itemId: number) => {
        setItems((prevItems) => prevItems.filter(item => item.id !== itemId));
    }, []);

    const updateQuantity = useCallback((itemId: number, quantity: number) => {
        if (quantity <= 0) {
            removeItem(itemId);
        } else {
            setItems((prevItems) =>
                prevItems.map(item =>
                    item.id === itemId ? { ...item, quantity } : item
                )
            );
        }
    }, [removeItem]);

    const clearCart = useCallback(() => {
        setItems([]);
    }, []);

    const getTotalPrice = () => {
        return items.reduce((sum, item) => sum + (item.price * item.quantity), 0);
    };

    return {
        items,
        addItem,
        removeItem,
        updateQuantity,
        clearCart,
        getTotalPrice,
    };
};
