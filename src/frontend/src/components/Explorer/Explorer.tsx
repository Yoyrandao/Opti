import React from 'react';
import { useDirectory } from '../../hooks/useDirectory';
import { DirectoryItem } from '../DirectoryItem';

import styles from './index.css';

interface ExplorerProps {
  directory: string;
}

const Explorer: React.FC<ExplorerProps> = ({ directory }: ExplorerProps) => {
  const data = useDirectory(directory);

  return (
    <div className={styles.container}>
      {data?.map((x) => (
        <DirectoryItem info={x} key={x.name} />
      ))}
    </div>
  );
};

export { Explorer, ExplorerProps };
