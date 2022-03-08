import { combineReducers } from '@reduxjs/toolkit'
import { competitionSlice } from './competition/competitionSlice'
import { competitorSlice } from './competitor/competitorSlice'
import { scoreboardSlice } from './scoreboard/scoreboardSlice'

const rootReducer = combineReducers({
  competitionSlice: competitionSlice.reducer,
  competitorSlice: competitorSlice.reducer,
  scoreboardSlice: scoreboardSlice.reducer
})

export default rootReducer
