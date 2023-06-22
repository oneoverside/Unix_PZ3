cd "Unix-PZ-3-K8s"
kubectl apply -f redis.yaml
kubectl apply -f postgres.yaml
kubectl apply -f dictionary.yaml
kubectl apply -f init-db-job.yaml
kubectl apply -f data-updater.yaml
kubectl apply -f loganalizer.yaml

sleep 5