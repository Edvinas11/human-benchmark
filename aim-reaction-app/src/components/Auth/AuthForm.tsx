import React, { useState } from "react";
import AuthService from "./AuthService"; 
import styles from "./AuthStyles.module.css";

const AuthForm: React.FC = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [name, setName] = useState(""); 
  const [isLogin, setIsLogin] = useState(true); 

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    
    if (isLogin) {
      const response = await AuthService.login({ email, password });
      if (response) {
        alert(`Welcome, ${response}`);
        // redirect or update UI
      }
    } else {
      const response = await AuthService.register({ email, password, name });
      if (response) {
        alert("Registration successful! Please log in.");
        //redirect or update UI
        setIsLogin(!isLogin); // change like this just for now
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
      <button type="submit">{isLogin ? "Login" : "Register"}</button>
      <p className={styles.switchText} onClick={() => setIsLogin(!isLogin)}>
        Switch to {isLogin ? "Register" : "Login"}
      </p>
    </form>
  );
};

export default AuthForm;
