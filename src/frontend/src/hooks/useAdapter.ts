import {
  deleteFile,
  open,
  rename,
  moveFile,
  getSize,
} from '../helpers/fsAdapter';

export const useAdapter = (path: string) => {
  return {
    deleteFile: () => deleteFile(path),
    open: () => open(path),
    rename: (newName: string) => rename(path, newName),
    move: (oldPath: string) => moveFile(oldPath, path),
    getSize: () => getSize(path),
  };
};
