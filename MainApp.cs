using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Security.Permissions;
using System.Linq;

namespace RSF
{
    public partial class RSF : Form
    {
        bool minimalizedOnce = false;

        bool CheckingIfIsImage(string element)
        {
            byte[] streamByte = new byte[8];
            FileStream fs = File.OpenRead(element);
            fs.Read(streamByte, 0, 8);

            if (streamByte[0] == 255 && streamByte[1] == 216 && streamByte[2] == 255)
            {
                return true; //its JPG image
            }

            if (streamByte[0] == 71 && streamByte[1] == 73
                && streamByte[2] == 70 && streamByte[3] == 56
                && (streamByte[4] == 57 || streamByte[4] == 55) && streamByte[5] == 97)
            {
                return true; //its GIF image
            }

            if (streamByte[0] == 137 && streamByte[1] == 80
                && streamByte[2] == 78 && streamByte[3] == 71
                && streamByte[4] == 13 && streamByte[5] == 10
                && streamByte[6] == 26 && streamByte[7] == 10)
            {
                return true; //its PNG image
            }

            return false;
        }

        bool LoadingJson(string folderName)
        {
            if (File.Exists("Json/" + folderName + ".json"))
            {
                var reader = File.ReadAllBytes("Json/" + folderName + ".json");
                string result = System.Text.Encoding.UTF8.GetString(reader);
                try
                {
                    JsonConvert.DeserializeObject<List<Images>>(result);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    return false;
                }
                imagesList = JsonConvert.DeserializeObject<List<Images>>(result);
                if (imagesList.Count != 0) return true;
                else return false;
            }
            return false;
        }

        void SearchingForFilesWithJson(Array dir)
        {
            logBox.Invoke(new MethodInvoker(delegate { logBox.Text += "Images: " + Environment.NewLine; }));
            for (int i = 0; i < dir.GetLength(0); i++) //Gets all files from directory
            {
                var element = dir.GetValue(i).ToString();
                var extension = Path.GetExtension(element).ToLower();
                var filename = Path.GetFileName(element);
                filename = filename.Remove(filename.Length - extension.Length, extension.Length);
                if (extension == ".jpg" || extension == ".png" || extension == ".gif" || extension == ".jpeg")
                {

                    //Checking if file is 0 Bytes
                    long length = new FileInfo(element).Length;
                    if (length == 0)
                    {
                        File.Delete(element);
                        continue;
                    }

                    if (CheckingIfIsImage(element))
                    {
                        int indexOnImageList = imagesList.FindIndex(x => x.path.Contains(element));
                        if (indexOnImageList != -1) //Checking if Image is alredy at imagesList
                        {
                            logBox.Invoke(new MethodInvoker(delegate { logBox.Text += "- " + filename + extension + " " + Environment.NewLine; }));
                            comparing(imagesList[indexOnImageList], indexOnImageList);
                        }
                        else
                        {
                            Images image = new Images(filename, extension, element, (int)length, Hash(element), false);
                            logBox.Invoke(new MethodInvoker(delegate { logBox.Text += "- " + filename + extension + " " + Environment.NewLine; }));
                            image.imageHash = imageHashing(image.path);
                            comparing(image, -2);
                        }

                    }
                    else
                    {
                        Console.WriteLine("This element wants to be image but it isn't (has it's extension): " + element);
                    }

                }
                progressBar1.Invoke(new MethodInvoker(delegate { progressBar1.Value++; }));

            }
        }

        void SearchingForFiles(Array dir) //Searching files from drive only
        {
            logBox.Invoke(new MethodInvoker(delegate { logBox.Text += "Images: " + Environment.NewLine; }));
            for (int i = 0; i < dir.GetLength(0); i++)  //TODO TRY DOING THIS ON PARREL FOR WITH LOCKING 
            {
                var element = dir.GetValue(i).ToString();
                var extension = Path.GetExtension(element).ToLower();
                var filename = Path.GetFileName(element);
                filename = filename.Remove(filename.Length - extension.Length, extension.Length);
                if (extension == ".jpg" || extension == ".png" || extension == ".gif" || extension == ".jpeg")
                {
                    //Checking if file is 0 Bytes
                    long length = new FileInfo(element).Length;
                    if (length == 0)
                    {
                        File.Delete(element);
                        continue;
                    }

                    if (CheckingIfIsImage(element))
                    {
                        Images image = new Images(filename, extension, element, (int)length, Hash(element), false); //Images image = new Images(filename, extension, element, Hash(element));
                        logBox.Invoke(new MethodInvoker(delegate { logBox.Text += "- " + filename + extension + " " + Environment.NewLine; }));
                        image.imageHash = imageHashing(image.path);
                        comparing(image, -2);
                    }
                    else
                    {
                        Console.WriteLine("This element wants to be image but it isn't: " + element);
                    }

                }
                progressBar1.Invoke(new MethodInvoker(delegate { progressBar1.Value++; }));

            }
        } //TODO Dodać wczytywanie listy komend z pliku(wykorzystać 2 przycisk)


