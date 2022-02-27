import { Typography } from "@mui/material"
import { CurrentStatus } from "."

export const ScoreboardControlsView = () => {
  return (
    <>
      <Typography variant="h1">
        Scoreboard Controls
      </Typography>

      <CurrentStatus />
    </>
  )
}
