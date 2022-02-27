import { Card, CardContent, CardHeader, List, ListItem, ListItemButton, ListItemText, Typography } from "@mui/material"
import { useAppSelector } from "../../components"
import { PoleSportResultModel } from "../../services/openapi"

export const CurrentStatus = () => {
  const state = useAppSelector(state => state.scoreboardSlice)

  const scoreFormat = (result: PoleSportResultModel) => {
    return `Total: ${result.total}, A: ${result.artisticScore} E: ${result.executionScore} D: ${result.difficultyScore} HJ: ${result.headJudgePenalty}`
  }

  const upcoming =
    <List>
      {
        state.upcomingCompetitors.map(item => <ListItem key={item.id} disablePadding>
          <ListItemButton>
            <ListItemText primary={item.competitors.map(s => `${s.name} ${s.team}`)} />
          </ListItemButton>
        </ListItem>
        )
      }
    </List>

  const status = <List>
    {
      state.results.map((item, index) =>
        <ListItem key={item.id} disablePadding>
          <ListItemText
            primary={`${index + 1}. ${item.competitors.map(s => `${s.name} ${s.team}`)}`}
            secondary={item.result ? scoreFormat(item.result) : ''}
          />
        </ListItem>
      )
    }
  </List>


  return (
    <Card>
      <CardHeader title="Current state" />
      <CardContent>
        <Typography> Mode: {state.scoreboardMode} </Typography>
        <Typography> Selected divison: {state.division ?? 'No division selected.'} </Typography>
        <Typography> Upcoming: </Typography>
        {upcoming}
        <Typography> Status: </Typography>
        {status}
      </CardContent>
    </Card>
  )
}
