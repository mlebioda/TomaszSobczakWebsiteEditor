using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Strona.ViewModel.Commands;
using Strona.Model;
namespace Strona.ViewModel
{
    public class JsonVM : INotifyPropertyChanged
    {
        string path;
        NavItem obrazy;
        NavItem fotografia;
        NavItem teksty;
        List<string> tags;

        Image obrazySelectedItem;
        Image tekstySelectedItem;
        Image fotografiaSelectedItem;

        int obrazySelectedIndex; 
        int tekstySelectedIndex; 
        int fotografiaSelectedIndex; 

        public OpenFolderCommand OpenFolderCommand { get; set; }
        public JsonVM()
        {
            path = "";
            obrazy = new NavItem();
            fotografia = new NavItem();
            teksty = new NavItem();
            OpenFolderCommand = new OpenFolderCommand(this);
            tags = new List<string>();

            obrazySelectedItem = new Image();
            tekstySelectedItem = new Image();
            fotografiaSelectedItem = new Image();

            obrazySelectedIndex = 0;
            tekstySelectedIndex = 0;
            fotografiaSelectedIndex = 0;

        }

        public string Path
        {
            get { return path; }
            
            set
            {
                if( !value.Equals("") && !value.Equals(path) )
                {
                    path = value;


                    RaisePropertyChanged();
                }
            }
        }

        public NavItem Obrazy
        {
            get { return obrazy; }
            set
            {
                if( obrazy != value )
                {
                    obrazy = value;
                    ObrazyItems = obrazy.Images;
                    RaisePropertyChanged();
                }
            }
        }

        public NavItem Fotografia
        {
            get { return fotografia; }
            set
            {
                if (fotografia != value)
                {
                    fotografia = value;
                    FotografiaItems = fotografia.Images;
                    RaisePropertyChanged();
                }
            }
        }

        public NavItem Teksty
        {
            get { return teksty; }
            set
            {
                if (teksty != value)
                {
                    teksty = value;
                    this.TekstyItems = teksty.Images;
                    RaisePropertyChanged();
                }
            }
        }

        public List<Image> TekstyItems
        {
            get { return teksty.Images; }
            set
            {
             //   if (teksty.Images != value)
              //  {
                    teksty.Images = value;
                    RaisePropertyChanged();
               // }
            }
        }
        public List<Image> ObrazyItems
        {
            get { return obrazy.Images; }
            set
            {
               // if (obrazy.Images != value)
               // {
                    obrazy.Images = value;
                    RaisePropertyChanged();
              //  }
            }
        }
        public List<Image> FotografiaItems
        {
            get { return fotografia.Images; }
            set
            {
           //     if (fotografia.Images != value)
            //    {
                    fotografia.Images = value;
                    RaisePropertyChanged();
             //   }
            }
        }

        public Image TekstySelectedItem
        {
            get { return tekstySelectedItem; }
            set
            {
                if (tekstySelectedItem != value)
                {
                    tekstySelectedItem = value;
                    RaisePropertyChanged();
                }
            }
        } 
        public Image ObrazySelectedItem
        {
            get { return obrazySelectedItem; }
            set
            {
                if (obrazySelectedItem != value)
                {
                    obrazySelectedItem = value;
                    RaisePropertyChanged();
                }
            }
        } 
        public Image FotografiaSelectedItem
        {
            get { return fotografiaSelectedItem; }
            set
            {
                if (fotografiaSelectedItem != value)
                {
                    fotografiaSelectedItem = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int ObrazySelectedIndex
        {
            get { return obrazySelectedIndex; }
            set
            {
                if (obrazySelectedIndex != value)
                {
                    obrazySelectedIndex = value;
                    RaisePropertyChanged();
                }
            }
        }
        public int TekstySelectedIndex
        {
            get { return tekstySelectedIndex; }
            set
            {
                if (tekstySelectedIndex != value)
                {
                    tekstySelectedIndex = value;
                    RaisePropertyChanged();
                }
            }
        }
        public int FotografiaSelectedIndex
        {
            get { return fotografiaSelectedIndex; }
            set
            {
                if (fotografiaSelectedIndex != value)
                {
                    fotografiaSelectedIndex = value;
                    RaisePropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
