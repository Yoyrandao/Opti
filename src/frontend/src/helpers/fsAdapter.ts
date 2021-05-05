import { shell } from 'electron';
import { readdir, unlink, rename as rn } from 'fs-extra';
import { join } from 'path';

const handleFilesFromDirectory = (
  path: string,
  callback: (e: NodeJS.ErrnoException, files: string[]) => void
) => {
  readdir(path, callback);
};

const deleteFile = (path: string) => {
  unlink(path);
};

const rename = (path: string, newName: string) => {
  const containingFolder = path.substring(0, path.lastIndexOf('\\') + 1);
  rn(path, join(containingFolder, newName));
};

const open = (path: string) => {
  shell.openPath(path);
};

export { handleFilesFromDirectory, deleteFile, rename, open };
