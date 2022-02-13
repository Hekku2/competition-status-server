import { Card, CardContent, CardHeader, List } from "@mui/material";
import { CompetitorResultListItem } from ".";
import { DivisionStatusModel } from "../../services/openapi";

type DivisionResultsCardProps = {
  division: DivisionStatusModel
}

export const DivisionResultsCard = ({ division }: DivisionResultsCardProps) => {
  return (
    <Card>
      <CardHeader title={division.name} subheader={"Results"}>
      </CardHeader>
      <CardContent>
        <List>
          {division.results.map(item => <CompetitorResultListItem key={item.id} competitor={item} />)}
        </List>
      </CardContent>
    </Card>
  );
}