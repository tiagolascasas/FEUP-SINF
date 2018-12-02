# Primavera API for the Online BookShop Web App

<table style="width: 100%;">
<tr>
  <th>Webservice ID</th><th>Webservice Description</th><th>Related Core View(s)</th><th>Input example</th><th>Expected Output</th>
</tr>
</table>

<table>
  <tr><td>Web Service</td><td>WS01</td></tr>
  <tr><td>Description</td><td>Creates a new user/client</td></tr>
  <tr><td>Related Core Views</td><td>Splash Page</td></tr>
  <tr><td>Route</td><td>Base/Clientes/Atualiza</td></tr>
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

<table>
  <tr><td>Web Service</td><td>WS02</td></tr>
  <tr><td>Description</td><td>Gets the relevant information of an user/client</td></tr>
  <tr><td>Related Core Views</td><td>Profile Page</td></tr>
  <tr><td>Route</td><td>Base/Clientes/Edita/{cliente_id}</td></tr>
  <tr><td>Input example</td><td>
<pre>
cliente_id = "TIAGO" (parameter in the route)
</pre></td></tr>
  <tr><td>Expected Output</td><td><pre>
  {
      "CodigoTabLog": "Cliente",
      "ChaveLog": "Cliente",
      "EstadoBE": "",
      "Cliente": "TIAGO",
      "Nome": "Tiago Santos",
      "Descricao": "",
      "Morada": "Rua Eng. Farinas de Almeida, 313, 4º esq",
      (...)
      "CamposUtil": [
          {
              "Conteudo": "ValorValoremailxyz@gmail.com",
              "Nome": "CDU_email",
              "Valor": "emailxyz@gmail.com",
              "Objecto": null,
              "Tipo": 1,
              "ChaveLog": "Nome",
              "EstadoBE": "",
              "TipoSimplificado": 1
          },
          {
              "Conteudo": "ValorValor03AC674216F3E15C761EE1A5E255F067953623C8B388B4459E13F978D7C846F4",
              "Nome": "CDU_password_hash",
              "Valor": "03AC674216F3E15C761EE1A5E255F067953623C8B388B4459E13F978D7C846F4",
              "Objecto": null,
              "Tipo": 1,
              "ChaveLog": "Nome",
              "EstadoBE": "",
              "TipoSimplificado": 1
          }
      ],
      (...)
  }
</pre></td></tr>
</table>

<table>
  <tr><td>Web Service</td><td>WS03</td></tr>
  <tr><td>Description</td><td>Updates the information of an user/client</td></tr>
  <tr><td>Related Core Views</td><td>Profile Page</td></tr>
  <tr><td>Route</td><td>Base/Clientes/Actualiza</td></tr>
  <tr><td>Input example</td><td>
<pre>
//Input is the same as the output of WS02, but with some changed attributes,
//such as Morada, CDU_email and CDU_password_hash
{
    "CodigoTabLog": "Cliente",
    "ChaveLog": "Cliente",
    "EstadoBE": "",
    "Cliente": "TIAGO",
    "Nome": "Tiago Santos",
    "Descricao": "",
    "Morada": "Nova Morada, 451",
    (...)
    "CamposUtil": [
        (...)
        {
            "Conteudo": "ValorValornewemail@gmail.com",
            "Nome": "CDU_email",
            "Valor": "newemail@gmail.com",
            "Objecto": null,
            "Tipo": 1,
            "ChaveLog": "Nome",
            "EstadoBE": "",
            "TipoSimplificado": 1
        },
        {
            "Conteudo": "ValorValor253C2E786C2414DCAEC8DBF11DF515B5075371454B93A5687D24D96DDBF3B939",
            "Nome": "CDU_password_hash",
            "Valor": "253C2E786C2414DCAEC8DBF11DF515B5075371454B93A5687D24D96DDBF3B939",
            "Objecto": null,
            "Tipo": 1,
            "ChaveLog": "Nome",
            "EstadoBE": "",
            "TipoSimplificado": 1
        }
    ],
    (...)
}
</pre></td></tr>
  <tr><td>Expected Output</td><td>HTTP 204</td></tr>
</table>

