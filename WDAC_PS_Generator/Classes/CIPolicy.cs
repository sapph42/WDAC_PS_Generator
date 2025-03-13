using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;

namespace WDAC_PS_Generator.Classes; 
public class CIPolicy {
    public string PolicyXml;
    public byte[] PolicyData;
    public Cert SigningCert;
    public bool SmartCard;
    public CIPolicy(string PolicyXml, Cert signingCert, bool smartCard = false) {
        this.PolicyXml = PolicyXml;
        SigningCert = signingCert;
        PolicyData = ConvertXmlToP7b();
        SmartCard = smartCard;
    }
    public CIPolicy(byte[] PolicyData, Cert signingCert, bool smartCard = false) {
        this.PolicyData = PolicyData;
        PolicyXml = ConvertP7bToXml();
        SigningCert = signingCert;
        SmartCard = smartCard;
    }
    public void AddSigner(Cert CACert) {
        XDocument xmldoc = XDocument.Parse(PolicyXml);
        XNamespace ns = "urn:schemas-microsoft-com:sipolicy";
        if (xmldoc.Root?.Element(ns + "Signers") is null || xmldoc.Root?.Element(ns + "UpdatePolicySigners") is null)
            return;
        string id = CACert.SimpleName.Replace(" ", "_");
        string raw = CACert.RawData;
        XElement signersNode = xmldoc.Root.Element(ns + "Signers")!;
        XElement updateNode = xmldoc.Root?.Element(ns + "UpdatePolicySigners")!;
        XElement newSigner = new(ns + "Signer",
            new XAttribute("ID", id),
            new XElement(ns + "CertRoot",
                new XAttribute("Type", "TBS"),
                new XAttribute("Value", raw)
            )
        );
        XElement newUpdate = new(ns + "UpdatePolicySigner",
            new XAttribute("SignerID", id)
        );
        signersNode.Add(newSigner);
        updateNode.Add(newUpdate);
        PolicyXml = xmldoc.ToString();
        PolicyData = ConvertXmlToP7b();
    }
    private byte[] ConvertXmlToP7b() {
        var xmlBytes = Encoding.UTF8.GetBytes(PolicyXml);
        ContentInfo contentInfo = new(xmlBytes);
        CmsSigner signer;
        if (SmartCard) {
            using RSA rsa = SigningCert.Certificate.GetRSAPrivateKey();
            //TODO - Collect PIN?
            signer = new(SubjectIdentifierType.IssuerAndSerialNumber, SigningCert.Certificate) {
                IncludeOption = X509IncludeOption.EndCertOnly
            };
        } else {
            signer = new(SigningCert.Certificate);
        }
        SignedCms signedCms = new(contentInfo, detached: false);
        signedCms.ComputeSignature(signer);
        return signedCms.Encode();
    }
    private string ConvertP7bToXml() {
        SignedCms signedCms = new();
        signedCms.Decode(PolicyData);
        byte[] xmlData = signedCms.ContentInfo.Content;
        return Encoding.UTF8.GetString(xmlData);
    }
}
