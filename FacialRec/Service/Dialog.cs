using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FacialRec.Service
{
    public class Dialog : IDialog
    {
        public bool ToonMessageboxReturnAntwoord(string bericht)
        {
            bool result = false;
            if (MessageBox.Show(bericht,"Opletten", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                result = true;
            }

            return result;
        }
        public void ToonMessageBox(string bericht)
        {
            MessageBox.Show(bericht);
        }
    }
}
