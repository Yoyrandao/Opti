/* eslint-disable react/jsx-props-no-spreading */
import React, { useCallback } from 'react';
import { useDropzone } from 'react-dropzone';
import { useAdapter } from '../../hooks/useAdapter';
import { useDirectory } from '../../hooks/useDirectory';
import { ContextMenu } from '../ContextMenu';
import { DirectoryItem } from '../DirectoryItem';

import styles from './index.css';

interface ExplorerProps {
  directory: string;
}

const Explorer: React.FC<ExplorerProps> = ({ directory }: ExplorerProps) => {
  const files = useDirectory(directory);
  const { move } = useAdapter(directory);
  const onDrop = useCallback(
    (acceptedFiles: File[]) => {
      acceptedFiles.forEach((item) => {
        move(item.path);
      });
    },
    [move]
  );
  const { getRootProps, getInputProps, isDragActive } = useDropzone({
    onDrop,
    noClick: true,
  });

  return (
    <>
      <div
        className={isDragActive ? styles.containerBlurred : styles.container}
        {...getRootProps()}
      >
        <input {...getInputProps()} />
        {files?.map((x, i) => (
          <ContextMenu
            id={`${i}_context_item`}
            path={x.path}
            extension={x.extension}
            key={x.name}
          >
            <DirectoryItem info={x} />
          </ContextMenu>
        ))}
      </div>
    </>
  );
};

export { Explorer, ExplorerProps };
