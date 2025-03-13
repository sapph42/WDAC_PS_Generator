using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using WDAC_PS_Generator.Classes;

namespace WDAC_PS_Generator.Forms;

public partial class Main : Form {
    private Version version = new Version(1,0,0,0);
    private Guid policyID = Guid.NewGuid();
    private string template = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<SiPolicy xmlns=""urn:schemas-microsoft-com:sipolicy"" PolicyType=""Base Policy"">
	<PolicyID>{{{0}}}</PolicyID>
	<BasePolicyID>{{{0}}}</BasePolicyID>
	<PlatformID>{{2E07F7E4-194C-4D20-B7C9-6F44A6C5A234}}</PlatformID>
	<VersionEx>{1}</VersionEx>
	<Rules />
	<Signers>
		<Signer ID=""{2}"" Name=""{2}"">
			<CertRoot Type=""TBS"" Value=""{3}"" />
		</Signer>
	</Signers>
	<UpdatePolicySigners>
		<UpdatePolicySigner SignerId=""{2}"" />
	</UpdatePolicySigners>
</SiPolicy>
";
    private Cert? csCert;
    private CIPolicy? ciPolicy;
    public Main() {
        InitializeComponent();
    }
    private void SelectCert() {
        using X509Store store = new("MY", StoreLocation.CurrentUser);
        store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
        var allCerts = (X509Certificate2Collection)store.Certificates;
        X509Certificate2Collection col = X509Certificate2UI.SelectFromCollection(
            allCerts,
            "Certificate Selection",
            "Select a code signing certificate to extrapolate a root CA from.",
            X509SelectionFlag.SingleSelection);
        if (col.Count == 0)
            return;
        csCert = new Cert(col.First());
    }
    private void NewButton_Click(object sender, EventArgs e) {
        SelectCert();
        policyID = Guid.NewGuid();
        if (csCert is null || csCert.CA is null) {
            MessageBox.Show("No CA could be found in the selected certificate trust chain.");
            return;
        }
        Cert CA = csCert.CA;
        string id = CA.SimpleName.Replace(" ", "_");
        string xml = string.Format(template, policyID.ToString(), version.ToString(), id, CA.RawData);
        ciPolicy = new(xml, csCert, SmartCardCheck.Checked);
        SaveFileDialog dialog = new() {
            Filter = "*.p7b"
        };
        if (dialog.ShowDialog() != DialogResult.OK)
            return;
        File.WriteAllBytes(dialog.FileName, ciPolicy.PolicyData);
    }

    private void AddButton_Click(object sender, EventArgs e) {
        OpenFileDialog dialog = new() {
            Filter = "*.p7b",
            Title = "CIPolicy File To Modify"
        };
        if (dialog.ShowDialog() != DialogResult.OK)
            return;
        SelectCert();
        if (csCert is null || csCert.CA is null) {
            MessageBox.Show("No CA could be found in the selected certificate trust chain.");
            return;
        }
        Cert CA = csCert.CA;
        ciPolicy = new(File.ReadAllBytes(dialog.FileName), csCert, SmartCardCheck.Checked);
        ciPolicy.AddSigner(CA);
    }
}
