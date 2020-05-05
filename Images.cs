using Newtonsoft.Json;
using System.Drawing;
using System.IO;
using System;
using ImageProcessor.Plugins.WebP.Imaging.Formats;

namespace RSF
{
    public class Images
    {
        public string filename;
        public string extension;
        public string path;
        public int size;
        public string hash;
        public bool[] imageHash;
        public string repeatedWith;
        public bool notFoundGUI;
        public bool repeatedNotFoundGUI;

        [JsonConstructor]
        public Images(string _filename, string _extension, string _path, int _size, bool _repeatedBigger = false)
        {
            filename = _filename;
            extension = _extension;
            path = _path;
            size = _size;
            hash = HashElement(_path);
            repeatedBigger = _repeatedBigger;
            notFoundGUI = false;
            repeatedNotFoundGUI = false;
        }

        public Images(bool[] _imageHash)
        {
            imageHash = _imageHash;
        }

        public Images(string _repeatedWith)
        {
            repeatedWith = _repeatedWith;
        }

        public string repeatedWithPath { get; set; }
        public bool repeatedBigger { get; set; }

        
        public bool[] imageHashing(string path, string extension) //GETTING VALUES OF LIGHT AND DARK (FOR IMAGE COMPARING)
        {
            bool[] b = new bool[Settings.Accuracy * Settings.Accuracy];

            Bitmap bitmap = loadBitmap(path, extension);

            int k = 0;
            for (int i = 0; i < Settings.Accuracy; i++)
            {
                for (int j = 0; j < Settings.Accuracy; j++)
                {
                    b[k] = (bitmap.GetPixel(i, j).GetBrightness() < 0.5f); //TODO całą tablicę byte[] zapisać jako string i porównywać czy takie same
                    //Console.WriteLine("i:" + i + ", j:" + j);
                    k++;
                }
            }

            return b;
        }

        //GETING FILE HASH (FOR EASY FILE COMPARING)
        public string HashElement(string element)
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

        public static Bitmap loadBitmap(string path, string extension, int sizeX = -1, int sizeY = -1)
        {
            Bitmap bitmap;

            if (extension == ".webp")
            {
                WebPFormat webpDecoder = new WebPFormat();
                bitmap = (Bitmap)webpDecoder.Load(File.Open(path, FileMode.Open));
            }

            else
            {
                if (sizeX == -1 || sizeY == -1)
                {
                    bitmap = new Bitmap(Image.FromFile(path), new Size(Settings.Accuracy, Settings.Accuracy));
                }
                else
                {
                    bitmap = new Bitmap(Image.FromFile(path), new Size(sizeX, sizeY));
                }
            }

            return bitmap;           
        }
    }
}
