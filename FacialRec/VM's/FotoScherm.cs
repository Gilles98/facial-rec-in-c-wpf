using FacialRec.Service;
using FacialRec_DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FacialRec.VM_s
{
    public class FotoScherm:Basis
    {
        private BitmapImage _foto;
        public Context _context;
        public override string this[string columnName] => throw new NotImplementedException();

        public BitmapImage Foto
        {
            get
            {
                return _foto;
            }
            set
            {
                _foto = value;
                NotifyPropertyChanged();
            }
        }

        public FotoScherm(BitmapImage img)
        {
            _context = new Context();
            Dialog d = new Dialog();
            if (d.ToonMessageboxReturnAntwoord("Wilt u deze foto opslagen in de database?"))
            {
                Foto = img;
                FotoOpslagen();
                
            }
            else
            {
                d.ToonMessageBox("De eerste opgeslagen afbeelding wordt nu getoond!");
                Foto = FotoOphalen(_context.Personen.Select(x => x.Foto).SingleOrDefault());
            }
        }
        public BitmapImage FotoOphalen(byte[] byteArr)
        {
            using (MemoryStream ms = new MemoryStream(byteArr))
            {
                ms.Seek(0, SeekOrigin.Begin);
                var image = new BitmapImage();
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();

                return image;
            }
        }
        public void FotoOpslagen()
        {
            //preset voor testing
            string voornaam = "Gilles";
            string achternaam = "Gui";
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(Foto));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            _context.Personen.Add(new FacialRec_DAL.DomainModels.Persoon { Achternaam = achternaam, Voornaam = voornaam, Foto = data });
            int ok = _context.SaveChanges();
            if (ok > 0)
            {
                Dialog d = new Dialog();
                d.ToonMessageBox("Geslaagd!");
            }
        }
        public override bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public override void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
