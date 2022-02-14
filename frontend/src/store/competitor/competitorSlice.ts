import { createAsyncThunk, createSlice, SerializedError } from '@reduxjs/toolkit'
import { CompetitorService, ParticipationModel } from '../../services/openapi'
import { RootState } from '../store'

export interface CompetitorState {
  isLoadingCompetitors: boolean
  all: ParticipationModel[]
  error: SerializedError | undefined
}

export const initialState: CompetitorState = {
  isLoadingCompetitors: false,
  all: [],
  error: undefined
}

export const fetchCompetitors = createAsyncThunk('competitor/getAll', async () => {
  return await CompetitorService.competitorGetAll()
})

export const competitorSlice = createSlice({
  name: 'competitor',
  initialState,
  reducers: {
  },
  extraReducers: builder => {
    builder
      .addCase(fetchCompetitors.pending, (state) => {
        state.isLoadingCompetitors = true
        state.error = undefined
      })
      .addCase(fetchCompetitors.fulfilled, (state, action) => {
        state.all = action.payload
        state.isLoadingCompetitors = false
      })
      .addCase(fetchCompetitors.rejected, (state, action) => {
        state.isLoadingCompetitors = false
        state.error = action.error
      })
  }
})

export const competitorSelection = (state: RootState) => state.competitionSlice
export default competitorSlice.reducer
