import { combineReducers } from '@reduxjs/toolkit'
import { competitionSlice } from './competition/competitionSlice'

const rootReducer = combineReducers({
  competitionSlice: competitionSlice.reducer,
})

export default rootReducer