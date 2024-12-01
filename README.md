## Project One

Small store for products

Activities:
1. CRUD for Clients: minimum fields needed -> name, NIF, address
2. CRUD for products: minimum fields needed -> name, code
3. Operation: Sell a product -> it requires product, client and quantity. This operation can accept more than one product to sell, the quantity must be an integer, decimal values must be rejected
4. Operation: See past sales -> it requires client id and must show: one by one the sales, if the sale includes more than one product, it should include all the products
5. Operation: Income report -> it requries product id and must show the product and the number of units sells ordered descendent (most sell first)
6. Add tests for your code
7. Include jacoco to see code coverage, the minimum coverage of lines must be 50%

Notes: 
- Stock for products is infinite
- For each sale save a log in mongoDB per client
- Testing with Xunit

