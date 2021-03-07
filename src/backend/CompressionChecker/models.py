class file_data_contract:
	""" Request model for primary controller """

	def __init__(self):
		self.file_name = ''
		self.size 		 = 0
		self.type 		 = ''
		self.entropy   = 0


class checked_response:
	""" Response model for primary controller """

	def __init__(self, _name: str, _decision: bool):
		self.name     = _name
		self.decision = _decision


class settings:
	""" Settings model """

	def __init__(self):
		self.port = 0
		self.host = ''
		self.name = ''
		self.version = ''