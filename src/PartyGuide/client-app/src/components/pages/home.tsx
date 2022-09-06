import { Button, Grid } from "@mui/material";
import { Link } from "react-router-dom";
import { motion } from "framer-motion";

const Home : React.FC = () => (
  <Grid
    container
    direction="column"
    rowSpacing={3}
    justifyContent="center"
    alignItems="center"
  >
    <Grid item xs={12}>
      <motion.div
        initial={{ opacity: 0, scale: 0.5 }}
        animate={{ opacity: 1, scale: 1 }}
        transition={{ duration: 0.5 }}
      >
        <Button 
          component={Link}
          to="/questions"
          variant="contained" 
          size="large"
        >
          Start
        </Button>
      </motion.div>
    </Grid>
  </Grid>
)

export default Home;
