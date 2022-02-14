import { Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import { useEffect } from "react";
import { TableLoadRow } from "../../components";
import { useAppDispatch, useAppSelector } from "../../components/hooks";
import { fetchCompetitors } from "../../store/competitor/competitorSlice";

export const CompetitorsView = () => {
  const state = useAppSelector(state => state.competitorSlice)
  const dispatch = useAppDispatch()

  useEffect(function () {
    dispatch(fetchCompetitors())
  }, [dispatch])

  return (
    <>
      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>ID</TableCell>
              <TableCell>Division</TableCell>
              <TableCell>Competitor(s)</TableCell>
              <TableCell>Result</TableCell>
              <TableCell>Forfeit?</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {state.isLoadingCompetitors ? <TableLoadRow colSpan={5} /> :
              state.all.map((row) => (
                <TableRow
                  key={row.id}
                  sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                >
                  <TableCell component="th" scope="row">
                    <Typography variant="body2">
                      {row.id}
                    </Typography>
                  </TableCell>
                  <TableCell component="th" scope="row">
                    <Typography variant="body1" >
                      {row.division}
                    </Typography>
                  </TableCell>
                  <TableCell>{row.competitors.map(item => `${item.name} ${item.team}`).join(", ")}</TableCell>
                  <TableCell>
                    {
                      row.result && <>
                        <Typography variant="body1">
                          {row.result?.total}
                        </Typography>
                        <Typography variant="body2">
                          {`(A: ${row.result?.artisticScore} E: ${row.result?.executionScore} D: ${row.result?.difficultyScore} HJ: ${row.result?.headJudgePenalty})`}
                        </Typography>
                      </>
                    }
                  </TableCell>
                  <TableCell>
                    <Typography>
                      {`${row.forfeit}`}
                    </Typography>
                  </TableCell>
                </TableRow>
              ))
            }
          </TableBody>
        </Table>
      </TableContainer>
    </>
  )
}
