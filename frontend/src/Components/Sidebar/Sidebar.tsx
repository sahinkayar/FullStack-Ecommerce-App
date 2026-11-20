import { useEffect, useState } from "react";
import api from "../../api";
import { TiThMenu } from "react-icons/ti";

interface Category {
  id: number;
  name: string;
}
interface SidebarProps {
  selectedCategoryId: number | null;
  onSelectCategory: (id: number | null) => void;
}
function Sidebar({ selectedCategoryId, onSelectCategory }: SidebarProps) {
  const [categories, setCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState(true);
  const [barState, setBarState] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    api
      .get<Category[]>("/categories")
      .then((res) => {
        setCategories(res.data);
      })
      .catch(() => {
        setError("Kategoriler yüklenemedi.");
      })
      .finally(() => {
        setLoading(false);
      });
  }, []);

  return (
    <div className="shrink-0">
      {barState ? (
        <aside className="w-64 border-r border-gray-200 bg-white px-4 py-6">
          <div className="mb-6 flex items-center justify-between">
            <h2 className="text-xl font-bold text-gray-900">Categories</h2>

            <button
              type="button"
              className="cursor-pointer text-2xl text-gray-700 hover:text-black"
              onClick={() => setBarState(false)}
              aria-label="Close sidebar"
            >
              <TiThMenu />
            </button>
          </div>

          {loading && <p className="text-gray-500">Loading...</p>}
          {error && <p className="text-red-500">{error}</p>}

          {!loading && !error && (
            <ul className="space-y-2">
              <li>
                <button
                  type="button"
                  className={`w-full rounded-lg px-4 py-3 text-left cursor-pointer ${
                    selectedCategoryId === null ? "bg-blue-300" : "bg-blue-100"
                  }`}
                  onClick={() => onSelectCategory(null)}
                >
                  All
                </button>
              </li>
              {categories.map((category) => (
                <li key={category.id}>
                  <button
                    type="button"
                    className={`w-full cursor-pointer rounded-lg px-4 py-3 text-left transition ${
                      selectedCategoryId === category.id
                        ? "bg-blue-300"
                        : "bg-blue-100"
                    }`}
                    onClick={() => onSelectCategory(category.id)}
                  >
                    {category.name}
                  </button>
                </li>
              ))}
            </ul>
          )}
        </aside>
      ) : (
        <button
          type="button"
          className="m-3 cursor-pointer rounded-lg border border-gray-200 p-3 text-2xl text-gray-700 hover:bg-gray-100"
          onClick={() => setBarState(true)}
          aria-label="Open sidebar"
        >
          <TiThMenu />
        </button>
      )}
    </div>
  );
}

export default Sidebar;
