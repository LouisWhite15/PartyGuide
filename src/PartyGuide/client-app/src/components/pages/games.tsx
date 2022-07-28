import { Box, Card, CardActionArea, CardContent, Grid, Typography } from "@mui/material";
import Game from "../games/game";

interface GamesProps {
  games: Game[],
}

const renderGames = ( games: Game[] ) => {
  if (games.length > 0) {
    return games.map(game => {
      return (
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


const Games : React.FC<GamesProps> = ({ games }) => (
  <Grid
    container
    direction="column"
    rowSpacing={3}
    justifyContent="center"
    alignItems="center"
  >
    <Box>{ renderGames(games) }</Box>
  </Grid>
)

export default Games;
