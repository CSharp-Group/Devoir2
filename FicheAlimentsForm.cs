#region Commentaires
/*
 Programmeurs :  Avery Doucet, Albert Jean-Michiel, Alou Marie-Louise, Umunoza Adolphe, Annoir Idrissa
 Date         :  4 octobre 2024
 But          :  Devoir 2 Phase A 
 Solution     :  FicheAliment.sln
 Project      :  FicheAliment.csproj
 Classe       :  FicheAliment.cs
 */
#endregion

#region Using...
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

using g = FicheAliments.ClassGeneralcs;

#endregion

namespace FicheAliments
{
    public partial class Parent : Form
    {
        #region Variables
        private bool enregistrementBool = false;
        private bool modificationBool = false;
        #endregion

        #region Load
        public Parent()
        {
            InitializeComponent();
        }

        private void Parent_Load(object sender, EventArgs e)
        {
            AssocierImage();
        }
        #endregion

        #region Images
        private void AssocierImage()
        {
            boldToolStripButton.Image = Properties.Resources.boldhs;
            italicToolStripButton.Image = Properties.Resources.ItalicHS;
            underlineToolStripButton.Image = Properties.Resources.Underline_11700_32;
            leftAlignToolStripButton.Image = Properties.Resources.AlignTableCellMiddleLeftJustHS;
            centerAlignToolStripButton.Image = Properties.Resources.AlignTableCellMiddleCenterHS;
            rightAlignToolStripButton.Image = Properties.Resources.AlignTableCellMiddleRightHS;
            helpToolStripButton.Image = Properties.Resources.Help;
        }
        #endregion

        #region Methodes

        #region Formulaire enfant
        private void FichierNouveauDocument_Click(object sender, EventArgs e)
        {
            FicheAlimentEnfantForm oAliment;

            try
            {
                oAliment = new FicheAlimentEnfantForm();
                oAliment.Text = oAliment.Text + " " + FicheAlimentEnfantForm.Numero().ToString();
                oAliment.MdiParent = this;
                oAliment.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}");
            }
        }
        #endregion

        #region Layout
        private void layoutMdiMenuItems_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;

            int layoutInt;

            layoutInt = fenetreToolStripMenuItem.DropDownItems.IndexOf(item);
            Console.WriteLine(layoutInt);

            this.LayoutMdi((MdiLayout)layoutInt);

            g.EnleverCrochetSousMenu(fenetreToolStripMenuItem);
            (item).Checked = true;
        }
        #endregion

        #region Affichage
        private void affichageMenuStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;

            int renderModeInt;

            renderModeInt = barreOutilsToolStripMenuItem.DropDownItems.IndexOf(item) + 1;
            Console.WriteLine(renderModeInt);

            ficheAlimentsStatusStrip.RenderMode = (ToolStripRenderMode)renderModeInt;

            g.EnleverCrochetSousMenu(barreOutilsToolStripMenuItem);
            (item).Checked = true;
        }
        #endregion

        #region ToolStripPanel
        private void updateToolStripPanel(object sender, ControlEventArgs e)
        {
            string parentName = e.Control.Parent.Name;
            ToolStrip item = e.Control as ToolStrip;
            if (parentName == "leftToolStripPanel" || parentName == "rightToolStripPanel")
            {
                item.TextDirection = ToolStripTextDirection.Vertical90;
                item.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;

                if (item is MenuStrip)
                {
                    questionToolStripTextBox.Visible = false;
                }
                else
                {
                    toolStripComboBox1.Visible = false;
                    toolStripComboBox2.Visible = false;
                }
            }
            else if (parentName == "topToolStripPanel" || parentName == "bottomToolStripPanel")
            {
                item.TextDirection = ToolStripTextDirection.Horizontal;
                item.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;

                if (item is MenuStrip)
                {
                    questionToolStripTextBox.Visible = true;
                }
                else
                {
                    toolStripComboBox1.Visible = true;
                    toolStripComboBox2.Visible = true;
                }
            }
        }
        #endregion

        #endregion

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

        #region Méthodes partagées


        public void Ouvrir()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Fichiers RTF|*.rtf",
                Title = "Ouvrir un fichier"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (Path.GetExtension(openFileDialog.FileName).ToLower() != ".rtf")
                    {
                        MessageBox.Show("Veuillez choisir un fichier avec l'extension .rtf.");
                        return;
                    }

                    FicheAlimentEnfantForm alimentEnfant = new FicheAlimentEnfantForm();
                    alimentEnfant.clientRichTextBox.LoadFile(openFileDialog.FileName);
                    alimentEnfant.Show();

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de l'ouverture du fichier : {ex.Message}");
                }
            }
            #endregion

            #region Méthodes d'enregistrement


            public void Enregistrer(FicheAlimentEnfantForm alimentEnfant)
            {
                if (alimentEnfant.clientRichTextBox.Modified || Modification)
                {
                    if (!Enregistrement)
                    {
                        EnregistrerSous(alimentEnfant);
                    }
                    else
                    {
                        RichTextBox ortf = new RichTextBox
                        {
                            Rtf = alimentEnfant.clientRichTextBox.Rtf
                        };

                        ortf.SelectionStart = 0;
                        ortf.SelectionLength = 0;
                        ortf.SelectedText = alimentEnfant.clientTextBox.Text + Environment.NewLine;

                        ortf.SaveFile(alimentEnfant.Text);

                        Modification = false;
                        alimentEnfant.clientRichTextBox.Modified = false;
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

                        alimentEnfant.clientRichTextBox.SaveFile(sfd.FileName);
                        alimentEnfant.Text = sfd.FileName; // Met à jour le titre du formulaire

                        Enregistrement = true;
                        Modification = false;
                        alimentEnfant.clientRichTextBox.Modified = false;
                    }
                }
            }
            #region Changement dans TextBox

            private void clientTextBox_TextChanged(object sender, EventArgs e)
            {
                Modification = true;
            }

            #endregion

            #endregion
        }
    }
