import { Box, Typography } from "@mui/material"
import { useAppSelector } from "../../components"
import { PoleSportResultModel } from "../../services/openapi"

export const CompetitorResultsView = () => {
  const state = useAppSelector(state => state.scoreboardSlice)

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
    }}
    >
      <Typography variant="h1">
        {state.result?.division}
      </Typography>
      {state.result?.competitors.map(s => <Typography key={`${s.name}${s.team}`} variant="h1" >{s.name} {s.team}</Typography>)}

      <Typography variant="h1">
        {state.result ? scoreFormat(state.result.result) : ''}
      </Typography>
    </Box>
  )
}
