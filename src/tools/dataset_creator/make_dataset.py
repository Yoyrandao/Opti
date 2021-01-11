from zlib import compress, decompress

from os import listdir, remove
from os.path import join, getsize, splitext

from entropy import calc_file_entropy, calc_image_entropy

import numpy as np
import pandas as pd


sources_paths = ['.\\sources\\images', '.\\sources\\docs', '.\\sources\\text', '.\\sources\\random']
columns=['size', 'type', 'entropy', 'need_compression']
data = []

for sources_path in sources_paths:
	for file in listdir(sources_path):
		file = join(sources_path, file)
		file_, file_extension = splitext(file)
		compressed_file = file_ + '.compressed' + file_extension
		entropy = 0

		if file_extension == '.jpg' or file_extension == '.png':
			entropy = calc_image_entropy(file)
		else:
			entropy = calc_file_entropy(file)

		with open(file, 'rb') as sample:
			with open(file_ + '.compressed' + file_extension, 'wb+') as compressed:
				compressed.write(compress(sample.read(), 5))
		
		old_size = getsize(file)
		compressed_size = getsize(compressed_file)
		ratio = old_size / compressed_size

		compress_flag = True
		if ratio - 1.0 < .1:
			compress_flag = False

		data.append([old_size, file_extension.upper().replace('.', ''), entropy, int(compress_flag)])

		print('%s; OLD: %d; NEW: %d; ENTROPY: %f; RATIO: %f; COMPRESS?: %d' % (file, old_size, compressed_size, entropy, ratio, compress_flag))
		remove(compressed_file)

df = pd.DataFrame(np.array(data), columns=columns)
df.to_csv('./dataset.csv', index=False)

print('Analysis done.')