        public static List<Images> repeatedImages = new List<Images>();
        bool repetings = false;

        //COMPARING FOR IMAGES ONLY
        bool comparing(Images image, int indexOnImageList)
        {
            bool breakLoop = false;
            if (imagesList.Count == 0)
            {
                imagesList.Add(image);
                return false;
            }
            else
            {
                int count = imagesList.Count;
                if (imagesList.Count > 100)
                {
                    Parallel.For(0, count, (k, state) =>
                    {
                        if (k == indexOnImageList) return;
                        int comparability = 0;
                        for (int i = 0; i < accuracy * accuracy; i++)
                        {
                            if (image.imageHash[i] == imagesList[k].imageHash[i]) comparability++;
                        }
                        comparability = (comparability / (accuracy * accuracy)) * 100;
                        if (comparability > 90)
                        {
                            state.Break();
                            if (repetings == false)
                            {
                                repetings = true;
                            }
                            image.repeatedWith = imagesList[k].filename + imagesList[k].extension;
                            image.repeatedWithPath = imagesList[k].path;
                            if (image.size > imagesList[k].size) image.repeatedBigger = true;
                            repeatedImages.Add(image);
                            breakLoop = true;
                        }
                    });

                    if (!breakLoop)
                    {
                        if (imagesList.FindIndex(x => x.path.Contains(image.path)) != indexOnImageList)
                        {
                            imagesList.Add(image);
                        }
                        return false;
                    }
                }
                else
                {
                    int j = imagesList.Count - 1;
                    for (; j >= 0; j--) //repeat dodaje 10 sec do roboty na fate ale znalazł 2 więcej pliki
                    {
                        if (j == indexOnImageList) continue;
                        int comparability = 0;
                        for (int i = 0; i < accuracy * accuracy; i++)
                        {
                            if (image.imageHash[i] == imagesList[j].imageHash[i]) comparability++;
                            //image.imageHash[i] == imagesList[j].imageHash[i]
                            //imagesList[j].imageHash[i] == imagesList[j - 1].imageHash[i]
                        }
                        comparability = (comparability / (accuracy * accuracy)) * 100;
                        if (comparability > 90)
                        {
                            if (repetings == false)
                            {
                                repetings = true;
                            }
                            image.repeatedWith = imagesList[j].filename + imagesList[j].extension;
                            image.repeatedWithPath = imagesList[j].path;
                            repeatedImages.Add(image);
                            return true;
                        }
                    }

                    if (imagesList.FindIndex(x => x.path.Contains(image.path)) != indexOnImageList)
                    {
                        imagesList.Add(image);
                    }
                    return false;
                }
                return true;
            }
        }

        List<Images> imagesList = new List<Images>();
        int accuracy = 32;

        //GETTING VALUES OF LIGHT AND DARK (FOR IMAGE COMPARING)
        public bool[] imageHashing(string path)
        {
            bool[] b = new bool[accuracy * accuracy];
            Bitmap bitmap = new Bitmap(Image.FromFile(path), new Size(accuracy, accuracy));

            int k = 0;
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    b[k] = (bitmap.GetPixel(i, j).GetBrightness() < 0.5f); //TODO całą tablicę byte[] zapisać jako string i porównywać czy takie same
                    k++;
                }
            }

