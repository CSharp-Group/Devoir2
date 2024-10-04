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

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade);
        }

        private void mosaiquehorizontaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileHorizontal);
        }

        private void mosaiqueverticaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileVertical);
        }

        private void reorgraniserIconesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.ArrangeIcons);
        }
        
        private void fenetreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g.EnleverCrochetSousMenu(fenetreToolStripMenuItem);
            (sender as ToolStripMenuItem).Checked = true;
        }
    }
}
