import React from "react";
import { useAuth } from "../../contexts/AuthContext";
import { Navigate } from "react-router-dom";

interface PrivateRouteProps {
  element: React.ComponentType<any>;
}

const PrivateRoute: React.FC<PrivateRouteProps> = ({ element: Component }) => {
  const { isAuthenticated } = useAuth(); // Use authentication context

  // If the user is not authenticated, redirect to the register page
  if (!isAuthenticated) {
    return <Navigate to="/login" />;
  }

  // Render the protected component if authenticated
  return <Component />;
};

export default PrivateRoute;
