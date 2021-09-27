using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Strona.Model
{
    public class NavItem : INotifyPropertyChanged
    {
        FiltersItem filters;
        List<Image> images;

        public NavItem()
        {
            filters = new FiltersItem();
            images = new List<Image>();
        }

        public FiltersItem Filters
        {
            get { return filters; }
            set
            {
                if( filters != value)
                {
                    filters = value;
                    RaisePropertyChanged();
                }
            }
        }

        public List<Image> Images
        {
            get { return images; }
            set
            {
                if( images != value )
                {
                    images = value;
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
