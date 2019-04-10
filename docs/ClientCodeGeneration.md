# Possible way to generate Zonky API client code

Official API blueprint is available on https://zonky.docs.apiary.io/.

* Install api-spec-converter<br/>
<code>npm install -g api-spec-converter</code>

* Convert specification to Swagger 2 standard<br/>
<code>api-spec-converter --from=api_blueprint --to=swagger_2 --syntax=json --order=openapi https://zonky.docs.apiary.io/api-description-document > swagger2api.json</code>

* Try to create code<br/>
<code>autorest --input-file=swagger2api.json --csharp --namespace=Zonky.Api</code>

Repair possible errors in swagger definition file and try it again or zou can use NSwagStudio for client code generation.