ToFormatSharp
============

![](https://img.shields.io/nuget/v/ToFormatSharp.Core)
![](https://img.shields.io/nuget/dt/ToFormatSharp.Core?color=laim)
![](https://img.shields.io/appveyor/build/XeroXP/toformatsharp/master)
![](https://img.shields.io/appveyor/tests/XeroXP/toformatsharp/master)

Adds a `ToFormat` instance method to `Big` or `BigDecimal`.


Documentation
=============

* [Installation](#installation)
* [Use](#use)
* [System requirements](#system-requirements)
* [Development and testing](#development-and-testing)
* [Contributors](#contributors)


Installation
------------

You can install ToFormatSharp for Big or BigDecimal via [NuGet](https://www.nuget.org/):

package manager:

    $ PM> Install-Package BigSharp.ToFormat

    $ PM> Install-Package DecimalSharp.ToFormat

NET CLI:

	$ dotnet add package BigSharp.ToFormat

	$ dotnet add package DecimalSharp.ToFormat

or [download source code](../../releases).


Use
-----

```csharp
using BigSharp;
using BigSharp.ToFormat;

var format = new FormatOptions
{
	DecimalSeparator = ".",
	GroupSeparator = ",",
	GroupSize = 3,
	SecondaryGroupSize = 0,
	FractionGroupSeparator = "",
	FractionGroupSize = 0
};

x = new Big(9876.54321)
x.ToFormat(2, format)                       // "9,876.54"

format.DecimalSeparator = ","
format.GroupSeparator = " "
format.groupSize = 2
x.ToFormat(1, format)    // "98 76,5"
```


System requirements
-------------------

ToFormatSharp supports:

* Net 6


Development and testing
------------------------

Make sure to rebuild projects every time you change code for testing.

### Testing

To run tests:

    $ dotnet test


Contributors
------------

[XeroXP](../../../).
