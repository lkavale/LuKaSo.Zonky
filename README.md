# Lukaso Zonky API
### About the project
Main goal of this project is provide client library for <a href="http://www.zonky.cz">Zonky</a> investor API. 
Zonky provides documentation for their API and blueprint for automatic client code generation <a href="https://zonky.docs.apiary.io/#">here</a>, but when I tried generate client code by many tools the result was incomplete and contains many errors. After that I was decide to write my own client based on generated codes and watched network activity of Zonky site.
### Supported functionalities
* Log-in to investor user account
* Receive primary and secondary market loans
* Receive investments owned by investor
* Receive details of investor account (wallet, bank account, etc.)
* Buying loan participations on primary and secodary market
* Selling participations on secondary market
### Exclusion of liability
This library is only an example of possible implementation of API client. There is no warranty that Zonky doesn't change the public API or make some changes that could caused wrong function of this library. When you use this library you are exposed to the risk that the program can make some unwanted operations with your investor account. The author bears no responsibility for any damage incurred by using of this library.
