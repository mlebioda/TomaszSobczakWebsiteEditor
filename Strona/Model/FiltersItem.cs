using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace Strona.Model 
{
    public class FiltersItem : INotifyPropertyChanged
    {
        List<Filter> m_filters;
        Dropdown m_dropdown;

        public FiltersItem()
        {
            m_filters = new List<Filter>();
            //m_dropdown = new Dropdown();
        }

        public List<Filter> Filters
        {
            get { return m_filters; }
            set
            {
                if(m_filters != value)
                {
                    m_filters = value;
                    RaisePropertyChanged();
                }
            }
        }

        public Dropdown Dropdown
        {
            get { return m_dropdown; }
            set
            {
                if( m_dropdown != value)
                {
                    m_dropdown = value;
                    RaisePropertyChanged();
                }
            }
        }

        public bool IsDropdown()
        {
            if (m_dropdown == null)
                return false;
            else
                return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}
