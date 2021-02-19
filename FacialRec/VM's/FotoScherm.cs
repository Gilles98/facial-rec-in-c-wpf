using FacialRec.Service;
using FacialRec_DAL;
using FacialRec_DAL.DomainModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private string _voornaam;
        private string _achternaam;
        public override string this[string columnName]
        {
            get
            {
                string melding = "* Verplicht veld";
                if (columnName  == "Voornaam" && string.IsNullOrWhiteSpace(Voornaam))
                {
                    return melding;
                }
                if (columnName == "Achternaam" && string.IsNullOrWhiteSpace(Achternaam))
                {
                    return melding;
                }
                return "";
            }
        }

        public byte[] Bin { get; set; }
        public string Voornaam
        {
            get
            {
                return _voornaam;
            }
            set
            {
                _voornaam = value;
                NotifyPropertyChanged();
            }
        }

        public string Achternaam
        {
            get
            {
                return _achternaam;
            }
            set
            {
                _achternaam = value;
                NotifyPropertyChanged();
            }
        }
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

        private void OutputSchrijven()
        {
            for (int i = 0; i <= Bin.Length -1; i++)
            {
                Console.WriteLine("Byte " + i + ": " + Bin[i]);
            }
        }
        private async void OutputAsync()
        {
            Task t = Task.Run(new Action(OutputSchrijven));
            await t;
        }
        
        public FotoScherm(BitmapImage img)
        {
            _context = new Context();
            Foto = img;
            Bin = _context.Personen.Select(x => x.Foto).FirstOrDefault();
            OutputAsync();
        }
        //voor later te gebruiken
        //public BitmapImage FotoOphalen(byte[] byteArr)
        //{
        //    using (MemoryStream ms = new MemoryStream(byteArr))
        //    {
        //        ms.Seek(0, SeekOrigin.Begin);
        //        var image = new BitmapImage();
        //        image.BeginInit();
        //        image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
        //        image.CacheOption = BitmapCacheOption.OnLoad;
        //        image.StreamSource = ms;
        //        image.EndInit();

        //        return image;
        //    }
        //}
        public void FotoOpslagen()
        {
            //preset voor testing
            if (this.IsGeldig())
            {
                byte[] data;
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(Foto));
                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    data = ms.ToArray();
                }
                _context.Personen.Add(new FacialRec_DAL.DomainModels.Persoon { Achternaam = Achternaam, Voornaam = Voornaam, Foto = data });
                int ok = _context.SaveChanges();
                if (ok > 0)
                {
                    Dialog d = new Dialog();
                    d.ToonMessageBox("Geslaagd!");
                }
            }
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            if (parameter.ToString() == "Registreer")
            {
                FotoOpslagen();
            }
        }
    }
}
