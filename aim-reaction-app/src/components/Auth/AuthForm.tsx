import React, { useState } from "react";
import Button from "../Button/Button";
import styles from "./AuthStyles.module.css";

const AuthForm: React.FC = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [name, setName] = useState(""); 
  const [isLogin, setIsLogin] = useState(true); 

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
        alert(`Welcome, ${data}`);
      } catch (error) {
        if (error instanceof Error) {
          console.error(error);
          alert("Login failed: " + error.message);
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
        setIsLogin(true);
      } catch (error) {
        if (error instanceof Error) {
          console.error(error);
          alert("Registration failed: " + error.message);
        }
      }
    }
  };

  return (
    <form className={styles.form} onSubmit={handleSubmit}>
      <h2>{isLogin ? "Login" : "Register"}</h2>

      {!isLogin && (
        <div>
          <input
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
          type="email"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
        />
      </div>

      <div>
        <input
          type="password"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
        />
      </div>

      <Button
        label={isLogin ? "Login" : "Register"}
        variant="secondary"
      />

      <p className={styles.switchText} onClick={() => setIsLogin(!isLogin)}>
        Switch to {isLogin ? "Register" : "Login"}
      </p>
    </form>
  );
};

export default AuthForm;
