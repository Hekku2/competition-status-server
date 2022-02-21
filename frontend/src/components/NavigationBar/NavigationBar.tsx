import { Card } from "@mui/material"
import { TopNavLink } from "."

type ContentBarProps = {
  children: React.ReactNode
}

const ContentBar = ({ children }: ContentBarProps) => {
  return (
    <Card
      sx={{
        width: "100%",
        display: 'flex',
        alignItems: 'center',
        textAlign: 'center',
        height: "50px"
      }}>
      {children}
    </Card>)
}

export const NavigationBar = () => {
  return (
    <ContentBar>
      <TopNavLink to="/" text="Main" />
      <TopNavLink to="/competitors" text="Competitors" />
      <TopNavLink to="/scoreboard" text="Scoreboard" />
    </ContentBar>
  )
}
