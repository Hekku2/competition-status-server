import { AppBar, MenuItem, Toolbar, Box } from "@mui/material"
import { Link } from "react-router-dom"
import { ScoreboardStatusBanner } from "."

export const NavigationBar = () => {
  return (
    <AppBar position="static">
      <Toolbar>
        <Box sx={{ flexGrow: 1, display: { xs: 'none', md: 'flex' } }}>
          <MenuItem component={Link} to={"/"}>
            Main
          </MenuItem>
          <MenuItem component={Link} to={"/competitors"}>
            Competitors
          </MenuItem>
          <MenuItem component={Link} to={"/scoreboard-controls"}>
            Scoreboard controls
          </MenuItem>
          <MenuItem component={Link} to={"/scoreboard"}>
            Scoreboard
          </MenuItem>
        </Box>
        <Box sx={{ flexGrow: 0 }}>
          <ScoreboardStatusBanner />
        </Box>
      </Toolbar>
    </AppBar>
  )
}
