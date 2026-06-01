import React, { createContext, useContext, ReactNode } from 'react';
import { useCart, CartItem } from '../hooks/useCart';

interface CartContextType {
    items: CartItem[];
    addItem: (item: CartItem) => void;
    removeItem: (itemId: number) => void;
    updateQuantity: (itemId: number, quantity: number) => void;
    getTotalPrice: () => number;
    clearCart: () => void;
}

const CartContext = createContext<CartContextType | undefined>(undefined);

export function CartProvider({ children }: { children: ReactNode }) {
    const cart = useCart();

    return (
        <CartContext.Provider value={cart}>
            {children}
        </CartContext.Provider>
    );
}

export function useCartContext() {
    const context = useContext(CartContext);
    if (!context) {
        throw new Error('useCartContext must be used within CartProvider');
    }
    return context;
}
