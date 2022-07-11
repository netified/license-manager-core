# Netified.LicenseManager.Core

![GitHub](https://img.shields.io/github/license/netified/license-manager-core?style=for-the-badge)
![GitHub Sponsors](https://img.shields.io/github/sponsors/netified?style=for-the-badge)
![GitHub Workflow Status](https://img.shields.io/github/workflow/status/netified/license-manager-core/Continuous%20integration?style=for-the-badge)
![Nuget](https://img.shields.io/nuget/dt/Netified.LicenseManager.Core?style=for-the-badge)
![Nuget](https://img.shields.io/nuget/v/Netified.LicenseManager.Core?style=for-the-badge)

## Installation

The library is available as a nuget package. You can install it as any other nuget package from your IDE, try to search by `Netified.LicenseManager.Core`. You can find package details [on this webpage](https://github.com/netified/license-manager-core).

```xml
// Package Manager
Install-Package Netified.LicenseManager.Core

// .NET CLI
dotnet add package Netified.LicenseManager.Core

// Package reference in .csproj file
<PackageReference Include="Netified.LicenseManager.Core" Version="1.0.0" />
```

## Usage

### Create a private and public key for your product

This project uses the Elliptic Curve Digital Signature Algorithm (ECDSA) to ensure the license cannot be altered after creation.

First you need to create a new public/private key pair for your product:

```csharp
var keyGenerator = KeyGenerator.Create(); 
var keyPair = keyGenerator.GenerateKeyPair(); 
var privateKey = keyPair.ToEncryptedPrivateKeyString(passPhrase);  
var publicKey = keyPair.ToPublicKeyString();
```

Store the private key securely and distribute the public key with your product.
Normally you create one key pair for each product, otherwise it is possible to use a license with all products using the same key pair.
If you want your customer to buy a new license on each major release you can create a key pair for each release and product.

### Create the license generator

Now we need something to generate licenses.
This could be easily done with the `LicenseFactory`:

```csharp
var license = License.New()  
    .WithUniqueIdentifier(Guid.NewGuid())  
    .As(LicenseType.Trial)  
    .ExpiresAt(DateTime.Now.AddDays(30))
    .WithProductFeatures(new Dictionary<string, string>
    {  
        {"Sales Module", "yes"},  
        {"Purchase Module", "yes"},  
        {"Maximum Transactions", "10000"}  
    })  
    .LicensedTo("John Doe", "john.doe@example.com")  
    .CreateAndSignWithPrivateKey(privateKey, passPhrase);
```

Now you can take the license and save it to a file:

```csharp
File.WriteAllText("License.lic", license.ToString(), Encoding.UTF8);
```

or

```csharp
license.Save(xmlWriter);
```

### Validate the license in your application

The easiest way to assert the license is in the entry point of your application.

First load the license from a file or resource:

```csharp
var license = License.Load(...);
```

Then you can assert the license:

```csharp
var validationFailures = license.Validate()  
    .ExpirationDate()  
    .When(lic => lic.Type == LicenseType.Trial)  
    .And()  
    .Signature(publicKey)  
    .AssertValidLicense();
```

Standard.Licensing will not throw any Exception and just return an enumeration of validation failures.

Now you can iterate over possible validation failures:

```csharp
foreach (var failure in validationFailures)
{
     Console.WriteLine(failure.GetType().Name + ": " + failure.Message + " - " + failure.HowToResolve)
}
```

Or simply check if there is any failure:

```csharp
if (validationFailures.Any())
   // ...
```

Make sure to call `validationFailures.ToList()` or `validationFailures.ToArray()` before using the result multiple times.

## Please show the value

Choosing a project dependency could be difficult. We need to ensure stability and maintainability of our projects.
Surveys show that GitHub stars count play an important factor when assessing library quality.

⭐ Please give this repository a star. It takes seconds and help thousands of developers! ⭐

## Support development

It doesn't matter if you are a professional developer, creating a startup or work for an established company.
All of us care about our tools and dependencies, about stability and security, about time and money we can safe, about quality we can offer.
Please consider sponsoring to give me an extra motivational push to develop the next great feature.

> If you represent a company, want to help the entire community and show that you care, please consider sponsoring using one of the higher tiers.
Your company logo will be shown here for all developers, building a strong positive relation.

## How to Contribute

Everyone is welcome to contribute to this project! Feel free to contribute with pull requests, bug reports or enhancement suggestions.

## Bugs and Feedback

For bugs, questions and discussions please use the [GitHub Issues](https://github.com/netified/license-manager-core/issues).

## License

This project is licensed under [MIT License](https://github.com/netified/license-manager-core/blob/main/LICENSE).