# Google Cloud Management Files

The following list gives a quick overview on how to use the files in this
directory:

1. **init** selects the current project for the following `gcloud`
   commands
2. **up** starts up the kubernetes cluster running the docker image
3. **build** creates a docker image from the current fat jar
4. **push** transfers the docker image to Google Cloud and updates the
   running cluster
5. **down** shuts down the Google Cloud cluster, i.e. deletes the load
   balancer, the pods and the cluster
   