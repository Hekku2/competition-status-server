import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import { ParticipationRowModel, PerformanceResultsContentModel, ScoreboardModeModel, ScoreboardStatusModel, UpcomingCompetitorModel } from '../../services/openapi'
import { RootState } from '../store'

export interface ScoreboardState {
  scoreboardMode: ScoreboardModeModel
  result: PerformanceResultsContentModel | undefined
  upcomingCompetitors: Array<UpcomingCompetitorModel>
  results: Array<ParticipationRowModel>
}

export const initialState: ScoreboardState = {
  scoreboardMode: ScoreboardModeModel.UNKNOWN,
  result: undefined,
  upcomingCompetitors: [],
  results: []
}

export const scoreboardSlice = createSlice({
  name: 'scoreboard',
  initialState,
  reducers: {
    setScoreboardStatus(state, action: PayloadAction<ScoreboardStatusModel>) {
      state.scoreboardMode = action.payload.scoreboardMode || ScoreboardModeModel.UNKNOWN
      state.result = action.payload.result
      state.upcomingCompetitors = action.payload.upcomingCompetitors
      state.results = action.payload.results
    }
  }
})

export const scoreboardSelector = (state: RootState) => state.competitionSlice
export const { setScoreboardStatus } = scoreboardSlice.actions
export default scoreboardSlice.reducer