<table>
  <tr><td>Web Service</td><td>WS04</td></tr>
  <tr><td>Description</td><td>Gets all the relevant information of a book</td></tr>
  <tr><td>Related Core Views</td><td>Book page</td></tr>
  <tr><td>Route</td><td>Administrador/Consulta</td></tr>
  <tr><td>Input example</td><td>
<pre>
-- select the relevant info of book "B0001"

Select Descricao, CDU_Autor, CDU_Sinopse, CDU_ISBN,
CDU_Editora, CDU_Capa, CDU_Paginas, CDU_Ano, CDU_Dimensoes,
Familia, CDU_Idioma, PVP1
from Artigo, ArtigoMoeda
where Artigo.Artigo = 'B0001' and Artigo.Artigo = ArtigoMoeda.Artigo
</pre></td></tr>
  <tr><td>Expected Output</td><td><pre>
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
                "CDU_Idioma": English,
                "PVP1": 25.99
            }
        ]
    },
    "Query": "System.Data.SqlClient.SqlCommand"
}
</pre></td></tr>
</table>

<table>
  <tr><td>Web Service</td><td>WS05</td></tr>
  <tr><td>Description</td><td>Gets a set of books that belong to the specified category</td></tr>
  <tr><td>Related Core Views</td><td>Book page</td></tr>
  <tr><td>Route</td><td>Administrador/Consulta</td></tr>
  <tr><td>Input example</td><td>
<pre>
-- select all books that belong to the category "IT"

Select Artigo.Artigo, Descricao, PVP1, CDU_Autor
from Artigo, ArtigoMoeda
where Familia = 'IT' and Artigo.Artigo = ArtigoMoeda.Artigo
</pre></td></tr>
  <tr><td>Expected Output</td><td><pre>
{
    "DataSet": {
        "Table": [
            {
                "Artigo": "B0003",
                "Descricao": "Introdução à Programação em Java",
                "PVP1": 20,
                "CDU_Autor": "António Manuel Adrego da Rocha"
            },
            {
                "Artigo": "B0004",
                "Descricao": "Introdução à programação em C",
                "PVP1": 25,
                "CDU_Autor": "António Manuel Adrego da Rocha"
            }
        ]
    },
    "Query": "System.Data.SqlClient.SqlCommand"
}
</pre></td></tr>
</table>


<table>
  <tr><td>Web Service</td><td>WS06</td></tr>
  <tr><td>Description</td><td>Searches books by title, including partial strings</td></tr>
  <tr><td>Related Core Views</td><td>Splash page, Search Results Page</td></tr>
  <tr><td>Route</td><td>Administrador/Consulta</td></tr>
  <tr><td>Input example</td><td>
<pre>
-- select all books with "Intro" in the title

"Select Artigo.Artigo, Descricao, CDU_Autor, PVP1
from Artigo, ArtigoMoeda
where Descricao like '%Intro%' and Artigo.Artigo = ArtigoMoeda.Artigo"
</pre></td></tr>
  <tr><td>Expected Output</td><td><pre>
{
    "DataSet": {
        "Table": [
            {
                "Artigo": "B0003",
                "Descricao": "Introdução à Programação em Java",
                "CDU_Autor": "António Manuel Adrego da Rocha",
                "PVP1": 20
            },
            {
                "Artigo": "B0004",
                "Descricao": "Introdução à programação em C",
                "CDU_Autor": "António Manuel Adrego da Rocha",
                "PVP1": 25
            }
        ]
    },
    "Query": "System.Data.SqlClient.SqlCommand"
}
</pre></td></tr>
</table>

<table>
  <tr><td>Web Service</td><td>WS07</td></tr>
  <tr><td>Description</td><td>Gets a list of books ordered by existing stock, except books with no stock</td></tr>
  <tr><td>Related Core Views</td><td>Book page</td></tr>
  <tr><td>Route</td><td>Administrador/Consulta</td></tr>
  <tr><td>Input example</td><td>
