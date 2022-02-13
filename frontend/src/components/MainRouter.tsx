import {
  BrowserRouter,
  Routes,
  Route
} from "react-router-dom";
import { MainView } from "../views";
import { NavigationBar } from "./NavigationBar";

const MainRouter = () => {
  return (
    <BrowserRouter>
      <NavigationBar />

      <Routes>
        <Route path="/" element={<MainView />} />
      </Routes>
    </BrowserRouter>
  )
}

export default MainRouter