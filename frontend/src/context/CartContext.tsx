import {
  createContext,
  useContext,
  useState,
  useCallback,
  useEffect,
  type ReactNode,
} from "react";
import api from "../api";
import { useAuth } from "./AuthContext";

export interface CartItem {
  productId: number;
  productName: string;
  quantity: number;
  price: number;
  lineTotal: number;
}

interface CartData {
  cartItemDtos: CartItem[];
  totalPrice: number;
}

interface CartContextType {
  cart: CartData | null;
  addToCart: (productId: number) => Promise<void>;
  updateQuantity: (productId: number, quantity: number) => Promise<void>;
  removeFromCart: (productId: number) => Promise<void>;
  refreshCart: () => Promise<void>;
}

const CartContext = createContext<CartContextType | undefined>(undefined);

export function CartProvider({ children }: { children: ReactNode }) {
  const { token } = useAuth();
  const [cart, setCart] = useState<CartData | null>(null);

  const refreshCart = useCallback(async () => {
    if (!token) {
      setCart(null);
      return;
    }
    const res = await api.get<CartData>("/cart");
    setCart(res.data);
  }, [token]);

  useEffect(() => {
    refreshCart().catch(() => setCart(null));
  }, [refreshCart]);

  const addToCart = async (productId: number) => {
    const res = await api.post<CartData>("/cart/items", {
      productId,
      quantity: 1,
    });
    setCart(res.data);
  };

  const updateQuantity = async (productId: number, quantity: number) => {
    const res = await api.put<CartData>(`/cart/items/${productId}`, {
      quantity,
    });
    setCart(res.data);
  };

  const removeFromCart = async (productId: number) => {
    const res = await api.delete<CartData>(`/cart/items/${productId}`);
    setCart(res.data);
  };

  return (
    <CartContext.Provider
      value={{ cart, addToCart, updateQuantity, removeFromCart, refreshCart }}
    >
      {children}
    </CartContext.Provider>
  );
}

export function useCart() {
  const context = useContext(CartContext);
  if (!context) throw new Error("useCart must be used inside CartProvider");
  return context;
}
