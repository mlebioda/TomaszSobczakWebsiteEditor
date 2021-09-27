using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strona.Model
{
    public class JsonItem
    {
        NavItem obrazy;
        NavItem fotografia;
        NavItem teksty;

        public JsonItem( NavItem o, NavItem f, NavItem t)
        {
            obrazy = o;
            fotografia = f;
            teksty = t;
        }

        public NavItem Obrazy
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

        public NavItem Fotografia
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

        public NavItem Teksty
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
