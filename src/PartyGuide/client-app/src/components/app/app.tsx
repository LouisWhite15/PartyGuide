import { Box, Container } from '@mui/material';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Header from '../layout/header';
import Home from '../pages/home';
import Questions from '../pages/questions';

function App() {
  return (
    <Container maxWidth="sm">
      <Box sx={{ my: 4 }}>
        <Header
          title='Party Guide'
        />
        <BrowserRouter>
          <Routes>
            <Route path='/' element={<Home />} />
            <Route path='/questions' element={<Questions />} />
          </Routes>
        </BrowserRouter>
      </Box>
    </Container>
  );
}

export default App;
