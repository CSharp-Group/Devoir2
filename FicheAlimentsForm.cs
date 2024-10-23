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
using System.Drawing.Text;
using System.Globalization;
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
            DesactiverOperationsMenusBarreOutils();
            langueToolStripStatusLabel.Text = CultureInfo.CurrentCulture.NativeName;
            if (System.Console.CapsLock)
                capsToolStripStatusLabel.Text = "MAJ";
            else
                capsToolStripStatusLabel.Text = "";

            AfficherPolicesInstallées(sender,e);

            this.toolStripComboBoxPolice.SelectedIndexChanged -= toolStripComboBoxPolice_SelectedIndexChanged;
            this.toolStripComboBoxTaillesDePolice.SelectedIndexChanged -= toolStripComboBoxTaillesDePolice_SelectedIndexChanged;

            this.toolStripComboBoxPolice.SelectedIndexChanged += toolStripComboBoxPolice_SelectedIndexChanged;
            this.toolStripComboBoxTaillesDePolice.SelectedIndexChanged += toolStripComboBoxTaillesDePolice_SelectedIndexChanged;
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

        #region Nouveau
        private void FichierNouveau(object sender, EventArgs e)
        {
            FicheAlimentEnfantForm oEnfant;

            try
            {
                oEnfant = new FicheAlimentEnfantForm();
                oEnfant.Text = oEnfant.Text + " " + FicheAlimentEnfantForm.Numero().ToString();
                oEnfant.MdiParent = this;
                oEnfant.ModeInsertion = true;
                oEnfant.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}");
            }

            try
            {
                ActiverOperationsMenusBarreOutils();
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
        private void Affichage_Click(object sender, EventArgs e)
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
                    toolStripComboBoxPolice.Visible = false;
                    toolStripComboBoxTaillesDePolice.Visible = false;
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
                    toolStripComboBoxPolice.Visible = true;
                    toolStripComboBoxTaillesDePolice.Visible = true;
                }
            }
        }
        #endregion

        #region Ouvrir
        private void FichierOuvrir(object sender, EventArgs e)
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
                    oEnfant.ModeInsertion = true;

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
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}");
            }

            try
            {
                ActiverOperationsMenusBarreOutils();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}");
            }
        }
        #endregion

        #region Enregistrer
        private void FichierEnregistrer(object sender, EventArgs e)
        {
            try
            {
                if (this.ActiveMdiChild != null)
                {
                    FicheAlimentEnfantForm oEnfant;
                    oEnfant = (FicheAlimentEnfantForm)this.ActiveMdiChild;
                    oEnfant.Enregistrer();

                    ficherToolStripStatusLabel.Text = oEnfant.Text;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}");
            }
        }
        #endregion

        #region EnregistrerSous
        private void FichierEnregistrerSous(object sender, EventArgs e)
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
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}");
            }
        }
        #endregion

        #region Sortir / Quitter
        private void Quitter_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Fermer
        private void Fermer_Click(object sender, EventArgs e)
        {
            if (this.ActiveControl != null)
            {
                FicheAlimentEnfantForm oEnfant = (FicheAlimentEnfantForm)this.ActiveControl;
                oEnfant.Close();
            }
        }
        #endregion

        #region DesactiverOperationsMenusBarreOutils
        public void DesactiverOperationsMenusBarreOutils()
        {
            // Placer tout a false.
            foreach (ToolStripItem item in fichesAlimentsToolStrip.Items)
            {
                item.Enabled = false;
            }

            foreach (ToolStripItem item in fichesAlimentsMenuStrip.Items)
            {
                Console.WriteLine($"{item.Name} -> {item.GetType()}");
                if (item is ToolStripMenuItem menuItem)
                {
                    Console.WriteLine($"{item.Name} is MenuItem");
                    foreach (ToolStripItem subItem in menuItem.DropDownItems)
                    {
                        subItem.Enabled = false;
                    }
                }
            }

            // Activer des boutons et menus
            nouveauToolStripButton.Enabled = true;
            nouveauToolStripMenuItem.Enabled = true;

            ouvrireToolStripButton.Enabled = true;
            ouvrirToolStripMenuItem.Enabled = true;

            sortirToolStripMenuItem.Enabled = true;
            aideListeToolStripMenuItem.Enabled = true;
        }
        #endregion

        #region ActiverOperationsMenusBarreOutils
        public void ActiverOperationsMenusBarreOutils()
        {
            // Placer tout a true.
            foreach (ToolStripItem item in fichesAlimentsToolStrip.Items)
            {
                item.Enabled = true;
            }

            foreach (ToolStripItem item in fichesAlimentsMenuStrip.Items)
            {
                Console.WriteLine($"{item.Name} -> {item.GetType()}");
                if (item is ToolStripMenuItem menuItem)
                {
                    Console.WriteLine($"{item.Name} is MenuItem");
                    foreach (ToolStripItem subItem in menuItem.DropDownItems)
                    {
                        subItem.Enabled = true;
                    }
                }
            }

            // Desactiver des boutons et menus
            copierToolStripButton.Enabled = false;
            copierToolStripMenuItem.Enabled = false;

            collerToolStripButton.Enabled = false;
            collerToolStripMenuItem.Enabled = false;

            couperToolStripButton.Enabled = false;
            couperToolStripMenuItem.Enabled = false;

            // Check Clipboard
            if (Clipboard.ContainsText() || Clipboard.ContainsImage())
            {
                collerToolStripButton.Enabled = true;
                collerToolStripMenuItem.Enabled = true;
            }
            else
            {
                collerToolStripButton.Enabled = false;
                collerToolStripMenuItem.Enabled = false;
            }
        }
        #endregion

        #region Alignement
        private void Alignement_Click(object sender, EventArgs e)
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

        #region Edition
        private void Edition_Click(object sender, EventArgs e)
        {
            try
            {
                FicheAlimentEnfantForm client = (FicheAlimentEnfantForm)this.ActiveMdiChild;

                if (sender == couperToolStripMenuItem || sender == couperToolStripButton)
                {
                    client.infoRichTextBox.Cut();
                }
                else if (sender == copierToolStripMenuItem || sender == copierToolStripButton)
                {
                    client.infoRichTextBox.Copy();
                }
                else if (sender == collerToolStripMenuItem || sender == collerToolStripButton)
                {
                    client.infoRichTextBox.Paste();
                }
                else if (sender == effacerToolStripMenuItem)
                {
                    client.infoRichTextBox.Clear();
                }
                else if (sender == selectionnerToolStripMenuItem)
                {
                    client.infoRichTextBox.SelectAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}");
            }
        }
        #endregion

        #region MdiChildActivate
        public void Parent_MdiChildActivate()
        {
            if (ActiveMdiChild == null)
            {
                DesactiverOperationsMenusBarreOutils();
            }
        }
        #endregion

        #region Style Police
        private void StylePolice_Click(object sender, EventArgs e)
        {
            try
            {
                // V�rifier si un formulaire enfant est actif
                if (this.ActiveMdiChild is FicheAlimentEnfantForm oEnfant)
                {
                    // D�terminer quel bouton a �t� cliqu� et appeler ChangerAttributsPolice
                    if (sender == boldToolStripButton)
                    {
                        if (!oEnfant.infoRichTextBox.SelectionFont.Bold)
                        {
                            oEnfant.ChangerAttributsPolice(FontStyle.Bold);
                        }
                        else
                        {
                            // Optionnel : Vous pouvez choisir de retirer le style si d�j� appliqu�
                            oEnfant.ChangerAttributsPolice(FontStyle.Regular);
                        }
                    }
                    else if (sender == italicToolStripButton)
                    {
                        if (!oEnfant.infoRichTextBox.SelectionFont.Italic)
                        {
                            oEnfant.ChangerAttributsPolice(FontStyle.Italic);
                        }
                        else
                        {
                            oEnfant.ChangerAttributsPolice(FontStyle.Regular);
                        }
                    }
                    else if (sender == underlineToolStripButton)
                    {
                        if (!oEnfant.infoRichTextBox.SelectionFont.Underline)
                        {
                            oEnfant.ChangerAttributsPolice(FontStyle.Underline);
                        }
                        else
                        {
                            oEnfant.ChangerAttributsPolice(FontStyle.Regular);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Aucun document actif � modifier.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur: " + ex.Message);
            }
        }
        #endregion

        #region KeyDown

        private void Parent_KeyDown(object sender, KeyEventArgs e)
        {
            FicheAlimentEnfantForm oEnfant = this.ActiveMdiChild as FicheAlimentEnfantForm;

            if (Control.IsKeyLocked(Keys.CapsLock))
                capsToolStripStatusLabel.Text = "MAJ";
            else
                capsToolStripStatusLabel.Text = "";

            if (e.KeyCode == Keys.Insert)
            {
                if (insertStripStatusLabel.Text == "INS")
                {
                    insertStripStatusLabel.Text = "RFP";
                    (this.ActiveMdiChild as FicheAlimentEnfantForm).ModeInsertion = false;
                }
            }
            else
            {
                if (oEnfant != null)
                {
                    insertStripStatusLabel.Text = "INS";
                    (this.ActiveMdiChild as FicheAlimentEnfantForm).ModeInsertion = true;
                }
            }
        }

        #region MdiChildActivate

        private void Parent_MdiChildActivate(object sender, EventArgs e)
        {
            insertStripStatusLabel.Text = "INS";
            FicheAlimentEnfantForm oEnfant = (FicheAlimentEnfantForm)this.ActiveMdiChild;

            if (this.ActiveMdiChild == null)
            {
                ficherToolStripStatusLabel.Text = "Crée ou ouvrir un aliment";
                DesactiverOperationsMenusBarreOutils();
            }
            else
            {
                if (oEnfant.ModeInsertion)
                {
                    ficherToolStripStatusLabel.Text = this.ActiveMdiChild.Text;
                }
            }
        }

        #endregion

        #endregion

        #region AfficherPolicesInstallées
        private void AfficherPolicesInstallées(object sender, EventArgs e)
        {
            try
            {
                // Créer une collection de polices et obtenir les polices installées
                FontFamily[] fontFamilies;
                InstalledFontCollection installedFontCollection = new InstalledFontCollection();

                fontFamilies = installedFontCollection.Families;

                // Ajouter les tailles de police au ToolStripComboBoxTailePolice 8-16
                for (int i = 8; i <= 16; i += 2)
                {
                    toolStripComboBoxTaillesDePolice.Items.Add(i);
                }

                // Ajouter les noms des polices installées au ToolStripComboBox
                foreach (FontFamily fontFamily in fontFamilies)
                {
                    toolStripComboBoxPolice.Items.Add(fontFamily.Name);
                }

                // Sélectionner la première police et la première taille de police
                if (toolStripComboBoxTaillesDePolice.Items.Count > 0)
                    toolStripComboBoxTaillesDePolice.SelectedIndex = 0;
                if (toolStripComboBoxPolice.Items.Count > 0)
                    toolStripComboBoxPolice.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}", "Erreur pendant l'affichage des polices installées");
            }

            // Event handler pour dessiner les polices dans le ToolStripComboBox
            ComboBox cb = toolStripComboBoxPolice.ComboBox;
            cb.DrawMode = DrawMode.OwnerDrawFixed;

            cb.DrawItem += new DrawItemEventHandler(ComboBox_DrawItem);
            cb.MeasureItem += new MeasureItemEventHandler(ComboBox_MeasureItem);
        }

        private void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            ComboBox cb = sender as ComboBox;
            string text = cb.Items[e.Index].ToString();
            FontFamily fontFamily = new FontFamily(text);
            Font font = new Font(fontFamily, 12);

            e.DrawBackground();
            e.Graphics.DrawString(text, font, Brushes.Black, e.Bounds.X, e.Bounds.Y);
        }

        private void ComboBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            ComboBox cb = sender as ComboBox;
            string text = cb.Items[e.Index].ToString();
            FontFamily fontFamily = new FontFamily(text);
            Font font = new Font(fontFamily, 12);

            e.ItemHeight = (int)e.Graphics.MeasureString(text, font).Height;
        }
        #endregion

        #region Police Selected Index Change
        private void toolStripComboBoxPolice_SelectedIndexChanged(object sender, EventArgs e)
        {
            FicheAlimentEnfantForm oEnfant = this.ActiveMdiChild as FicheAlimentEnfantForm;

            try
            {
                if (oEnfant != null && oEnfant.infoRichTextBox != null && oEnfant.infoRichTextBox.SelectionFont != null)
                {
                    Font enfantRichTextBoxFont = oEnfant.infoRichTextBox.SelectionFont;

                    string selectedFont = toolStripComboBoxPolice.SelectedItem.ToString();

                    if (enfantRichTextBoxFont != null)
                    {
                        oEnfant.infoRichTextBox.SelectionFont = new Font(selectedFont, enfantRichTextBoxFont.Size);
                    }

                    oEnfant.infoRichTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}");
            }
        }
        #endregion

        #region Taille de Police Selected Index Change
        private void toolStripComboBoxTaillesDePolice_SelectedIndexChanged(object sender, EventArgs e)
        {
            FicheAlimentEnfantForm oEnfant = this.ActiveMdiChild as FicheAlimentEnfantForm;
            try
            {
                if (oEnfant != null && oEnfant.infoRichTextBox != null && oEnfant.infoRichTextBox.SelectionFont != null)
                {
                    Font enfantRichTextBoxFont = oEnfant.infoRichTextBox.SelectionFont;
                    float size = float.Parse(toolStripComboBoxTaillesDePolice.SelectedItem.ToString());
                    if (enfantRichTextBoxFont != null)
                    {
                        oEnfant.infoRichTextBox.SelectionFont = new Font(enfantRichTextBoxFont.FontFamily,
                            size, enfantRichTextBoxFont.Style);
                    }
                    oEnfant.infoRichTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}");
            }
        }
        #endregion

        #region PoliceToolStripMenuItem

        private void policeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ActiveMdiChild is FicheAlimentEnfantForm oEnfant)
                {
                    RichTextBox enfantRichTextBox = oEnfant.infoRichTextBox;

                    using (FontDialog fontDialog = new FontDialog())
                    {
                        fontDialog.Font = enfantRichTextBox.SelectionFont;

                        if (fontDialog.ShowDialog() == DialogResult.OK)
                        {
                            enfantRichTextBox.SelectionFont = fontDialog.Font;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}");
            }
        }

        #endregion
        #endregion
    }
}

    


