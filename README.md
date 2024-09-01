# TrelloConverter

**TrelloConverter** is a powerful .NET application meticulously designed to transform Trello board data into a variety of formats, including CSV, Markdown, and LaTeX. Whether you are a project manager, developer, or data analyst, this tool ensures seamless conversion processes with high reliability, supported by comprehensive unit tests.

## Table of Contents

- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [Contributing](#contributing)
- [License](#license)

## Features

TrelloConverter is equipped with a robust set of features to cater to diverse data conversion needs:

- **CSV Conversion**: Effortlessly convert Trello JSON data into CSV format, allowing for easy manipulation and analysis in spreadsheet applications.
- **Markdown Generation**: Automatically generate well-structured Markdown files from Trello data, ideal for documentation and project reports.
- **LaTeX Export**: Produce high-quality LaTeX files from Trello boards, perfect for academic papers and professional presentations.
- **Customizable Conversion Options**:
  - **Preserve Original Format**: Maintain the original format of your data.
  - **Enumerate/De-enumerate**: Add or remove enumeration prefixes in task names.
  - **Reverse Order**: Reverse the order of tasks for backward planning.
  - **Auto-Close**: Option to automatically close the application after conversion.

## Screenshot

![TrelloConverter Screenshot](https://github.com/Cal-ly/TrelloConverter/blob/master/Screenshot%20TrelloConverter.png)

## Installation

### Quick Setup

Download the latest release from the [Releases](https://github.com/Cal-ly/TrelloConverter/releases) page and run the executable. No installation is required.

### Manual Setup

For those who prefer building from source, follow these steps:

#### Prerequisites

Ensure your environment is ready by installing the following:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio](https://visualstudio.microsoft.com/)

#### Clone the Repository

```bash
git clone https://github.com/Cal-ly/TrelloConverter.git
cd TrelloConverter
```

#### Build the Solution

Open the solution in Visual Studio and build it. This will restore all necessary NuGet packages and compile the project, preparing it for execution.

## Usage

### Running the Application

To run the application in a development environment:

1. Open the solution in Visual Studio.
2. Set `TrelloConverter` as the startup project.
3. Press `F5` to run the application.

### Converting Trello Data

1. **Choose a File**: Select the "Choose File" button to open a dialog and select your Trello JSON export.
2. **Set Conversion Options**: Customize the conversion settings to fit your needs, such as output format and task order.
3. **Convert**: Click the "Convert" button to generate output files in your chosen formats.

### Unit Tests

To ensure the application runs smoothly and accurately:

1. Open the Test Explorer in Visual Studio.
2. Run all tests to verify the functionality of the conversion processes.

## Project Structure

The project is organized to facilitate both development and testing:

- **TrelloConverter**: This directory contains the core application code.
  - `Converter.cs`: Implements the primary logic for converting Trello JSON data into different formats.
- **TrelloConverterTests**: Houses the unit tests to validate the application's functionality.
  - `ConverterTests.cs`: Contains test cases for the `Converter.cs` class, ensuring reliable conversions.
- **TrelloConverter.Models**: Defines the base classes and data models used for manipulating and structuring Trello data.

## Contributing

We welcome contributions to TrelloConverter! To contribute, please:

1. Fork this repository.
2. Create a new branch for your feature or bug fix (`git checkout -b feature-branch`).
3. Make and commit your changes (`git commit -am 'Add new feature'`).
4. Push your branch to GitHub (`git push origin feature-branch`).
5. Submit a Pull Request with a detailed explanation of your changes.

## License

TrelloConverter is licensed under the MIT License, a permissive license that allows for commercial and private use, modification, and distribution. For more details, please refer to the [LICENSE](LICENSE) file.
