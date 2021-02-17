import { app, BrowserWindow } from 'electron';

const createWindow = (): void => {
  let window = new BrowserWindow({
    width: 800,
    height: 600,
    webPreferences: {
      nodeIntegration: true
    }
  });

  window.loadFile('index.html');
}

app.on('ready', createWindow);