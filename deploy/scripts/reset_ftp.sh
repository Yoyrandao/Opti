
FTP_ACCOUNT=$1
SERVICE_NAME=$2

echo 'recreating ftp fs...'
docker exec -it $SERVICE_NAME rm -rf /home/$FTP_ACCOUNT/ 
docker exec -it $SERVICE_NAME mkdir /home/$FTP_ACCOUNT/aaron
echo 'success.'