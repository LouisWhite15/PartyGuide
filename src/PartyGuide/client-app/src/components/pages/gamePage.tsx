import { Box } from "@mui/material";
import { useLocation } from "react-router-dom";
import { AnimatedBody, AnimatedHeader, AnimatedSubHeader } from "../common/animatedTypography";
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
      <AnimatedHeader 
        content={game.name} 
        delay={0}
        sx={{ my: 2 }}
      />
      <AnimatedBody 
        content={game.description} 
        delay={0.2}
        sx={{ my: 2 }}
      />
      <AnimatedSubHeader 
        content="Required Equipment"
        delay={0.4}
        sx={{ my: 2, mt: 4 }}
      />
      <AnimatedBody
        content={formatRequiredEquipment()}
        delay={0.6}
        sx={{ my: 2 }}
      />
      <AnimatedSubHeader 
        content="Rules"
        delay={0.8}
        sx={{ my: 2, mt: 4 }}
      />
      <AnimatedBody
        content={game.rules}
        delay={1.0}
        sx={{ my: 2 }}
      />
    </Box>
  )
}

export default GamePage;