import { useAppSelector } from "../../components"

export const ScoreboardView = () => {
  const state = useAppSelector(state => state.scoreboardSlice)

  return (
    <>
      Empty page! {state.scoreboardMode}
    </>
  )
}
