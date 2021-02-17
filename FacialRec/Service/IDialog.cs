using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacialRec.Service
{
    public interface IDialog
    {
        void ToonMessageBox(string bericht);
        bool ToonMessageboxReturnAntwoord(string bericht);
    }
}
