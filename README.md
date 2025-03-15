# WSOD_PS_Generator

WDAC policies can shift PowerShell scripts into Constrained Language Mode.

WDAC can be set to trust specific CAs. Doing so will allow PowerShell scripts signed by a Code Signing certificate with that CA as its root to run in Full Language Mode.

This utility will generate minimal XML and/pr P7B files used by WDAC to enable that trust.

The code signing certificate in question must be available, both to follow the issuance chain up to the CA, and to sign the P7B

![Screenshot of application](Docs/Screenshot.png?b)

Note that while you can choose to only emit XML files, WDAC requires P7B files. If you want to make your own from XML, or alter the XML before conversion, you can use the PowerShell command `ConvertFrom-CiPolicy -XmlFilePath <string> -BinaryFilePath <string>`
