using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Strona.Model;
using Newtonsoft.Json;
using Strona.View;
namespace Strona.ViewModel.Commands.Helpers
{
    public static class CommandsHelpers
    {
    #region ReadJson

        /// <summary>
        /// Opens json file
        /// </summary>
        /// <returns>Selected file paht</returns>
        public static string OpenFile()
        {
            string path = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "json files (*.json)|*.json";


            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openFileDialog.FileName))
                {
                    return openFileDialog.FileName;

                }
                else
                {
                    MessageBox.Show("PLik nie istnieje");
                }
            }

            return path;
        }

        /// <summary>
        /// Gets NavItems data from json file
        /// </summary>
        /// <param name="path">path to json file</param>
        /// <param name="obrazy">NavItem - obrazy</param>
        /// <param name="fotografia">NavItem - fotografia</param>
        /// <param name="teksty">NavItem - teksty</param>
        public static void GetItemsFromFile(string path, ref NavItem<Image> obrazy, ref NavItem<Image> fotografia, ref NavItem<TextItem> teksty)
        {
            try
            {
                string json = File.ReadAllText(path);

                JsonItem item =JsonConvert.DeserializeObject<JsonItem>(json);
                obrazy = item.Obrazy;
                fotografia = item.Fotografia;
                teksty = item.Teksty;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }

        #endregion

    #region readDirectory

        /// <summary>
        /// Opens directory */assets/img
        /// </summary>
        /// <returns>Path to selected directory</returns>
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



        /// <summary>
        /// Gets NavItems data from directory
        /// </summary>
        /// <param name="path">path to */assets/img directory</param>
        /// <param name="obrazy">NavItem - obrazy</param>
        /// <param name="fotografia">NavItem - fotografia</param>
        /// <param name="teksty">NavItem - teksty</param>
        public static void GetItemsFromDirectory(string path, ref NavItem<Image> obrazy, ref NavItem<Image> fotografia, ref NavItem<TextItem> teksty)
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
                    getNavItemFromDirectory<Image>(dir, ref obrazy, ItemType.image);
                }
                else if(dir.Name.Equals(FOTOGRAFIE_NAME))
                {
                    fotografieExists = true;
                    getNavItemFromDirectory<Image>(dir, ref fotografia, ItemType.image);
                }
                else if(dir.Name.Equals(TEKSTY_NAME))
                {
                    tekstyExists = true;
                    getNavItemFromDirectory<TextItem>(dir, ref teksty, ItemType.text);
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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mainDir"></param>
        /// <param name="navItem"></param>
        /// <param name="type"></param>
        private static void getNavItemFromDirectory<T>(DirectoryInfo mainDir/*np obrazy*/, ref NavItem<T> navItem, ItemType type)
        {
            if( mainDir.GetFiles().Length > 0 && mainDir.GetDirectories().Length > 0)
            {
                MessageBox.Show("Folder " + mainDir.Name + " powinien zawierać tylko podkatalogi lub tylko pliki. Pliki w tym folderze zostaną pominięte");
            }

            navItem = new NavItem<T>(type);

            if(mainDir.GetDirectories().Length > 0) // są w obrazy podfoldery więc trzeba olać pliki
            {
                List<Filter> item = new List<Filter>(); //Dodawanie filtra
                

                foreach(DirectoryInfo dir in mainDir.GetDirectories()) //podfoldery w np obrazy
                {
                    
                    if( dir.GetDirectories().Length > 0 ) // podfolder obrazy ma podfolder czyli będzie dropdownem //nie może dodać plików z folderu
                    {
                        Filter filter = new Filter();
                        filter.Caption = dir.Name;
                        Dropdown dropdown = new Dropdown(); //dropdown może mieć tylko filtry, jeśli filter ma podkatalog to jest ul

                        getDropdown(ref dropdown, ref navItem, dir);

                        filter.Dropdown = dropdown;

                        item.Add(filter);
                        //item.Dropdown = dropdown;
                       // navItem.Filters.Dropdown = dropdown;
                        

                       
                    }
                    else // nie ma podfolderu czyli ten katalog to filter
                    {
                        Filter filter = new Filter();
                        filter.Caption = dir.Name;
                        filter.Tag = "."+prepareName(dir.Name);
                        item.Add(filter);
                      
                        //Nie ma podfolderów więc można wziąć z niego pliki

                        foreach(FileInfo file in dir.GetFiles())
                        {
                            if (navItem.getType() == ItemType.image)
                                navItem.Items.Add(
                                    (T)Convert.ChangeType(
                                        getImageFromFile(prepareName(dir.Name) ,file), typeof(T))
                                    );
                            else
                                navItem.Items.Add(
                                    (T)Convert.ChangeType(
                                        getTextItemFromFile(prepareName(dir.Name), file), typeof(T)
                                        )
                                    );
                            //navItem.Items.Add(getItemFromFile(prepareName(dir.Name), file));
                        }


                    }
                }
                navItem.Filters = item;
            }
            else  // MainDir.GetFiles
            {
                foreach(FileInfo file in  mainDir.GetFiles())
                {

                    if (navItem.getType() == ItemType.image)
                        navItem.Items.Add(
                            (T)Convert.ChangeType(
                                getImageFromFile("", file), typeof(T))
                            );
                    else
                        navItem.Items.Add(
                            (T)Convert.ChangeType(
                                getTextItemFromFile("", file), typeof(T)
                                )
                            );
                }
            }


        }

        private static UlItem getUlItem<T>(DirectoryInfo dir, ref NavItem <T>navItem)
        {
            UlItem ul = new UlItem();
            ul.Caption = dir.Name;

            foreach( DirectoryInfo subDir in dir.GetDirectories() )
            {
                if(subDir.GetDirectories().Length > 0)
                {
                    ul.Ul = getUlItem<T>(subDir, ref navItem);

                }
                else
                {
                    Filter filter = new Filter();
                    filter.Caption = subDir.Name;
                    filter.Tag = "."+prepareName(subDir.Name);

                    ul.Filters.Add(filter);

                    foreach (FileInfo file in subDir.GetFiles())
                    {
                        if(navItem.getType() == ItemType.image)
                            navItem.Items.Add(
                                (T)Convert.ChangeType(
                                    getImageFromFile(prepareName(subDir.Name)/*, subDir.Name*/, file), typeof(T))
                                );
                        else
                            navItem.Items.Add(
                                (T)Convert.ChangeType(
                                    getTextItemFromFile(prepareName(subDir.Name)/*, subDir.Name*/, file) , typeof(T)
                                    )
                                );

                    }

                }
            }
            return ul;

        }

        private static void getDropdown<T>( ref Dropdown dropdown, ref NavItem<T> navItem, DirectoryInfo dropdownDir )
        {
            //dropdown.Caption = dropdownDir.Name;
            foreach (DirectoryInfo subDir in dropdownDir.GetDirectories())
            {
                if( subDir.GetDirectories().Length > 0) // Ul
                {
                    UlItem ul = new UlItem();
                    ul = getUlItem<T>(subDir, ref navItem);
                    dropdown.Ul = ul;
                }
                else
                {
                    Filter filter = new Filter();
                    filter.Caption = subDir.Name;
                    filter.Tag = "."+prepareName(subDir.Name);
                    dropdown.Filters.Add(filter);

                    foreach (FileInfo file in subDir.GetFiles())
                    {
                        if (navItem.getType() == ItemType.image)
                            navItem.Items.Add(
                                (T)Convert.ChangeType(
                                    getImageFromFile(prepareName(subDir.Name)/*, subDir.Name*/, file), typeof(T))
                                );
                        else
                            navItem.Items.Add(
                                (T)Convert.ChangeType(
                                    getTextItemFromFile(prepareName(subDir.Name)/*, subDir.Name*/, file), typeof(T)
                                    )
                                );

                    }
                }
            }
        }


        private static string getPath(string filePath)
        {
            string newPath = "assets\\img";

            if (filePath.Contains(newPath))
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

                    if (part.Equals("img"))
                        canAdd = true;
                }
            }

            return newPath;
        }

        private static Image getImageFromFile(string filterTag, /*string maindirName,*/ FileInfo file)
        {
           
            //TODO
            Image img = new Image();
            img.Caption = Path.GetFileNameWithoutExtension(file.Name);
            img.Alt = Path.GetFileNameWithoutExtension(file.Name);
            img.Material = "";
            img.Tag = filterTag;
            img.Size = "";
            img.Src = getPath(file.FullName); //asets/img/MaindirName + path

            return img;
        }

        private static TextItem getTextItemFromFile(string filterTag, /*string maindirName,*/ FileInfo file)
        {

            //TODO
            TextItem txt = new TextItem();
            txt.Caption = Path.GetFileNameWithoutExtension(file.Name);
            txt.Tag = filterTag;
            txt.Src = getPath(file.FullName); //asets/img/MaindirName + path
            txt.Adjust = TextAdjust.left;

            return txt;
        }


        /// <summary>
        /// Removes signs not acceptable in website
        /// </summary>
        /// <param name="name">item name</param>
        /// <returns>Modified name</returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private static string getResultText( bool val )
        {
            if (val)
                return " istnieje";
            else
                return " nie istnieje";
        }
        #endregion

        #region saveJsonCommand

        public static void SaveJson(JsonItem item)
        {
            try
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "json files (*.json)|*.json";
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    JsonSerializer serializer = new JsonSerializer();
                    //serializer.Converters.Add(new JavaScriptDateTimeConverter());
                    serializer.NullValueHandling = NullValueHandling.Ignore;

                    using (StreamWriter sw = new StreamWriter(dialog.FileName))
                    using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        serializer.Serialize(writer, item);
                        // {"ExpiryDate":new Date(1230375600000),"Price":0}
                    }
                    MessageBox.Show("Gotowe");
                }
            }
            catch( Exception e)
            {
                MessageBox.Show(e.Message);
            }



        }

        #endregion
        #region Gui

        /// <summary>
        /// Use when you use open/update commadns.
        /// If vm items are not empty ask user whether overwrite data
        /// </summary>
        /// <param name="vm"> Command View Model</param>
        /// <returns>
        /// true if vm is not valid or user selects Yes
        /// false if vm user selects No
        /// </returns>
        public static bool AskForOverwriteFile(JsonVM vm)
        {
            if (vm.isValid(false))
            {
                if (System.Windows.MessageBox.Show(
                     "W programie wprowadzone są dane. Jeśli klikniesz tak zostaną utracone",
                     "Uwaga",
                     System.Windows.MessageBoxButton.YesNo,
                     System.Windows.MessageBoxImage.Information
                     ) == System.Windows.MessageBoxResult.No)
                {
                    return false;
                }

            }
            return true;
        }
        #endregion

        #region Update

        /// <summary>
        /// Updates dirItems with data form json
        /// If dir not contains data from json
        /// this json item is saved to redundantItems
        /// </summary>
        /// <param name="dirNav"></param>
        /// <param name="jsonNav"></param>
        /// <param name="redundantItems"></param>
        public static void UpdateItems(
            ref NavItem<Image> dirNav,
            NavItem<Image> jsonNav,
            ref List<string> redundantItems
            )
        {
            for (int i = 0; i < jsonNav.Items.Count; i++)
            {
                for (int j = 0; j < dirNav.Items.Count; j++)
                {
                    if (dirNav.Items[j].Src.Equals(jsonNav.Items[i].Src))
                    {
                        dirNav.Items[j].Size = jsonNav.Items[i].Size;
                        dirNav.Items[j].Material = jsonNav.Items[i].Material;
                        dirNav.Items[j].Caption = jsonNav.Items[i].Caption;
                        dirNav.Items[j].Alt = jsonNav.Items[i].Alt;
                        break;
                    }
                    else
                    {
                        if (j == dirNav.Items.Count - 1)
                        {
                            redundantItems.Add(jsonNav.Items[i].Src);
                        }
                    }
                }
            }
        }

        public static void UpdateItems(
           ref NavItem<TextItem> dirNav,
           NavItem<TextItem> jsonNav,
           ref List<string> redundantItems
           )
        {
            for (int i = 0; i < jsonNav.Items.Count; i++)
            {
                for (int j = 0; j < dirNav.Items.Count; j++)
                {
                    if (dirNav.Items[j].Src.Equals(jsonNav.Items[i].Src))
                    {
                        dirNav.Items[j].Adjust = jsonNav.Items[i].Adjust;
                        dirNav.Items[j].AdjustI = jsonNav.Items[i].AdjustI;
                        dirNav.Items[j].Caption = jsonNav.Items[i].Caption;
                        break;
                    }
                    else
                    {
                        if (j == dirNav.Items.Count - 1)
                        {
                            redundantItems.Add(jsonNav.Items[i].Src);
                        }
                    }
                }
            }
        }

        

        /// <summary>
        /// Update Items from directory with data from json
        /// </summary>
        /// <param name="dirFotografia"></param>
        /// <param name="dirObrazy"></param>
        /// <param name="dirTeksty"></param>
        /// <param name="jsonFotografia"></param>
        /// <param name="jsonObrazy"></param>
        /// <param name="jsonTeksty"></param>
        public static void UpdateItemsFromDirectory(
            ref NavItem<Image> dirFotografia,
            ref NavItem<Image> dirObrazy,
            ref NavItem<TextItem> dirTeksty,
            NavItem<Image> jsonFotografia,
            NavItem<Image> jsonObrazy,
            NavItem<TextItem> jsonTeksty
            )
        {
            List<String> redundanFotografia = new List<string>();
            List<String> redundanObrazy = new List<string>();
            List<String> redundanTeksty = new List<string>();
            UpdateItems(ref dirFotografia, jsonFotografia, ref redundanFotografia);
            UpdateItems(ref dirTeksty, jsonTeksty, ref redundanTeksty );
            UpdateItems(ref dirObrazy, jsonObrazy, ref redundanObrazy );

            string result = "";

            result = PrepareResult(redundanObrazy, result, "OBRAZY");
            result = PrepareResult(redundanFotografia, result, "FOTOGRAFIA");
            result = PrepareResult(redundanTeksty, result, "TEKSTY");
            
            if(!result.Equals(""))
            {
                ShowResultDialog(result);
            }

            
        }

        public static string PrepareResult(
            List<string> redundantItems,
            string result,
            string name
            )
        {

            if(redundantItems.Count > 0)
            {
                result += "====================================================================================\n";
                result += "====================================================================================\n";
                result += "====================================================================================\n";
                result += "Pliki z folderu "+name + ",  które są w starym pliku, a nie ma ich w folderze:\n";

                foreach(string item in redundantItems)
                {
                    result += item + "\n";
                }
                result += "====================================================================================\n";
                result += "====================================================================================\n";
                result += "====================================================================================\n\n";

            }
            return result;
        }

        #endregion

        #region dialog
        public static void ShowResultDialog(string content)
        {
            if (String.IsNullOrEmpty(content))
                return;
            Result resultDialog = new Result();
            resultDialog.content.Text = content;
            resultDialog.Show();
        }
        #endregion
    }
}
