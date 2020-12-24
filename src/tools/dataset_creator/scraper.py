from google_images_search import GoogleImagesSearch

search_engine = GoogleImagesSearch('AIzaSyAJfYLlqn31pcvottHNPQGnjhULSTj90tg', 'eb1b3a383f242e0ac')

_search_params = {
	'q': 'art',
	'num': 25,
	'fileType': 'jpg',
	'imgDominantColor': 'brown'
}

search_engine.search(search_params=_search_params)
i = 0
for image in search_engine.results():
	print(i, end=' ')
	try:
		image.download('./images/colors/')
		print(' image downloaded.')
	except:
		print(' downloading error')

	i += 1
