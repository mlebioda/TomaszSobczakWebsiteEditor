using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Strona.Model
{
    public class Image : INotifyPropertyChanged
    {
        string caption;
        string tag;
        string size;
        string material;
        string alt;
        string src;


        public Image()
        {
            caption = "";
            tag = "";
            size = "";
            material = "";
            alt = "";
            src = "";
        }

        public string Caption 
        {
            get { return caption; }
            set
            {
                if (caption != value)
                {
                    caption = value;
                    RaisePropertyChanged();
                }

            }
        }
        public string Tag
        {
            get { return tag; }
            set
            {
                if (tag != value)
                {
                    tag = value;
                    RaisePropertyChanged();
                }

            }
        }

        public string Size
        {
            get { return size; }
            set
            {
                if (size != value)
                {
                    size = value;
                    RaisePropertyChanged();
                }

            }
        }
        public string Material
        {
            get { return material; }
            set
            {
                if (material != value)
                {
                    material = value;
                    RaisePropertyChanged();
                }

            }
        }
        public string Alt
        {
            get { return alt; }
            set
            {
                if (alt != value)
                {
                    alt = value;
                    RaisePropertyChanged();
                }

            }
        }
        public string Src
        {
            get { return src; }
            set
            {
                if (src != value)
                {
                    src = value;
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
