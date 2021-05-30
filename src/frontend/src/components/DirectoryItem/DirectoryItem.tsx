/* eslint-disable jsx-a11y/no-static-element-interactions */
/* eslint-disable jsx-a11y/click-events-have-key-events */
import React from 'react';
import { FileInfo } from '../../models/fileInfo';

import excelIcon from '../../../assets/types/excel.png';
import docIcon from '../../../assets/types/docx.png';
import textIcon from '../../../assets/types/text.png';
import unknownIcon from '../../../assets/types/unknown.png';
import { useAdapter } from '../../hooks/useAdapter';

interface DirectoryItemProps {
  info: FileInfo;
}

const DirectoryItem: React.FC<DirectoryItemProps> = ({
  info,
}: DirectoryItemProps) => {
  const { open, getSize } = useAdapter(info.path);

  const resolveItemContent = (fileInfo: FileInfo) => {
    switch (fileInfo.type) {
      case 'image':
        return <img className="itemImage" src={info.path} alt={info.name} />;
      case 'excel':
        return <img className="itemImage" src={excelIcon} alt={info.name} />;
      case 'doc':
        return <img className="itemImage" src={docIcon} alt={info.name} />;
      case 'text':
        return <img className="itemImage" src={textIcon} alt={info.name} />;
      default:
        return <img className="itemImage" src={unknownIcon} alt={info.name} />;
    }
  };

  return (
    <>
      <div className="itemContainer" onDoubleClick={open}>
        <div className="itemContent">{resolveItemContent(info)}</div>
        <div className="itemContentContainer">
          <div className="itemContentWithMargin">{info.name}</div>
          <div className="itemContentWithMargin">{`${Math.floor(
            getSize() / 1024
          )} КБ`}</div>
        </div>
      </div>
      <hr />
    </>
  );
};

export { DirectoryItem, DirectoryItemProps };
