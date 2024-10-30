import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import Button from "../Button/Button";
import styles from "./AuthStyles.module.css";
import { useAuth } from "../../contexts/AuthContext"; 

const AuthForm: React.FC = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [name, setName] = useState("");
  const [isLogin, setIsLogin] = useState(true);

  const navigate = useNavigate();
  const { login, isAuthenticated } = useAuth();

  // Redirect user if already authenticated
  useEffect(() => {
    if (isAuthenticated) {
      navigate("/"); // Redirect to home if already logged in
    }
  }, [isAuthenticated, navigate]);

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    const apiUrl = import.meta.env.VITE_API_URL;

    if (isLogin) {
      try {
        const response = await fetch(`${apiUrl}/Auth/login`, {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({ email, password }),
        });

        if (!response.ok) throw new Error("Login failed");

        const data = await response.json();

        login(data);
        navigate("/");
      } catch (error) {
        if (error instanceof Error) {
          console.error(error);
          // alert("Login failed: " + error.message);
        }
      }
    } else {
      try {
        const response = await fetch(`${apiUrl}/Auth/register`, {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({ email, password, name }),
        });

        if (!response.ok) throw new Error("Registration failed");

        alert("Registration successful! Please log in.");
        setIsLogin(true); // Switch to login mode after successful registration
      } catch (error) {
        if (error instanceof Error) {
          console.error(error);
          // alert("Registration failed: " + error.message);
        }
      }
    }
  };

  return (
    <form className={styles.form} onSubmit={handleSubmit}>
      <h2 className={styles.h2}>{isLogin ? "Login" : "Register"}</h2>

      {!isLogin && (
        <div>
          <input
            className={styles.input}
            type="text"
            placeholder="Name"
            value={name}
            onChange={(e) => setName(e.target.value)}
            required
          />
        </div>
      )}

      <div>
        <input
          className={styles.input}
          type="email"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
        />
      </div>

      <div>
        <input
          className={styles.input}
          type="password"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
        />
      </div>

      <Button label={isLogin ? "Login" : "Register"} variant="secondary" />

      <p className={styles.switchText} onClick={() => setIsLogin(!isLogin)}>
        {isLogin ? "Don't have an account? Register" : "Already have an account? Sign in"}
      </p>
    </form>
  );
};

export default AuthForm;
