###
# Aircraft Noise Recorder
#
# To build:
#   docker build -t boos/aircraft-noise-recorder .
# To run:
#   docker run -t -i -p 8080:8080 boos/aircraft-noise-recorder
###

# Extend vert.x image
FROM vertx/vertx3

ENV VERTICLE_NAME io.github.wonderbird.aircraftnoise.recorder.App
ENV VERTICLE_FILE target/aircraft-noise-1.0-SNAPSHOT-fat.jar

# Set the location of the verticles
ENV VERTICLE_HOME /usr/verticles

EXPOSE 8080

# Copy your verticle to the container
COPY $VERTICLE_FILE $VERTICLE_HOME/

# Launch the verticle
WORKDIR $VERTICLE_HOME
ENTRYPOINT ["sh", "-c"]
CMD ["exec vertx run $VERTICLE_NAME -cp $VERTICLE_HOME/*"]