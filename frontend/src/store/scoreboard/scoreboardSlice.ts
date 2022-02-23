import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import { PerformanceResultsContentModel, ScoreboardModeModel, ScoreboardStatusModel, UpcomingCompetitorModel } from '../../services/openapi'
import { RootState } from '../store'

export interface ScoreboardState {
  scoreboardMode: ScoreboardModeModel
  result: PerformanceResultsContentModel | undefined
  upcomimngCompetitors: Array<UpcomingCompetitorModel>
}

export const initialState: ScoreboardState = {
  scoreboardMode: ScoreboardModeModel.UNKNOWN,
  result: undefined,
  upcomimngCompetitors: []
}

export const scoreboardSlice = createSlice({
  name: 'scoreboard',
  initialState,
  reducers: {
    setScoreboardStatus(state, action: PayloadAction<ScoreboardStatusModel>) {
      state.scoreboardMode = action.payload.scoreboardMode || ScoreboardModeModel.UNKNOWN
      state.result = action.payload.result
      state.upcomimngCompetitors = action.payload.upcomingCompetitors
    }
  }
})

export const scoreboardSelector = (state: RootState) => state.competitionSlice
export const { setScoreboardStatus } = scoreboardSlice.actions
export default scoreboardSlice.reducer
