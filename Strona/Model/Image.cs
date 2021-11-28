using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Strona.Model
{
    public class Image : Item
    {
        string size;
        string material;
        string alt;


        public Image()
        {
            size = "";
            material = "";
            alt = "";
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

    }
}
