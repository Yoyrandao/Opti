import json
import logging as log

from configparser import ConfigParser

from models import file_data_contract, checked_response, settings
from custom_exceptions import request_deserialization_exception, invalid_environment_exception

log.root.setLevel(log.DEBUG)


def get_config(environment: str) -> settings:
	""" Method for reading and initializing configuration """

	if environment not in ['local', 'prod']:
		raise invalid_environment_exception('\'{}\' is not valid environment name'.format(environment))

	parser = ConfigParser()
	parser.read('settings.{}.cfg'.format(environment))

	settings_model = settings()
	settings_model.port = int(parser['webconfig']['port'])
	settings_model.host = parser['webconfig']['host']

	return settings_model


def fill_file_data_contract(request_data: object) -> file_data_contract:
	""" Method which fills object with request model parameters """

	model = file_data_contract()
	try:
		model.file_name = request_data['fileName']
		model.size = request_data['fileSize']
		model.type = request_data['fileType']
		model.entropy = request_data['fileEntropy']

		log.info('Model deserialized')
		
		return model
	except request_deserialization_exception as error:
		log.error(error)


class checked_response_encoder(json.JSONEncoder):
	""" Helper class for serializing checked_response object """

	def default(self, obj):
		if isinstance(obj, checked_response):
			return obj.__dict__
		return json.JSONEncoder.default(self, obj)