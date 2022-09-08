import { Box, Typography } from "@mui/material";
import { motion } from "framer-motion";
import { useLocation } from "react-router-dom";
import Equipment from "../common/types/equipment";
import Game from "../common/types/game";

const GamePage : React.FC = () => {
  const { state } = useLocation();
  let game = state as Game

  const formatRequiredEquipment = () => {
    const requiredEquipment = game.requiredEquipment.map(equipmentValue => Equipment[equipmentValue]);
    const stringRequiredEquipment = requiredEquipment.toString();
    return stringRequiredEquipment.replace(/([A-Z])/g, ' $1');
  }

  return (
    <Box sx={{ mx: 2, textAlign: "center" }} >
      <Typography 
        variant="h2"
        component={motion.div}
        initial={{ opacity: 0, scale: 0.5 }}
        animate={{ opacity: 1, scale: 1,  }}
        transition={{ duration: 0.5, delay: 0 }}
      >
        {game.name}
      </Typography>
      <Typography
        sx={{ my: 2 }}
        variant="body1"
        component={motion.div}
        initial={{ opacity: 0, scale: 0.5 }}
        animate={{ opacity: 1, scale: 1,  }}
        transition={{ duration: 0.5, delay: 0.2 }}
      >
        {game.description}
      </Typography>
      <Typography
        sx={{ my: 2, mt: 4 }}
        variant="h5"
        component={motion.div}
        initial={{ opacity: 0, scale: 0.5 }}
        animate={{ opacity: 1, scale: 1,  }}
        transition={{ duration: 0.5, delay: 0.4 }}
      >
        Required Equipment
      </Typography>
      <Typography
        sx={{ my: 2 }}
        variant="body1"
        component={motion.div}
        initial={{ opacity: 0, scale: 0.5 }}
        animate={{ opacity: 1, scale: 1,  }}
        transition={{ duration: 0.5, delay: 0.6 }}
      >
        {formatRequiredEquipment()}
      </Typography>
      <Typography
        sx={{ my: 2, mt: 4 }}
        variant="h5"
        component={motion.div}
        initial={{ opacity: 0, scale: 0.5 }}
        animate={{ opacity: 1, scale: 1,  }}
        transition={{ duration: 0.5, delay: 0.8 }}
      >
        Rules
      </Typography>
      <Typography
        sx={{ my: 2}}
        variant="body1"
        component={motion.div}
        initial={{ opacity: 0, scale: 0.5 }}
        animate={{ opacity: 1, scale: 1,  }}
        transition={{ duration: 0.5, delay: 1 }}
      >
        {game.rules}
      </Typography>
    </Box>
  )
}

export default GamePage;