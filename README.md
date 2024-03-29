# EasyLogin

This is a small project made in Windows Presentation Foundation (WPF). It provides basic functionalities for user authentication, including a login window and a registration window.

## Features

- User-friendly interface with separate windows for login and registration.
- Password encryption using BCrypt algorithm with SHA-512 hashing.
- Option to register a new account securely.
- Ability to login with a Microsoft account using the Microsoft Identity Client.

## Security

- Passwords are encrypted using BCrypt algorithm with a salt for enhanced security.
- Hashing type used for password encryption: SHA-512.
- To prevent SQL injection attacks, all database queries are parameterized. This means that user input is never directly interpolated into SQL queries, reducing the risk of malicious SQL injection.

## Licence
This project is licensed under the [GPL-3.0 licence](LICENSE).

## Screenshots
### Login
<img width="600" alt="MyPortfolio Login" src="https://github.com/0ls3n/MyPortfolio/assets/31800865/27106aa9-72c4-41d5-a001-7bf058b6e798">

### Register
<img width="600" alt="MyPortfolio Register" src="https://github.com/0ls3n/MyPortfolio/assets/31800865/d1da2abb-59fc-4d67-8c51-7ee3e70fcae2">