<pre>
Select Artigo.Artigo, Descricao, PVP1, CDU_Autor, STKActual
from Artigo, ArtigoMoeda
where Artigo.Artigo = ArtigoMoeda.Artigo and STKActual > 0 order by STKActual
</pre></td></tr>
  <tr><td>Expected Output</td><td><pre>
{
    "DataSet": {
        "Table": [
            {
                "Artigo": "B0003",
                "Descricao": "Introdução à Programação em Java",
                "PVP1": 20,
                "CDU_Autor": "António Manuel Adrego da Rocha",
                "STKActual": 1
            },
            {
                "Artigo": "B0005",
                "Descricao": "Foi Sem Querer Que Te Quis",
                "PVP1": 12,
                "CDU_Autor": "Raul Minh'alma ",
                "STKActual": 6
            },
            {
                "Artigo": "B0004",
                "Descricao": "Introdução à programação em C",
                "PVP1": 25,
                "CDU_Autor": "António Manuel Adrego da Rocha",
                "STKActual": 15
            },
            {
                "Artigo": "B0002",
                "Descricao": "Romeu e Julieta",
                "PVP1": 25,
                "CDU_Autor": "William Shakespeare ",
                "STKActual": 22
            },
            {
                "Artigo": "B0001",
                "Descricao": "A Brief History Of Time",
                "PVP1": 25.99,
                "CDU_Autor": "Stephen Hawking",
                "STKActual": 30
            }
        ]
    },
    "Query": "System.Data.SqlClient.SqlCommand"
}
</pre></td></tr>
</table>

<table>
  <tr><td>Web Service</td><td>WS08</td></tr>
  <tr><td>Description</td><td>Gets a list of books ordered by the last date they were updated (first is the most recent)</td></tr>
  <tr><td>Related Core Views</td><td>Splash page</td></tr>
  <tr><td>Route</td><td>Administrador/Consulta</td></tr>
  <tr><td>Input example</td><td>
<pre>
Select Artigo.Artigo, Descricao, PVP1, CDU_Autor, DataUltimaActualizacao
from Artigo, ArtigoMoeda
where Artigo.Artigo = ArtigoMoeda.Artigo order by DataUltimaActualizacao desc
</pre></td></tr>
  <tr><td>Expected Output</td><td><pre>
{
    "DataSet": {
        "Table": [
            {
                "Artigo": "B0003",
                "Descricao": "Introdução à Programação em Java",
                "PVP1": 20,
                "CDU_Autor": "António Manuel Adrego da Rocha",
                "DataUltimaActualizacao": "2018-12-02T02:04:21"
            },
            {
                "Artigo": "B0005",
                "Descricao": "Foi Sem Querer Que Te Quis",
                "PVP1": 12,
                "CDU_Autor": "Raul Minh'alma ",
                "DataUltimaActualizacao": "2018-12-02T02:03:39"
            },
            {
                "Artigo": "B0004",
                "Descricao": "Introdução à programação em C",
                "PVP1": 25,
                "CDU_Autor": "António Manuel Adrego da Rocha",
                "DataUltimaActualizacao": "2018-12-02T01:59:40"
            },
            {
                "Artigo": "B0002",
                "Descricao": "Romeu e Julieta",
                "PVP1": 25,
                "CDU_Autor": "William Shakespeare ",
                "DataUltimaActualizacao": "2018-12-02T01:50:31"
            },
            {
                "Artigo": "B0001",
                "Descricao": "A Brief History Of Time",
                "PVP1": 25.99,
                "CDU_Autor": "Stephen Hawking",
                "DataUltimaActualizacao": "2018-12-02T01:46:43"
            }
        ]
    },
    "Query": "System.Data.SqlClient.SqlCommand"
}
</pre></td></tr>
</table>

<table>
  <tr><td>Web Service</td><td>WS08</td></tr>
  <tr><td>Description</td><td>Makes a purchase of the specified items, producing an invoice</td></tr>
  <tr><td>Related Core Views</td><td>Splash page</td></tr>
  <tr><td>Route</td><td>Administrador/Consulta</td></tr>
  <tr><td>Input example</td><td>
<pre>
{
  "Linhas": [
    {
      "Artigo": "B0001",
      "Quantidade": "1"
    },
    {
      "Artigo": "B0002",
      "Quantidade": "1"
    }
  ],
  "Tipodoc": "FAR",
  "Serie": "A",
  "Entidade": "C0001",
  "TipoEntidade": "C",
  "DataDoc":"12/11/2018",
  "DataVenc":"12/12/2018"
}
</pre></td></tr>
  <tr><td>Expected Output</td><td>true</td></tr>
</table>
