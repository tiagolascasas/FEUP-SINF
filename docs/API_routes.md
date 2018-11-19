# Primavera API for the Online BookShop Web App

The API between Primavera and our Web app will be a REST API whose return values will be either HTTP codes or JSON strings.

<!--template
<table>
<tr>
  <th>ID</th>
  <th>Method</th>
  <th>Route</th>
  <th>Description</th>
  <th>Body</th>
  <th>Return</th>
</tr>
<tr>
  <td></td>
  <td></td>
  <td></td>
  <td></td>
  <td></td>
  <td>
  </td>
</tr>
</table>
-->

## 1. API for user-related functionalities

<table>
<tr>
  <th>ID</th>
  <th>Method</th>
  <th>Route</th>
  <th>Description</th>
  <th>Body</th>
  <th>Return</th>
</tr>
<tr>
  <td>101</td>
  <td>PUT</td>
  <td>/user/create</td>
  <td>Register a new user</td>
  <td>name, birthdate, address, postal_code, nif, phone, email, username, password</td>
  <td>Code 200 if OK, 500 otherwise</td>
</tr>
<tr>
  <td>102</td>
  <td>GET</td>
  <td>/user/{id}</td>
  <td>Get the profile of an user</td>
  <td>n/a</td>
  <td>
    <pre>
{
  name:string,
  birthdate:sting,
  address:string,
  postal_code:string,
  nif:int,
  phone:int,
  email:string,
  username:string
}
    </pre>
  </td>
</tr>
<tr>
  <td>103</td>
  <td>POST</td>
  <td>/user/{id}/edit</td>
  <td>Edit the password of an user</td>
  <td>password</td>
  <td>Code 200 if OK, 500 otherwise</td>
</tr>
</table>

## 2. API for books

<table>
<tr>
  <th>ID</th>
  <th>Method</th>
  <th>Route</th>
  <th>Description</th>
  <th>Body</th>
  <th>Returned JSON</th>
</tr>
<tr>
  <td>201</td>
  <td>GET</td>
  <td>/book/{id}</td>
  <td>Gets all info about a book</td>
  <td>n/a</td>
  <td>
    <pre>
{
  title:string,
  price:float,
  publisher:string,
  synopsis:string,
  image:string,
  isbn:int,
  pages:int,
  year:string,
  lang:string,
  dimensions:string,
  cover_type:string
}
    </pre>
  </td>
</tr>
<tr>
  <td>202</td>
  <td>GET</td>
  <td>/categories/{category}</td>
  <td>Gets all books from a given category</td>
  <td>n/a</td>
  <td>
    <pre>
{
  "books": [
    {id:int, title:string, price:float, cover:string},
    {id:int, title:string, price:float, cover:string},
    ...
    ]
}
</pre>
  </td>
</tr>
<tr>
  <td>203</td>
  <td>POST</td>
  <td>/search</td>
  <td>Get all books that match the specified parameters</td>
  <td>isbn, title, publisher</td>
  <td>
    <pre>
{
  "books": [
    {id:int, title:string, price:float, cover:string},
    {id:int, title:string, price:float, cover:string},
    ...
    ]
}
    </pre>
  </td>
</tr>
</table>

## 3. API for purchases

<table>
<tr>
  <th>ID</th>
  <th>Method</th>
  <th>Route</th>
  <th>Description</th>
  <th>Body</th>
  <th>Return</th>
</tr>
<tr>
  <td>301</td>
  <td>PUT</td>
  <td>/user/{user_id}/cart/{book_id}</td>
  <td>Adds a book to the user's cart</td>
  <td>n/a</td>
  <td>Code 200 if OK, 500 otherwise</td>
</tr>
<tr>
  <td>302</td>
  <td>DELETE</td>
  <td>/user/{user_id>/cart/{book_id}</td>
  <td>Removes a book from the user's cart</td>
  <td>n/a</td>
  <td>Code 200 if OK, 500 otherwise</td>
</tr>
<tr>
  <td>303</td>
  <td>GET</td>
  <td>/user/{id}/cart</td>
  <td>Gets all the items on the user's cart</td>
  <td>n/a</td>
  <td>
    <pre>
{
   "books": [
  {id:int, title:string, price:float, cover:string},
  {id:int, title:string, price:float, cover:string},
  ...
  ]   
}
    </pre>
  </td>
</tr>
<tr>
  <td>304</td>
  <td>PUT</td>
  <td>/user/{id}/purchase</td>
  <td>Finalizes the purchase of the current books in the cart</td>
  <td>n/a</td>
  <td>Code 200 if OK, 500 otherwise</td>
</tr>
</table>

## 4. API for past sales information

<table>
<tr>
  <th>ID</th>
  <th>Method</th>
  <th>Route</th>
  <th>Description</th>
  <th>Body</th>
  <th>Return</th>
</tr>
<tr>
  <td>401</td>
  <td>GET</td>
  <td>/user/{id}/history</td>
  <td>Gets all the sales history of the user</td>
  <td>n/a</td>
  <td>
    <pre>
{
  orders: [
    {
      order_id:int,
      total_price:float,
      payment_method:string,
      date:string,
      status:string,
      items: [
        {book_id:int, cover:string},
        {book_id:int, cover:string},
        ...
      ]
    },
    ...
  ]
}
    </pre>
  </td>
</tr>
</table>

