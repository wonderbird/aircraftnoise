describe("Aircraft Noise App", () => {
  const roesrathForsbach = { latitude: 50.92381, longitude: 7.18231 };

  it("locates next measurement station", () => {
    cy.visit("/", fakeLocation(roesrathForsbach));

    cy.log("Get next measurement station");
    cy.get('[data-testid="locate-button"]').click();
    cy.get('[data-testid="next-measurement-station-info"]')
      .should("be.visible")
      .and("contain", "Rösrath-Forsbach");

    cy.log(
      "Record noise event between Apr. 9 00:00 AM - 02:00 AM CET, the range of our measurement data",
    );
    const now = Date.UTC(2025, 3, 8, 21, 0, 0, 0);
    cy.clock(now);
    cy.get('[data-testid="record-button"]').click();
    cy.get('[data-testid="events"] li:first-child').should(
      "have.text",
      "4/8/2025, 11:00:00 PM",
    );

    cy.log("Query noise level");
    cy.get('[data-testid="get-noise-button"]').click();
    cy.get('[data-testid="missing-measurement-data-warning"]')
      .should("be.visible")
      .and("contain", "keine Messdaten");
  });

  // Replace the browser location function by a stub allowing to configure fake locations.
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
