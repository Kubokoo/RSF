using System.Drawing;
using System.IO;
using System.Windows.Forms;

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

            if (RSF.repeatedImages.Count == i) i = 0;
            if (i == -1) i = RSF.repeatedImages.Count - 1;

            try
            {
                TextBoxLeft.Text = RSF.repeatedImages[i].filename.ToString() + RSF.repeatedImages[i].extension.ToString();
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
    }
}
