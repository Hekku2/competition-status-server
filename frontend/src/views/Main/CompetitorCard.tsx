import { Card, CardContent, CardHeader, Typography } from "@mui/material";
import { CurrentCompetitorContentModel } from "../../services/openapi";

type CompetitorCardProps = {
  header: string
  competitor: CurrentCompetitorContentModel
}

export const CompetitorCard = ({ competitor, header }: CompetitorCardProps) => {
  return (
    <Card>
      <CardHeader title={header} subheader={competitor.division}>
      </CardHeader>
      <CardContent>
        {competitor.competitors.map(com => <Typography key={com.name} variant="body1">{com.name} {com.team}</Typography>)}
      </CardContent>
    </Card>
  );
}