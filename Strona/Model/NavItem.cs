using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace Strona.Model
{
    
    public class NavItem<T> : INotifyPropertyChanged
    {
        // FiltersItem filters;
        ObservableCollection<Filter> filters;
        ObservableCollection<T> items;
        ItemType type;

        public NavItem(ItemType t)
        {
            //filters = new FiltersItem();
            filters = new ObservableCollection<Filter>();
            items = new ObservableCollection<T>();
            type = t;
        }

        public ItemType getType()
        {
            return type;
        }

        /* public FiltersItem Filters
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
        
        */
        public bool isValid()
        {
            if (items.Count > 0 )
                return true;
            else
                return false;
        }

        public ObservableCollection<Filter> Filters
        {
            get { return filters; }
            set
            {
                if (filters != value)
                {
                    filters = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<T> Items
        {
            get { return items; }
            set
            {
                if( items != value )
                {
                    items = value;
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
