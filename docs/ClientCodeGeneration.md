# Possible way to generate Zonky API client code

Official API blueprint is available [here](https://zonky.docs.apiary.io/).

* Install api-spec-converter  
```npm install -g api-spec-converter```

* Convert specification to Swagger 2 standard  
```api-spec-converter --from=api_blueprint --to=swagger_2 --syntax=json --order=openapi https://zonky.docs.apiary.io/api-description-document > swagger2api.json```

* Try to create code  
```autorest --input-file=swagger2api.json --csharp --namespace=Zonky.Api```

Repair possible errors in swagger definition file and try it again or use NSwagStudio for client code generation.<p>
But the API definition is not complete, some method is still missing and many properties is not covered.