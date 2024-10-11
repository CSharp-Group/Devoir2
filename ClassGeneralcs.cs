using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FicheAliments
{
    internal class ClassGeneralcs
    {
        #region Initialisation
        public ClassGeneralcs()
        {
            InitMessagesErreurs();
        }
        #endregion

        #region EnleverCrochets
        public static void EnleverCrochetSousMenu(ToolStripMenuItem parentMenu)
        {
            if (parentMenu != null)
            {
                foreach (ToolStripItem oToolStripItem in parentMenu.DropDownItems)
                {
                    if (oToolStripItem is ToolStripMenuItem)
                        ((ToolStripMenuItem)oToolStripItem).Checked = false;

                    if (oToolStripItem.Name == "toolStripSeparator2")
                    {
                        break;
                    }
                }
            }
        }
        #endregion

        #region Code d'erreurs

        public enum CodeErreurs
        {
            ceOutOfMemory,
            ceException,
        }

        #endregion

        #region Messages d'erreurs

        public static string[] tMessagesErreursStr = new string[2];

        public static void InitMessagesErreurs()
        {
            tMessagesErreursStr[(int)CodeErreurs.ceOutOfMemory] = "Plus de mémoire";
            tMessagesErreursStr[(int)CodeErreurs.ceException] = "Une erreur s'est produite. Veuillez contacter le programmeur.";
        }

        #endregion
    }
}
