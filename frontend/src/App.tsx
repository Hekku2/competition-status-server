import { useEffect, useState } from 'react';
import './App.css';
import { ConfigService } from './services/configService';
import { OpenAPI } from './services/openapi';
import { ThemeProvider } from '@emotion/react';
import { CssBaseline } from '@mui/material';
import { theme } from './util';
import MainRouter from './components/MainRouter';

const App = () => {
  const [isLoading, setLoading] = useState<boolean>(true)

  useEffect(function () {
    ConfigService.getConfig().then((config) => {
      OpenAPI.BASE = config.baseUrl || ""

      setLoading(false)
    })
  }, [])

  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      {isLoading ? <></> : <MainRouter />}

    </ThemeProvider>
  );
}

export default App;
