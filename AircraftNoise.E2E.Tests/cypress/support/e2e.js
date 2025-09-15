// ***********************************************************
// This example support/e2e.js is processed and
// loaded automatically before your test files.
//
// This is a great place to put global configuration and
// behavior that modifies Cypress.
//
// You can change the location of this file or turn off
// automatically serving support files with the
// 'supportFile' configuration option.
//
// You can read more here:
// https://on.cypress.io/configuration
// ***********************************************************

// Import commands.js using ES2015 syntax:
import "./commands";

// Timestamp related tests are run for German timezone
// This configuration affects both the developer's computer and the GitHub pipeline
// https://github.com/cypress-io/cypress/issues/1043#issuecomment-2089006560
Cypress.on("test:before:run", () => {
  Cypress.automation("remote:debugger:protocol", {
    command: "Emulation.setTimezoneOverride",
    params: {
      timezoneId: "Europe/Berlin",
    },
  });
});
