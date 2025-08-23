In C# (.NET), there are several libraries and built-in options for XML serialization and deserialization. Here’s a comprehensive list of the most common and widely used options:

---

## **1. Built-in .NET Libraries**

| Library/Class | Description | Notes |
|--------------|------------|-------|
| **`System.Xml.Serialization.XmlSerializer`** | The standard .NET XML serializer. Supports attributes for customization. | Simple, attribute-based, but slower for large objects. |
| **`System.Runtime.Serialization.DataContractSerializer`** | Part of WCF, supports both XML and binary. | Less flexible than `XmlSerializer`, but faster. |
| **`System.Runtime.Serialization.NetDataContractSerializer`** | Similar to `DataContractSerializer`, but preserves type information. | Useful for round-trip serialization. |
| **`System.Xml.XmlReader`/`XmlWriter`** | Low-level, forward-only XML reading/writing. | High performance, manual control. |
| **`System.Xml.Linq.XDocument`/`XElement`** | LINQ to XML, provides a DOM-like API. | Good for manual XML manipulation. |

---

## **2. Third-Party Libraries**

| Library | Description | Notes |
|---------|------------|-------|
| **[Newtonsoft.Json (Xml)](https://www.newtonsoft.com/json/help/html/ConvertingJSONandXML.htm)** | Supports XML via `XmlNodeConverter`. | Mostly JSON-focused, but can handle XML. |
| **[ServiceStack.Text](https://github.com/ServiceStack/ServiceStack.Text)** | High-performance XML/JSON/CSV serializer. | Fast, supports POCOs. |
| **[Protobuf-net](https://github.com/protobuf-net/protobuf-net)** | Primarily for protobuf, but can do XML. | Not recommended for XML, but possible. |
| **[YAXLib](https://github.com/sina-irv/YAXLib)** | Yet Another XML Serializer. | Supports complex scenarios, attributes, collections. |
| **[XmlSerializerFormat (WCF)](https://docs.microsoft.com/en-us/dotnet/framework/wcf/feature-details/using-the-xmlserializer-class)** | WCF-specific XML serialization. | Used in WCF services. |
| **[SharpYaml](https://github.com/xoofx/sharpyaml)** | YAML library, but can convert to/from XML. | Indirect XML support. |
| **[XmlSchemaClassGenerator](https://github.com/mganss/XmlSchemaClassGenerator)** | Generates C# classes from XSD. | Useful for schema-first approaches. |

---

## **3. Code Generation Tools**

| Tool | Description | Notes |
|------|------------|-------|
| **`xsd.exe`** | Generates C# classes from XSD schemas. | Built into .NET SDK. |
| **`svcutil.exe`** | Generates classes from WSDL/XSD (WCF). | For SOAP/WCF services. |

---

## **4. Modern/Alternative Options**

| Library | Description | Notes |
|---------|------------|-------|
| **[System.Text.Json (XML support via community)](https://github.com/dotnet/runtime/issues/30902)** | Native high-performance JSON serializer, but XML support is limited/community-driven. | Not recommended for XML. |
| **[Utf8Xml](https://github.com/neuecc/Utf8Xml)** | High-performance, zero-allocation XML reader/writer. | Low-level, not a full serializer. |
| **[Sylvan.Data.Csv](https://github.com/MikeHadland/Sylvan)** | Primarily for CSV, but can be used for XML via conversion. | Not a direct XML serializer. |

---

## **Summary Table**

| Use Case | Recommended Library |
|----------|---------------------|
| Simple, attribute-driven | `XmlSerializer` |
| High performance, WCF | `DataContractSerializer` |
| Manual XML control | `XmlReader`/`XmlWriter`, `XDocument` |
| Complex scenarios, attributes | YAXLib |
| Schema-first | `xsd.exe`, XmlSchemaClassGenerator |
| High performance, low-level | Utf8Xml |
| JSON-focused, but XML possible | Newtonsoft.Json |

---