            return b;
        }

        //GETING FILE HASH (FOR EASY FILE COMPARING)
        public string Hash(string element)
        {
            var md5 = System.Security.Cryptography.MD5.Create();
            int size = 10000;
            byte[] streamByte = new byte[size];
            FileStream fs = File.OpenRead(element);
            fs.Read(streamByte, 0, size); //streamByte.Length
            var hash = md5.ComputeHash(streamByte);
            var hashFinal = BitConverter.ToString(hash).Replace("-", "").ToLower();
            fs.Close();
            return hashFinal;
        }

        //CLENING GLOBAL VARIABLES FOR SECOND RUN
        public void cleaningVariables()
        {
            imagesList.Clear();
            repeatedImages.Clear();
            repetings = false;
        }

        //TODO .json, how to get subfolders

        public RSF()
        {
            InitializeComponent();
        }

        bool jsonSaving = true;

        private async void start_Click(object sender, EventArgs e)
        {
            //CLENING GLOBAL VARIABLES FOR SECOND RUN
            cleaningVariables();

            //TRYING TO GET FILES FROM USER SPECIFIED DIRECTORY
            bool error = false;
            Array dir;
            logBox.Text += DateTime.Now.ToString() + Environment.NewLine; //Showing date (for easy displaing of time passed)

            try
            {
                Directory.GetFiles(textBoxDirectory.Text, "*.*", SearchOption.AllDirectories);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException) MessageBox.Show("Nie podałeś ścieżki do folderu", "Error",
    MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (ex is IOException) MessageBox.Show("Podana scieżka jest nie prawidłowa", "Error",
    MessageBoxButtons.OK, MessageBoxIcon.Error);
                error = true;
            }


            if (error == false)
            {
                string folderName = textBoxDirectory.Text;
                if (folderName.LastIndexOf(@"\") == folderName.Length - 1) folderName = folderName.Remove(folderName.Length - 1);
                folderName = folderName.Remove(0, folderName.LastIndexOf(@"\") + 1);

                progressBar1.Value = 1;

                dir = Directory.GetFiles(textBoxDirectory.Text, "*.*", SearchOption.AllDirectories);
                progressBar1.Maximum = dir.Length + 1;

                logBox.Text += "Number of elements: " + dir.GetLength(0) + Environment.NewLine + Environment.NewLine;

                //Checking if json file is 0 bytes
                if (File.Exists("Json/" + folderName + ".json"))
                {
                    long length = new FileInfo("Json/" + folderName + ".json").Length;
                    if (length == 0)
                    {
                        File.Delete("Json/" + folderName + ".json");
                    }
                }

                if (LoadingJson(folderName))
                {
                    var TempImageList = imagesList.AsParallel() //Parallel removing non existing files from list
                        .Where(f => File.Exists(f.path))
                        .ToList();
                    imagesList = TempImageList;

                    try
                    {
                        await Task.Run(() => SearchingForFilesWithJson(dir));
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.ToString());
                        logBox.Text += ex.ToString();
                    }
                }
                else
                {
                    try
                    {
                        await Task.Run(() => SearchingForFiles(dir));
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.ToString());
                        logBox.Text += ex.ToString();
                    }
                }

                //DISPLAING REAPATED FILES
                if (repetings == true)
                {
                    try
                    {
                        await Task.Run(() => ShowingRepeatedElements());
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.ToString());
                        logBox.Text += ex.ToString();
                    }

                    ResultsWindowShow();
                }

                try
                {
                    File.WriteAllText("log.txt", logBox.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    logBox.Text += ex.ToString();
                    throw;
                }
                

                if (jsonSaving) //Checks if user wants to save json file
                {

                    if (Directory.Exists("Json"))
                    {
                        File.Delete("Json/" + folderName + ".json");
                        File.WriteAllText("Json/" + folderName + ".json", JsonConvert.SerializeObject(imagesList, Formatting.None)); // .Indented gives more readable Json file but it take a lot of space 2,22 MB vs 5,18 MB
                    }

                    else
                    {
                        Directory.CreateDirectory("Json");
                        File.WriteAllText("Json/" + folderName + ".json", JsonConvert.SerializeObject(imagesList, Formatting.None));
                    }
                }

                GC.Collect();

                FileSystemWatcher fw = new FileSystemWatcher(textBoxDirectory.Text);
                fw.NotifyFilter = NotifyFilters.LastWrite;
                fw.Filter = "*.*";
                fw.Changed += new FileSystemEventHandler(FileChanged);
                fw.EnableRaisingEvents = true;
            }
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]  //TODO LIST WITH CAHED IMAGES TO TAKE FOR PROCESSING
        private void FileChanged(object sender, FileSystemEventArgs e) //TODO MODYFING ORGINAL FUNCTION TO WORK WITH THIS
        {
            string element = e.FullPath;
            var extension = Path.GetExtension(element).ToLower();
            var filename = Path.GetFileName(element);
            filename = filename.Remove(filename.Length - extension.Length, extension.Length);
            if (extension == ".jpg" || extension == ".png" || extension == ".gif" || extension == ".jpeg")
            {

                //Checking if file is 0 Bytes
                long length = new FileInfo(element).Length;
                if (length == 0)
                {
                    File.Delete(element);
                    return;
                }
                //TODO CHECK WHY TEST.zip BREAKS PROGRAM
                if (CheckingIfIsImage(element))
                {
                    int indexOnImageList = imagesList.FindIndex(x => x.path.Contains(element));
                    if (indexOnImageList != -1) //Checks if Image is alredy at imagesList
                    {
                        logBox.Invoke(new MethodInvoker(delegate { logBox.Text += "- " + filename + extension + " " + Environment.NewLine; }));
                        if(comparing(imagesList[indexOnImageList], indexOnImageList))
                        {
                            NewFileRepeated();
                        }
                    }
                    else
                    {
                        Images image = new Images(filename, extension, element, (int)length, Hash(element), false);
                        logBox.Invoke(new MethodInvoker(delegate { logBox.Text += "- " + filename + extension + " " + Environment.NewLine; }));
                        image.imageHash = imageHashing(image.path);
                        if (comparing(image, -2))
                        {
                            NewFileRepeated();
                        }
                    }

                }
                else
                {
                    Console.WriteLine("This element wants to be image but it isn't (has it's extension): " + element);
                }
            }
        }

        private void Results_Click(object sender, EventArgs e)
        {
            ResultsWindowShow();
        }

        void ResultsWindowShow()
        {
            ResultsWindow window = new ResultsWindow();
            window.Show();
        }

        void ShowingRepeatedElements()
        {
            logBox.Invoke(new MethodInvoker(delegate { logBox.Text += Environment.NewLine + "Repeating elements:" + Environment.NewLine + Environment.NewLine; }));
            for (int i = 0; i < repeatedImages.Count; i++)
            {
                if (repeatedImages[i].repeatedWith != null)
                {
                    logBox.Invoke(new MethodInvoker(delegate { logBox.Text += repeatedImages[i].filename + repeatedImages[i].extension + " -> " + repeatedImages[i].repeatedWith + Environment.NewLine; }));
                }
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e) //For restring window from being minimalized
        {
            notifyIcon.Visible = false;
            Show();
            WindowState = FormWindowState.Normal;
        }


        private void Window_Minimalize(object sender, EventArgs e) //On minializing window crates tray icon and shows popup on first minimalization
        {
            if (FormWindowState.Minimized == WindowState)
            {
                notifyIcon.Visible = true;
                Hide();
                if (minimalizedOnce == false)
                {
                    notifyIcon.ShowBalloonTip(500, "RSF", "This app will still work in background", ToolTipIcon.Info);
                    minimalizedOnce = true;
                }
            }
        }
        
        private void NewFileRepeated() //Creates new popup window when new filee has benn  marked as repated (by fileWatcher)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(500, "RSF", "New file that has been added as repeated with other file and has been added to results window.", ToolTipIcon.Info);
                Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon.Visible = false;
                Show();
            }
        }

        private void logBox_TextChanged(object sender, EventArgs e) //Auto scroll on new text
        {
            logBox.SelectionStart = logBox.Text.Length;
            logBox.ScrollToCaret();
        }


    }

    public class Images
    {
        public string filename;
        public string extension;
        public string path;
        public int size;
        public string hash;
        public bool[] imageHash;
        public string repeatedWith;

        [JsonConstructor]
        public Images(string _filename, string _extension, string _path, int _size, string _hash, bool _repeatedBigger)
        {
            filename = _filename;
            extension = _extension;
            path = _path;
            size = _size;
            hash = _hash;
            repeatedBigger = _repeatedBigger;
        }

        public Images(bool[] _imageHash)
        {
            imageHash = _imageHash;
        }

        public Images(string _repeatedWith)
        {
            repeatedWith = _repeatedWith;
        }

        public string repeatedWithPath {get;set;}
        public bool repeatedBigger { get; set; }
    }
}