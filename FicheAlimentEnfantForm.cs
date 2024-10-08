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
        public static int numeroInt = 1;
        private bool enregistrementBool = false;
        private bool modificationBool = false;

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

        private void FicheAlimentEnfantForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult oDialogResult;

            if (nomTextBox.Modified || prenomTextBox.Modified || telephoneMaskedTextBox.Modified || infoRichTextBox.Modified || Modification)
            {
                oDialogResult = MessageBox.Show("Modification. Enregistrer ?", "Modification", MessageBoxButtons.YesNoCancel);

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
