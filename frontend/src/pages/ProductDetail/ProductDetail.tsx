interface ProductDetailProps {
  product: {
    id: number;
    name: string;
    description: string;
    price: number;
    stock: number;
  };
}

function ProductDetail({ product }: ProductDetailProps) {
  return (
    <div className=" p-4">
      <h1 className="text-2xl font-bold text-red-500">{product.name}</h1>

      <p className="mt-2 text-gray-600">{product.description}</p>

      <p className="mt-4 font-semibold">Price: {product.price.toFixed(2)} TL</p>

      <p className="mt-1 text-sm text-gray-500">Stock: {product.stock}</p>
    </div>
  );
}

export default ProductDetail;
