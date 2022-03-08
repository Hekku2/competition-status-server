import { Button, Card, CardActions, CardContent, CardHeader, FormControl, FormControlLabel, FormLabel, Radio, RadioGroup } from "@mui/material"
import { Form, Formik } from "formik"
import { useAppDispatch, useAppSelector } from "../../components"
import { ScoreboardModeModel } from "../../services/openapi"
import { setMode } from "../../store/scoreboard/scoreboardSlice"

export const Management = () => {
  const dispatch = useAppDispatch()
  const state = useAppSelector(state => state.scoreboardSlice)

  return (
    <>
      {
        !state.isSettingScoreboardMode &&
        <Formik
          initialValues={{
            scoreboardMode: state.scoreboardMode,
          }}
          onSubmit={(values) => {
            dispatch(setMode(values.scoreboardMode))
          }}
        >
          {
            ({ values, setFieldValue }) => (
              <Form>
                <Card>
                  <CardHeader title="Manage" />
                  <CardContent>
                    <FormControl component="fieldset">
                      <FormLabel id="scoreboard-mode-select">Scoreboard mode</FormLabel>
                      <RadioGroup
                        name="scoreboardMode"
                        value={values.scoreboardMode.toString()}
                        onChange={(event) => {
                          setFieldValue('scoreboardMode', event.currentTarget.value, false)
                        }}
                      >
                        <FormControlLabel value={ScoreboardModeModel.UNKNOWN.toString()} control={<Radio />} label="Unknown" />
                        <FormControlLabel value={ScoreboardModeModel.COMPETITOR_RESULTS.toString()} control={<Radio />} label="Single competitor result" />
                        <FormControlLabel value={ScoreboardModeModel.DIVISION_STATUS.toString()} control={<Radio />} label="Division status" />
                        <FormControlLabel value={ScoreboardModeModel.UPCOMING_COMPETITORS.toString()} control={<Radio />} label="Upcoming competitors" />
                      </RadioGroup>
                    </FormControl>

                  </CardContent>
                  <CardActions>
                    <Button type="submit">Activate</Button>
                  </CardActions>
                </Card>
              </Form>
            )
          }
        </Formik>
      }
    </>
  )
}
