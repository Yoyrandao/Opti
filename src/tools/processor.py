import joblib
import pandas as pd

from sklearn.preprocessing import LabelEncoder
from os.path import getsize

test_image = './test/2.jpg'

def calc_entropy(file_path):
	from PIL import Image
	import skimage.measure as measure
	import numpy as np
	

	img = Image.open(file_path)
	return measure.shannon_entropy(np.asarray(img))


model = joblib.load('dtc_compression.sav')


size = getsize(test_image)
t = 0
entropy = calc_entropy(test_image)

print(model.predict([[size, t, entropy]]))
