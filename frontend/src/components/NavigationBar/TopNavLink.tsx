import { Typography } from "@mui/material"
import { Link } from "react-router-dom"

type TopNavLinkProps = {
  to: string,
  text: string
}

export const TopNavLink = ({ to, text }: TopNavLinkProps) => {
  return (
    <Typography sx={{ minWidth: 100 }}><Link to={to}>{text}</Link></Typography>
  )
}
