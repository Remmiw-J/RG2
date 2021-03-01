docker build -f ".\RG2.React\Dockerfile" . -t r-g2
docker tag r-g2 r-g2.azurecr.io/signum/r-g2
az acr login --name r-g2
docker push r-g2.azurecr.io/signum/r-g2
az webapp restart --name r-g2-webapp --resource-group r-g2-resourceGroup
