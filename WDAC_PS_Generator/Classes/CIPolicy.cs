using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Xml.Linq;

namespace WDAC_PS_Generator.Classes; 
public class CIPolicy {
    public string PolicyXml;
    public byte[] PolicyData;
    public Cert SigningCert;
    public CIPolicy(string PolicyXml, Cert signingCert) {
        this.PolicyXml = PolicyXml;
        PolicyData = ConvertXmlToP7b();
        SigningCert = signingCert;
    }
    public CIPolicy(byte[] PolicyData, Cert signingCert) {
        this.PolicyData = PolicyData;
        PolicyXml = ConvertP7bToXml();
        SigningCert = signingCert;
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
        SignedCms signedCms = new(contentInfo, detached: false);
        CmsSigner signer = new(SigningCert.Certificate);
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
