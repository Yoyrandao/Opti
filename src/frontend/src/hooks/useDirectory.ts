import { watch } from 'fs-extra';
import { join, extname } from 'path';
import { useEffect, useState } from 'react';
import { error } from 'electron-log';
import { FileInfo, FileType } from '../models/fileInfo';
import { handleFilesFromDirectory } from '../helpers/fsAdapter';

export const useDirectory = (dir: string) => {
  const [state, setState] = useState<FileInfo[]>([]);

  const resolveFileType = (extension: string): FileType => {
    switch (extension) {
      case '.txt':
        return 'text';

      case '.doc':
      case '.docx':
        return 'doc';

      case '.xls':
      case '.xlsx':
        return 'excel';

      case '.png':
      case '.jpg':
      case '.jpeg':
        return 'image';

      default:
        return 'file';
    }
  };

  const getFileInfo = (item: string): FileInfo => {
    const extension = extname(item);

    return {
      extension: extension.toLowerCase(),
      name: item,
      path: join(dir, item),
      type: resolveFileType(extension.toLowerCase()),
    };
  };

  const updateDirectoryInfo = () => {
    handleFilesFromDirectory(dir, (err, files) => {
      if (err) {
        setState([]);
        error('cannot read directory');
        return;
      }

      setState(files.map((x) => ({ ...getFileInfo(x) })));
    });
  };

  // eslint-disable-next-line react-hooks/exhaustive-deps
  useEffect(() => updateDirectoryInfo(), [dir]);

  watch(dir, () => updateDirectoryInfo());
  return state;
};
