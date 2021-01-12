FROM python:3.8-slim-buster

COPY . /app
WORKDIR /app

RUN python3 -m pip install pipenv
RUN pipenv lock --requirements > requirements.txt
RUN python3 -m pip install -r requirements.txt

ENTRYPOINT [ "python3" ]
CMD [ "/app/main.py", "prod" ]
