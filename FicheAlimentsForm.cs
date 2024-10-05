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

        public Parent()
        {
            InitializeComponent();
        }        

        private void Parent_Load(object sender, EventArgs e)
        {
            AssocierImage();
        }

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

        private void nouveauToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FicheAlimentEnfantForm oAliment;
            
            try
            {
                oAliment = new FicheAlimentEnfantForm();
                oAliment.Text = oAliment.Text + " " + FicheAlimentEnfantForm.Numero().ToString();
                oAliment.MdiParent = this;
                oAliment.Show();

            }
            catch (Exception)
            {
                MessageBox.Show("Il est impossible de cree un aliment");
            }

        }

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
    }
}
