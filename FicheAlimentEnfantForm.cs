using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FicheAliments
{
    public partial class FicheAlimentEnfantForm : Form
    {
        public static int numero = 1;

        public FicheAlimentEnfantForm()
        {
            InitializeComponent();
        }

        public static int Numero()
        {
            try
            {
                return numero++;
            }
            catch
            {
                throw new IndexOutOfRangeException("Erreur");
            }
        }

        private void ouvrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FicheAlimentEnfantForm oEnfant = new FicheAlimentEnfantForm();
                oEnfant.MdiParent = this;
                oEnfant.Text = ofd.FileName;

                // oEnfant.clientRichTextBox.LoadFile(ofd.FileName);

                RichTextBox ortf = new RichTextBox();

                ortf.LoadFile(ofd.FileName);
                oEnfant.nomTextBox.Text = ortf.Lines[0];

                ortf.SelectionStart = 0;
                ortf.SelectionLength = ortf.Lines[0].Length + 1; // ne pas oublier changement de ligne
                ortf.SelectedText = String.Empty;

                oEnfant.infoRichTextBox.Rtf = ortf.Rtf;

                //oEnfant.Enregistrement = true;
                oEnfant.infoRichTextBox.Modified = false;
                //oEnfant.Modification = false;

                oEnfant.Show();
            }
        }
    }
}
