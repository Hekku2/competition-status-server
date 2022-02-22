import { CompetitorResultsView, DefaultView, DivisionStatusView, UpcomingCompetitorsView } from "."
import { useAppSelector } from "../../components"
import { ScoreboardModeModel } from "../../services/openapi"

export const ScoreboardView = () => {
  const state = useAppSelector(state => state.scoreboardSlice)

  return (
    <>
      {state.scoreboardMode === ScoreboardModeModel.UPCOMING_COMPETITORS && <UpcomingCompetitorsView />}
      {state.scoreboardMode === ScoreboardModeModel.DIVISION_STATUS && <DivisionStatusView />}
      {state.scoreboardMode === ScoreboardModeModel.COMPETITOR_RESULTS && <CompetitorResultsView />}
      {state.scoreboardMode === ScoreboardModeModel.UNKNOWN && <DefaultView />}
    </>
  )
}
