import { Typography } from "@mui/material"
import { useAppSelector } from ".."

export const ScoreboardStatusBanner = () => {
  const state = useAppSelector(state => state.scoreboardSlice)

  return (
    <Typography sx={{ minWidth: 100 }}>
      {state.scoreboardMode}
    </Typography>
  )
}
