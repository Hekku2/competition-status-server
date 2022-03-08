import { Box, List, ListItem, ListItemButton, ListItemText, Typography } from "@mui/material"
import { useAppSelector } from "../../components"

export const UpcomingCompetitorsView = () => {
  const state = useAppSelector(state => state.scoreboardSlice)

  const listEmpty = state.upcomingCompetitors.length === 0

  return (
    <Box sx={{
      width: "100%",
      minHeight: "100vh",
      display: "flex",
      flexDirection: "column",
      justifyContent: "space-evenly",
      alignItems: "center"
    }}>
      <Typography variant="h1">
        {state.division}
      </Typography>

      <Typography>
        {!listEmpty ? 'Next Up:' : 'Division finished'}
      </Typography>

      <List>
        {
          state.upcomingCompetitors.map(item => <ListItem key={item.id} disablePadding>
            <ListItemButton>
              <ListItemText primary={item.competitors.map(s => `${s.name} ${s.team}`)} />
            </ListItemButton>
          </ListItem>
          )
        }
      </List>
    </Box>
  )
}
