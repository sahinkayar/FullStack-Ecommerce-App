import { useEffect, useState } from "react";
import api from "../../api";
import ProductDetail from "../ProductDetail/ProductDetail";

interface Product {
  id: number;
  name: string;
  description: string;
  price: number;
  stock: number;
  categoryId: number | null;
}
interface ProductsProps {
  selectedCategoryId: number | null;
}

function Products({ selectedCategoryId }: ProductsProps) {
  const [products, setProducts] = useState<Product[]>([]);

  useEffect(() => {
    api.get<Product[]>("/products").then((res) => setProducts(res.data));
  }, []);

  const filteredProducts =
    selectedCategoryId === null
      ? products
      : products.filter((p) => p.categoryId === selectedCategoryId);

  return (
    <div className="min-h-screen w-full bg-gray-50  py-10">
      <div className="mx-auto mb-8 max-w-7xl">
        <h1 className="text-3xl font-bold text-gray-900">Products</h1>
        <p className="mt-2 text-gray-500">Browse our available products.</p>
      </div>

      <div className="mx-auto grid max-w-7xl grid-cols-1 gap-6 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 cursor-pointer">
        {filteredProducts.map((p) => (
          <div
            key={p.id}
            className="rounded-xl border border-gray-200 bg-white p-5 shadow-sm transition duration-200 hover:-translate-y-1 hover:shadow-md"
          >
            <ProductDetail product={p} />
          </div>
        ))}
      </div>

      {products.length === 0 && (
        <p className="py-12 text-center text-gray-500">No products found.</p>
      )}
    </div>
  );
}

export default Products;
