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
        #region variable 
        private bool enregistrementBool = false;
        private bool modificationBool = false;
        public static int numeroInt = 1;
        #endregion

        public FicheAlimentEnfantForm()
        {
            InitializeComponent();
        }

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

        public static int Numero()
        {
            try
            {
                return numeroInt++;
            }
            catch
            {
                throw new IndexOutOfRangeException("Erreur");
            }

        }

        public void EnregistrerSous()
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Rich Text Format (*.rtf)|*.rtf|Tous les fichiers (*.*)|*.*",
                Title = "Enregistrer sous"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Vérifier l'extension du fichier
                    string extension = System.IO.Path.GetExtension(sfd.FileName);
                    if (extension.ToLower() != ".rtf" && extension.ToLower() != ".txt")
                    {
                        MessageBox.Show("Format de fichier non valide. Veuillez utiliser un fichier .rtf ou .txt.", "Erreur de sauvegarde", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Copier le contenu dans un RichTextBox temporaire (si nécessaire)
                    // Cela peut ne pas être nécessaire selon votre logique.
                    using (RichTextBox tempRichTextBox = new RichTextBox())
                    {
                        tempRichTextBox.Text = infoRichTextBox.Text;

                        // Sauvegarder le fichier
                        tempRichTextBox.SaveFile(sfd.FileName);
                    }

                    // Mettre à jour les propriétés
                    this.Text = sfd.FileName;
                    enregistrementBool = true;
                    modificationBool = false;
                    infoRichTextBox.Modified = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la sauvegarde du fichier : {ex.Message}", "Erreur de sauvegarde", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
        private void clientTextBox_TextChanged(object sender, EventArgs e)
        {
            Modification = true;
        }

        private void FicheAlimentEnfantForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult oDialogResult;

            if (infoRichTextBox.Modified || Modification)
            {
                oDialogResult = MessageBox.Show("Voulez vous enregistrer les modification?", "Modification", MessageBoxButtons.YesNoCancel);

                switch (oDialogResult)
                {
                    case DialogResult.Yes:
                        //Enregistrer() 
                        //Doit attendre que la partie Enregister du ChildForm sois fini
                        this.Dispose();
                        break;

                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;

                    case DialogResult.No:
                        this.Dispose();
                        break;
                }
            }
        }
    }



}

