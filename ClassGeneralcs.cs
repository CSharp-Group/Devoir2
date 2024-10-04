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
        public static void EnleverCrochetSousMenu(ToolStripMenuItem parentMenu)
        {
            if (parentMenu != null)
            {
                foreach (ToolStripItem oToolStripItem in parentMenu.DropDownItems)
                {
                    if (oToolStripItem is ToolStripMenuItem)
                        ((ToolStripMenuItem)oToolStripItem).Checked = false;

                }
            }
        }
    }
}
