import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import { ScoreboardModeModel, ScoreboardStatusModel } from '../../services/openapi'
import { RootState } from '../store'

export interface ScoreboardState {
  scoreboardMode: ScoreboardModeModel
}

export const initialState: ScoreboardState = {
  scoreboardMode: ScoreboardModeModel.UNKNOWN,
}

export const scoreboardSlice = createSlice({
  name: 'scoreboard',
  initialState,
  reducers: {
    setScoreboardStatus(state, action: PayloadAction<ScoreboardStatusModel>) {
      state.scoreboardMode = action.payload.scoreboardMode || ScoreboardModeModel.UNKNOWN
    }
  }
})

export const scoreboardSelector = (state: RootState) => state.competitionSlice
export const { setScoreboardStatus } = scoreboardSlice.actions
export default scoreboardSlice.reducer
