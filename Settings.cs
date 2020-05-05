using System.Drawing;
using ImageProcessor.Plugins.WebP.Imaging.Formats;
using System.IO;

namespace RSF
{
    public static class Settings
    {
        private static int accuracy;
        private static bool jsonSaving;

        public static int Accuracy
        {
            get
            {
                return accuracy;
            }

            set
            {
                accuracy = value;
            }
        }

        public static bool JsonSaving
        {
            get
            {
                return jsonSaving;
            }

            set
            {
                jsonSaving = value;
            }
        }

        public static Bitmap loadBitmap (string path, string extension)
        {
            Bitmap bitmap;          

            if (extension == ".webp")
            {
                WebPFormat webpDecoder = new WebPFormat();
                bitmap = (Bitmap)webpDecoder.Load(File.Open(path, FileMode.Open));
            }
            else
            {
                bitmap = new Bitmap(Image.FromFile(path), new Size(Accuracy, Accuracy));
            }

            return bitmap;
        }
    }
}
