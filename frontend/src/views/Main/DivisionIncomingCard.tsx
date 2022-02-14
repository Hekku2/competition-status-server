import { Card, CardContent, CardHeader, List } from "@mui/material";
import { UpcomingCompetitorListItem } from ".";
import { DivisionStatusModel } from "../../services/openapi";

type DivisionIncomingCardProps = {
  division: DivisionStatusModel
}

export const DivisionIncomingCard = ({ division }: DivisionIncomingCardProps) => {
  return (
    <Card key={division.name}>
      <CardHeader title={division.name} subheader={"Upcoming"}></CardHeader>
      <CardContent>
        <List>
          {division.upcomingCompetitorModels.map(item => <UpcomingCompetitorListItem key={item.id} competitor={item} />)}
        </List>
      </CardContent>
    </Card>
  );
}