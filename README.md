# Pokedex Api
A pokedex API that provides Pokémon information

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

To run this code on your machine you will need to have the following installed:
- Latest version of Docker, which can be downloaded [here](https://docs.docker.com/get-docker/)
- .Net Core 5, which can be downloaded [here](https://dotnet.microsoft.com/download)
- A web browser such as Chrome, Edge, Firefox, etc.

### Installing

#### Clone

- Clone this repo to your local machine using:
```shell
git clone https://github.com/armandombi/PokedexApi.git
```
#### Setup

- Once you have cloned the repository change into the downloaded project folder (PokedexApi) and run the following commands:
    - docker build -t pokedex_api:dev -f PokedexApi\Dockerfile .
    - docker run -d --name PokedexApi -p 5000:80 pokedex_api:dev

- Open a new browser tab and navigate to http://localhost:5000/swagger and the swagger API page should be displayed.
- Basic API documentation will be shown in this page and you can now start testing the API capabilities directly from the interface provided by swagger.

* You can also choose to run the project by opening the solution in Visual Studio 2019 and running the Docker profile from within, with debugging capabilities.

## Running the tests

- To run the test you will use .Net Core 5 so please verify this is working in your system by typing dotnet in any cmd window and this should be a recognized command
- From the main application folder (\PokedexApi) we change into the test project folder "PokedexApi.Tests" (Ex: cd PokedexApi.Tests)
- Execute "dotnet build" to build the test project
- Execute "dotnet test" to run all the tests

* You can also choose to run the tests from the project by opening the solution in Visual Studio 2019 and running the tests on the test explorer

## Limitations

* Simple implementation to demostrate correct functionality and use best coding practices
* Only a small amount of tests was included in the project
* The system has only been tested in a Windows environment running in docker with Linux containers

## Things to Improve for a live environment

- More meaninfull testing scenarios, integration test, etc
- Add https configuration on docker
- Add Authentication for the API endpoints
- Use docker-compose / Kubernetes for a larger implementation of interconnected services
- Add more fun endpoints (maybe a meme converter of some sort)
