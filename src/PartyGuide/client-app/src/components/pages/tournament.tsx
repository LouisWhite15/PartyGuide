import { LoadingButton } from "@mui/lab";
import { Button, Card, CardActionArea, CardContent, Grid, TextField, Typography } from "@mui/material";
import axios from "axios";
import { useState } from "react";
import Tournament from "../common/types/tournament";
import Participant from "../common/types/participant";
import Match from "../common/types/match";
import { motion } from "framer-motion";

const TournamentPage : React.FC = () => {
  const [state, setState] = useState({
    participants: [] as string[],
    isLoading: false,
    matches: [] as Match[]
  })

  const onAddParticipant = () => {
    const { participants, isLoading } = state;
    const newParticipants = participants.slice(0);
    newParticipants.push("");
    setState({ ...state, participants: newParticipants })
  }

  const handleChange = (index: number) => (event: any) => {
    const { participants , isLoading} = state;
    const newParticipants = participants.slice(0);
    newParticipants[index] = event.target.value;
    setState({ ...state, participants: newParticipants });
  }

  const submitTournament = async () => {
    setState({ ...state, isLoading: true });
    let tournamentResponse = await getTournament() as Tournament;
    setState({ ...state, isLoading: false, matches: tournamentResponse.matches });
  }

  async function getTournament() {
    try {
      const { data } = await axios.post<Tournament>(
        "/api/tournament",
        { 
          name: "Tournament", 
          participants: state.participants.map(participantName => {
            return {
              name: participantName
            } as Participant
          })
        },
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
  
  const renderParticipants = () => {
    return (
      state.participants.map((_, index) => (
        <TextField
          sx={{ my: 1 }}
          id={`player-${index}`}
          label={`Player ${index+1}`}
          key={index}
          onChange={handleChange(index)}  
        />
      ))
    )
  }

  const renderMatches = () => {
    return (
      state.matches.map((match, index) => {
        return (
          <Card
            key={`match-${index}`}
            component={motion.div}
            initial={{ opacity: 0, scale: 0.5 }}
            animate={{ opacity: 1, scale: 1, }}
            transition={{ duration: 0.5, delay: index * 0.2 }}
          >
            <CardActionArea>
              <CardContent>
                <Typography variant="body2" color="text.secondary">
                  {match.participants.at(0)?.name}
                </Typography>
                <Typography variant="body2" color="text.secondary">
                  {match.participants.at(1)?.name}
                </Typography>
              </CardContent>
            </CardActionArea>
          </Card>
        );
      })
    )
  }

  return (
    <Grid
      container
      direction="column"
      rowSpacing={3}
      justifyContent="center"
      alignItems="center"
    >
      { renderParticipants() }
      <Button 
          sx={{ my: 1 }}
          variant="outlined" 
          size="medium"
          onClick={onAddParticipant}
        >
          Add Participant
      </Button>
      <LoadingButton
        sx={{ my: 2 }}
        type="submit"
        variant="contained" 
        size="large"
        onClick={submitTournament}
        loading={state.isLoading}
      >
        Create Tournament
      </LoadingButton>
      { renderMatches() }
    </Grid>
  )
}

export default TournamentPage;
