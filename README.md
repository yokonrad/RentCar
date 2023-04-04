# RentCar

​1. First decision which i had to make was about using a proper architecture for whole web app. I decided to use an onion architecture due to separation of layers. It is easy to modify and expand for further functionality.<br />
2. Next step was projecting database scheme. This time it was kinda easy because we had to store data in only three tables - cars, locations and reservations. I decided to create a database using Entity Framework because i had to create entities for the other part of application anyway. Further database operations are made with Dapper because i like to have a control on my queries.<br />
3. I made whole backend like proper repositores, controllers and views to make a crud operations on all data from the tables.<br />
4. Later i've implemented simple UI theme and functionality to make a reservations on home page.<br />

TODO:
- Next step should be implementing authentitication handlers to hide admin functionality (like crud operations on tables) and rethink about client/server side validations.
