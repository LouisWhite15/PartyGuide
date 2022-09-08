import axios from 'axios';
import { Checkbox, FormControl, FormControlLabel, FormGroup, Grid, Typography } from "@mui/material";
import LoadingButton from '@mui/lab/LoadingButton';
import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import GetGamesResponse from '../common/types/getGamesResponse';
import Equipment from '../common/types/equipment';
import { motion } from 'framer-motion';

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

    navigate('/games', { state: gamesResponse });
  };

  async function getGames() {
    try {
      const { data } = await axios.post<GetGamesResponse>(
        "/api/game/getGames",
        { selectedEquipment: getSelectedEquipment() },
        {
          headers: {
            'Content-Type': 'application/json',
            Accept: 'application/json'
          },
        },
      );

      return data;
    } catch (error) {
      if (axios.isAxiosError(error)) {
        return error.message;
      } else {
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
      <Typography 
        variant="body1"
        component={motion.div}
        initial={{ opacity: 0, scale: 0.5 }}
        animate={{ opacity: 1, scale: 1 }}
        transition={{ duration: 0.5 }}
      >
        Please select the equipment that you have
      </Typography>
      <FormControl sx={{ m: 3 }} component="fieldset" variant="standard">
        <FormGroup>
          <motion.div
            initial={{ opacity: 0, scale: 0.5 }}
            animate={{ opacity: 1, scale: 1 }}
            transition={{ duration: 0.5, delay: 0.2 }}
          >
            <FormControlLabel 
              control={
                <motion.div
                  whileHover={{ scale: 1.1 }}
                  whileTap={{ scale: 0.9 }}
                >
                  <Checkbox checked={cards} onChange={handleChange} name="cards" />
                </motion.div>
              }
              label="Cards"
            />
          </motion.div>
          <motion.div
            initial={{ opacity: 0, scale: 0.5 }}
            animate={{ opacity: 1, scale: 1 }}
            transition={{ duration: 0.5, delay: 0.4 }}
          >
            <FormControlLabel 
              control={
                <motion.div
                  whileHover={{ scale: 1.1 }}
                  whileTap={{ scale: 0.9 }}
                >
                  <Checkbox checked={pingPongBalls} onChange={handleChange} name="pingPongBalls" />
                </motion.div>
              }
              label="Ping Pong Balls"
            />
          </motion.div>
          <motion.div
            initial={{ opacity: 0, scale: 0.5 }}
            animate={{ opacity: 1, scale: 1 }}
            transition={{ duration: 0.5, delay: 0.6 }}
          >
            <FormControlLabel 
              control={
                <motion.div
                  whileHover={{ scale: 1.1 }}
                  whileTap={{ scale: 0.9 }}
                >
                  <Checkbox checked={cups} onChange={handleChange} name="cups" />
                </motion.div>
              }
              label="Cups"
            />
          </motion.div>
        </FormGroup>
      </FormControl>
      <motion.div
        initial={{ opacity: 0, scale: 0.5 }}
        animate={{ opacity: 1, scale: 1 }}
        transition={{ duration: 0.5, delay: 0.8 }}
      >
      <LoadingButton
        type="submit"
        variant="contained" 
        size="small"
        onClick={onNext}
        loading={isLoading}
      >
        Next
      </LoadingButton>
      </motion.div>
    </Grid>
  );
}

export default Questions;
