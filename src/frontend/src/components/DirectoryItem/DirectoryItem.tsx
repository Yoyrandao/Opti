import React from 'react';
import { FileInfo } from '../../models/fileInfo';

import styles from './index.css';

interface DirectoryItemProps {
  info: FileInfo;
}

const DirectoryItem: React.FC<DirectoryItemProps> = ({
  info,
}: DirectoryItemProps) => {
  return (
    <div className={styles.itemContainer}>
      <div>{info.type}</div>
      <br />
      <br />
      <div>{info.name}</div>
    </div>
  );
};

export { DirectoryItem, DirectoryItemProps };
