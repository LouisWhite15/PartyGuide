import { Box, Container } from '@mui/material';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Header from '../layout/header';
import Games from '../pages/games';
import Home from '../pages/home';
import Questions from '../pages/questions';

function App() {
  return (
    <Container maxWidth="sm">
      <Box sx={{ my: 4 }}>
        <BrowserRouter>
          <Header
            title='Party Guide'
          />
          <Routes>
            <Route path='/' element={<Home />} />
            <Route path='/questions' element={<Questions />} />
            <Route path='/games' element={<Games games={[]} />} />
          </Routes>
        </BrowserRouter>
      </Box>
    </Container>
  );
}

export default App;
