import { Box, Card, CardActionArea, CardContent, Typography } from "@mui/material";
import { motion } from "framer-motion";
import { useLocation } from "react-router-dom";
import Game from "../common/types/game";

const Games : React.FC = () => {
  const { state } = useLocation();
  let games = state as Game[]

  const renderGames = ( games: Game[] ) => {
    if (games.length > 0) {
      return games.map((game, index) => {
        return (
          <Box sx={{ my: 2 }}>
            <Card 
              key={game.id}
              component={motion.div}
              initial={{ opacity: 0, scale: 0.5 }}
              animate={{ opacity: 1, scale: 1,  }}
              transition={{ duration: 0.5, delay: index * 0.2 }}
            >
              <CardActionArea>
                <CardContent>
                <Typography variant="h5" component="div">
                    {game.name}
                </Typography>
                <Typography variant="body2" color="text.secondary">
                    {game.description}
                </Typography>
                </CardContent>
              </CardActionArea>
            </Card>
          </Box>
        )
      });
    }
    else {
      return (
        <Box sx={{ my: 2, textAlign: "center" }}>
          <Typography 
            variant="body1" 
            component={motion.div}
            initial={{ opacity: 0, scale: 0.5 }}
            animate={{ opacity: 1, scale: 1 }}
            transition={{ duration: 0.5 }}
          >
            No games found.
          </Typography>
        </Box>
      )
    }
  };
  
  return (
    <Box sx={{ mx: 2 }}>
      { renderGames(games) }
    </Box>
  )
}

export default Games;
