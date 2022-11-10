import { Box, Button, Grid, TextField } from "@mui/material";
import { useState } from "react";

const TournamentPage : React.FC = () => {
  const [state, setState] = useState({
    participants: [""]
  })

  const onAddParticipant = () => {
    const { participants } = state;
    const newParticipants = participants.slice(0);
    newParticipants.push("");
    setState({ participants: newParticipants })
  }

  const handleChange = (index: number) => (event: any) => {
    const { participants } = state;
    const newParticipants = participants.slice(0);
    newParticipants[index] = event.target.value;
    setState({ participants: newParticipants });
  }
  
  const renderParticipants = () => {
    return (
      state.participants.map((participant, index) => (
        <TextField
          sx={{ my: 1 }}
          id={`participant-${index}`}
          label={`Participant ${index}`}
          key={index}
          onChange={handleChange(index)}  
        />
      ))
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
          variant="contained" 
          size="large"
          onClick={onAddParticipant}
        >
          Add Participant
        </Button>
    </Grid>
  )
}

export default TournamentPage;
