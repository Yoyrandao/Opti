import json
import joblib
import argparse
import configparser

import numpy as np
import logging as log

from flask import Flask, Response, request
from sklearn.preprocessing import LabelEncoder

from models import checked_response, file_data_contract
from helpers import fill_file_data_contract, checked_response_encoder, get_config


""" Additional configuration """

log.root.setLevel(log.DEBUG)

arg_parser = argparse.ArgumentParser()
arg_parser.add_argument('environment', metavar='env', type=str, help='and environment identifier')
args = arg_parser.parse_args()

config = get_config(args.environment)


""" Main section """

_app = Flask(__name__)

@_app.route('/check', methods=['POST'])
def confirm_check_request():
	""" Primary controller """

	try:
		request_data = request.get_json()
		contract = fill_file_data_contract(request_data)

		model = joblib.load('./data/dtc_compression.sav')
		
		encoder = LabelEncoder()
		encoder.classes_ = np.load('./data/encoder_classes.npy', allow_pickle=True)
		contract.type = encoder.transform([contract.type.upper().replace('.', '')])

		decision = model.predict([[contract.size, contract.type, contract.entropy]])[0]

		response = checked_response(contract.file_name, bool(decision))
		json_response = json.dumps(response, cls=checked_response_encoder)

		return Response(json_response, status=200, mimetype='application/json')
	except Exception as error:
		return Response(error, status=500, mimetype='application/json')

if __name__ == '__main__':
	log.info('Started CompressionChecker at {}'.format(config.port))
	_app.run(debug=True if args.environment == 'local' else False, host=config.host, port=config.port)