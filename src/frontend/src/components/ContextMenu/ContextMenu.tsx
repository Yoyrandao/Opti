import React from 'react';
import {
  ContextMenu as Menu,
  ContextMenuTrigger,
  MenuItem,
} from 'react-contextmenu';
import { useAdapter } from '../../hooks/useAdapter';

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
    <div style={{ width: '80%', height: '60px' }}>
      <ContextMenuTrigger id={id}>{children}</ContextMenuTrigger>

      <Menu id={id} className="contextMenu">
        <MenuItem data={{ path }} onClick={open} className="contextMenuItem">
          Открыть
        </MenuItem>
        <MenuItem
          data={{ path }}
          onClick={() => rename(`a${extension}`)}
          className="contextMenuItem"
        >
          Переименовать
        </MenuItem>
        <MenuItem
          data={{ path }}
          onClick={deleteFile}
          className="contextMenuItem"
        >
          Удалить
        </MenuItem>
      </Menu>
    </div>
  );
};

export { ContextMenu, ContextMenuProps };
