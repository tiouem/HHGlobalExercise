### Building and running your application

When you're ready, start your application by running:
`docker compose up --build`.

Your application will be available at http://localhost:12.

### Callin application

Call with Postman or tool of your choice **POST** method on `http://localhost:12/api/CalculateTaxForJobs`
with this Json body:
`
{
"incomingJobs": [
{
"extraMargin": true,
"items": [
{
"name": "envelopes",
"price": 520.00,
"exempt": false
},
{
"name": "letterhead",
"price": 1983.37,
"exempt": true
}
]
},
{
"extraMargin": false,
"items": [
{
"name": "t-shirts",
"price": 294.04,
"exempt": false
}
]
},
{
"extraMargin": true,
"items": [
{
"name": "frisbees",
"price": 19385.38,
"exempt": true
},
{
"name": "yo-yos",
"price": 1829,
"exempt": true
}
]
}
]
}`