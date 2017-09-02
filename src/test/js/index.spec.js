const chai = require('chai')
const chaiAsPromised = require('chai-as-promised')
const path = require('path')

chai.Should()
chai.use(chaiAsPromised)

chaiAsPromised.transferPromiseness = browser.transferPromiseness

const timeout = 1800000

describe('Aircraft Noise Recorder Home Page', function () {
    this.timeout(timeout)

    before(function () {
    })

    beforeEach(function () {
    })

    afterEach(function () {
    })

    it('shall display the version number in the footer', function () {
        return browser
            .url('/')
            .waitForVisible('#version', 1000, false)
            .getText('#version')
            .should.eventually.match(/^\d+\.\d+\.\d+$/)
    })
})
