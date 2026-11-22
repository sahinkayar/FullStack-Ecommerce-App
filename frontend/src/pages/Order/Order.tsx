import { useEffect, useState } from "react";
import api from "../../api";

interface OrderItem {
  productId: number;
  productName: string;
  quantity: number;
  price: number;
  lineTotal: number;
}

interface Order {
  id: number;
  createdAt: string;
  totalPrice: number;
  items: OrderItem[];
}

function Orders() {
  const [orders, setOrders] = useState<Order[]>([]);

  useEffect(() => {
    api.get<Order[]>("/Order").then((res) => setOrders(res.data));
  }, []);

  return (
    <div className="mx-auto max-w-3xl px-6 py-10">
      <h1 className="mb-6 text-3xl font-bold">My Orders</h1>
      {orders.length === 0 && (
        <p className="text-gray-500">You don't have any orders yet.</p>
      )}
      <div className="space-y-4">
        {orders.map((o) => (
          <div key={o.id} className="rounded-xl border bg-white p-4 shadow-sm">
            <div className="mb-2 flex justify-between text-sm text-gray-500">
              <span>Order #{o.id}</span>
              <span>{new Date(o.createdAt).toLocaleDateString("tr-TR")}</span>
            </div>
            {o.items.map((i) => (
              <div key={i.productId} className="flex justify-between text-sm">
                <span>
                  {i.productName} x{i.quantity}
                </span>
                <span>{i.lineTotal} TL</span>
              </div>
            ))}
            <div className="mt-2 border-t pt-2 text-right font-bold">
              {o.totalPrice} TL
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}

export default Orders;
