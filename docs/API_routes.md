# Primavera API for the Online BookShop Web App

The API between Primavera and our Web app will be a REST API whose content will be serialized C# objects, given that both Primavera and our Web app, written in ASP.NET, use this language.

## 1. API for user-related functionalities

|ID|Method|Route|Description|Body|
|---|---|---|---|---|
101|POST|/register|Register a new user|n/a|
102|POST|/login|Login an user|n/a|
103|GET|/user/{id}|Get the profile of an user|n/a|
104|POST|/user/{id}/edit|Edit the profile of an user|n/a|

## 2. API for books

|ID|Method|Route|Description|Body|
|---|---|---|---|---|
201|GET|/book/{id}|Gets all info about a book|n/a|
202|GET|/categories/{category}|Gets all books from a given category|n/a|
203|POST|/search|Get all books that match the specified parameters|n/a|

## 3. API for purchases

|ID|Method|Route|Description|Body|
|---|---|---|---|---|
301|PUT|/user/{user_id}/cart/{book_id}|Adds a book to the user's cart| n/a|
302|DELETE|/user/{user_id}/cart/{book_id}|Removes a book from the user's cart| n/a|
303|GET|/user/{id}/cart|Gets all the items on the user's cart| n/a|
304|PUT|/user/{id}/purchase|Finalizes the purchase of the current books in the cart|n/a|

## 4. API for past sales information

|ID|Method|Route|Description|Body|
|---|---|---|---|---|
401|GET|/user/{id}/history|Gets all the sales history of the user|n/a|
