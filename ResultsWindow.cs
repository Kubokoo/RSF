using System.Drawing;
using System.Windows.Forms;

namespace RSF
{
    public partial class ResultsWindow : Form
    {
        public ResultsWindow()
        {
            InitializeComponent();
            TextBoxLeft.Text = RSF.repeatedImages[0].filename.ToString() + RSF.repeatedImages[0].extension.ToString(); //TODO ADD EXEPTOIONS IF NOT PROCESSED (NO FILE TO SHOW)
            Bitmap previewLeft = new Bitmap(Image.FromFile(RSF.repeatedImages[0].path), pictureBoxLeft.Size);
            pictureBoxLeft.Image = previewLeft;

            TextBoxRight.Text = RSF.repeatedImages[0].repeatedWith.ToString();
            Bitmap previewRight = new Bitmap(Image.FromFile(RSF.repeatedImages[0].repeatedWithPath), pictureBoxLeft.Size); //TODO ADD PROPER RESIVE OF FIELDS WITH IMAGES
            pictureBoxRight.Image = previewRight;

            //for(int i = 0; i < RSF.repeatedImages.Count(); i++)
            //{
            //    TextBoxLeft.Text += RSF.repeatedImages[i].path.ToString();
            //}
        }

    }
}
