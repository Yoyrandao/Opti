import React from 'react';
import {
  ContextMenu as Menu,
  ContextMenuTrigger,
  MenuItem,
} from 'react-contextmenu';
import { useAdapter } from '../../hooks/useAdapter';

import styles from './index.css';

interface ContextMenuProps {
  id: string;
  path: string;
  extension: string;
}

const ContextMenu = ({
  id,
  path,
  extension,
  children,
}: React.PropsWithChildren<ContextMenuProps>) => {
  const { deleteFile, open, rename } = useAdapter(path);

  return (
    <>
      <ContextMenuTrigger id={id}>{children}</ContextMenuTrigger>

      <Menu id={id} className={styles.contextMenu}>
        <MenuItem
          data={{ path }}
          onClick={open}
          className={styles.contextMenuItem}
        >
          Open
        </MenuItem>
        <MenuItem
          data={{ path }}
          onClick={() => rename(`a${extension}`)}
          className={styles.contextMenuItem}
        >
          Rename
        </MenuItem>
        <MenuItem
          data={{ path }}
          onClick={deleteFile}
          className={styles.contextMenuItem}
        >
          Delete
        </MenuItem>
      </Menu>
    </>
  );
};

export { ContextMenu, ContextMenuProps };
