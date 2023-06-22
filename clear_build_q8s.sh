. deploy_dictionary.sh

cd ..
cd "Unix-PZ-3-K8s"
# Clearing
kubectl delete ReplicaSets --all
kubectl delete statefulsets --all
kubectl delete deployments --all
#kubectl delete persistentvolumeclaims --all
#kubectl delete persistentvolumes --all
kubectl delete jobs --all
kubectl delete services --all
kubectl delete pods --all

# Building
kubectl apply -f redis.yaml
kubectl apply -f postgres.yaml
kubectl apply -f dictionary.yaml
kubectl apply -f init-db-job.yaml
kubectl apply -f data-updater.yaml
kubectl apply -f loganalizer.yaml

sleep 100