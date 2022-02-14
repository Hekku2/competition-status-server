import {
  BrowserRouter,
  Routes,
  Route
} from "react-router-dom";
import { MainView } from "../views";
import { CompetitorsView } from "../views/Competitors";
import { NavigationBar } from "./NavigationBar";

const MainRouter = () => {
  return (
    <BrowserRouter>
      <NavigationBar />

      <Routes>
        <Route path="/" element={<MainView />} />
        <Route path="/competitors" element={<CompetitorsView />} />

      </Routes>
    </BrowserRouter>
  )
}

export default MainRouter
