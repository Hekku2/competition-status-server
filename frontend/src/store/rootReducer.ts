import { combineReducers } from '@reduxjs/toolkit'
import { competitionSlice } from './competition/competitionSlice'
import { competitorSlice } from './competitor/competitorSlice'

const rootReducer = combineReducers({
  competitionSlice: competitionSlice.reducer,
  competitorSlice: competitorSlice.reducer,
})

export default rootReducer
