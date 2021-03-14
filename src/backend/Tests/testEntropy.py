def calc_file_entropy(file_path):
	import pandas as pd
	import scipy.stats as stats
	

	with open(file_path, 'rb') as sample:
		data = pd.Series(list(sample.read()))
		counts = data.value_counts()
		entropy = stats.entropy(counts)

		return entropy


print(calc_file_entropy('./testfile.txt'))