using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strona.Model
{
    public class JsonItem
    {
        NavItem<Image> obrazy;
        NavItem<Image> fotografia;
        NavItem<TextItem> teksty;

        public JsonItem( NavItem<Image> o, NavItem<Image> f, NavItem<TextItem> t)
        {
            obrazy = o;
            fotografia = f;
            teksty = t;
        }

        public NavItem<Image> Obrazy
        {
            get { return obrazy; }
            set
            {
                if (obrazy != value)
                {
                    obrazy = value;
                }
            }
        }

        public NavItem<Image> Fotografia
        {
            get { return fotografia; }
            set
            {
                if (fotografia != value)
                {
                    fotografia = value;
                }
            }
        }

        public NavItem<TextItem> Teksty
        {
            get { return teksty; }
            set
            {
                if (teksty != value)
                {
                    teksty = value;
                }
            }
        }
    }
}
