# Introduction

- An API "SampleAPP" is provided with operations over a simple data model with two entities 1...N.
- The API is structured using a layered architecture.
- The solution has two JSONs included used to seed the entity framework context (works InMemory).
- All the functionalities are defined in the Controllers layer and it uses an ISampleService interface with the definition of all the functionalities to be implemented.

# Requirements

## Generic

These are generic requirements that apply to all the requests in the test:

- No internal id can be retrieved through the API. You will have to find a way to identify the Samples and SubSamples without exposing it's true Id.
- All the requests made to the API should be logged in a disk folder (headers, body and client IP). The folder location must be retrieved from the appsettings.json.

## Implement ISampleService

Create an implementation of the interface ISampleService with all it's business logic. The Infrastructure layer already provides the entity framework context (filled with data) but you will have to extract the data the way you see fit.

## Validations

The Create and Update functionalities must implement validations of the request:

- Sample Name and Id are mandatory.
- Sample Name has a max length of 32 chars.
- SubSample Id is mandatory.
- SubSample Info has a max length of 128 chars.
- You can implement any other more "business related" validations as you see fit.

These validations must be implemented using FluentValidations.

## GetAllAsync

Apart from the expected logic described in the controller, the number of rows retrieved must be limited based on a configuration key. 
*For example: you can limit a request to the last 1000 rows based on creation date.*

- No entendí muy bien esta clausula, por lo tanto implementé con LINQ la selección de las ultimas 1000 filas ordenadas por el campo Created.

## New Functionality

New endpoint to retrieve the SubSamples and it's Sample data flattened. Additionally:

- The request should allow to filter the results based on a "Sample"."Created" range. 
- The request must implement a pagination. The number of rows per page must come from the request.

Expected response structure:

```JSON
[
    {
        "SampleId": "",
        "SampleName": "",
        "SampleCreated": "",
        "SubSampleId": "",
        "SubSampleInfo": ""
    }
]
```

Implement validations as you see fit.

## Unit Test

Pick one of the functionalities provided by the API and create a unit test to cover it.

# Questions over the API

- Why do you think the dependency injection is implemented in each layer and not only in the Api?

- Respuesta: Porque hay que inyectar instancias, como por ejemplo la instancia del contexto de base de datos desde el proyecto de infraestructura, una instancia del mapper en la capa
de aplicación para que sea utilizada en el SampleAppService y mapear entre DTOs y Entidades, o la instancia del SampleAppService en la API.

- Do you think there is anything that can be improved in the SamplesController?

- Respuesta: Creo que el controlador está bien construido porque:
    - No hay acceso a datos desde el controlador. 
    - No hay mapeo de datos.
    - No hay lógica de negocio.
    - Se utiliza inyección de dependencias para inyectar el servicio de la capa de aplicación.
    - Estan bien definidas las acciones de una APIRestful.

Lo que se podría implementar es un contexto de autorización, en donde la Api tenga que recibir un token de autenticación para poder ejecutar el request.

*These questions can be answered in Spanish*.

# Requirements summary
To help you keep track of it:
- Do not expose any internal Id.
- Log every request.
- ISampleService implementation.
- Validations Create method.
- Validations Update method.
- Limit to GetAllAsync
- New functionality with flattened data.
- Create one unit test.
- Answer the questions
