# Event Management Application

This is an Event Management Application built using ASP.NET Core Razor Pages with a Clean Architecture. It allows users to manage events, venues, and tickets with different roles (admin and user).

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or [Visual Studio Code](https://code.visualstudio.com/)

### Installation

1. Clone the repository:
    ```sh
    git clone https://github.com/yourusername/EventMgmt.git
    cd EventMgmt
    ```

2. Build the solution:
    ```sh
    dotnet build
    ```

(project in this repository using in memory database)
3. Apply migrations and seed the database (optional, if using a real database):
    ```sh
    dotnet ef database update
    ```

4. Run the application:
    ```sh
    dotnet run --project WebAPI
    ```

### Usage

After running the application, navigate to `https://localhost:7051` to view the application. The Swagger documentation will be available at `https://localhost:7051/swagger`.

### Configuration

The application settings are configured in the appsettings.json file. You can modify the settings according to your environment.

```
{
  "JwtSettings": {
    "SecretKey": "YourVeryStrongSecretKeyOf32Chars",
    "Issuer": "https://localhost:7051",
    "Audience": "https://localhost:7051",
    "ExpiryMinutes": 60
  }
}
```
### Login Informations

Here are the default login credentials for users and admin created during the database initialization:

Admin

Login: admin
Password: admin123

User
Login: user1, user2
Password: user1234, user5678

