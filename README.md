# TrelloConverter

TrelloConverter is a .NET application designed to convert Trello board data into various formats such as CSV, Markdown, and LaTeX. This project includes both the main application and unit tests to ensure the functionality of the conversion processes.

## Table of Contents

- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Features

- Convert Trello JSON data to CSV format.
- Convert Trello JSON data to Markdown format.
- Convert Trello JSON data to LaTeX format.
- Options to keep original format, enumerate, de-enumerate, reverse order, and close application after conversion.

## Installation

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio](https://visualstudio.microsoft.com/)

### Clone the Repository

git clone https://github.com/Cal-ly/TrelloConverter.git cd TrelloConverter


### Build the Solution

Open the solution in Visual Studio and build it. This will restore the necessary NuGet packages and compile the project.

## Usage

### Running the Application

1. Open the solution in Visual Studio.
2. Set `TrelloConverter` as the startup project.
3. Run the application.

### Converting Trello Data

1. Use the "Choose File" button to select the Trello JSON file.
2. Configure the desired options (e.g., output format, enumeration).
3. Click the "Convert" button to generate the output files.

### Unit Tests

To run the unit tests:

1. Open the Test Explorer in Visual Studio.
2. Run all tests to ensure everything is working correctly.

## Project Structure

- **TrelloConverter**: Contains the main application code.
  - `Converter.cs`: Core logic for converting Trello data.
- **TrelloConverterTests**: Contains unit tests for the application.
  - `TrelloConverterTests.csproj`: Project file for the test project.

## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature-branch`).
3. Make your changes.
4. Commit your changes (`git commit -am 'Add new feature'`).
5. Push to the branch (`git push origin feature-branch`).
6. Create a new Pull Request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
