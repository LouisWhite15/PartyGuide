import { Checkbox, FormControl, FormControlLabel, FormGroup, Grid, Typography } from "@mui/material";
import LoadingButton from '@mui/lab/LoadingButton';
import React, { useState } from "react";
import { useNavigate } from "react-router-dom";

const Questions : React.FC = () => {
  let navigate = useNavigate();

  const [state, setState] = useState({
    cards: false,
    pingPongBalls: false,
    cups: false,
    isLoading: false,
  })

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setState({
      ...state,
      [event.target.name]: event.target.checked,
    });
  };

  const setLoading = (isLoading: boolean) => {
    setState({
      ...state,
      isLoading: isLoading
    });
  };

  const onNext = async () => {
    setLoading(true);

    // TODO: API call to the backend to save the users answers
    // Wait 2 seconds to simulate loading time
    await new Promise(resolve => setTimeout(resolve, 2000));

    console.log(`Cards: ${cards}`);
    console.log(`Ping Pong Balls: ${pingPongBalls}`);
    console.log(`Cups: ${cups}`);

    setLoading(false);

    navigate('/games');
  };

  const { cards, pingPongBalls, cups, isLoading } = state;

  return (
    <Grid
      container
      direction="column"
      rowSpacing={3}
      justifyContent="center"
      alignItems="center"
    >
      <Typography variant="body1">Please select the equipment that you have</Typography>
      <FormControl sx={{ m: 3 }} component="fieldset" variant="standard">
        <FormGroup>
          <FormControlLabel 
            control={
              <Checkbox checked={cards} onChange={handleChange} name="cards" />
            }
            label="Cards"
          />
          <FormControlLabel 
            control={
              <Checkbox checked={pingPongBalls} onChange={handleChange} name="pingPongBalls" />
            }
            label="Ping Pong Balls"
          />
          <FormControlLabel 
            control={
              <Checkbox checked={cups} onChange={handleChange} name="cups" />
            }
            label="Cups"
          />
        </FormGroup>
      </FormControl>
      <LoadingButton
          type="submit"
          variant="contained" 
          size="small"
          onClick={onNext}
          loading={isLoading}
        >
          Next
       </LoadingButton>
    </Grid>
  );
}

export default Questions;
