import { BrowserRouter, Route, Routes } from "react-router-dom"
import Home from "./pages/Home/Home"
import './App.css';
import Navbar from "./components/Navbar/Navbar";
import CreateGame from "./pages/CreateGame/CreateGame";
import Leaderboard from "./pages/Leaderboard/Leaderboard";
import ReflexTest from "./pages/ReflexTest/ReflexTest";

function App() {
  return (
    <BrowserRouter>
    <Navbar />
      <Routes>
        <Route path="/" element={<Home />}/>
        <Route path="/create-game" element={<CreateGame />}/>
        <Route path="/leaderboards" element={<Leaderboard />}/>
        <Route path="/reflex-test" element={<ReflexTest />}/>
      </Routes>
    </BrowserRouter>
  )
}

export default App
