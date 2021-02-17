import { app, BrowserWindow } from 'electron';
import isDev from 'electron-is-dev';

const createWindow = (): void => {
  let window = new BrowserWindow({
    width: 800,
    height: 600,
    webPreferences: {
      nodeIntegration: true
    }
  });

  window.loadURL(isDev ? 'http://localhost:9000' : `file://${app.getAppPath()}/index.html`)
}

app.on('ready', createWindow);