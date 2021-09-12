using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strona.Model
{
    public class Filters
    {
        List<Filter> m_filters;
        Dropdown m_dropdown;

        public Filters()
        {
            m_filters = new List<Filter>();
            //DropDown = null
        }

        public List<Filter> filters
        {
            get { return m_filters; }
            set
            {
                if(m_filters != value)
                {
                    m_filters = value;
                    //RaisePropertyChanged
                }
            }
        }

        public Dropdown dropdown
        {
            get { return m_dropdown; }
            set
            {
                if( m_dropdown != value)
                {
                    dropdown = value;
                    //RaisePropertyChanged
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


    }
}
