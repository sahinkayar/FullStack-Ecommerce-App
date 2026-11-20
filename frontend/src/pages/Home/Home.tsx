import { useState } from "react";
import Sidebar from "../../Components/Sidebar/Sidebar";
import Products from "../../pages/Products/Products";

function Home() {
  const [selectedCategoryId, setSelectedCategoryId] = useState<number | null>(
    null,
  );

  return (
    <div className="flex">
      <Sidebar
        selectedCategoryId={selectedCategoryId}
        onSelectCategory={setSelectedCategoryId}
      />
      <Products selectedCategoryId={selectedCategoryId} />
    </div>
  );
}

export default Home;
