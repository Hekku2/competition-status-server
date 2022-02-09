docker-compose -f docker-compose.yml build

if ($? -eq $false) {
  exit
}

docker-compose --env-file .env.simulator -f docker-compose.yml up --renew-anon-volumes --abort-on-container-exit  console-client
