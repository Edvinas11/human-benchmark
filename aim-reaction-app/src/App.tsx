import { BrowserRouter, Route, Routes } from "react-router-dom"
import Home from "./pages/Home/Home"
import './App.css';
import Navbar from "./components/Navbar/Navbar";
import CreateGame from "./pages/CreateGame/CreateGame";
import Leaderboard from "./pages/Leaderboard/Leaderboard";
import ReflexTest from "./pages/ReflexTest/ReflexTest";
import Register from "./pages/Register/Register";
import MovingTarget from "./pages/MovingTargets/MovingTargets";

function App() {
  return (
    <BrowserRouter>
    <Navbar />
      <Routes>
        <Route path="/" element={<Home />}/>
        <Route path="/create-game" element={<CreateGame />}/>
        <Route path="/leaderboards" element={<Leaderboard />}/>
        <Route path="/reflex-test" element={<ReflexTest />} />
        <Route path="/movingTargets" element={<MovingTarget />} />
        <Route path="/register" element={<Register />} />
      </Routes>
    </BrowserRouter>
  )
}

export default App
