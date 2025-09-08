describe("Aircraft Noise App", () => {
  const roesrathForsbach = { latitude: 50.92381, longitude: 7.18231 };

  it("locates next measurement station", () => {
    cy.visit("/", fakeLocation(roesrathForsbach));

    cy.get('[data-testid="locate-button"]').click();

    cy.get('[data-testid="next-measurement-station-info"]')
      .should("be.visible")
      .and("contain", "Rösrath-Forsbach");
  });

  // https://www.browserstack.com/guide/cypress-geolocation-testing
  function fakeLocation(coords: { latitude: number; longitude: number }) {
    return {
      onBeforeLoad(win) {
        cy.stub(win.navigator.geolocation, "getCurrentPosition").callsFake(
          (cb, err) => {
            if (coords && coords.latitude && coords.longitude) {
              return cb({ coords });
            }

            throw err({
              message:
                "You made a mistake when configuring the `fakeLocation` function. Probably your parameters are incorrect.",
            });
          },
        );
      },
    };
  }
});
