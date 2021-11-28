using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Strona.Model
{
    public class Website : INotifyPropertyChanged
    {

        NavItem<Image> obrazy;
        NavItem<TextItem> teksty;
        NavItem<Image> fotografia;

        public Website()
        {
            obrazy = new NavItem<Image>(ItemType.image);
            teksty = new NavItem<TextItem>(ItemType.image);
            fotografia = new NavItem<Image>(ItemType.text);
        }

        public NavItem<Image> Obrazy
        {
            get { return obrazy; }
            set
            {
                if(obrazy != value)
                {
                    obrazy = value;
                    RaisePropertyChanged();
                }
            }
        }
        public NavItem<TextItem> Teksty
        {
            get { return teksty; }
            set
            {
                if(teksty != value)
                {
                    teksty = value;
                    RaisePropertyChanged();
                }
            }
        }
        
        public NavItem<Image> Fotografia
        {
            get { return fotografia; }
            set
            {
                if(fotografia != value)
                {
                    fotografia = value;
                    RaisePropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}
