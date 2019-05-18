# An Example WebApi Implementation for a Product API

# Overview

The API is implemented in the `RestExampleApi` project with the `RestExample` project containing the core service and repository.

Unit tests are in `RestExampleApi.Tests` and `RestExample.Tests`.

The API is implemented using WebApi2 as per the instructions instead of ASP.NET Core. Please note that I have not used WebApi2 for a couple of years as my recent development experience has been using Dotnet Core.

# Building, Running and Tests

Build using Visual Studio 2019/2017.

e.g.

    nuget restore RestExample.sln
    devenv RestExample.sln /Build

Install the NUnit 3 Test adapter for the tests to run in Visual Studio's Test Explorer.

# Model Notes

The internal product model is in the class `ProductEntity` this is mapped to/from `Product` at the service. At the API, `Product*Request` and `Product*Response` are used to avoid leaking the internal abstractions.

# Persistence store

A ConcurrentDictionary is used for the persistent store for the sake of this exercise.

Note: update operations are not atomic as the Microsoft documentation states:

    `updateValueFactory` delegates are called outside the locks to avoid the problems that can arise from executing unknown code under a lock. Therefore, AddOrUpdate is not atomic with regards to all other operations on the ConcurrentDictionary<TKey,TValue> class.

## Notes on Tests

* Controller/Service tests - my experience with CRUD based microservices is that there is often little benefit to writing unit tests for all levels and that end to end tests yield results faster. Nevertheless, I have added tests to demonstrate a BDD/TDD approach.
* Repository tests - often there is no means to unit test a repository as this can involve integration with some actual persistent storage. As an in memory storage is being used then a set of tests are only provided for completeness.
* Contract tests (e.g. Pact tests) - none have been implemented as this should be driven by a known consumer.
* API end to end tests - Not implemented due to time constraints but something like https://github.com/cucumber/cucumber-ruby could be used
* Integration tests - Not implemented due to time constraints but I often use end to end tests instead when building microservices.

# Filtering

Filtering is implemented on the `GET products` endpoint.

e.g. `GET products?description=super`

# A UI

An OpenApi interface is provided as a 'poor man's' UI. This is accessible via the `/swagger` route.

# Authentication/Authorization

The write operations (PUT, POST and DELETE) are only authorized to a user `jb`.

This user can authenticate (with password `hifi`) using basic authentication. i.e. the header:

    Authorization:Basic amI6aGlmaQ==

