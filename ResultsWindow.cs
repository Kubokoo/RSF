using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

namespace RSF
{
    public partial class ResultsWindow : Form
    {
        int i = 0;

        void ShowResults() //TODO USES TO MUCH RAM!!!!!!!
        {
            if(RSF.repeatedImages.Count == 0)   //USED WHEN NO REPATED IMAGES ARE AVALIABLE
            {
                MessageBox.Show("There's no repeated images to show.");
                pictureBoxRight.Image = Properties.Resources.link_broken;
                pictureBoxLeft.Image = Properties.Resources.link_broken;
                return;
            }

            imageLoad(null,null);
            
        }

        public ResultsWindow()
        {
            InitializeComponent();
            ShowResults();
        }

        private void buttonNext_Click(object sender, System.EventArgs e)
        {
            i++;
            ShowResults();
        }

        private void buttonPrevious_Click(object sender, System.EventArgs e)
        {
            i--;
            ShowResults();
        }

        private void imageLoad(object sender, System.EventArgs e)
        {
            if (pictureBoxLeft.Image != null)
            {
                pictureBoxLeft.Image.Dispose();
                pictureBoxRight.Image.Dispose();
                System.GC.Collect();
            }

            if (i >= RSF.repeatedImages.Count) i = 0;
            if (i <= -1) i = RSF.repeatedImages.Count - 1;

            try
            {
                TextBoxLeft.Text = RSF.repeatedImages[i].filename.ToString() + RSF.repeatedImages[i].extension.ToString();  //TODO BITMAP CLASS HAS THUMBNAIL OPTION(using it intsted of normal image[what dimentions it has)
                Bitmap previewLeft = new Bitmap(Image.FromFile(RSF.repeatedImages[i].path), pictureBoxRight.Size);  //TODO Add bigger reolution for bigger window
                pictureBoxLeft.Image = previewLeft; //TODO TRY DISPOSING IT OUTSIDE TRY
            }
            catch (IOException)
            {
                MessageBox.Show("This file no longer exists: " + RSF.repeatedImages[i].filename.ToString() + RSF.repeatedImages[i].extension.ToString());
                pictureBoxLeft.Image = Properties.Resources.link_broken;
            }

            try
            {
                TextBoxRight.Text = RSF.repeatedImages[i].repeatedWith.ToString();
                Bitmap previewRight = new Bitmap(Image.FromFile(RSF.repeatedImages[i].repeatedWithPath), pictureBoxLeft.Size); //TODO ADD PROPER RESIVE OF FIELDS WITH IMAGES
                pictureBoxRight.Image = previewRight;
            }
            catch (IOException)
            {
                MessageBox.Show("This file no longer exists" + RSF.repeatedImages[i].repeatedWith.ToString());
                pictureBoxRight.Image = Properties.Resources.link_broken;
            }
        }

        private void DeleteLeft_Click(object sender, System.EventArgs e)
        {
            int temp = i;
            i--;
            imageLoad(null, null);
            FileSystem.DeleteFile(RSF.repeatedImages[temp].path, UIOption.AllDialogs, RecycleOption.SendToRecycleBin);
        }

        private void DeleteRight_Click(object sender, System.EventArgs e)
        {
            int temp = i;
            i++;
            imageLoad(null, null);
            try
            {
                FileSystem.DeleteFile(RSF.repeatedImages[temp].repeatedWithPath, UIOption.AllDialogs, RecycleOption.SendToRecycleBin);
            }
            catch (System.Exception)
            {
                MessageBox.Show("File coudn't be removed");
            }
            
        } 
    }
}
