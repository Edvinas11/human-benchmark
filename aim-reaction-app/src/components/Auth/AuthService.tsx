

const API_URL = "https://localhost:5109/api/Auth";

const AuthService = {
  register: async (userData: { email: string; password: string; name: string }) => {
    try {
      const response = await fetch(`${API_URL}/register`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(userData),
      });
      
      if (!response.ok) throw new Error("Registration failed");

      const data = await response.json();
      return data;
    } catch (error: unknown) {
      if (error instanceof Error) {
        console.error(error);
        alert("Registration failed: " + error.message);
      } else {
        console.error("An unexpected error occurred:", error);
        alert("Registration failed: An unexpected error occurred.");
      }
    }
  },

  login: async (userData: { email: string; password: string }) => {
    try {
      const response = await fetch(`${API_URL}/login`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(userData),
      });
      
      if (!response.ok) throw new Error("Login failed");

      const data = await response.json();
      return data;
    } catch (error: unknown) {
      if (error instanceof Error) {
        console.error(error);
        alert("Login failed: " + error.message);
      } else {
        console.error("An unexpected error occurred:", error);
        alert("Login failed: An unexpected error occurred.");
      }
    }
  },
};

export default AuthService;
