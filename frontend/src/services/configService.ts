import { Config } from "./config";

export class ConfigService {
  public static async getConfig(): Promise<Config> {
    var useLocalConfig = process.env.REACT_APP_USE_LOCAL_CONFIG;
    const cacheKey = 'app_config';

    var cached = sessionStorage.getItem(cacheKey);
    if (cached) {
      var parsed = JSON.parse(cached);
      return Promise.resolve(parsed);
    }

    const configSource = useLocalConfig !== 'true' ? '/config.json' : '/config.local.json';
    const response = await fetch(configSource);
    const parsedJson = await response.json();
    var saveFormat = {
      baseUrl: parsedJson.backendUrl
    };
    sessionStorage.setItem(cacheKey, JSON.stringify(saveFormat));
    return saveFormat;
  }
}