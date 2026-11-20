import { useState } from "react";
import type { FormEvent } from "react";
import { Link, useNavigate } from "react-router-dom";
import api from "../../api";
import { useAuth } from "../../context/AuthContext";
function Login() {
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);
  const { login } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    setError("");
    setLoading(true);

    try {
      const res = await api.post("/account/login", {
        userName,
        password,
      });

      if (!res.data.token) {
        setError("Login response did not contain a token.");
        return;
      }

      login(res.data.token);
      navigate("/");
    } catch {
      setError("Username or password is incorrect.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <form
      onSubmit={handleSubmit}
      className="mx-auto mt-16 flex max-w-sm flex-col gap-4 rounded-xl border border-gray-200 bg-white p-6 shadow-sm"
    >
      <h2 className="text-2xl font-bold">Login</h2>

      <label className="flex flex-col gap-1 text-sm font-medium">
        Username
        <input
          required
          autoComplete="username"
          className="rounded-lg border border-gray-300 px-3 py-2 outline-none focus:border-black"
          placeholder="Username"
          value={userName}
          onChange={(e) => setUserName(e.target.value)}
        />
      </label>

      <label className="flex flex-col gap-1 text-sm font-medium">
        Password
        <input
          required
          autoComplete="current-password"
          type="password"
          className="rounded-lg border border-gray-300 px-3 py-2 outline-none focus:border-black"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
      </label>

      <button
        type="submit"
        disabled={loading}
        className="rounded-lg bg-black px-3 py-2 text-white transition hover:bg-gray-800 disabled:cursor-not-allowed disabled:opacity-60 cursor-pointer"
      >
        {loading ? "Logging in..." : "Login"}
      </button>

      {error && (
        <p role="alert" className="text-sm text-red-500">
          {error}
        </p>
      )}

      <p className="text-center text-sm text-gray-600">
        Don't have an account?{" "}
        <Link
          to="/signup"
          className="font-medium text-black underline underline-offset-4"
        >
          Sign up
        </Link>
      </p>
    </form>
  );
}

export default Login;
