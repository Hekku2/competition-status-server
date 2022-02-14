import { Card, CardContent, CardHeader, Typography } from "@mui/material";
import { PerformanceResultsContentModel } from "../../services/openapi";

type ResultCardProps = {
  results: PerformanceResultsContentModel
}

export const ResultCard = ({ results }: ResultCardProps) => {
  return (
    <Card>
      <CardHeader title={"Result"} subheader={results.division}>
      </CardHeader>
      <CardContent>
        <Typography gutterBottom variant="h5" component="div">
          {results.currentPlace}.
          {results.competitors.map(com => `${com.name} ${com.team}`)}
        </Typography>

        <Typography
          component="p"
          variant="body1"
          color="text.primary"
        >
          Total {results.result?.total}
        </Typography>
        <Typography
          component="p"
          variant="body2"
          color="text.primary"
        >
          {`(A: ${results.result?.artisticScore} E: ${results.result?.executionScore} D: ${results.result?.difficultyScore} HJ: ${results.result?.headJudgePenalty})`}
        </Typography>

      </CardContent>
    </Card>
  );
}
