type FileType = 'image' | 'text' | 'doc' | 'excel' | 'file';

interface FileInfo {
  type: FileType;
  path: string;
  name: string;
  extension: string;
}

export { FileInfo, FileType };
