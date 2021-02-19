using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using AForge.Imaging.Filters;
using AForge.Video;
using AForge.Video.DirectShow;

namespace FacialRec.VM_s
{
    public class MainwindowViewmodel : Basis
    {
        private FilterInfoCollection _fic;
        private VideoCaptureDevice _vcd;
        private List<string> _lijstApparaten;
        private int _selectedCamera;
        private Bitmap _img;
        private Image _img2;
        private BitmapImage _bi;
        private BitmapImage _bi2;
        public BitmapImage Bi
        {
            get
            {
                return _bi;
            }

            set
            {
                _bi = value;
                NotifyPropertyChanged();
            }
        }

        public BitmapImage Bi2
        {
            get
            {
                return _bi2;
            }

            set
            {
                _bi2 = value;
                NotifyPropertyChanged();
            }
        }
        public Bitmap Img
        {
            get
            {
                return _img;
            }
            set
            {
                _img = value;
                NotifyPropertyChanged();
            }
        }

        public Image Img2
        {
            get
            {
                return _img2;
            }
            set
            {
                _img2 = value;
                NotifyPropertyChanged();
            }
        }
        public int SelectedCamera
        {
            get
            {
                return _selectedCamera;
            }
            set
            {
                _selectedCamera = value;
                NotifyPropertyChanged();
            }
        }
        public override string this[string columnName]
        {
            get
            {
                if (columnName == "SelectedCamera" && SelectedCamera == -1)
                {
                    return "Er is geen camera geselecteerd!";
                }
                return "";
            }
        }


        public VideoCaptureDevice VCD
        {
            get
            {
                return _vcd;
            }

            set
            {
                _vcd = value;
                NotifyPropertyChanged();
            }
        }
        public List<string> LijstApparaten
        {
            get
            {
                return _lijstApparaten;
            }
            set
            {
                _lijstApparaten = value;
                NotifyPropertyChanged();
            }
        }
        public FilterInfoCollection Fic {

            get
            {
                return _fic;
            }
            set
            {
                _fic = value;
                NotifyPropertyChanged();
            }
        }

        public MainwindowViewmodel()
        {
            Fic = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            LijstApparaten = new List<string>();
            foreach (FilterInfo item in Fic)
            {
                LijstApparaten.Add(item.Name);
            }
            VCD = new VideoCaptureDevice();
            Camera();
        }

        public void Camera()
        {
            if (this.IsGeldig())
            {
                    VCD = new VideoCaptureDevice(Fic[SelectedCamera].MonikerString);
                    Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
                    VCD.NewFrame += (s, e) =>
                    {
                       
                        Img = filter.Apply((Bitmap)e.Frame.Clone());
                        Img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                     
                        var bi = new BitmapImage() ;
                        bi.BeginInit();
                        MemoryStream ms = new MemoryStream();
                        //Rectangle rect = new Rectangle(0, 0, 100, 100);
                        //Bitmap bmp = new Bitmap(rect.Width, rect.Height, System.Drawing.Imaging.PixelFormat.Format16bppGrayScale);
                        //Graphics g = Graphics.FromImage(bmp);
                        //g.CopyFromScreen(rect.Left, rect.Top, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);
                        Img.Save(ms, ImageFormat.Jpeg);
                        ms.Seek(0, SeekOrigin.Begin);

                        bi.StreamSource = ms;
                        bi.EndInit();
                        bi.Freeze();
                       
                        Dispatcher.CurrentDispatcher.Invoke(() => Bi = bi);
                    };
                    VCD.Start();
            }
            
        }

        public void FotoNemen()
        {

            if (Bi != null)
            {

                Bi2 = Bi;
                FotoScherm vm = new FotoScherm(Bi2);
                Window1 window = new Window1();
                window.DataContext = vm;
                window.Show();
            }
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
           
                FotoNemen();
           
        }
    }
}
