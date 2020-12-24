from os import listdir, rename, path
from uuid import uuid4

source_path = './sources/images' 

for file in listdir(source_path):
	rename(path.join(source_path, file), path.join(source_path, str(uuid4())) + path.splitext(file)[1])