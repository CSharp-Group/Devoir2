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
        #endregion
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



    }



}

