import {
  BrowserRouter,
  Routes,
  Route
} from "react-router-dom";
import { NavigationBar } from "./NavigationBar";

const MainRouter = () => {
  return (
    <BrowserRouter>
      <NavigationBar />

      <Routes>
        <Route path="/" element={<></>} />
      </Routes>
    </BrowserRouter>
  )
}

export default MainRouter