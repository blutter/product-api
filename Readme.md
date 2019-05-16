# An Example WebApi Implementation for a Product API

# Building, Running and Tests

Install the NUnit 3 Test adapter for the tests to run in Visual Studio's Test Explorer

# Model Notes

The basic model is implemented in Product.cs. The API represents this as different request and response types to avoid leaky abstractions. This is more elaborate than necessary for this example but is not indicative of how I would implement a real-world API.

## Notes on Tests

* Controller/Service tests - my experience with CRUD based microservices is that there is often little benefit to writing unit tests for all levels and that end to end tests yield results faster. Nevertheless, I have added tests to demonstrate a BDD/TDD approach.
* Repository tests - often there is no means to unit test a repository as this can involve integration with some actual persistent storage. As an in memory storage is being used then a set of tests are only provided for completeness.
* Contract tests (e.g. Pact tests) - none have been implemented as this should be driven by a known consumer.
* API end to end tests - TBD

# A UI

An OpenApi interface is provided as a 'poor man's' UI.