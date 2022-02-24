import { Box, List, ListItem, ListItemButton, ListItemText, Typography } from "@mui/material"
import { useAppSelector } from "../../components"

export const UpcomingCompetitorsView = () => {
  const state = useAppSelector(state => state.scoreboardSlice)

  return (
    <Box sx={{
      width: "100%",
      minHeight: "100vh",
      display: "flex",
      flexDirection: "column",
      justifyContent: "space-evenly",
      alignItems: "center"
    }}>
      <Typography>
        Next Up:
      </Typography>

      <List>
        {
          state.upcomingCompetitors.map(item => <ListItem key={item.id} disablePadding>
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
