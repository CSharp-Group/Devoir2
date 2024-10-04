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
    }
}
