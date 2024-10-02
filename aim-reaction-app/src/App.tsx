import { BrowserRouter, Route, Routes } from "react-router-dom"
import Home from "./pages/Home/Home"
import './App.css';
import Navbar from "./components/Navbar/Navbar";
import CreateGame from "./pages/CreateGame/CreateGame";
<<<<<<< HEAD
import Leaderboard from "./pages/Leaderboard/Leaderboard";
=======
import ReflexTest from "./pages/ReflexTest/ReflexTest";
>>>>>>> f2f4c6297ccc4c06c5739cd832d44201a821d094

function App() {
  return (
    <BrowserRouter>
    <Navbar />
      <Routes>
        <Route path="/" element={<Home />}/>
        <Route path="/create-game" element={<CreateGame />}/>
<<<<<<< HEAD
        <Route path="/leaderboards" element={<Leaderboard />}/>
=======
        <Route path="/reflex-test" element={<ReflexTest />}/>
>>>>>>> f2f4c6297ccc4c06c5739cd832d44201a821d094
      </Routes>
    </BrowserRouter>
  )
}

export default App
