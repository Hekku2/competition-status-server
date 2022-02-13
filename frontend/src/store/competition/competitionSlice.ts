import { createAsyncThunk, createSlice, SerializedError } from '@reduxjs/toolkit'
import { CompetitionService, CompetitionStatusContentModel } from '../../services/openapi'
import { RootState } from '../store'

export interface CompetitionState {
  isLoadingCompetitionStatus: boolean,
  competitionStatus: CompetitionStatusContentModel | undefined,
  error: SerializedError | undefined
}

export const initialState: CompetitionState = {
  isLoadingCompetitionStatus: false,
  competitionStatus: undefined,
  error: undefined
}

export const fetchCompetitionStatus = createAsyncThunk('competition/getStatus', async () => {
  return await CompetitionService.competitionGetCompetitionStatus()
})

export const competitionSlice = createSlice({
  name: 'competition',
  initialState,
  reducers: {
  },
  extraReducers: builder => {
    builder
      .addCase(fetchCompetitionStatus.pending, (state) => {
        state.isLoadingCompetitionStatus = true
        state.error = undefined
      })
      .addCase(fetchCompetitionStatus.fulfilled, (state, action) => {
        state.competitionStatus = action.payload.content
        state.isLoadingCompetitionStatus = false
      })
      .addCase(fetchCompetitionStatus.rejected, (state, action) => {
        state.isLoadingCompetitionStatus = false
        state.error = action.error
      })
  }
})

export const competitionSelector = (state: RootState) => state.competitionSlice
export default competitionSlice.reducer