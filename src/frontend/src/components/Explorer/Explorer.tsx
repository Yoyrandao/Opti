import React from 'react';
import { useDirectory } from '../../hooks/useDirectory';
import { ContextMenu } from '../ContextMenu';
import { DirectoryItem } from '../DirectoryItem';

import styles from './index.css';

interface ExplorerProps {
  directory: string;
}

const Explorer: React.FC<ExplorerProps> = ({ directory }: ExplorerProps) => {
  const files = useDirectory(directory);

  return (
    <>
      <div className={styles.container}>
        {files?.map((x, i) => (
          <ContextMenu id={`${i}_context_item`} path={x.path} key={x.name}>
            <DirectoryItem info={x} />
          </ContextMenu>
        ))}
      </div>
    </>
  );
};

export { Explorer, ExplorerProps };
