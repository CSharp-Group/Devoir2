using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FicheAliments
{
    public partial class FicheAlimentEnfantForm : Form
    {
        public static int numero = 1;
        #region Variables
        private bool enregistrementBool = false;
        private bool modificationBool = false;
        #endregion

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

        #region Propriétés
        public bool Enregistrement
        {
            get
            {
                return enregistrementBool;
            }
            set
            {
                enregistrementBool = value;
            }
        }

        public bool Modification
        {
            get
            {
                return modificationBool;
            }
            set
            {
                modificationBool = value;
            }
        }
        #endregion

        public void Enregistrer(FicheAlimentEnfantForm alimentEnfant)
        {
            if (alimentEnfant.infoRichTextBox.Modified || Modification)
            {
                if (!Enregistrement)
                {
                    EnregistrerSous(alimentEnfant);
                }
                else
                {
                    RichTextBox ortf = new RichTextBox
                    {
                        Rtf = alimentEnfant.infoRichTextBox.Rtf
                    };

                    ortf.SelectionStart = 0;
                    ortf.SelectionLength = 0;
                    ortf.SelectedText = alimentEnfant.nomTextBox.Text + Environment.NewLine;

                    ortf.SaveFile(alimentEnfant.Text);

                    Modification = false;
                    alimentEnfant.infoRichTextBox.Modified = false;
                }
            }
        }

        public void EnregistrerSous(FicheAlimentEnfantForm alimentEnfant)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Fichiers RTF|*.rtf";
                sfd.Title = "Enregistrer sous";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (Path.GetExtension(sfd.FileName).ToLower() != ".rtf")
                    {
                        MessageBox.Show("Veuillez choisir un fichier avec l'extension .rtf.");
                        return;
                    }

                    alimentEnfant.infoRichTextBox.SaveFile(sfd.FileName);
                    alimentEnfant.Text = sfd.FileName; // Met à jour le titre du formulaire

                    Enregistrement = true;
                    Modification = false;
                    alimentEnfant.infoRichTextBox.Modified = false;
                }
            }
        }
        private void clientTextBox_TextChanged(object sender, EventArgs e)
        {
            Modification = true;
        }
    }
}
