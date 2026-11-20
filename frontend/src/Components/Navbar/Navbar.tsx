import { Link, NavLink, useNavigate } from "react-router-dom";
import { useAuth } from "../../context/AuthContext";

function Navbar() {
  const { token, logout } = useAuth();
  const navigate = useNavigate();

  const linkClass = ({ isActive }: { isActive: boolean }) =>
    `rounded-lg px-4 py-2 text-sm font-medium transition ${
      isActive
        ? "bg-white/20 text-white"
        : "text-white/90 hover:bg-white/10 hover:text-white"
    }`;
  const handleLogout = () => {
    logout();
    navigate("/login");
  };

  return (
    <nav className="sticky top-0 z-50 bg-blue-400 shadow-md">
      <div className="mx-auto flex max-w-7xl items-center justify-between px-6 py-3">
        <Link
          to="/"
          className="text-2xl font-extrabold tracking-wide text-white"
        >
          E<span className="text-blue-100">Commerce</span>
        </Link>

        <div className="flex items-center gap-2">
          {!token ? (
            <>
              <NavLink to="/login" className={linkClass}>
                Login
              </NavLink>

              <Link
                to="/signup"
                className="rounded-lg bg-white px-4 py-2 text-sm font-semibold text-blue-500 shadow-sm transition hover:bg-red-50"
              >
                Sign Up
              </Link>
            </>
          ) : (
            <button
              type="button"
              onClick={handleLogout}
              className="cursor-pointer rounded-lg  bg-white/10 px-5 py-2 text-base font-semibold tracking-wide text-white shadow-sm transition-all duration-200 hover:border-white/50 hover:bg-red hover:text-red-500 active:scale-95"
            >
              Logout
            </button>
          )}
        </div>
      </div>
    </nav>
  );
}

export default Navbar;
