# Software Center 

They maintain the list of supported software for our company.

We are building them an API.

## Vendors

We have arrangements with vendors. Each vendor has:

- And ID we assign - 99.999% this is immutable.
- A Name - Nope. Immutable
- A Website URL - Immutable
- A Point of Contact - This will change over time.
  - Name
  - Email
  - Phone Number

Vendors have a set of software they provide that we support.

## Catalog Items

Catalog items are instances of software a vendor provides.

A catalog item has:
- An ID we assign
- A vendor the item is associated with
- The name of the software item
- A description
- A version number (we prefer SEMVER, but not all vendors use it)


"Http Requests Must Contain All the Data The Server Needs to Fulfill the Request" - a rule. Just how it is.

- Http Requests can put data in the following locations:
  - The URL - including route parameters (e.g. GET /vendors/{id:guid}). (GET, POST, PUT, DELETE)
    - "Query String Parameters" - GET /vendors?addedBy=sue@aol.com (GET on a collection is probably the place you should use these)
  - Headers
      - Authorization - this is (in an API) always how we answer the "who" question. (GET POST PUT DELETE)
  - You can send a "body" (entity). (POST, PUT)
    - you have to tell the server how to "decode this" through the content-type header. 

Note - One catalog item may have several versions. Each is it's own item.

Design:
  - What is the resource
  - What is the representation
  - What is the method

```http
POST /vendors/3898398983/items
Content-Type: application/json

{
  "name": "Visual Studio Code",
  "version": "1.102.3",
  "description": "Cross platform edit for programmers and nerds",
}

```

```http
GET /vendors/f2a1fb72-58bc-436b-a299-77809ae85b3f

{
  "id": "f2a1fb72-58bc-436b-a299-77809ae85b3f",
  "addedBy": "sue@aol.com",
  "name": "Docker",
  "url": "https://docker.com",
  "pointOfContact": {
    "name": "Silvio",
    "phone": "800 Write-Code",
    "email": "silvio@jbrain.com"
  }

}
```

PUT /vendors/f2a1fb72-58bc-436b-a299-77809ae85b3f/pointofcontact

{
    "name": "Bart",
    "phone": "800 Write-Code",
    "email": "silvio@jbrain.com"
  }



GET /employees/101

{
  "id": 101,
  "name": "Dylan H",
  "phone": "888-8888",
  "email": "dylan@company.com",
  "links": {
    "manager": "/employees/1",
    "performancehistory": "https://otherserver.com/performance-histories/101"

  }
}


GET /employees/101/manager

{
  "id": 1,
  "name": "Sue",
  "phone": "888-8888",
  "email": "dylan@company.com"
}


GET /employees/101/performance-history

## Use Cases

The Software Center needs a way for managers to add vendors. Normal members of the team cannot add vendors.
Software Center team members may add catalog items to a vendor.
Software Center team members may add versions of catalog items.
Software Center may deprecate a catalog items. (effectively retiring them, so they don't show up on the catalog)

Any employee in the company can use our API to get a full list of the software catalog we currently support.


## "Candidate" Resources

- vendors
- items


- roles
    - software center managers
    - software center team members
    - employees

 
