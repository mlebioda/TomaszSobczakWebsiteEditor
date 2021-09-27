using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Strona.Model;

namespace Strona.ViewModel.Commands.Helpers
{
    public static class CommandsHelpers
    {
        public static string OpenFolder()
        {
            string path = "";
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Wybierz folder assets/img";

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                if(Directory.Exists( folderBrowserDialog.SelectedPath ))
                {
                    DirectoryInfo info = new DirectoryInfo(folderBrowserDialog.SelectedPath);
                    if( !info.Name.Equals("img") || !info.Parent.Name.Equals("assets") )
                    {
                        MessageBox.Show("Wybrano zły folder");
                        return "";
                    }
                    path = folderBrowserDialog.SelectedPath;

                }
                else
                {
                    MessageBox.Show("Folder nie istnieje");
                }
            }

            return path;


        }
        public const string OBRAZY_NAME = "Obrazy";
        public const string FOTOGRAFIE_NAME = "Fotografie";
        public const string TEKSTY_NAME = "Teksty";

        public static void GetItems(string path, ref NavItem obrazy, ref NavItem fotografia, ref NavItem teksty)
        {
            DirectoryInfo info = new DirectoryInfo(path);
            bool obrazyExists = false;
            bool tekstyExists = false;
            bool fotografieExists = false;
            foreach( DirectoryInfo dir in info.GetDirectories())
            {
                if(dir.Name.Equals(OBRAZY_NAME) )
                {
                    obrazyExists = true;
                    getNavItemFromDirectory(dir, ref obrazy);
                }
                else if(dir.Name.Equals(FOTOGRAFIE_NAME))
                {
                    fotografieExists = true;
                    getNavItemFromDirectory(dir, ref fotografia);
                }
                else if(dir.Name.Equals(TEKSTY_NAME))
                {
                    tekstyExists = true;
                    getNavItemFromDirectory(dir, ref teksty);
                }
            }
           
            if (!obrazyExists || !fotografieExists || !tekstyExists)
            {
                MessageBox.Show(
                    "Foldery: \n Obrazy - "
                    + getResultText(obrazyExists) +
                    "\n Fotografie - " + getResultText(fotografieExists) +
                    "\n Teksty - " + getResultText(tekstyExists)
                    );
            }
        }

        private static void getNavItemFromDirectory(DirectoryInfo mainDir/*np obrazy*/, ref NavItem navItem)
        {
            if( mainDir.GetFiles().Length > 0 && mainDir.GetDirectories().Length > 0)
            {
                MessageBox.Show("Folder " + mainDir.Name + " powinien zawierać tylko podkatalogi lub tylko pliki. Pliki w tym folderze zostaną pominięte");
            }

            navItem = new NavItem();

            if(mainDir.GetDirectories().Length > 0) // są w obrazy podfoldery więc trzeba olać pliki
            {
                List<Filter> item = new List<Filter>(); //Dodawanie filtra
                

                foreach(DirectoryInfo dir in mainDir.GetDirectories()) //podfoldery w np obrazy
                {
                    
                    if( dir.GetDirectories().Length > 0 ) // podfolder obrazy ma podfolder czyli będzie dropdownem //nie może dodać plików z folderu
                    {
                        Dropdown dropdown = new Dropdown(); //dropdown może mieć tylko filtry, jeśli filter ma podkatalog to jest ul

                        getDropdown(ref dropdown, ref navItem, dir);
                        //item.Dropdown = dropdown;
                        navItem.Filters.Dropdown = dropdown;
                        

                       
                    }
                    else // nie ma podfolderu czyli ten katalog to filter
                    {
                        Filter filter = new Filter();
                        filter.Caption = dir.Name;
                        filter.Tag = prepareName(dir.Name);
                        item.Add(filter);
                      
                        //Nie ma podfolderów więc można wziąć z niego pliki

                        foreach(FileInfo file in dir.GetFiles())
                        {
                            navItem.Images.Add(getImageFromFile(filter.Tag, mainDir.Name, file));
                        }


                    }
                }
                navItem.Filters.Filters = item;
            }
            else  // MainDir.GetFiles
            {
                foreach(FileInfo file in  mainDir.GetFiles())
                {
                    navItem.Images.Add(getImageFromFile("", mainDir.Name, file));
                }
            }


        }

        private static UlItem getUlItem(DirectoryInfo dir, ref NavItem navItem)
        {
            UlItem ul = new UlItem();

            foreach( DirectoryInfo subDir in dir.GetDirectories() )
            {
                if(subDir.GetDirectories().Length > 0)
                {
                    ul.Ul = getUlItem(subDir, ref navItem);

                }
                else
                {
                    Filter filter = new Filter();
                    filter.Caption = subDir.Name;
                    filter.Tag = prepareName(subDir.Name);

                    ul.Filters.Add(filter);

                    foreach (FileInfo file in subDir.GetFiles())
                    {
                        navItem.Images.Add(getImageFromFile(filter.Tag, subDir.Name, file));

                    }

                }
            }
            return ul;

        }

