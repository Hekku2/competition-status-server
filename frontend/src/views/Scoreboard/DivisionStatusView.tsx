import { Box, List, ListItemText, ListItem, Typography } from "@mui/material"
import { useAppSelector } from "../../components"
import { PoleSportResultModel } from "../../services/openapi"

export const DivisionStatusView = () => {
  const state = useAppSelector(state => state.scoreboardSlice)

  const listEmpty = state.results.length === 0

  const scoreFormat = (result: PoleSportResultModel) => {
    return `Total: ${result.total}, A: ${result.artisticScore} E: ${result.executionScore} D: ${result.difficultyScore} HJ: ${result.headJudgePenalty}`
  }

  return (
    <Box sx={{
      width: "100%",
      minHeight: "100vh",
      display: "flex",
      flexDirection: "column",
      justifyContent: "space-evenly",
      alignItems: "center"
    }}>
      <Typography variant="h1">
        {state.division}
      </Typography>

      <Typography>
        {!listEmpty ? 'Current scores:' : 'No results for division'}
      </Typography>

      <List>
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
    </Box>
  )
}
