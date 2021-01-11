def calc_image_entropy(file_path):
	from PIL import Image
	import skimage.measure as measure
	import numpy as np
	

	img = Image.open(file_path)
	return measure.shannon_entropy(np.asarray(img))

def calc_file_entropy(file_path):
	import pandas as pd
	import scipy.stats as stats
	

	with open(file_path, 'rb') as sample:
		data = pd.Series(list(sample.read()))
		counts = data.value_counts()
		entropy = stats.entropy(counts)

		return entropy