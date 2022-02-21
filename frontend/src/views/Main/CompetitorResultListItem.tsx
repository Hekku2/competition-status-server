import { ListItem, ListItemText, Typography } from "@mui/material";
import { ParticipationRowModel } from "../../services/openapi";

type CompetitorResultListItemProps = {
  competitor: ParticipationRowModel
}

export const CompetitorResultListItem = ({ competitor }: CompetitorResultListItemProps) => {
  return (
    <ListItem key={competitor.id}>
      <ListItemText
        primary={competitor.competitors.map(item => `${item.name} ${item.team}`).join(", ")}
        secondary={
          <>
            <Typography
              sx={{ display: 'inline' }}
              component="span"
              variant="body2"
              color="text.primary"
            >
              {competitor.result?.total}
            </Typography>
            {`(A: ${competitor.result?.artisticScore} E: ${competitor.result?.executionScore} D: ${competitor.result?.difficultyScore} HJ: ${competitor.result?.headJudgePenalty})`}
          </>
        }
      />
    </ListItem>
  );
}
