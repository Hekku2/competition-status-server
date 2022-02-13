import { Alert, CircularProgress } from "@mui/material";
import { useEffect } from "react";
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
        <>This page should shown the status of current competition</>}
    </>
  );
}