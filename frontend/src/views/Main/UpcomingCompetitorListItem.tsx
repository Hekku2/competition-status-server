import { ListItem, Typography } from "@mui/material";
import { UpcomingCompetitorModel } from "../../services/openapi";

type UpcomingCompetitorListItemProps = {
  competitor: UpcomingCompetitorModel
}

export const UpcomingCompetitorListItem = ({ competitor }: UpcomingCompetitorListItemProps) => {
  return (
    <ListItem key={competitor.id}>
      {competitor.competitors.map(com => <Typography key={com.name} variant="body1">{com.name} {com.team}</Typography>)}
    </ListItem>
  );
}