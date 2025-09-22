describe("Aircraft Noise App", () => {
  it("locates next measurement station", () => {
    cy.visit("/");

    cy.log("Record noise event outside measurement data range");
    const now = Date.UTC(2025, 8, 1, 13, 5, 3, 0);
    cy.clock(now);
    cy.get('[data-testid="record-button"]').click();
    cy.get('[data-testid="events"] li:first-child').should(
      "have.text",
      "9/1/2025, 3:05:03 PM",
    );

    cy.log("Move event timestamp");
    cy.get('[data-testid="move-events-button"]').click();
    cy.get('[data-testid="events"] li:first-child').should(
      "have.text",
      "4/9/2025, 1:05:03 AM",
    );
  });
});
