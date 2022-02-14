import { useEffect, useState } from 'react';
import './App.css';
import { ConfigService } from './services/configService';
import { OpenAPI } from './services/openapi';
import { ThemeProvider } from '@emotion/react';
import { CssBaseline } from '@mui/material';
import { HubConnectionProvider, theme } from './util';
import MainRouter from './components/MainRouter';

const App = () => {
  const [isLoading, setLoading] = useState<boolean>(true)
  const [baseUrl, setBaseUrl] = useState<string>('')

  useEffect(function () {
    ConfigService.getConfig().then((config) => {
      OpenAPI.BASE = config.baseUrl
      setBaseUrl(config.baseUrl)
      setLoading(false)
    })
  }, [])

  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      {!isLoading &&
        <HubConnectionProvider baseUrl={baseUrl}>
          <MainRouter />
        </HubConnectionProvider>
      }

    </ThemeProvider>
  );
}

export default App;
