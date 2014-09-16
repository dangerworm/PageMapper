using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using HtmlAgilityPack;
using RITCHARD_Data;

namespace Sandbox
{
    public partial class frmInterface : Form
    {
        public frmInterface()
        {
            InitializeComponent();
        }

        private void btnGenerateMap_Click(object sender, EventArgs e)
        {
            try
            {
                Map pageMap;

                if (txtTargetCode.Text.StartsWith("<"))
                {
                    pageMap = Mapper.GenerateMapFromCodeTarget(txtBaseURL.Text.Trim(), txtQuery.Text.Trim(), txtTargetCode.Text.Trim());
                }
                else
                {
                    pageMap = Mapper.GenerateMapFromTextTarget(txtBaseURL.Text.Trim(), txtQuery.Text.Trim(), txtTargetCode.Text.Trim());
                }

                DialogResult result = MessageBox.Show(pageMap.GetText(), "Is this what you want?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    pageMap.SaveToDatabase(txtName.Text);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            PageMap map = Mapper.RetrieveMapFromDatabase(txtName.Text);
            
            HtmlWeb Browser = new HtmlWeb();
            var Document = Browser.Load(map.BaseURL + txtQuery.Text);
            
            List<string> bits = Mapper.GetRelevantTextFromDocumentUsingMap(Document, map);
            
            string bitString = "";
            foreach (string bit in bits)
                bitString += bit + Environment.NewLine;
            MessageBox.Show(bitString);
        }
    }
}
