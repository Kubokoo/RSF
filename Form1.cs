using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace RSF
{
    public partial class RSF : Form
    {
        bool CheckingIfIsImage(string element)
        {
            byte[] streamByte = new byte[8];
            FileStream fs = File.OpenRead(element);
            fs.Read(streamByte, 0, 8); //TODO zrobić na array i for(może)

            if(streamByte[0] == 255 && streamByte[1] == 216 && streamByte[2] == 255)
            {
                return true; //its JPG image
            }

            if(streamByte[0] == 47 && streamByte[1] == 49
                && streamByte[2] == 46 && streamByte[3] == 38
                && (streamByte[4] == 37 || streamByte[4] == 39) && streamByte[5] == 61)
            {
                return true; //its GIF image
            }

            if(streamByte[0]== 137 && streamByte[1] == 80 
                && streamByte[2] == 78 && streamByte[3] == 71
                && streamByte[4] == 13 && streamByte[5] == 10 
                && streamByte[6] == 26 && streamByte[7] == 10)
            {
                return true; //its PNG image
            }

            //Console.WriteLine(streamByte.ToString());

            return false;
        }
        
        //TODO przemyśleć jak zrobić buforowanie następnej bitmapy

       bool LoadingJson(string folderName)
       {
           if (File.Exists("../../" + folderName + ".json"))
           {
               var reader = File.ReadAllBytes("../../" + folderName + ".json");
               string result = System.Text.Encoding.UTF8.GetString(reader);
               imagesList = JsonConvert.DeserializeObject<List<Images>>(result);
               if (imagesList.Count != 0) return true;
               else return false;
           }
           return false;
       }        

        void SearchingForFiles(Array dir)
        {
            logBox.Invoke(new MethodInvoker(delegate { logBox.Text += "Images: " + Environment.NewLine; }));
            //Console.WriteLine("Number of elements: " + dir.GetLength(0));
            for (int i = 0; i < dir.GetLength(0); i++)
            {
                var element = dir.GetValue(i).ToString();
                var extension = Path.GetExtension(element).ToLower();
                var filename = Path.GetFileName(element);
                filename = filename.Remove(filename.Length - extension.Length, extension.Length);
                //Debug.WriteLine(extension);
                if (extension == ".jpg" || extension == ".png" || extension == ".gif" || extension == ".jpeg")
                {
                    //MemoryStream cache = new MemoryStream(File.ReadAllBytes(dir.GetValue(i+1).ToString()));

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
                        comparing(image);
                    }
                    else
                    {
                        Console.WriteLine("This element wants to be image but it isn't: " + element);
                    }

                    //TODO Zrobić pokazanie który plik jest większy
                }
                progressBar1.Invoke(new MethodInvoker(delegate { progressBar1.Value++; }));
                
            }
        } //TODO Dodać wczytywanie listy komend z pliku(wykorzystać 2 przycisk)


        List<Images> repeatedImages = new List<Images>();
        bool repetings = false;

        //COMPARING FOR IMAGES ONLY
        bool comparing(Images image)
        {
            bool breakLoop = false;
            if (imagesList.Count == 0)
            {
                imagesList.Add(image);
                return false;
            }
            else
            {
                if(imagesList.Count > 100)
                {
                    Parallel.ForEach(imagesList, (currentImage,state) => //repeat dodaje 10 sec do roboty na fate ale znalazł 2 więcej pliki
                    {
                        int comparability = 0;
                        for (int i = 0; i < accuracy * accuracy; i++)
                        {
                            if (image.imageHash[i] == currentImage.imageHash[i]) comparability++;
                        } 
                        //Parallel.For(0, max, i =>
                        //{
                        //    //image.imageHash[i] == imagesList[j].imageHash[i]
                        //    //imagesList[j].imageHash[i] == imagesList[j - 1].imageHash[i] //Coś działa ale za dużo powtórzeń (działa dla accurycy 32) (przestawać szukać po znalezeniu powtórki?)
                        //    if (image.imageHash[i] == imagesList[j].imageHash[i]) comparability++;  //184,713,810 Porównań lub 369,446,841 Porówań // 78,086
                        //}); //TODO zprawdzić czy for i parell for dają takie same rezultaty (powtarzające się obrazy)
                        comparability = (comparability / (accuracy * accuracy)) * 100;
                        //Console.WriteLine(comparability); STRASZNIE SPOWANLNIAWYKONYWANIE KODU 17 sek bez - 1:17 z wypisywaniem //REZERO w 2:24(2:04 z Paraell foreach) //IDK w 2 min
                        if (comparability > 90)
                        {
                            state.Break();
                            if (repetings == false)
                            {
                                repetings = true;
                            }
                            image.repeatedWith = currentImage.filename + currentImage.extension;
                            if (image.size > currentImage.size) image.repeatedBigger = true;
                            repeatedImages.Add(image);
                            breakLoop = true;
                        }
                    });
                    if (!breakLoop)
                    {
                        imagesList.Add(image);
                        return false;
                    }
                }
                else
                {
                    int j = imagesList.Count - 1;
                    for (; j >= 0; j--) //repeat dodaje 10 sec do roboty na fate ale znalazł 2 więcej pliki
                    {
                        int comparability = 0;
                        for (int i = 0; i < accuracy * accuracy; i++)
                        {
                            //image.imageHash[i] == imagesList[j].imageHash[i]
                            //imagesList[j].imageHash[i] == imagesList[j - 1].imageHash[i] //Coś działa ale za dużo powtórzeń (działa dla accurycy 32) (przestawać szukać po znalezeniu powtórki?)
                            if (image.imageHash[i] == imagesList[j].imageHash[i]) comparability++;  //184,713,810 Porównań lub 369,446,841 Porówań
                        }
                        comparability = (comparability / (accuracy * accuracy)) * 100;
                        //Console.WriteLine(comparability); STRASZNIE SPOWANLNIAWYKONYWANIE KODU 17 sek bez - 1:17 z wypisywaniem //REZERO w 2:24 //IDK w 2 min
                        if (comparability > 90)
                        {
                            if (repetings == false)
                            {
                                repetings = true;
                            }
                            image.repeatedWith = imagesList[j].filename + imagesList[j].extension;
                            repeatedImages.Add(image);
                            return true;
                        }
                    }

                    imagesList.Add(image);
                    return false;
                }
                return true;
                //for (; j >= 0; j--) //repeat dodaje 10 sec do roboty na fate ale znalazł 2 więcej pliki
                //{
                //    int comparability = 0; //TODO Przemyśleć przejście na true false
                //    for (int i = 0; i < accuracy * accuracy; i++)
                //    {
                //        //image.imageHash[i] == imagesList[j].imageHash[i]
                //        //imagesList[j].imageHash[i] == imagesList[j - 1].imageHash[i] //Coś działa ale za dużo powtórzeń (działa dla accurycy 32) (przestawać szukać po znalezeniu powtórki?)
                //        if (image.imageHash[i] == imagesList[j].imageHash[i]) comparability++;  //184,713,810 Porównań lub 369,446,841 Porówań
                //    }  //re:zero 40 min i nie skończył
                //    //Parallel.For(0, max, i =>
                //    //{
                //    //    //image.imageHash[i] == imagesList[j].imageHash[i]
                //    //    //imagesList[j].imageHash[i] == imagesList[j - 1].imageHash[i] //Coś działa ale za dużo powtórzeń (działa dla accurycy 32) (przestawać szukać po znalezeniu powtórki?)
                //    //    if (image.imageHash[i] == imagesList[j].imageHash[i]) comparability++;  //184,713,810 Porównań lub 369,446,841 Porówań // 78,086
                //    //}); //TODO zprawdzić czy for i parell for dają takie same rezultaty (powtarzające się obrazy)
                //    comparability = (comparability / (accuracy * accuracy)) * 100;
                //    if (comparability > 90)
                //    {
                //        if (repetings == false)
                //        {
                //            repetings = true;
                //        }
                //        image.repeatedWith = imagesList[j].filename + imagesList[j].extension;
                //        imagesList.Add(image);
                //        return true;
                //    }
                //}

                //imagesList.Add(image);
                //return false;

                //int j = imagesList.Count - 1;
                //Parallel.For(0, j, k => //ZERO PRZYŚPIESZENIA IDK W 6 MIN NA OBU METODACH
                //{
                //   int comparability = 0;
                //   for (int i = 0; i < accuracy * accuracy; i++)
                //   {
                //        if (image.imageHash[i] == imagesList[k].imageHash[i]) comparability++;
                //   }
                //   comparability = (comparability / (accuracy * accuracy)) * 100;
                //   Console.WriteLine(comparability);
                //   if (comparability > 90)
                //   {
                //       if (repetings == false)
                //       {
                //           repetings = true;
                //       }
                //       image.repeatedWith = imagesList[k].filename + imagesList[k].extension;
                //       imagesList.Add(image);
                //   }
                //});
                //if (repetings == false)
                //{
                //    imagesList.Add(image);
                //}
            }
        }

        List<Images> imagesList = new List<Images>();
        int accuracy = 32;

        //GETTING VALUES OF LIGHT AND DARK (FOR IMAGE COMPARING)
        public bool[] imageHashing(string path)
        {
            bool[] b = new bool[accuracy * accuracy];
            Bitmap bitmapTemp = new Bitmap(path);
            Rectangle rect = new Rectangle(0, 0, bitmapTemp.Width, bitmapTemp.Height);

            System.Drawing.Imaging.BitmapData bmpData =
                bitmapTemp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bitmapTemp.PixelFormat);

            bmpData.PixelFormat = System.Drawing.Imaging.PixelFormat.Format16bppGrayScale;

            bitmapTemp.UnlockBits(bmpData);
            Bitmap bitmap = new Bitmap(bitmapTemp, new Size(accuracy, accuracy));

            //bitmapTemp.Dispose();

            int k = 0;
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    b[k] = (bitmap.GetPixel(i, j).GetBrightness() < 0.5f);
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

        //TODO zrobić wczytywanie wyników z porpzedniego przejscia(gdzie trzymać pliki .json, jak rozpracować podfoldery)

        public RSF()
        {
            InitializeComponent();
        }

        bool logfile = true;

        Font bolded = new Font("Georgia", 14, FontStyle.Bold);
        Font normal = new Font("Georgia", 14, FontStyle.Regular);

        private async void start_Click(object sender, EventArgs e)
        {
            //CLENING GLOBAL VARIABLES FOR SECOND RUN
            cleaningVariables();

            //TRYING TO GET FILES FROM USER SPECIFIED DIRECTORY
            bool error = false;
            Array dir;

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

            //TODO zróbić funkcje do wczytywanie json i porównyania

            if (error == false)
            {
                string folderName = textBoxDirectory.Text;
                if (folderName.LastIndexOf(@"\") == folderName.Length - 1) folderName = folderName.Remove(folderName.Length - 1);
                folderName = folderName.Remove(0, folderName.LastIndexOf(@"\") + 1);

                progressBar1.Value = 1;

                //logBox.SelectionBullet = true;
                //logBox.SelectionFont = new Font("Arial", 100,FontStyle.Bold);
                //logBox.SelectedText = "Apples" + "\n";

                //logBox.Font = bolded; //TODO poza ifem działa, w nim już nie (prawdopodobnie działa tylko na 1 tekst a potem się nie zmienia)
                //logBox.Text += "Test";

                dir = Directory.GetFiles(textBoxDirectory.Text, "*.*", SearchOption.AllDirectories);
                progressBar1.Maximum = dir.Length + 1;

                logBox.Font = bolded;
                logBox.Text += "Number of elements: " + dir.GetLength(0) + Environment.NewLine + Environment.NewLine;
                logBox.Font = normal;

                ////TODO osobna funkcja jeśli imagelist z pliku(sprawdzenie czy nie ma nowych plików w folderze i porównywanie)
                LoadingJson(folderName);


                try
                {
                    await Task.Run(() => SearchingForFiles(dir));
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    Console.WriteLine(ex.InnerException);
                }


                //DISPLAING REAPATED FILES
                if (repetings == true)
                {
                        logBox.Text += Environment.NewLine + "Repeating elements:" + Environment.NewLine + Environment.NewLine;
                        for (int i = 0; i < repeatedImages.Count; i++)
                        {
                            //Console.WriteLine(imagesList[i].repeatedWith);
                            if (repeatedImages[i].repeatedWith != null)
                            {
                                //if(imagesList[i].repeatedBigger == true)
                                //{
                                //    logBox.Font = bolded;
                                //    logBox.Text += imagesList[i].filename + imagesList[i].extension;
                                //    logBox.Font = normal;
                                //    logBox.Text += " -> " + imagesList[i].repeatedWith + Environment.NewLine;
                                //}
                                logBox.Text += repeatedImages[i].filename + repeatedImages[i].extension + " -> " + repeatedImages[i].repeatedWith + Environment.NewLine;
                            }
                        }
                    
                }

                File.WriteAllText("log.txt", logBox.Text);

                if (logfile)
                {
                    
                    if (Directory.Exists("Json"))
                    {
                        File.WriteAllText("Json/" + folderName+".json", JsonConvert.SerializeObject(imagesList, Formatting.Indented)); 
                    }
                    else
                    {
                        Directory.CreateDirectory("Json");
                        File.WriteAllText("Json/" + folderName + ".json", JsonConvert.SerializeObject(imagesList, Formatting.Indented));
                    }
                }

                //bolded.Dispose();
            }
        }

        private void stop_Click(object sender, EventArgs e)
        {

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
        public bool repeatedBigger;

        [JsonConstructor]
        public Images(string _filename, string _extension, string _path, int _size, string _hash,bool _repeatedBigger)
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

        public Images(bool _repeatedBigger)
        {
            repeatedBigger = _repeatedBigger;
        }
    }
}
