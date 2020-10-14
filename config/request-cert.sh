openssl req -x509 -keyout ./docker-poc-cert.key -out ./docker-poc-cert.crt -nodes -config ./docker-poc-cert.req  &&
openssl pkcs12 -export -in docker-poc-cert.crt -inkey docker-poc-cert.key -out docker-poc-cert.pfx -passout pass:selfsigned-pwd
rm ./docker-poc-cert.key ./docker-poc-cert.crt