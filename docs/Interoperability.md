# Primavera API for the Online BookShop Web App

<table>
<tr>
  <th>Webservice ID</th><th>Webservice Description</th><th>Related Core View(s)</th><th>Input example</th><th>Expected Output</th>
</tr>
</table>

<table>

  <tr><td>Web Service</td><td>WS01</td></tr>
  <tr><td>Description</td><td>Creates a new user/client</td></tr>
  <tr><td>Related Core Views</td><td>Splash Page</td></tr>
  <tr><td>Input example</td><td>
<pre>
{
    "Cliente": "1",
    "Nome": "Manfred Reubens",
    "Morada": "Praceta Conde Arnoso 60",
    "CodigoPostal": "2640-467",
    "Telefone": "6705966965",
    "NumContribuinte": "64287048860",
    "Pais": "PT",
    "Moeda": "EUR"
}
</pre></td></tr>
  <tr><td>Expected Output</td><td>HTTP 204</td></tr>
</table>

<tr>
  <td>WS02</td>
  <td>Gets the relevant information of an user/client</td>
  <td>Profile Page</td>
  <td>
<pre>
Select Cliente, Nome, Fac_Mor, Fac_Local, Fac_Tel,
NumContrib, CDU_email, CDU_password_hash
from Clientes
where Cliente = 'TIAGO'
</pre>
  </td>
  <td>
<pre>
{
    "DataSet": {
        "Table": [
            {
                "Cliente": "TIAGO",
                "Nome": "Tiago Santos",
                "Fac_Mor": "Rua Eng. Farinas de Almeida, 313, 4º esq",
                "Fac_Local": null,
                "Fac_Tel": "961843943",
                "NumContrib": "64287048860",
                "CDU_email": null,
                "CDU_password_hash": "03AC674216F3E15C761EE1A5E255F067953623C8B388B4459E13F978D7C846F4"
            }
        ]
    },
    "Query": "System.Data.SqlClient.SqlCommand"
}
</pre>
  </td>
</tr>
<tr>
  <td>WS03</td>
  <td>Updates the information of an user/client</td>
  <td>Profile Page</td>
  <td>
<pre>
"Select Cliente, Nome, Fac_Mor, Fac_Local, Fac_Tel, NumContrib from Clientes"
</pre>
  </td>
  <td>HTTP 204</td>
</tr>
<tr>
  <td>WS04</td>
  <td>Gets all the information of a book</td>
  <td>Book page</td>
  <td>
<pre>
"Select Descricao, CDU_Autor, CDU_Sinopse, CDU_ISBN,
CDU_Editora, CDU_Capa, CDU_Paginas, CDU_Ano,
CDU_Dimensoes, Familia, CDU_Idioma
from Artigo
where Artigo = 'B0001'"
</pre>
  </td>
  <td>
<pre>
{
    "DataSet": {
        "Table": [
            {
                "Descricao": "A Brief History Of Time",
                "CDU_Autor": "Stephen Hawking",
                "CDU_Sinopse": "A landmark volume in science writing by one of the great minds of our time",
                "CDU_ISBN": "0857501003",
                "CDU_Editora": "Transworld Publishers Ltd",
                "CDU_Capa": "Paperback",
                "CDU_Paginas": 272,
                "CDU_Ano": "05 Feb 2015",
                "CDU_Dimensoes": "127 x 198 x 17mm",
                "Familia": "FICTION",
                "CDU_Idioma": "English"
            }
        ]
    },
    "Query": "System.Data.SqlClient.SqlCommand"
}
</pre>
  </td>
</tr>
<tr>
  <td>WS05</td>
  <td>Gets a set of books that belong to the specified category</td>
  <td>Book page</td>
  <td>
<pre>
"Select Artigo, Descricao, CDU_Autor
from Artigo
where Familia = 'IT'"
</pre>
  </td>
  <td>
<pre>
{
    "DataSet": {
        "Table": [
            {
                "Artigo": "B0003",
                "Descricao": "Introdução à Programação em Java",
                "CDU_Autor": "António Manuel Adrego da Rocha"
            },
            {
                "Artigo": "B0004",
                "Descricao": "Introdução à programação em C",
                "CDU_Autor": "António Manuel Adrego da Rocha"
            }
        ]
    },
    "Query": "System.Data.SqlClient.SqlCommand"
}
</pre>
  </td>
</tr>
<tr>
  <td>WS06</td>
  <td>Searches books by title, including partial strings</td>
  <td>Book page</td>
  <td>
<pre>
"Select Descricao, CDU_Autor
from Artigo
where Descricao like '%Intro%'"
</pre>
  </td>
  <td>
<pre>
{
    "DataSet": {
        "Table": [
            {
                "Descricao": "Introdução à Programação em Java",
                "CDU_Autor": "António Manuel Adrego da Rocha"
            },
            {
                "Descricao": "Introdução à programação em C",
                "CDU_Autor": "António Manuel Adrego da Rocha"
            }
        ]
    },
    "Query": "System.Data.SqlClient.SqlCommand"
}
</pre>
  </td>
</tr>
</table>
