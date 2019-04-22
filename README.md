# Lukaso Zonky API
### About the project
Main goal of this project is provide client library for [Zonky](http://www.zonky.cz) investor's API. 
Zonky provides documentation for their API and blueprint for automatic client code generation [here](https://zonky.docs.apiary.io/#), but 
it is still incomplete and contains some errors, undocumented methods and properties. After that I was decide to write my own client based on generated codes and watched network activity of Zonky site with implementation of methods and functionalities that I need.
### Supported functionalities
* Log-in to investor user account
* Receive primary and secondary market loans
* Receive investments owned by investor
* Receive details of investor account (wallet, bank account, etc.)
* Buying loan participations on primary and secodary market
* Selling participations on secondary market
### Usage
* Create ZonkyClient and pass account informations  
```var client = new ZonkyClient("user", "password", false); // last parameter enable trading```
* Get uncovered primary marketplace loans  
```var loans = await client.GetAllUncoveredPrimaryMarketPlaceAsync();```
* Get all investors participations  
```var participations = await client.GetAllInvestmentsAsync();```
### API and client class
Library contains ZonkyApi class that contains only API methods for communication with Zonky with no additional functionalities.
ZonkyClient implements functionalities for user authorization, keeping the session and other top level functionalies.
### Tests
Tests contains some unit tests for auxilarity classes and integration tests with Zonky Apiary mock test server and Zonky production server.
Apiary mock server tests doesn't provide a proof test results and cannot offer bigger test coverage. Some methods are completely missing, some methods returns only a blank responses and some methods returns only a small amount of test data. Testing for non-standard behavior is not possible, mock server always returns possitive responses even for wrong requests.
Production server tests provide better test coverage but the responces cannot be predicted, requires investor's account and trading methods cannot be tested without making real trades (tests have no trading capatibilities).
### Exclusion of liability
This library is only an example of possible implementation of API client. There is no warranty that Zonky doesn't change the public API or make some changes that could caused wrong function of this library. When you use this library you are exposed to the risk that the program can make some unwanted operations with your investor account. The author bears no responsibility for any damage incurred by using of this library.