        private static void getDropdown( ref Dropdown dropdown, ref NavItem navItem, DirectoryInfo dropdownDir )
        {
            dropdown.Caption = dropdownDir.Name;
            foreach (DirectoryInfo subDir in dropdownDir.GetDirectories())
            {
                if( subDir.GetDirectories().Length > 0) // Ul
                {
                    UlItem ul = new UlItem();
                    ul = getUlItem(subDir, ref navItem);
                    dropdown.Ul = ul;
                }
                else
                {
                    Filter filter = new Filter();
                    filter.Caption = subDir.Name;
                    filter.Tag = prepareName(subDir.Name);
                    dropdown.Filters.Add(filter);

                    foreach (FileInfo file in subDir.GetFiles())
                    {
                        navItem.Images.Add(getImageFromFile(filter.Tag, subDir.Name, file));

                    }
                }
            }
        }


        private static string getPath(string mainDirName, string filePath)
        {
            const string PART = "\\assets\\img";
            string newPath = Path.Combine(PART, mainDirName);

            if(filePath.Contains(mainDirName))
            {
                string[] parts = filePath.Split('\\');

                bool canAdd = false;
                foreach( string part in parts )
                {
                    if(canAdd)
                    {
                        newPath = Path.Combine(newPath, part);
                        continue;
                    }

                    if (part.Contains(mainDirName))
                        canAdd = true;
                }
            }

            return newPath;
        }

        private static Image getImageFromFile(string filterTag, string maindirName, FileInfo file)
        {
            Image img = new Image();
            img.Caption = file.Name;
            img.Alt = Path.GetFileName(file.Name);
            img.Material = "";
            img.Tag = filterTag;
            img.Size = "";
            img.Src = getPath(maindirName, file.FullName); //asets/img/MaindirName + path

            return img;
        }

        

        private static string prepareName(string name)
        {
            string newName = name;

            if (newName.Contains("ą"))
                newName = newName.Replace("ą", "a");
            if (newName.Contains("ę"))
                newName = newName.Replace("ę", "e");
            if (newName.Contains("ć"))
                newName = newName.Replace("ć", "c");
            if (newName.Contains("ł"))
                newName = newName.Replace("ł", "l");
            if (newName.Contains("ń"))
                newName = newName.Replace("ń", "n");
            if (newName.Contains("ó"))
                newName = newName.Replace("ó", "o");
            if (newName.Contains("ś"))
                newName = newName.Replace("ś", "s");
            if (newName.Contains("ż"))
                newName = newName.Replace("ż", "z");
            if (newName.Contains("ź"))
                newName = newName.Replace("ź", "z");

            if (newName.Contains("!"))
                newName = newName.Replace("!", "");
            if (newName.Contains("@"))
                newName = newName.Replace("@", "");
            if (newName.Contains("#"))
                newName = newName.Replace("$", "");
            if (newName.Contains("%"))
                newName = newName.Replace("%", "");
            if (newName.Contains("^"))
                newName = newName.Replace("^", "");
            if (newName.Contains("&"))
                newName = newName.Replace("&", "");
            if (newName.Contains("*"))
                newName = newName.Replace("*", "");
            if (newName.Contains("("))
                newName = newName.Replace("(", "");
            if (newName.Contains(")"))
                newName = newName.Replace(")", "");
            if (newName.Contains("_"))
                newName = newName.Replace("_", "");
            if (newName.Contains("-"))
                newName = newName.Replace("-", "");
            if (newName.Contains("+"))
                newName = newName.Replace("+", "");
            if (newName.Contains("="))
                newName = newName.Replace("=", "");
            if (newName.Contains("`"))
                newName = newName.Replace("`", "");
            if (newName.Contains("~"))
                newName = newName.Replace("~", "");
            if (newName.Contains("\\"))
                newName = newName.Replace("\\", "");
            if (newName.Contains("|"))
                newName = newName.Replace("|", "");
            if (newName.Contains("]"))
                newName = newName.Replace("]", "");
            if (newName.Contains("}"))
                newName = newName.Replace("}", "");
            if (newName.Contains("{"))
                newName = newName.Replace("{", "");
            if (newName.Contains("["))
                newName = newName.Replace("[", "");
            if (newName.Contains("]"))
                newName = newName.Replace("]", "");
            if (newName.Contains(";"))
                newName = newName.Replace(";", "");
            if (newName.Contains(":"))
                newName = newName = newName.Replace(":", "");
            if (newName.Contains("\""))
                newName = newName.Replace("\"", "");
            if (newName.Contains("'"))
                newName = newName.Replace("'", "");
            
            if (newName.Contains("<"))
                newName = newName.Replace("<", "");
            if (newName.Contains(">"))
                newName = newName.Replace(">", "");
            if (newName.Contains(","))
                newName = newName.Replace(",", "");
            if (newName.Contains("."))
                newName = newName.Replace(".", "");
            
            if (newName.Contains("/"))
                newName = newName.Replace("/", "");
            if (newName.Contains("?"))
                newName = newName.Replace("?", "");
            if (newName.Contains(" "))
                newName = newName.Replace(" ", "");


            return newName;
        }

        private static string getResultText( bool val )
        {
            if (val)
                return " istnieje";
            else
                return " nie istnieje";
        }

    }
}
