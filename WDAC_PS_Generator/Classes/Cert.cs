using System.Security.Cryptography.X509Certificates;

namespace WDAC_PS_Generator.Classes;

public class Cert {
    private X509Certificate2 _baseCert;
    private Cert? _issuerCert;
    private bool _isCA;
    public X509Certificate2 Certificate {
        get => _baseCert;
        set {
            _baseCert = value;
        }
    }
    public Cert? Issuer => _isCA ? this : _issuerCert;
    public Cert? CA => Issuer?.Issuer;
    public bool IsCA => _isCA;
    public string RawData => BitConverter.ToString(_baseCert.RawData).Replace("-", "");
    public string SimpleName => _baseCert.GetNameInfo(X509NameType.SimpleName, false);
    public Cert(X509Certificate2 Certificate) {
        _baseCert = Certificate;
        _issuerCert = GetIssuer();
    }
    private Cert? GetIssuer() {
        if (_baseCert.IssuerName.Name == _baseCert.SubjectName.Name) {
            _isCA = true;
            return null;
        }
        using var store = new X509Store("CA",StoreLocation.LocalMachine);
        store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
        var collection = (X509Certificate2Collection)store.Certificates;
        var issuer = collection.Find(X509FindType.FindBySubjectDistinguishedName, _baseCert.IssuerName.Name, true).FirstOrDefault();
        if (issuer is null) {
            using var rootStore = new X509Store("Root",StoreLocation.LocalMachine);
            rootStore.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            collection = (X509Certificate2Collection)rootStore.Certificates;
            issuer = collection.Find(X509FindType.FindBySubjectDistinguishedName, _baseCert.IssuerName.Name, true).FirstOrDefault();
        }
        if (issuer is null) 
            return null;
        return new Cert(issuer);
    }
}
