$cert = New-SelfSignedCertificate -FriendlyName DockerPoc -DnsName localhost, host.docker.internal, docker-poc-web, docker-poc-api, docker-poc-oauth, docker-poc-mssql, docker-poc-rabbitmq -CertStoreLocation cert:\LocalMachine\My
$thumbprint = $cert.Thumbprint
$password = ConvertTo-SecureString -String "selfsigned-pwd" -Force -AsPlainText
Export-PfxCertificate -Cert cert:\LocalMachine\My\$thumbprint -FilePath .\docker-poc-cert.pfx -Password $password