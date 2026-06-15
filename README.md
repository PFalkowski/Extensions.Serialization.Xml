# Extensions.Serialization.Xml

[![CI](https://github.com/PFalkowski/Extensions.Serialization.Xml/actions/workflows/ci.yml/badge.svg)](https://github.com/PFalkowski/Extensions.Serialization.Xml/actions/workflows/ci.yml)
[![NuGet version](https://img.shields.io/nuget/v/Extensions.Serialization.Xml.svg)](https://www.nuget.org/packages/Extensions.Serialization.Xml/)
[![NuGet downloads](https://img.shields.io/nuget/dt/Extensions.Serialization.Xml.svg)](https://www.nuget.org/packages/Extensions.Serialization.Xml/)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=PFalkowski_Extensions.Serialization.Xml&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=PFalkowski_Extensions.Serialization.Xml)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=PFalkowski_Extensions.Serialization.Xml&metric=coverage)](https://sonarcloud.io/summary/new_code?id=PFalkowski_Extensions.Serialization.Xml)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://choosealicense.com/licenses/mit/)
[![Buy Me a Coffee](https://img.shields.io/badge/Buy%20Me%20a%20Coffee-support-yellow.svg)](https://www.buymeacoffee.com/piotrfalkowski)

XML serialization extension methods for `XDocument` and `XmlDocument`, plus conversion between the two.

## Usage

```csharp
// Serialize any object to XDocument
XDocument xDoc = myObject.SerializeToXDoc();

// Serialize any object to XmlDocument
XmlDocument xmlDoc = myObject.SerializeToXmlDoc();

// Deserialize from XDocument
MyType obj = xDoc.Deserialize<MyType>();

// Deserialize from XmlDocument
MyType obj = xmlDoc.Deserialize<MyType>();

// Convert between document types
XmlDocument asXmlDoc = xDoc.ToXmlDocument();
XDocument asXDoc = xmlDoc.ToXDocument();
```
