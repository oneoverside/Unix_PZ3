# shellcheck disable=SC2164
cd "C:\Users\oneov\RiderProjects\ProjectWithDeployment\Project\services\dictionary"
docker build -t dictionary .
docker tag dictionary 62136/dictionary
docker push 62136/dictionary