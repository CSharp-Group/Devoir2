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
        #region Variables
        
        public static int numeroInt = 1;
        private bool enregistrementBool = false;
        private bool modificationBool = false;

        #endregion

        #region Propriétés

        public bool ModeInsertion
        {
            get
            {
                return infoRichTextBox.SelectionProtected;
            }
            set
            {
                infoRichTextBox.SelectionProtected = value;
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

        #region Initialization

        public FicheAlimentEnfantForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Methodes

        #region Text Changed
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            Modification = true;
        }
        #endregion

        #region FormClosing
        private void EnfantFormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult oDialogResult;

            try
            {
                if (infoRichTextBox.Modified || Modification)
                {
                    oDialogResult = MessageBox.Show("Voulez vous enregistrer les modification?", "Modification", MessageBoxButtons.YesNoCancel);

                    switch (oDialogResult)
                    {
                        case DialogResult.Yes:
                            Enregistrer(); 
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
            catch (Exception ex) 
            {
                MessageBox.Show($"Erreur: {ex.Message}");
            }
            
        }
        #endregion

        #region Enregistrer
        public void Enregistrer()
        {
            try
            {
                if (infoRichTextBox.Modified || Modification)
                {
                    if (!Enregistrement)
                        EnregistrerSous();
                    else
                    {
                        RichTextBox ortf = new RichTextBox();

                        ortf.Rtf = infoRichTextBox.Rtf;

                        ortf.SelectionStart = 0;
                        ortf.SelectionLength = 0;
                        ortf.SelectedText = nomTextBox.Text + Environment.NewLine +
                            prenomTextBox.Text + Environment.NewLine +
                            telephoneMaskedTextBox.Text;

                        ortf.SaveFile(this.Text);

                        Modification = false;
                        infoRichTextBox.Modified = false;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}");
            }
        }
        #endregion

        #region EnregistrerSous
        public void EnregistrerSous()
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();

                sfd.Filter = "Fichiers RTF|*.rtf";
                sfd.Title = "Enregistrer sous";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (Path.GetExtension(sfd.FileName).ToLower() != ".rtf")
                    {
                        MessageBox.Show("Veuillez choisir un fichier avec l'extension .rtf.");
                        return;
                    }
                    else
                    {
                        infoRichTextBox.SaveFile(sfd.FileName);
                        this.Text = sfd.FileName; // Met à jour le titre du formulaire

                        Enregistrement = true;
                        Modification = false;
                        infoRichTextBox.Modified = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}");
            }
        }
        #endregion

        #region SelectionChanged
        private void infoRichTextBox_SelectionChanged(object sender, EventArgs e)
        {
            Parent oParent = this.MdiParent as Parent;

            oParent.boldToolStripButton.Checked = infoRichTextBox.SelectionFont.Bold;
            oParent.italicToolStripButton.Checked = infoRichTextBox.SelectionFont.Italic;
            oParent.underlineToolStripButton.Checked = infoRichTextBox.SelectionFont.Underline;

            if(Clipboard.ContainsText() || Clipboard.ContainsImage())
            {
                oParent.collerToolStripMenuItem.Enabled = true;
                oParent.collerToolStripButton.Enabled = true;
                oParent.couperToolStripMenuItem.Enabled = true;
                oParent.copierToolStripMenuItem.Enabled = true;
            }
            else
            {
                oParent.collerToolStripMenuItem.Enabled = false;
                oParent.collerToolStripButton.Enabled = false;
                oParent.couperToolStripMenuItem.Enabled = false;
                oParent.copierToolStripMenuItem.Enabled = false;

            }

            oParent.copierToolStripMenuItem.Enabled = infoRichTextBox.SelectionLength > 0;
            oParent.couperToolStripMenuItem.Enabled = infoRichTextBox.SelectionLength > 0;
            oParent.copierToolStripButton.Enabled = infoRichTextBox.SelectionLength > 0;
            oParent.couperToolStripButton.Enabled = infoRichTextBox.SelectionLength > 0;

            if (infoRichTextBox.SelectionAlignment == HorizontalAlignment.Left)
            {
                oParent.leftAlignToolStripButton.Checked = true; 
                oParent.centerAlignToolStripButton.Checked = false;
                oParent.rightAlignToolStripButton.Checked = false;
            }
            else if (infoRichTextBox.SelectionAlignment == HorizontalAlignment.Center)
            {
                oParent.leftAlignToolStripButton.Checked = false; 
                oParent.centerAlignToolStripButton.Checked = true; 
                oParent.rightAlignToolStripButton.Checked = false;
            }
            else if (infoRichTextBox.SelectionAlignment == HorizontalAlignment.Right)
            {
                oParent.leftAlignToolStripButton.Checked = false; 
                oParent.centerAlignToolStripButton.Checked = false;
                oParent.rightAlignToolStripButton.Checked = true; 
            }
        }
        #endregion

        #region ChangerAttributsPolice
        public void ChangerAttributsPolice(FontStyle style)
        {
            try
            {
                if (infoRichTextBox.SelectionFont.FontFamily.IsStyleAvailable(style))
                    infoRichTextBox.SelectionFont = new Font(infoRichTextBox.SelectionFont,
                        infoRichTextBox.SelectionFont.Style | style);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}");
            }
        }
        #endregion

        #region ClientActivated
        private void ClientActivated(object sender, EventArgs e)
        {
            infoRichTextBox_SelectionChanged(null, null);
        }
        #endregion

        #endregion

    }
}
