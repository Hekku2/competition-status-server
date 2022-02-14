import { createAsyncThunk, createSlice, PayloadAction, SerializedError } from '@reduxjs/toolkit'
import { CompetitionService, CompetitionStatusContentModel, CurrentCompetitorContentModel } from '../../services/openapi'
import { RootState } from '../store'

export interface CompetitionState {
  isLoadingCompetitionStatus: boolean,
  competitionStatus: CompetitionStatusContentModel | undefined,
  currentCompetitor: CurrentCompetitorContentModel | undefined
  error: SerializedError | undefined
}

export const initialState: CompetitionState = {
  isLoadingCompetitionStatus: false,
  competitionStatus: undefined,
  currentCompetitor: undefined,
  error: undefined
}

export const fetchCompetitionStatus = createAsyncThunk('competition/getStatus', async () => {
  return await CompetitionService.competitionGetCompetitionStatus()
})

export const competitionSlice = createSlice({
  name: 'competition',
  initialState,
  reducers: {
    setCurrentCompetitor(state, action: PayloadAction<CurrentCompetitorContentModel | undefined>) {
      state.currentCompetitor = action.payload
    }
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
export const { setCurrentCompetitor } = competitionSlice.actions
export default competitionSlice.reducer
