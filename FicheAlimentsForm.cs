﻿#region Commentaires
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

        private void ouvrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            try 
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string nom, prenom, tel;

                    FicheAlimentEnfantForm oEnfant = new FicheAlimentEnfantForm();
                    oEnfant.MdiParent = this;
                    oEnfant.Text = ofd.FileName;

                    RichTextBox ortf = new RichTextBox();

                    ortf.LoadFile(ofd.FileName);

                    nom = ortf.Lines[0];
                    prenom = ortf.Lines[1];
                    tel = ortf.Lines[2];

                    oEnfant.nomTextBox.Text = nom;
                    oEnfant.prenomTextBox.Text = prenom;
                    oEnfant.telephoneMaskedTextBox.Text = tel;

                    ortf.SelectionStart = 0;
                    ortf.SelectionLength = nom.Length + prenom.Length + tel.Length + 3;
                    ortf.SelectedText = String.Empty;

                    oEnfant.infoRichTextBox.Rtf = ortf.Rtf;

                    oEnfant.Enregistrement = true;
                    oEnfant.infoRichTextBox.Modified = false;
                    oEnfant.Modification = false;

                    oEnfant.Show();
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show($"Erreur: {ex.Message}");
            }
            
        }

        private void enregistrerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ActiveMdiChild != null)
                {
                    FicheAlimentEnfantForm oEnfant;
                    oEnfant = (FicheAlimentEnfantForm)this.ActiveMdiChild;
                    oEnfant.Enregistrer();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}");
            }
            
        }

        private void enregistrerSousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ActiveMdiChild != null)
                {
                    FicheAlimentEnfantForm oEnfant;
                    oEnfant = (FicheAlimentEnfantForm)this.ActiveMdiChild;
                    oEnfant.EnregistrerSous();
                }
            }
            catch(Exception ex) 
            { 
                MessageBox.Show($"Erreur: {ex.Message}");
            }
            
        }

        private void sortirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fermerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveControl != null)
            {
                FicheAlimentEnfantForm oEnfant = (FicheAlimentEnfantForm)this.ActiveControl;
                oEnfant.Close();
            }
        }

        #region Alignement
        private void Alignement(object sender)
        {
            try
            {
                FicheAlimentEnfantForm oEnfant = (FicheAlimentEnfantForm)this.ActiveMdiChild;

                if (sender == leftAlignToolStripButton)
                {
                    
                    oEnfant.infoRichTextBox.SelectionAlignment = HorizontalAlignment.Left;
                    centerAlignToolStripButton.Checked = false;
                    rightAlignToolStripButton.Checked = false;
                }
                else if (sender == centerAlignToolStripButton)
                {
                    oEnfant.infoRichTextBox.SelectionAlignment = HorizontalAlignment.Center;
                    leftAlignToolStripButton.Checked = false;
                    rightAlignToolStripButton.Checked = false;
                }
                else if (sender == rightAlignToolStripButton)
                {
                    oEnfant.infoRichTextBox.SelectionAlignment = HorizontalAlignment.Right;
                    leftAlignToolStripButton.Checked = false;
                    centerAlignToolStripButton.Checked = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du changement d'alignement : {ex.Message}");
            }
        }
        #endregion
    }
}
