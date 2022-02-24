import { Button } from "@mui/material";
import { useState } from "react";
import {
  BrowserRouter,
  Routes,
  Route
} from "react-router-dom";
import { MainView } from "../views";
import { CompetitorsView } from "../views/Competitors";
import { ScoreboardView } from "../views/Scoreboard";
import { NavigationBar } from "./NavigationBar";

const MainRouter = () => {
  const [isLoading, setLoading] = useState<boolean>(true)

  return (
    <BrowserRouter>
      {isLoading && <NavigationBar />}
      <Button
        sx={{
          position: "fixed",
          top: 0,
          left: 0,
          width: "40px",
          minWidth: "40px",
          height: "50px"
        }}
        onClick={() => setLoading(!isLoading)}
      >
      </Button>

      <Routes>
        <Route path="/" element={<MainView />} />
        <Route path="/competitors" element={<CompetitorsView />} />
        <Route path="/scoreboard" element={<ScoreboardView />} />
      </Routes>
    </BrowserRouter>
  )
}

export default MainRouter
