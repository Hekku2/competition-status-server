import { Alert, Box, Card, CardContent, CardHeader, CircularProgress, List } from "@mui/material";
import { useEffect } from "react";
import { CompetitorCard, DivisionResultsCard, UpcomingCompetitorListItem } from ".";
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
          {state.competitionStatus?.currentCompetitor && <CompetitorCard header={"Current"} competitor={state.competitionStatus?.currentCompetitor} />}

          <Box sx={{
            display: "flex",
            marginTop: "1vh"
          }}>
            {state.competitionStatus?.divisions.map(division =>
              <>
                <DivisionResultsCard key={division.name} division={division} />
                <Card key={division.name}>
                  <CardHeader title={division.name} subheader={"Upcoming"}></CardHeader>
                  <CardContent>
                    <List>
                      {division.upcomingCompetitorModels.map(item => <UpcomingCompetitorListItem key={item.id} competitor={item} />)}
                    </List>
                  </CardContent>
                </Card>
              </>
            )}
          </Box>

        </>
      }
    </>
  )
}