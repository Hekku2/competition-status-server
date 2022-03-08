import { Button, Card, CardActions, CardContent, CardHeader, FormControl, FormControlLabel, FormLabel, InputLabel, MenuItem, Radio, RadioGroup, Select } from "@mui/material"
import { Field, Form, Formik } from "formik"
import { useEffect } from "react"
import { useAppDispatch, useAppSelector } from "../../components"
import { ScoreboardModeModel } from "../../services/openapi"
import { fetchCompetitionStatus } from "../../store/competition/competitionSlice"
import { setActiveDivision, setMode } from "../../store/scoreboard/scoreboardSlice"

const CustomizedSelectForFormik = ({ children, form, field }: any) => {
  const { name, value } = field;
  const { setFieldValue } = form;

  return (
    <Select
      name={name}
      value={value}
      onChange={e => {
        setFieldValue(name, e.target.value);
      }}
    >
      {children}
    </Select>
  );
};

export const Management = () => {
  const dispatch = useAppDispatch()
  const scoreboardState = useAppSelector(state => state.scoreboardSlice)
  const competitionState = useAppSelector(state => state.competitionSlice)

  useEffect(function () {
    dispatch(fetchCompetitionStatus())
  }, [dispatch])

  return (
    <>
      {
        !scoreboardState.isSettingScoreboardMode && !scoreboardState.isSettingActiveDivision && !competitionState.isLoadingCompetitionStatus &&
        <Formik
          initialValues={{
            scoreboardMode: scoreboardState.scoreboardMode,
            activeDivision: scoreboardState.division
          }}
          onSubmit={(values) => {
            dispatch(setMode(values.scoreboardMode))
            dispatch(setActiveDivision(values.activeDivision))
          }}
        >
          {
            ({ values, setFieldValue }) => (
              <Form>
                <Card>
                  <CardHeader title="Manage" />
                  <CardContent>
                    <FormControl fullWidth component="fieldset">
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

                    <FormControl fullWidth>
                      <InputLabel id="activeDivision">Active division</InputLabel>
                      <Field name="activeDivision" component={CustomizedSelectForFormik}>
                        {
                          competitionState.competitionStatus?.divisions.map(division => <MenuItem value={division.name} key={division.name}>{division.name}</MenuItem>)
                        }
                      </Field>
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
