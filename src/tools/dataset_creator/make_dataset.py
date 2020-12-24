from zlib import compress, decompress

from os import listdir, remove
from os.path import join, getsize, splitext

import numpy as np
import pandas as pd


sources_path = '.\sources\images'


def calc_entropy(file_path):
	from PIL import Image
	import skimage.measure as measure
	

	img = Image.open(file_path)
	return measure.shannon_entropy(np.asarray(img))


columns=['size', 'type', 'entropy', 'need_compression']
data = []

for file in listdir(sources_path):
	file = join(sources_path, file)
	file_, file_extension = splitext(file)
	compressed_file = file_ + '.compressed' + file_extension

	entropy = calc_entropy(file)

	with open(file, 'rb') as sample:
		with open(file_ + '.compressed' + file_extension, 'wb+') as compressed:
			compressed.write(compress(sample.read(), 5))
	
	old_size = getsize(file)
	compressed_size = getsize(compressed_file)
	ratio = old_size / compressed_size

	compress_flag = True
	if ratio < 1.0:
		compress_flag = False
	elif ratio - 1.0 < .15:
		compress_flag = False

	data.append([old_size, file_extension.upper().replace('.', ''), entropy, int(compress_flag)])

	print('%s; OLD: %d; NEW: %d; ENTROPY: %f; RATIO: %f; COMPRESS?: %d' % (file, old_size, compressed_size, entropy, ratio, compress_flag))
	remove(compressed_file)

df = pd.DataFrame(np.array(data), columns=columns)
df.to_csv('./dataset.csv', index=False)

print('Analysis done.')