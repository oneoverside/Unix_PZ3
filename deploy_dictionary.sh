minikube stop

# shellcheck disable=SC2164
cd "Unix-PZ-3-Main-Application"
docker build -t dictionary .
docker tag dictionary 62136/dictionary
docker push 62136/dictionary
cd ..

cd "Unix-PZ-3-Logger-Application\LogAnalizer"
docker build -t loganalizator .
docker tag loganalizator 62136/loganalizator
docker push 62136/loganalizator
cd ..

minikube start
