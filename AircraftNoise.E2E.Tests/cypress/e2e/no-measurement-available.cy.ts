describe("Aircraft Noise App", () => {
  it("warns if measurement data not ready", () => {
    cy.visit("/");

    cy.log(
      "Record noise event before Apr. 9 00:00 AM - 02:00 AM CET, the range of our measurement data",
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
});
