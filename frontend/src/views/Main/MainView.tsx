import { Alert, Card, CardContent, CardHeader, CircularProgress, List, ListItem, Typography } from "@mui/material";
import { useEffect } from "react";
import { useAppDispatch, useAppSelector } from "../../components/hooks";
import { fetchCompetitionStatus } from "../../store/competition/competitionSlice";
import { CompetitorCard } from "./CompetitorCard";

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

          {state.competitionStatus?.divisions.map(division => <Card key={division.name}>
            <CardHeader title="Upcoming" subheader={division.name}></CardHeader>
            <CardContent>
              <List>
                {division.upcomingCompetitorModels.map(item => <ListItem key={item.id}>
                  {item.competitors.map(com => <Typography key={com.name} variant="body1">{com.name} {com.team}</Typography>)}
                </ListItem>)}
              </List>
            </CardContent>
          </Card>)}
        </>}
    </>
  );
}