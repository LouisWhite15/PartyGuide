import axios from 'axios';
import { Checkbox, FormControl, FormControlLabel, FormGroup, Grid, Typography } from "@mui/material";
import LoadingButton from '@mui/lab/LoadingButton';
import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import GetGamesResponse from '../common/types/getGamesResponse';
import Equipment from '../common/types/equipment';

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
    let gamesResponse = await getGames() as GetGamesResponse;
    setLoading(false);

    console.log('gamesresponse')
    console.log(gamesResponse)

    navigate('/games', { state: gamesResponse });
  };

  async function getGames() {
    try {
      const { data } = await axios.post<GetGamesResponse>(
        `${process.env.REACT_APP_BACKEND_API_URL}/api/game/getGames`,
        { selectedEquipment: getSelectedEquipment() },
        {
          headers: {
            'Content-Type': 'application/json',
            Accept: 'application/json'
          },
        },
      );

      console.log(JSON.stringify(data, null, 4));

      return data;
    } catch (error) {
      if (axios.isAxiosError(error)) {
        console.log('error message: ', error.message);
        return error.message;
      } else {
        console.log('unexpected error: ', error);
        return 'An unexpected error occurred';
      }
    }
  }

  // TODO: There are better ways of doing this
  function getSelectedEquipment(): Equipment[] {
    let selectedEquipment: Equipment[] = [];
    
    if (cards) {
      selectedEquipment.push(Equipment.Cards)
    }

    if (pingPongBalls) {
      selectedEquipment.push(Equipment.PingPongBalls)
    }

    if (cups) {
      selectedEquipment.push(Equipment.Cups)
    }

    return selectedEquipment;
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
