import { Link, useNavigate } from "react-router-dom";
import { useCart } from "../../context/CartContext";
import { useState } from "react";
import api from "../../api";

function Cart() {
  const { cart, updateQuantity, removeFromCart, refreshCart } = useCart();
  const navigate = useNavigate();
  const [orderError, setOrderError] = useState("");
  if (!cart) {
    return <p className="p-6 text-gray-500">Yükleniyor...</p>;
  }
  const handleOrder = async () => {
    try {
      await api.post("/Order");
      await refreshCart();
      navigate("/orders");
    } catch (err: any) {
      setOrderError(err.response?.data ?? "Sipariş verilemedi");
    }
  };
  const items = cart.cartItemDtos ?? [];

  return (
    <div className="mx-auto max-w-3xl px-6 py-10">
      <h1 className="mb-6 text-3xl font-bold text-gray-900">My Cart</h1>

      {items.length === 0 ? (
        <div className="rounded-xl border border-gray-200 bg-white p-8 text-center">
          <p className="mb-4 text-gray-500">Your Cart is empty</p>
          <Link
            to="/"
            className="rounded-lg bg-blue-400 px-4 py-2 text-white hover:bg-blue-500"
          >
            Start shopping!!
          </Link>
        </div>
      ) : (
        <>
          <div className="space-y-4">
            {items.map((item) => (
              <div
                key={item.productId}
                className="flex items-center justify-between rounded-xl border border-gray-200 bg-white p-4 shadow-sm"
              >
                <div className="w-1/3">
                  <h3 className="font-semibold text-gray-900">
                    {item.productName}
                  </h3>
                  <p className="text-sm text-gray-500">{item.price} TL</p>
                </div>

                <div className="flex items-center gap-3">
                  <button
                    className="h-8 w-8 rounded border border-gray-300 hover:bg-gray-100 disabled:opacity-40 cursor-pointer"
                    disabled={item.quantity <= 1}
                    onClick={() =>
                      updateQuantity(item.productId, item.quantity - 1)
                    }
                  >
                    -
                  </button>
                  <span className="w-6 text-center">{item.quantity}</span>
                  <button
                    className="h-8 w-8 rounded border border-gray-300 hover:bg-gray-100 disabled:opacity-40 cursor-pointer"
                    disabled={item.quantity >= 100}
                    onClick={() =>
                      updateQuantity(item.productId, item.quantity + 1)
                    }
                  >
                    +
                  </button>
                </div>

                <div className="flex items-center gap-4">
                  <span className="font-semibold">{item.lineTotal} TL</span>
                  <button
                    className="text-sm text-red-500 hover:underline cursor-pointer"
                    onClick={() => removeFromCart(item.productId)}
                  >
                    remove
                  </button>
                </div>
              </div>
            ))}
          </div>

          <div className="mt-6 flex items-center justify-between border-t border-gray-200 pt-4">
            <span className="text-lg text-gray-600">Total</span>
            <span className="text-2xl font-bold text-gray-900">
              {cart.totalPrice} TL
            </span>
          </div>
          {orderError && <p className="mt-2 text-red-500">{orderError}</p>}
          <button
            onClick={handleOrder}
            className="mt-4 w-full rounded-lg bg-blue-600 py-3 font-semibold text-white hover:bg-blue-700 cursor-pointer disabled:cursor-not-allowed disabled:bg-gray-300"
          >
            Siparişi tamamla
          </button>
        </>
      )}
    </div>
  );
}

export default Cart;
