# OnlineShopping


## Overview

This project is a simple online shopping application built using **ASP.NET Core 7**. The application allows **Admin** and **Customer** users to manage orders, view items, apply discounts, and manage currency exchange rates.

The system includes basic user authentication, role management, and the ability to calculate order totals with currency conversion and discount application. Data is stored in **Microsoft SQL Server**.

---

## Features

- **User Roles**: Two user roles - **Admin** and **Customer**.
  - **Admin**: Can manage items, set exchange rates, and close orders.
  - **Customer**: Can register, log in, view items, add them to the cart, and place orders.

- **Discount Codes**: Admin can define discount promo codes, and customers can apply valid codes to receive discounts.

- **Order Management**: Customers can create and view their orders. Admin can close orders.

- **Detailed Pricing**: Total order prices are calculated based on the selected currency, exchange rates, and any applicable discounts.

---

## Technologies Used

- **Web Development**: 
  - ASP.NET Core 7

- **Identity**: ASP.NET Core Identity for user authentication and role management.

- **Database**: 
  - Microsoft SQL Server
  - Entity Framework Core

---

## System Requirements

- .NET Core 7
- Microsoft SQL Server

---

# API Endpoints

## **Account**
- **POST** `/api/Account/Register`  
  Register a new user (Customer or Admin).

- **POST** `/api/Account/Login`  
  Login and receive an authentication token.

---

## **Item**
- **POST** `/api/Item/PostItem`  
  Admin can add a new item.

- **DELETE** `/api/Item/DeleteItem/{id}`  
  Admin can delete an item by its ID.

- **GET** `/api/Item/GetAllItems`  
  Get all available items.

- **GET** `/api/Item/GetItemById/{id}`  
  Get an item by its ID.

- **PUT** `/api/Item/PutItem/{id}`  
  Admin can update an item.

---

## **Order**
- **POST** `/api/Order/AddNewOrder`  
  Create a new order with details.

- **PUT** `/api/Order/ChangeOrderStatus/{orderId}`  
  Admin can change the status of an order.

- **GET** `/api/Order/GetAllMyOrders`  
  Get all orders placed by the logged-in customer.

- **GET** `/api/Order/GetOrderById/{id}`  
  Get an order by its ID.

---

## **UOM (Unit of Measure)**
- **POST** `/api/UOM/AddNewUOM`  
  Admin can add a new unit of measure.

- **DELETE** `/api/UOM/DeleteUOM/{id}`  
  Admin can delete a unit of measure by its ID.

- **GET** `/api/UOM/GetAllUOMs`  
  Get all units of measure.

- **PUT** `/api/UOM/PutUOM/{id}`  
  Admin can update a unit of measure.

---

## **User**
- **GET** `/api/User/GetUserProfile`  
  Get the profile information of the logged-in user.

- **GET** `/api/User/GetAllUsers`  
  Admin can view all registered users.

---


---

## Setup Instructions

### 1. Clone the Repository

```bash
git clone <https://github.com/hassan9810/OnlineShopping>
cd <OnlineShopping>

