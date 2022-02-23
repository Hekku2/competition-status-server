import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import { PerformanceResultsContentModel, ScoreboardModeModel, ScoreboardStatusModel } from '../../services/openapi'
import { RootState } from '../store'

export interface ScoreboardState {
  scoreboardMode: ScoreboardModeModel
  result: PerformanceResultsContentModel | undefined
}

export const initialState: ScoreboardState = {
  scoreboardMode: ScoreboardModeModel.UNKNOWN,
  result: undefined
}

export const scoreboardSlice = createSlice({
  name: 'scoreboard',
  initialState,
  reducers: {
    setScoreboardStatus(state, action: PayloadAction<ScoreboardStatusModel>) {
      state.scoreboardMode = action.payload.scoreboardMode || ScoreboardModeModel.UNKNOWN
      state.result = action.payload.result
    }
  }
})

export const scoreboardSelector = (state: RootState) => state.competitionSlice
export const { setScoreboardStatus } = scoreboardSlice.actions
export default scoreboardSlice.reducer
