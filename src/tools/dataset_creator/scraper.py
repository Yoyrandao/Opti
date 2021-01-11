from google_images_search import GoogleImagesSearch
from uuid import uuid4
import os

def my_progressbar(url, progress):
    print(url + ' ' + str(progress) + '%')

search_engine = GoogleImagesSearch('AIzaSyAJfYLlqn31pcvottHNPQGnjhULSTj90tg', 'eb1b3a383f242e0ac', progressbar_fn=my_progressbar)
_format = 'png'

_search_params = {
	'q': 'minimalism art',
	'num': 25,
	'fileType': _format,
	# 'imgSize': 'MEDIUM',
	'imgDominantColor': 'green'
}

search_engine.search(search_params=_search_params)
i = 0
for image in search_engine.results():
	print(i, end=' ')
	try:
		image.download('./images/onetone/')
		dir = os.path.dirname(image.path)
		pre, ext = os.path.splitext(image.path)
		
		os.rename(image.path, dir + '/' + str(uuid4()) + '.' + _format)
	except BaseException as e:
		print(' downloading error')
		print(e)

	i += 1
