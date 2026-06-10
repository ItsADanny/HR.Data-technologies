import { useState, useEffect } from 'react';

interface ProductField {
	categoryName: string;
	productID: number;
	name: string;
	price: number;
	fieldName: string;
	fieldValue: string;
}

export interface ProductItem {
	id: number;
	categoryName: string;
	name: string;
	price: number;
	fields: Record<string, string>;
}

export const useProductsData = (categoryId?: string, brand?: string, page: number = 1, search?: string) => {
	const [products, setProducts] = useState<ProductItem[]>([]);
	const [loading, setLoading] = useState(true);
	const [categoryName, setCategoryName] = useState('Products');
	const [error, setError] = useState<string | null>(null);

	useEffect(() => {
		const fetchProducts = async () => {
			try {
				setLoading(true);

				// Use search endpoint if a search query is set, otherwise fall back to category/brand endpoints
				const url = search
					? `/api/product/search/${encodeURIComponent(search)}/${page}/100`
					: brand
						? `/api/product/with-category/${categoryId}/with-brand/${brand}?page=${page}`
						: `/api/product/with-category?categoryId=${categoryId}&page=${page}`;

				const response = await fetch(url);
				const data: ProductField[] = await response.json();

				// Group by productID
				const grouped = new Map<number, ProductField[]>();
				data.forEach(item => {
					if (!grouped.has(item.productID)) {
						grouped.set(item.productID, []);
					}
					grouped.get(item.productID)?.push(item);
				});

				// Convert to ProductItem array (unique products only)
				const items: ProductItem[] = Array.from(grouped.entries()).map(([id, fields]) => ({
					id,
					categoryName: fields[0].categoryName,
					name: fields[0].name,
					price: fields[0].price,
					fields: Object.fromEntries(fields.map(f => [f.fieldName, f.fieldValue]))
				}));

				setProducts(items);
				if (items.length > 0) {
					setCategoryName(items[0].categoryName);
				}
				setError(null);
			} catch (err) {
				console.error('Error fetching products:', err);
				setError(err instanceof Error ? err.message : 'Failed to fetch products');
			} finally {
				setLoading(false);
			}
		};

		fetchProducts();
	}, [categoryId, brand, page, search]);

	return { products, loading, categoryName, error };
};

export const GetBrandsInSameCategory = async (categoryId: string) => {
	try {
		const response = await fetch(`/api/product/GetAllBrandsThatInSameCategory/${categoryId}`);
		if (!response.ok) {
			throw new Error(`HTTP error! status: ${response.status}`);
		}
		const brands: string[] = await response.json();
		return brands;
	} catch (err) {
		console.error('Error fetching brands:', err);
		throw err instanceof Error ? err : new Error('Failed to fetch brands');
	}		
};

