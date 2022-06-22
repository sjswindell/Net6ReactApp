var React = require('react');
var expect = require('expect');
var Root = require('./test');
var TestUtils = require('react/lib/ReactTestUtils');

describe('root', function () {
	it('renders without problems', function () {
		expect(TestUtils.renderIntoDocument(<Root />)).toExist();
	});
});