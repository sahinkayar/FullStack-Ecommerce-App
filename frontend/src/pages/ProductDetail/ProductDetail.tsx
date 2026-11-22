import { useNavigate } from "react-router-dom";
import { useAuth } from "../../context/AuthContext";
import { useCart } from "../../context/CartContext";

interface ProductDetailProps {
  product: {
    id: number;
    name: string;
    description: string;
    price: number;
    stock: number;
  };
}

function getProductImage(name: string) {
  const productName = name.toLowerCase();

  if (productName.includes("iphone")) {
    return "https://images.unsplash.com/photo-1592899677977-9c10ca588bbd?auto=format&fit=crop&w=700&q=80";
  }

  if (productName.includes("galaxy")) {
    return "https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?auto=format&fit=crop&w=700&q=80";
  }

  if (productName.includes("airpods")) {
    return "https://images.unsplash.com/photo-1600294037681-c80b4cb5b434?auto=format&fit=crop&w=700&q=80";
  }

  if (productName.includes("macbook")) {
    return "https://images.unsplash.com/photo-1517336714731-489689fd1ca8?auto=format&fit=crop&w=700&q=80";
  }

  return "https://images.unsplash.com/photo-149 in?auto=format&fit=crop&w=700&q=80";
}

function ProductDetail({ product }: ProductDetailProps) {
  const { token } = useAuth();
  const { cart, addToCart } = useCart();
  const navigate = useNavigate();
  const isSoldOut = product.stock <= 0;
  const isInCart = Boolean(
    cart?.cartItemDtos?.some((item) => item.productId === product.id),
  );
  const image = getProductImage(product.name);

  const handleAdd = async () => {
    if (!token) {
      navigate("/login");
      return;
    }
    try {
      await addToCart(product.id);
    } catch {
      alert("Could not add product to cart. Please try again.");
    }
  };
  return (
    <div className="group flex h-full flex-col overflow-hidden rounded-2xl border border-gray-200 bg-white shadow-sm transition duration-300 hover:-translate-y-1 hover:shadow-xl">
      <div className="h-56 overflow-hidden bg-gray-100">
        <img
          src={image}
          alt={product.name}
          className="h-full w-full object-cover transition duration-300 group-hover:scale-105"
          loading="lazy"
        />
      </div>

      <div className="flex flex-1 flex-col p-5">
        <h2 className="text-lg font-bold text-gray-900">{product.name}</h2>

        <p className="mt-2 line-clamp-2 min-h-12 text-sm leading-6 text-gray-500">
          {product.description}
        </p>

        <div className="mt-4 flex items-center justify-between gap-2">
          <span className="text-xl font-bold text-gray-900">
            {product.price.toLocaleString("tr-TR")} TL
          </span>

          <span
            className={`rounded-full px-3 py-1 text-xs font-semibold ${
              product.stock > 0
                ? "bg-green-100 text-green-700"
                : "bg-red-100 text-red-700"
            }`}
          >
            {product.stock > 0 ? "In Stock" : "Out of Stock"}
          </span>
        </div>

        <button
          type="button"
          onClick={handleAdd}
          disabled={isSoldOut}
          className={`mt-5 flex w-full cursor-pointer items-center justify-center gap-2 rounded-lg px-4 py-3 font-semibold text-white transition disabled:cursor-not-allowed disabled:bg-gray-300 ${
            isInCart
              ? "bg-green-600 hover:bg-green-700"
              : "bg-blue-600 hover:bg-blue-700"
          }`}
        >
          <span>🛒</span>
          <span>
            {isSoldOut
              ? "Sold Out"
              : isInCart
                ? "Item is in Cart"
                : "Add to Cart"}
          </span>
        </button>
      </div>
    </div>
  );
}

export default ProductDetail;
