-- SETUP:
    -- Create a database server (docker)
        -- docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<password>" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
    -- Connect to the server (Azure Data Studio / Database extension)
    -- Test your connection with a simple query (like a select)
    -- Execute the Chinook database (to create Chinook resources in your db)


-- On the Chinook DB, practice writing queries with the following exercises

-- BASIC CHALLENGES
-- List all customers (full name, customer id, and country) who are not in the USA
select * from Customer where Country <> 'USA'
-- List all customers from Brazil
select * from Customer where Country = 'Brazil'
-- List all sales agents
select * from Employee where Title = 'Sales Support Agent'
-- Retrieve a list of all countries in billing addresses on invoices
select distinct BillingCountry from Invoice
-- Retrieve how many invoices there were in 2009, and what was the sales total for that year?
select count("InvoiceId") as InvoicesFrom09, sum("total") as SalesTotal from Invoice where InvoiceDate < '2010-01-01' and InvoiceDate > '2008-12-31'
    -- (challenge: find the invoice count sales total for every year using one query)
select year("InvoiceDate") as Year, sum("total") as SalesTotal from Invoice group by year("InvoiceDate")
-- how many line items were there for invoice #37
select InvoiceId, count("InvoiceLineId") as LineItems from InvoiceLine where InvoiceId = 37 group by InvoiceId
-- how many invoices per country? BillingCountry  # of invoices -
select BillingCountry, count("InvoiceId") as Invoices from Invoice group by BillingCountry
-- Retrieve the total sales per country, ordered by the highest total sales first.
select BillingCountry, sum("total") as SalesTotal from Invoice group by BillingCountry order by SalesTotal

-- JOINS CHALLENGES
-- Every Album by Artist
select Album.Title, Artist.Name from Album inner join Artist on Album.ArtistId = Artist.ArtistId

-- All songs of the rock genre (why is there an error?)
select Track.Name, Genre.Name from Track inner join Genre on Track.GenreId = Genre.GenreId where Genre.Name = 'Rock'

-- Show all invoices of customers from brazil (mailing address not billing)
select Invoice.InvoiceId, Customer.FirstName, Customer.Country from Invoice inner join Customer on Invoice.CustomerId = Customer.CustomerId where Country = 'Brazil'

-- Show all invoices together with the name of the sales agent for each one
select Invoice.*, Employee.FirstName as SalesAgent from Invoice left join Customer on Invoice.CustomerId = Customer.CustomerId inner join Employee on Customer.SupportRepId = Employee.EmployeeId

-- Which sales agent made the most sales in 2009? (why is there an error?)
select top 1 Employee.FirstName, count(Invoice.InvoiceId) as Sales
    from Invoice left join Customer on Invoice.CustomerId = Customer.CustomerId
    inner join Employee on Customer.SupportRepId = Employee.EmployeeId 
    where year(InvoiceDate) = 2009 group by Employee.FirstName order by Sales desc

-- How many customers are assigned to each sales agent?
select count(CustomerId) as Customers, Employee.FirstName as SalesAgent from Employee inner join Customer on Customer.SupportRepId = Employee.EmployeeId group by Employee.FirstName

-- Which track was purchased the most in 2010?
select Track.Name as MostPopularTrack, count(InvoiceLine.InvoiceLineId) as Purchases
    from Track
    left join InvoiceLine on Track.TrackId = InvoiceLine.TrackId
    left join Invoice on InvoiceLine.InvoiceId = Invoice.InvoiceId
    where year(Invoice.InvoiceDate) = 2010
    group by Track.Name order by Purchases desc

-- Show the top three best selling artists.
select top 3 Artist.Name, count(InvoiceLine.InvoiceLineId) as Sales
    from Artist
    left join Album on Artist.ArtistId = Album.ArtistId
    left join Track on Album.AlbumId = Track.AlbumId
    left join InvoiceLine on Track.TrackId = InvoiceLine.TrackId
    group by Artist.Name order by Sales desc

-- Which customers have the same initials as at least one other customer?
select T1.FirstName, T1.LastName, CONCAT(SUBSTRING(T1.FirstName, 1, 1), SUBSTRING(T1.LastName, 1, 1)) as Initials
    from Customer T1, Customer T2
    where CONCAT(SUBSTRING(T1.FirstName, 1, 1), SUBSTRING(T1.LastName, 1, 1)) = CONCAT(SUBSTRING(T2.FirstName, 1, 1), SUBSTRING(T2.LastName, 1, 1)) and T1.CustomerId <> T2.CustomerId

-- ADVACED CHALLENGES
-- solve these with a mixture of joins, subqueries, CTE, and set operators.
-- solve at least one of them in two different ways, and see if the execution
-- plan for them is the same, or different.

-- 1. which artists did not make any albums at all?
select Artist.Name, count(Album.AlbumId) as AlbumCount
    from Artist
    full outer join Album on Artist.ArtistId = Album.ArtistId
    where AlbumCount < 1 group by Artist.Name

-- 2. which artists did not record any tracks of the Latin genre?

-- 3. which video track has the longest length? (use media type table)

-- 4. find the names of the customers who live in the same city as the
--    boss employee (the one who reports to nobody)

-- 5. how many audio tracks were bought by German customers, and what was
--    the total price paid for them?

-- 6. list the names and countries of the customers supported by an employee
--    who was hired younger than 35.


-- DML exercises

-- 1. insert two new records into the employee table.

-- 2. insert two new records into the tracks table.

-- 3. update customer Aaron Mitchell's name to Robert Walter

-- 4. delete one of the employees you inserted.

-- 5. delete customer Robert Walter.
