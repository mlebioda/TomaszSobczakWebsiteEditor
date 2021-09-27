using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Strona.Model
{
    public class Filter
    {
        string m_caption;
        string m_tag;

        public Filter()
        {
            m_caption = "";
            m_tag = "";
        }

        public string Caption
        {
            get { return m_caption; }
            set
            {
                if (m_caption != value)
                {
                    m_caption = value;
                    RaisePropertyChanged();
                }
            }
        }
        public string Tag
        {
            get { return m_tag; }
            set
            {
                if (m_tag != value)
                {
                    m_tag = value;
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
