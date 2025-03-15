using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;

namespace WDAC_PS_Generator.Classes; 
public class CIPolicy {
    public string PolicyXml;
    public byte[] PolicyData;
    public Cert? SigningCert { get; internal set; }
    public bool SmartCard;
    private static readonly XNamespace ns = "urn:schemas-microsoft-com:sipolicy";
    public CIPolicy(string PolicyXml, Cert signingCert, bool smartCard = false) {
        this.PolicyXml = PolicyXml;
        SigningCert = signingCert;
        SmartCard = smartCard;
        PolicyData = ConvertXmlToP7b();
    }
    public CIPolicy(byte[] policyData, bool smartCard) : this(policyData, null, smartCard) { }

    public CIPolicy(byte[] PolicyData, Cert? signingCert, bool smartCard = false) {
        this.PolicyData = PolicyData;
        PolicyXml = ConvertP7bToXml();
        SigningCert = signingCert;
        SmartCard = smartCard;
    }
    [MemberNotNullWhen(true, nameof(SigningCert))]
    public bool SetSigningCert() {
        if ((SigningCert = Cert.SelectCert()) == null)
            return false;
        return true;
    }
    public void AddSigner(Cert CACert) {
        if (!ValidateXml(out XDocument? xmldoc) || SigningCert is null)
            return;
        if (xmldoc.Root?.Element(ns + "Signers") is null || xmldoc.Root?.Element(ns + "UpdatePolicySigners") is null)
            return;
        XElement signersNode = xmldoc.Root.Element(ns + "Signers")!;
        XElement updateNode = xmldoc.Root.Element(ns + "UpdatePolicySigners")!;
        XElement newSigner = new(ns + "Signer",
            new XAttribute("ID", CACert.Id),
            new XElement(ns + "CertRoot",
                new XAttribute("Type", "TBS"),
                new XAttribute("Value", CACert.RawData)
            )
        );
        XElement newUpdate = new(ns + "UpdatePolicySigner",
            new XAttribute("SignerID", CACert.Id)
        );
        signersNode.Add(newSigner);
        updateNode.Add(newUpdate);
        PolicyXml = ns + Environment.NewLine + xmldoc.ToString();
        PolicyData = ConvertXmlToP7b();
    }
    public List<string> GetSigners() {
        if (!ValidateXml(out XDocument? xmldoc))
            return [];
        return [.. 
            xmldoc.Descendants(ns + "Signer")
                .Select(e => e.Attribute("ID")?.Value)
                .Where(id => id is not null)
        ];
    }
    public void ReplaceSigner(string deprecatedCAName, Cert CACert) {
        if (!ValidateXml(out XDocument? xmldoc))
            return;
        if (xmldoc.Root?.Element(ns + "Signers") is null || xmldoc.Root?.Element(ns + "UpdatePolicySigners") is null)
            return;
        XElement? deprecatedSigner = xmldoc
            .Descendants(ns + "Signer")
            .FirstOrDefault(e => (string?)e.Attribute("ID") == deprecatedCAName);
        XElement? deprecatedUpdateSigner = xmldoc
            .Descendants(ns + "UpdatePolicySigner")
            .FirstOrDefault(e => (string?)e.Attribute("SignerID") == deprecatedCAName);
        if (deprecatedSigner is null || deprecatedUpdateSigner is null) {
            AddSigner(CACert);
            return;
        }
        XElement signersNode = xmldoc.Root.Element(ns + "Signers")!;
        XElement updateNode = xmldoc.Root.Element(ns + "UpdatePolicySigners")!;
        XElement newSigner = new(ns + "Signer",
            new XAttribute("ID", CACert.Id),
            new XElement(ns + "CertRoot",
                new XAttribute("Type", "TBS"),
                new XAttribute("Value", CACert.RawData)
            )
        );
        XElement newUpdate = new(ns + "UpdatePolicySigner",
            new XAttribute("SignerID", CACert.Id)
        );
        deprecatedSigner.ReplaceWith(signersNode);
        deprecatedUpdateSigner.ReplaceWith(updateNode);
        PolicyXml = ns + Environment.NewLine + xmldoc.ToString();
        PolicyData = ConvertXmlToP7b();
    }
    private bool ValidateXml([NotNullWhen(true)] out XDocument? doc) {
        try {
            doc = XDocument.Parse(PolicyXml);
            return true;
        } catch (Exception) {
            doc = null;
            return false;
        }
    }
    static RSACryptoServiceProvider GetInteractiveCspKey(X509Certificate2 cert) {
        if (cert.GetRSAPrivateKey() is not RSACryptoServiceProvider rsaCsp) {
            throw new InvalidOperationException("Certificate does not use a CSP-based private key.");
        }

        // Create a new CspParameters object with UI prompt enabled
        CspParameters cspParams = new(rsaCsp.CspKeyContainerInfo.ProviderType, rsaCsp.CspKeyContainerInfo.ProviderName) {
            KeyContainerName = rsaCsp.CspKeyContainerInfo.KeyContainerName,
            Flags = CspProviderFlags.UseExistingKey | CspProviderFlags.UseMachineKeyStore,  // Enables UI prompt
            KeyNumber = (int)KeyNumber.Signature  // Ensure signing key is used
        };

        // Return a new RSACryptoServiceProvider that forces UI prompt
        return new RSACryptoServiceProvider(cspParams);
    }
    private byte[] ConvertXmlToP7b() {
        var xmlBytes = Encoding.UTF8.GetBytes(PolicyXml);
        ContentInfo contentInfo = new(xmlBytes);
        CmsSigner signer;
        if (SigningCert is null && !SetSigningCert())
            return [];
        X509Certificate2 cert = new(SigningCert.Certificate.Export(X509ContentType.Pfx));
        if (SmartCard) {
            //using CngKey cngKey = CngKey.Open(SigningCert.Certificate.GetCertHashString(), CngProvider.MicrosoftSmartCardKeyStorageProvider);
            var rsaCsp = SigningCert.Certificate.GetRSAPrivateKey();
            signer = new(SubjectIdentifierType.IssuerAndSerialNumber, cert) {
                IncludeOption = X509IncludeOption.EndCertOnly,
                DigestAlgorithm = new Oid("2.16.840.1.101.3.4.2.1") // SHA-256
            };
            //signer.PrivateKey = new RSACng(cngKey);
            //signer.PrivateKey = rsaCsp;
        } else {
            signer = new(SigningCert.Certificate);
        }
        SignedCms signedCms = new(contentInfo, detached: false);
        signedCms.ComputeSignature(signer, false);
        return signedCms.Encode();
    }
    private string ConvertP7bToXml() {
        SignedCms signedCms = new();
        signedCms.Decode(PolicyData);
        byte[] xmlData = signedCms.ContentInfo.Content;
        return Encoding.UTF8.GetString(xmlData);
    }
}
