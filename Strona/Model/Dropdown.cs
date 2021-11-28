using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Strona.Model
{
    public class Dropdown : INotifyPropertyChanged
    {
        List<Filter> m_filters;
        UlItem m_ul;
        string m_caption;

        public Dropdown()
        {
            m_filters = new List<Filter>();
            m_ul = new UlItem();
            m_caption = "";
        }

        public string Caption
        {
            get
            {
                return m_caption;
            }
            set
            {
                if(value != m_caption)
                {
                    m_caption = value;
                    RaisePropertyChanged();
                }    
            }
        }

        public List<Filter> Filters
        {
            get { return m_filters; }
            set
            {
                if( m_filters != value)
                {
                    m_filters = value;
                    RaisePropertyChanged();
                }
            }
        }
        
        public UlItem Ul
        {
            get { return m_ul; }
            set
            {
                if(m_ul != value)
                {
                    m_ul = value;
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
