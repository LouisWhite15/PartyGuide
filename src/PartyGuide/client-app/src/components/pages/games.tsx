import { Box, Card, CardActionArea, CardContent, Typography } from "@mui/material";
import { useLocation } from "react-router-dom";
import Game from "../common/types/game";

const Games : React.FC = () => {
  const { state } = useLocation();
  let games = state as Game[]

  const renderGames = ( games: Game[] ) => {
    if (games.length > 0) {
      return games.map(game => {
        return (
          <Box sx={{ my: 2 }}>
            <Card key={game.id}>
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
        <Typography variant="body1" component="div">
          No games found.
        </Typography>
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
