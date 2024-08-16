# **Gaming Builder & Event Management System**

## Overview

The **Gaming Builder & Event Management System** is a collaborative project that manages vendors, components, events, and associations between users and components for events. The system is designed for administrators to monitor vendors, assign users to events, and control the number of vendors within each component category. Additionally, users can view events, register, and manage their participation in various events, allowing for easy association and component management.

This system comprises two main sections:
- **Gaming Builder**: Allows users to create, read, update, and delete components and builds, and associate components with builds.
- **Event Management System**: Enables CRUD operations for users and events and manages associations between users and events. Additionally, the system allows administrative control over these associations.

## Features

### Core Features
- **Users CRUD**: Create, read, update, and delete users.
- **Events CRUD**: Create, read, update, and delete events.
- **Components CRUD**: Manage components, including adding, editing, and removing components.
- **Builds CRUD**: Manage builds and their associated components.
- **Categories CRUD**: Manage categories for components and events.
- **Associations**: 
  - Assign users to events and components.
  - Assign components to builds.
  - Control the number of users per component category.

### Extended Features
- **ListUsersForComponent**: Displays all users associated with a component.
- **ListComponentsForUser**: Displays all components associated with a specific user.
- **ListEventsForUser**: Displays all events a user is enrolled in.
- **Administrative Permissions**: Only registered users can modify event associations.
- **Rich Text Editor for Event Details**: Provides a rich text editor for event descriptions, allowing detailed formatting.

## Project Structure

### Database ERD
The project has an entity-relationship diagram (ERD) that includes the following tables:
- **Users**: Manages users registered in the system.
- **Events**: Manages events that users can register for.
- **Components**: Manages different computer components.
- **Builds**: Manages builds, which are collections of components.
- **User_Events**: N-to-M association between users and events.
- **Event_Categories**: N-to-M association between events and categories.
- **Build_Components**: N-to-M association between builds and components.

### Tables

#### **Users Table**
- `user_id`: Primary key.
- `username`: User's chosen username.
- `email`: User's email address.
- `password`: Hashed password for login purposes.
- `created_at`: Timestamp for when the user was created.
- `updated_at`: Timestamp for the last update.

#### **Events Table**
- `event_id`: Primary key.
- `event_name`: Name of the event.
- `event_description`: Description of the event.
- `event_location`: Physical or virtual location of the event.
- `event_type`: Type of the event (e.g., conference, webinar).
- `event_date`: Date and time of the event.
- `created_at`: Timestamp for event creation.
- `updated_at`: Timestamp for the last update.

#### **Components Table**
- `component_id`: Primary key.
- `component_name`: Name of the component (e.g., CPU, GPU).
- `component_type`: Type of the component.
- `manufacturer`: Manufacturer of the component.
- `component_price`: Price of the component.
- `image_path`: URL or path to the image of the component.

#### **Builds Table**
- `build_id`: Primary key.
- `build_name`: Name of the build.
- `build_description`: Description of the build.

#### **Associative Tables**
- **User_Events**: Manages user-event associations with status and notes.
- **Build_Components**: Associates components with builds.
- **Event_Categories**: Associates events with categories.

## Installation and Setup

### Prerequisites
- **Visual Studio**: For ASP.NET MVC development.
- **SQL Server**: For database management.
- **Entity Framework**: For handling migrations and database interactions.

### Installation Steps
1. Clone the repository:
   ```bash
   git clone https://github.com/mikmok25/HTTP-5226-CollaborationProject
   cd HTTP-5226-CollaborationProject
   ```
2. Restore NuGet packages:
  ```bash
  nuget restore
  ```
3. Update-Database
  ```bash
  Update-Database
  ```
4. Run the application in Visual Studio (or with the command line)
  ```bash
  dotnet run
  ```
Navigate to https://localhost:5001/ to view the project.

### API Endpoints

| **Method** | **Endpoint**                                  | **Description**                                |
|------------|-----------------------------------------------|------------------------------------------------|
| `GET`      | `/api/CategoryData/ListCategories`            | List all categories.                           |
| `GET`      | `/api/CategoryData/FindCategory/{id}`         | Find a specific category by ID.                |
| `POST`     | `/api/CategoryData/AddCategory`               | Add a new category.                            |
| `POST`     | `/api/CategoryData/UpdateCategory/{id}`       | Update an existing category.                   |
| `POST`     | `/api/CategoryData/DeleteCategory/{id}`       | Delete a category.                             |
| `GET`      | `/api/EventData/ListEvents`                   | List all events.                               |
| `POST`     | `/api/EventData/AddEvent`                     | Add a new event.                               |

### License

This version uses proper markdown formatting for headings, code blocks, tables, and lists.


