import { Alert, Box, CircularProgress } from "@mui/material";
import { useEffect } from "react";
import { CompetitorCard, DivisionIncomingCard, DivisionResultsCard, ResultCard } from ".";
import { useAppDispatch, useAppSelector } from "../../components/hooks";
import { fetchCompetitionStatus } from "../../store/competition/competitionSlice";

export const MainView = () => {
  const state = useAppSelector(state => state.competitionSlice)
  const dispatch = useAppDispatch()

  useEffect(function () {
    dispatch(fetchCompetitionStatus())
  }, [dispatch])

  return (
    <>
      {state.error && <Alert severity="error">{state.error.message}</Alert>}
      {state.isLoadingCompetitionStatus ?
        <CircularProgress />
        :
        <>
          <Box sx={{
            display: "flex",
            marginTop: "1vh"
          }}>
            {state.currentCompetitor && <CompetitorCard header={"Current"} competitor={state.currentCompetitor} />}
            {state.latestResults && <ResultCard results={state.latestResults} />}
          </Box>

          <Box sx={{
            display: "flex",
            marginTop: "1vh"
          }}>
            {state.competitionStatus?.divisions.map(division =>
              <Box key={division.name}>
                <DivisionResultsCard division={division} />
                <DivisionIncomingCard division={division} />
              </Box>
            )}
          </Box>

        </>
      }
    </>
  )
}
