# Google Cloud Management Files

The following list gives a quick overview on how to use the files in this
directory:

1. **init** selects the current project for the following `gcloud`
   commands
2. **start-cluster** starts up the container cluster and runs the docker
   image
3. **start-loadbalancer** exposes the docker image to the world (**costs
   money**)
4. **build** creates a docker image from the current fat jar
5. **push** transfers the docker image to Google Cloud and updates the
   running cluster
6. **stop-loadbalancer** removes the outbound ip address (**saves
   money**)
7. **stop-cluster** shuts down the Google Cloud cluster, i.e. deletes
   the pods and the cluster
