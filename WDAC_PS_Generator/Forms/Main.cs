using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using WDAC_PS_Generator.Classes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace WDAC_PS_Generator.Forms;

public partial class Main : Form {
    private Version version = new Version(1, 0, 0, 0);
    private Guid policyID = Guid.NewGuid();
    private const string template = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
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
    private const string p7bFilter = "PKCS#7 Binary File|*.p7b";
    private const string xmlFilter = "XML File|*.xml";
    private Cert? csCert;
    private CIPolicy? ciPolicy;
    private string lastFile = "";
    private bool generateXml;
    private bool generateP7b;
    public Main() {
        InitializeComponent();
    }
    private void NewButton_Click(object sender, EventArgs e) {
        csCert = Cert.SelectCert();
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
        if (generateP7b)
            File.WriteAllBytes(Path.ChangeExtension(dialog.FileName, ".p7b"), ciPolicy.PolicyData);
        if (generateXml)
            File.WriteAllText(Path.ChangeExtension(dialog.FileName, ".xml"), ciPolicy.PolicyXml);
    }
    private void AddButton_Click(object sender, EventArgs e) {
        OpenFileDialog dialog = new() {
            Filter = generateP7b ? (generateXml ? string.Join('|', p7bFilter, xmlFilter) : p7bFilter) : xmlFilter,
            Title = "CIPolicy File To Modify"
        };
        if (dialog.ShowDialog() != DialogResult.OK)
            return;
        csCert = Cert.SelectCert();
        if (csCert is null || csCert.CA is null) {
            MessageBox.Show("No CA could be found in the selected certificate trust chain.");
            return;
        }
        Cert CA = csCert.CA;
        ciPolicy = new(File.ReadAllBytes(dialog.FileName), csCert, SmartCardCheck.Checked);
        ciPolicy.AddSigner(CA);
        if (generateP7b)
            File.WriteAllBytes(Path.ChangeExtension(dialog.FileName, ".p7b"), ciPolicy.PolicyData);
        if (generateXml)
            File.WriteAllText(Path.ChangeExtension(dialog.FileName, ".xml"), ciPolicy.PolicyXml);
    }
    private void ReplaceButton_Click(object sender, EventArgs e) {
        OpenFileDialog dialog = new() {
            Filter = generateP7b ? (generateXml ? string.Join('|', p7bFilter, xmlFilter) : p7bFilter) : xmlFilter,
            Title = "CIPolicy File To Modify"
        };
        if (dialog.ShowDialog() != DialogResult.OK)
            return;
        lastFile = dialog.FileName;
        ciPolicy = new(File.ReadAllBytes(lastFile), SmartCardCheck.Checked);
        CaSelection.DataSource = ciPolicy.GetSigners();
        CaSelection.Visible = true;
        CaLabel.Visible = true;
        ExecuteReplace.Visible = true;
    }

    private void ExecuteReplace_Click(object sender, EventArgs e) {
        string? deprecatedCA = (string?)CaSelection.SelectedValue;
        if (string.IsNullOrWhiteSpace(deprecatedCA))
            return;
        if (ciPolicy is null || !ciPolicy.SetSigningCert())
            return;
        if (ciPolicy.SigningCert.CA is null) {
            MessageBox.Show("No CA could be found in the selected certificate trust chain.");
            return;
        }
        Cert CA = ciPolicy.SigningCert.CA;
        ciPolicy.ReplaceSigner(deprecatedCA, CA);
        if (generateP7b)
            File.WriteAllBytes(Path.ChangeExtension(lastFile, ".p7b"), ciPolicy.PolicyData);
        if (generateXml)
            File.WriteAllText(Path.ChangeExtension(lastFile, ".xml"), ciPolicy.PolicyXml);
    }
    private void GenerateXml_CheckedChanged(object sender, EventArgs e) {
        generateXml = GenerateXml.Checked;
        if (!generateXml & !generateP7b)
            GenerateP7b.Checked = true;
    }
    private void GenerateP7b_CheckedChanged(object sender, EventArgs e) {
        generateP7b = GenerateP7b.Checked;
        if (!generateXml & !generateP7b)
            GenerateP7b.Checked = true;
    }
}
