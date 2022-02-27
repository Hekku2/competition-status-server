import { Typography, Stack } from "@mui/material"
import { CurrentStatus, Management } from "."

export const ScoreboardControlsView = () => {
  return (
    <>
      <Typography variant="h1">
        Scoreboard Controls
      </Typography>

      <Stack direction='row'>
        <Management />

        <CurrentStatus />
      </Stack>
    </>
  )
}
