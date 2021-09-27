using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Strona.Model
{
    public class UlItem
    {
        List<Filter> filters;
        UlItem ul;

        public UlItem()
        {
           // ul = new UlItem();
            filters = new List<Filter>();
        }

        public UlItem Ul
        {
            get { return ul; }
            set
            {
                if( ul != value)
                {
                    ul = value;
                    RaisePropertyChanged();
                }
            }
        }

        public List<Filter> Filters
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
