import { createAsyncThunk, createSlice, PayloadAction, SerializedError } from '@reduxjs/toolkit'
import { ParticipationRowModel, PerformanceResultsContentModel, ScoreboardModeModel, ScoreboardService, ScoreboardStatusModel, UpcomingCompetitorModel } from '../../services/openapi'
import { RootState } from '../store'

export interface ScoreboardState {
  isSettingScoreboardMode: boolean
  isSettingActiveDivision: boolean
  scoreboardMode: ScoreboardModeModel
  result: PerformanceResultsContentModel | undefined
  upcomingCompetitors: Array<UpcomingCompetitorModel>
  results: Array<ParticipationRowModel>,
  division: string | undefined | null,
  error: SerializedError | undefined
}

export const initialState: ScoreboardState = {
  isSettingScoreboardMode: true,
  isSettingActiveDivision: false,
  scoreboardMode: ScoreboardModeModel.UNKNOWN,
  result: undefined,
  upcomingCompetitors: [],
  results: [],
  division: undefined,
  error: undefined
}

export const setMode = createAsyncThunk('scoreboard/setMode', async (mode: ScoreboardModeModel) => {
  return await ScoreboardService.scoreboardSetScoreboardMode(mode)
})

export const setActiveDivision = createAsyncThunk('scoreboard/setActiveDivision', async (activeDvision: string | null | undefined) => {
  return await ScoreboardService.scoreboardSetActiveDivision(activeDvision || undefined)
})

export const scoreboardSlice = createSlice({
  name: 'scoreboard',
  initialState,
  reducers: {
    setScoreboardStatus(state, action: PayloadAction<ScoreboardStatusModel>) {
      state.scoreboardMode = action.payload.scoreboardMode || ScoreboardModeModel.UNKNOWN
      state.result = action.payload.result
      state.upcomingCompetitors = action.payload.upcomingCompetitors
      state.results = action.payload.results
      state.division = action.payload.division
      state.isSettingScoreboardMode = false
    }
  },
  extraReducers: builder => {
    builder
      .addCase(setMode.pending, (state) => {
        state.isSettingScoreboardMode = true
        state.error = undefined
      })
      .addCase(setMode.fulfilled, (state) => {
        state.isSettingScoreboardMode = false
      })
      .addCase(setMode.rejected, (state, action) => {
        state.isSettingScoreboardMode = false
        state.error = action.error
      })
      .addCase(setActiveDivision.pending, (state) => {
        state.isSettingActiveDivision = true
        state.error = undefined
      })
      .addCase(setActiveDivision.fulfilled, (state) => {
        state.isSettingActiveDivision = false
      })
      .addCase(setActiveDivision.rejected, (state, action) => {
        state.isSettingActiveDivision = false
        state.error = action.error
      })
  }
})

export const scoreboardSelector = (state: RootState) => state.competitionSlice
export const { setScoreboardStatus } = scoreboardSlice.actions
export default scoreboardSlice.reducer
