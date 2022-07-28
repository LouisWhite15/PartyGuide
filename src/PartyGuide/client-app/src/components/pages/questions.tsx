import { Grid, Typography } from "@mui/material";

const Questions : React.FC = () => (
  <Grid
    container
    direction="column"
    rowSpacing={3}
    justifyContent="center"
    alignItems="center"
  >
    <Grid item xs={12}>
      <Typography variant="h6">Questions</Typography>
    </Grid>
  </Grid>
)

export default Questions;
