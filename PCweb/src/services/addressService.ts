const API_BASE_URL = 'http://localhost:5221/api/address';

export interface Address {
    street: string;
    houseNumber: number;        // int in DB
    houseNumberAddition: string;
    city: string;
    postcode: string;
    country: string;
    userId: number;
}

export const addressService = {
    // Get all addresses
    getAll: async (): Promise<Address[]> => {
        try {
            const response = await fetch(`${API_BASE_URL}/get-all`);
            if (!response.ok) {
                throw new Error(`Failed to fetch addresses: ${response.statusText}`);
            }
            return await response.json();
        } catch (error) {
            console.error('Error fetching addresses:', error);
            throw error;
        }
    },

    // Get addresses by user ID
    getByUserId: async (userId: number): Promise<Address[]> => {
        try {
            const response = await fetch(`${API_BASE_URL}/user/${userId}`);
            if (!response.ok) {
                throw new Error(`Failed to fetch user addresses: ${response.statusText}`);
            }
            return await response.json();
        } catch (error) {
            console.error('Error fetching user addresses:', error);
            return [];
        }
    },

    // Get address by ID
    getById: async (id: number): Promise<Address> => {
        try {
            const response = await fetch(`${API_BASE_URL}/${id}`);
            if (!response.ok) {
                throw new Error(`Failed to fetch address: ${response.statusText}`);
            }
            return await response.json();
        } catch (error) {
            console.error('Error fetching address:', error);
            throw error;
        }
    },

    // Create a new address
    create: async (address: Address): Promise<any> => {
        try {
            const response = await fetch(`${API_BASE_URL}/create`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(address),
            });
            if (!response.ok) {
                throw new Error(`Failed to create address: ${response.statusText}`);
            }
            return await response.json();
        } catch (error) {
            console.error('Error creating address:', error);
            throw error;
        }
    },

    // Delete an address
    delete: async (id: number): Promise<any> => {
        try {
            const response = await fetch(`${API_BASE_URL}/${id}`, {
                method: 'DELETE',
            });
            if (!response.ok) {
                throw new Error(`Failed to delete address: ${response.statusText}`);
            }
            return await response.json();
        } catch (error) {
            console.error('Error deleting address:', error);
            throw error;
        }
    },
};
