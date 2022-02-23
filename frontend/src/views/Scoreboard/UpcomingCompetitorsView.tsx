import { Box, List, ListItem, ListItemButton, ListItemText, Typography } from "@mui/material"
import { useAppSelector } from "../../components"

export const UpcomingCompetitorsView = () => {
  const state = useAppSelector(state => state.scoreboardSlice)

  return (
    <Box>
      <Typography>
        Next Up:
      </Typography>

      <List>
        {
          state.upcomimngCompetitors.map(item => <ListItem key={item.id} disablePadding>
            <ListItemButton>
              <ListItemText primary={item.competitors[0].name} />
            </ListItemButton>
          </ListItem>
          )
        }
      </List>
    </Box>
  )
}
