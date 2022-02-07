docker-compose -f docker-compose.yml build

if ($? -eq $false) {
  exit
}

docker-compose -f docker-compose.yml up --renew-anon-volumes --abort-on-container-exit test
