import { LinearProgress, TableCell, TableRow } from "@mui/material"

type TableLoadRowProps = {
  colSpan: number
}

export const TableLoadRow = ({ colSpan }: TableLoadRowProps) => {
  return (
    <TableRow>
      <TableCell colSpan={colSpan}>
        <LinearProgress />
      </TableCell>
    </TableRow>
  )
}
