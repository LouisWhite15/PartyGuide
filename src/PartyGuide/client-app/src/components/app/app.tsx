import { Container } from '@mui/material';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Header from '../layout/header';
import Home from '../pages/home';

function App() {
  return (
    <Container maxWidth="sm">
      <Header />
      <BrowserRouter>
        <Routes>
          <Route path='/' element={<Home />} />
        </Routes>
      </BrowserRouter>
    </Container>
  );
}

export default App;
