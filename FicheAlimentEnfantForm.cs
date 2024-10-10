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
                        Enregistrer(this);
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
    }



}

