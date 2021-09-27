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

        NavItem obrazy;
        NavItem teksty;
        NavItem fotografia;

        public Website()
        {
            obrazy = new NavItem();
            teksty = new NavItem();
            fotografia = new NavItem();
        }

        public NavItem Obrazy
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
        public NavItem Teksty
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
        
        public NavItem Fotografia
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
