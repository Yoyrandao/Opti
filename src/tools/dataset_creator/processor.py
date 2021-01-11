import joblib
import pandas as pd
import numpy as np

from sklearn.preprocessing import LabelEncoder
from os.path import getsize, splitext

from entropy import calc_file_entropy, calc_image_entropy

test_file = './test/1.jpg'

model = joblib.load('dtc_compression.sav')
encoder = LabelEncoder()
encoder.classes_ = np.load('encoder_classes.npy', allow_pickle=True)

_, ext = splitext(test_file)

size = getsize(test_file)
t = encoder.transform([ext.upper().replace('.', '')])
entropy = calc_image_entropy(test_file)

print(model.predict([[size, t, entropy]]))
