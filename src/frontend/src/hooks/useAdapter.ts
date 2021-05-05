import { deleteFile, open, rename } from '../helpers/fsAdapter';

export const useAdapter = (path: string) => {
  return {
    deleteFile: () => deleteFile(path),
    open: () => open(path),
    rename: (newName: string) => rename(path, newName),
  };
};
