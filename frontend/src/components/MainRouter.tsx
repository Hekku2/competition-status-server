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
  return (
    <BrowserRouter>
      <NavigationBar />

      <Routes>
        <Route path="/" element={<MainView />} />
        <Route path="/competitors" element={<CompetitorsView />} />
        <Route path="/scoreboard" element={<ScoreboardView />} />
      </Routes>
    </BrowserRouter>
  )
}

export default MainRouter
