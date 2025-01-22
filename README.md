# XrmBedrock.Extensions.Client

## Overview
A powerful extension library for Dynamics 365/Dataverse client operations that simplifies authentication and service configuration through dependency injection. This library provides enhanced functionality for connecting to and interacting with the Microsoft Dataverse/Dynamics 365 platform.

## Installation

```powershell
Install-Package XrmBedrock.Extensions.Client
```

## Features
- Simplified Dependency Injection setup for Dataverse connections
- Built-in token provider implementations for client credentials flow
- Automatic tenant discovery
- Memory-cached token management
- Support for affinity cookies
- Interface-based token provider architecture for custom authentication scenarios

## Usage

### Basic Setup

1. Configure services in your `Program.cs` or `Startup.cs`:

```csharp
services.Configure<OrganizationServiceOptions>(configuration.GetSection("Dataverse"));
services.AddDataverse();
```

2. Add configuration to your `appsettings.json`:

```json
{
  "Dataverse": {
    "DataverseUrl": "https://your-org.crm.dynamics.com",
    "EnableAffinityCookie": true
  },
  "DataverseClientId": "your-client-id",
  "DataverseClientSecret": "your-client-secret"
}
```

### Using the Service

```csharp
public class MyService
{
    private readonly IOrganizationServiceAsync _service;

    public MyService(IOrganizationServiceAsync service)
    {
        _service = service;
    }

    public async Task DoSomething()
    {
        // Use the service to interact with Dataverse
        var result = await _service.RetrieveAsync("account", Guid.NewGuid(), new ColumnSet(true));
    }
}
```

## Requirements
- .NET 8.0+
- Microsoft.PowerPlatform.Dataverse.Client (1.1.27 or higher)
- Microsoft.Extensions.DependencyInjection.Abstractions (9.0.1 or higher)
- Valid Dynamics 365/Dataverse environment
- Application registration in Azure AD with appropriate permissions

## Authentication
The library supports client credentials flow out of the box through the `ClientCredentialsTokenProvider`. It automatically:
- Discovers tenant ID if not provided
- Manages token lifecycle
- Handles scope building for different Dataverse environments

## Contributing
1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

### Development Setup
1. Clone the repository including submodules:
```bash
git clone --recursive https://github.com/yourusername/XrmBedrock.Extensions.Client.git
```

2. Open the solution in Visual Studio 2022 or later
3. Restore NuGet packages
4. Build the solution

## License
MIT License

Copyright (c) 2024

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

---
Please note: This package extends the functionality of [XrmBedrock](https://github.com/delegateas/XrmBedrock) for client operations.
