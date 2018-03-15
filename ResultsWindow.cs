using System.Drawing;
using System.Windows.Forms;

namespace RSF
{
    public partial class ResultsWindow : Form
    {
        int i = 0;

        void ShowResults() //TODO USES TO MUCH RAM!!!!!!!
        {
            if(pictureBoxLeft.Image!=null)
            {
                pictureBoxLeft.Image.Dispose();
                pictureBoxRight.Image.Dispose();
            }
            
            if (RSF.repeatedImages.Count == i) i = 0;
            if (i == -1) i = RSF.repeatedImages.Count - 1;


            TextBoxLeft.Text = RSF.repeatedImages[i].filename.ToString() + RSF.repeatedImages[i].extension.ToString(); //TODO ADD EXEPTOIONS IF NOT PROCESSED (NO FILE TO SHOW)
            Bitmap previewLeft = new Bitmap(RSF.repeatedImages[i].path);   //TODO Add bigger reolution for bigger window
            pictureBoxLeft.Image = previewLeft;

            TextBoxRight.Text = RSF.repeatedImages[i].repeatedWith.ToString();
            Bitmap previewRight = new Bitmap(RSF.repeatedImages[i].repeatedWithPath); //TODO ADD PROPER RESIVE OF FIELDS WITH IMAGES
            pictureBoxRight.Image = previewRight;
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
    }
}
