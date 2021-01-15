![Logo](images/logo-name.png)
___
# Alpacko usage GUIDE

The first-time user have to setup a database in order to be able to run this application
follow these steps to create an Alpacko Database on your local pc.

## Step 1. <br/>
When you first open the project, go to the Solution Explorer and find Alpacko.Database. <br/>
![Alpacko.Database](images/Solution.exp.Alpacko.Database.PNG) <br/>
Right-click on Alpacko.Database and click "Publish" <br/>
![Alpacko.Database](images/Publish.Database.png)


## Step 2. <br/>
A window named "Publish Database" will open.![Alpacko.Database](images/Publish.Database.Edit.PNG) <br/>
At Target database connection select the button called "Edit" this will open up a new window named "connect" <br/>
Here select the tab "Browse" (1) and then expand the "Local" tree.<br/>
when expanded, you will see something name similarly to "MSSQLLocalDB".(2) Select that Database.![Alpacko.Database](images/Connect.PNG) <br/>
Click "OK" <br/>
The window will close down. <br/>
On this "Publish Database" window at "Database Name" <br/> 
Make sure you fill in the correct name "Alpacko.Database" and then Click on "Publish". ![Alpacko.Database](images/Database.Name.PNG) <br/>

___
# Create Dummy Data in Database

In order to create some dummy-data in the database to try out the application, switch to the branch named [165-automatically-create-dummy-data](https://github.com/Abooow/Alpacko/tree/165-automatically-create-dummy-data) <br/>

## This data is thats going to be created:
* 2 Users <br/>

  | Email             | Password | Role  |
  | ----------------- | -------- | ----- |
  | admin@alpacko.com | password | Admin |
  | user@alpacko.com  | password | User  |
 
  *\* these e-mailaddresses are not real* <br/>
  User with admin role will be connected to a postoffice named Alpacko. <br/>
* 10 Packages <br/>
* 1 Package Detail
* 1 Package Sender
* 1 Package Recipient
* 1 Package SizeName
* 1 Post Office
* 2 UserRole


