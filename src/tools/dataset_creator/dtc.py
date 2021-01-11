import pandas as pd
import numpy as np
import matplotlib.pyplot as plt

import joblib

from sklearn.preprocessing import LabelEncoder
from sklearn import tree

columns=['size', 'type', 'entropy', 'need_compression']

#-------------


ds = pd.read_csv('dataset.csv')

inputs = ds.drop('need_compression', axis='columns')
target = ds['need_compression']

type_encoder = LabelEncoder()

inputs['type'] = type_encoder.fit_transform(inputs['type'])
np.save('encoder_classes.npy', type_encoder.classes_)


#----------------

model = tree.DecisionTreeClassifier(criterion='gini')
model.fit(inputs, target)

print('Score: %f' % model.score(inputs, target))

plt.figure(figsize=(100,100))
tree.plot_tree(model, feature_names=columns, class_names=['Not compressed', 'Compressed'], filled=True)
plt.savefig('tree.png', format='png', dpi=100)

print('Tree plotted.')

model_filename = 'dtc_compression.sav'
joblib.dump(model, model_filename)