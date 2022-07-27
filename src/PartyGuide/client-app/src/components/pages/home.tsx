import { Button, Grid } from "@mui/material";

const Home : React.FC = () => (
  <Grid
    container
    direction="column"
    rowSpacing={3}
    justifyContent="center"
    alignItems="center"
  >
    <Grid item xs={12}>
      <Button variant="contained" size="large">Start</Button>
    </Grid>
  </Grid>
)

export default Home;
