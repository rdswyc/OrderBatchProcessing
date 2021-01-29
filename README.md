# Order batch processing

Implement a WebAPI with the following endpoints

- Create, Read, Update, Delete operations for Boutiques with the folling signature `/api/boutique/...`

- For simplicity, at every Boutique addition there will be a random number of Orders attatched to it.

- Calculate the total commissions that Farfetch should charge for each boutique on a given day `/api/comissions`

## Comissions endpoint
This endpoint should follow these rules
* Boutiques should be charged by 10% of the total value every order
* If the Boutique has more than 1 order, the order with the highest value of the day will not be subject to commission

This operation must produce 2 outputs
* Respond with HTTP 200 and response body in JSON with the following format
```
[
    {
        boutiqueId: <>,
        totalComission: <>
    },
    (...)
]
```

* Print to the console
<Boutique_ID>:<Total_Commission>

## Command line interface

The application is connecting to a sample data-base in the file `OrderBatchProcessing.db`.
There is also a Postman Collection for testing the endpoints.

To start the application, use the following command:

```
dotnet run OrderBatchProcessing.csproj
```

## Example
`api/comissions` input body:
```
B10,O1000,100.00
B11,O1001,100.00
B10,O1002,200.00
B10,O1003,300.00
```

The results should be
* HTTP200 JSON response
```
[
    {
        boutiqueId: B10,
        totalComission: 30
    },
    {
        boutiqueId: B11,
        totalComission: 10
    }
]
```
* Print to console
```
B10,30
B11,10
```

## Deliverable
We expect you to deliver a zip file containing the source code that implements the solution for this problem.
Please provide clear instructions on how to build and run the application.

# Tips
We are expecting an efficient and simple solution to this problem. No need to over-engineer it nor introduce unnecessary complexity.

Remember to keep the code elegant and efficient.
