import { Card, CardContent, CardHeader, Typography } from "@mui/material"
import { useAppSelector } from "../../components"

export const CurrentStatus = () => {
  const state = useAppSelector(state => state.scoreboardSlice)

  return (
    <Card>
      <CardHeader title="Current state" />
      <CardContent>
        <Typography> Mode: {state.scoreboardMode} </Typography>
        <Typography> Selected divison: {state.division ?? 'No division selected.'} </Typography>
      </CardContent>
    </Card>
  )
}
