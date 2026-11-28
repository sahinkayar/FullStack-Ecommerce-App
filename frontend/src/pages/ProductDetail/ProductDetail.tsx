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

const img = (id: string) =>
  `https://images.unsplash.com/${id}?auto=format&fit=crop&w=700&q=80`;

const IMAGE_RULES: { keywords: string[]; id: string }[] = [
  {
    keywords: ["apple watch", "galaxy watch", "watch"],
    id: "photo-1546868871-7041f2a55e12",
  },
  { keywords: ["ipad"], id: "photo-1544244015-0df4b3ffc6b0" },
  { keywords: ["galaxy tab"], id: "photo-1561154464-82e9adf32764" },
  { keywords: ["iphone"], id: "photo-1592899677977-9c10ca588bbd" },
  {
    keywords: ["galaxy", "pixel", "xiaomi"],
    id: "photo-1511707171634-5f897ff02aa9",
  },
  { keywords: ["airpods"], id: "photo-1600294037681-c80b4cb5b434" },
  { keywords: ["sony", "wh-1000"], id: "photo-1505740420928-5e560c06d30e" },
  { keywords: ["jbl"], id: "photo-1549638767-0ccf6cb1281b" },
  { keywords: ["macbook"], id: "photo-1517336714731-489689fd1ca8" },
  {
    keywords: ["dell", "xps", "thinkpad", "lenovo"],
    id: "photo-1496181133206-80ce9b88a853",
  },
  {
    keywords: ["mouse", "logitech", "mx master"],
    id: "photo-1527864550417-7fd91fc51a46",
  },
  { keywords: ["charger", "anker"], id: "photo-1583863788434-e58a36330cf0" },
];

function getProductImage(name: string) {
  const n = name.toLowerCase();
  const rule = IMAGE_RULES.find((r) => r.keywords.some((k) => n.includes(k)));
  return img(rule ? rule.id : "photo-1523275335684-37898b6baf30");
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
