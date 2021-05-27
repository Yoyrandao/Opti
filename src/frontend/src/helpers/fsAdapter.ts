import { shell } from 'electron';
import { readdir, unlink, rename as rn, move, statSync } from 'fs-extra';
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

const moveFile = (oldPath: string, folder: string) => {
  const filename = oldPath.replace(/^.*[\\/]/, '');
  move(oldPath, join(folder, filename));
};

const getSize = (path: string): number => {
  return statSync(path).size;
};

export {
  handleFilesFromDirectory,
  deleteFile,
  rename,
  open,
  moveFile,
  getSize,
};
