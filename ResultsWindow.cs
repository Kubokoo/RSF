﻿using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

namespace RSF
{
    public partial class ResultsWindow : Form
    {
        int i = 0;

        void ShowResults()
        {
            if (RSF.repeatedImages.Count == 0)   //USED WHEN NO REPATED IMAGES ARE AVALIABLE
            {
                MessageBox.Show("There's no repeated images to show.");
                return;
            }

            imageLoad();
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

        private void imageLoad(object sender = null, System.EventArgs e = null)
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
                if (RSF.repeatedImages[i].notFoundGUI)
                {
                    noFile(RSF.repeatedImages[i].filename + RSF.repeatedImages[i].extension, false, RSF.repeatedImages[i].notFoundGUI);
                }
                else
                {
                    TextBoxLeft.Text = RSF.repeatedImages[i].filename.ToString() + RSF.repeatedImages[i].extension.ToString();  //TODO BITMAP CLASS HAS THUMBNAIL OPTION(using it intsted of normal image[what dimentions it has)
                    textBoxSizeLeft.Text = RSF.repeatedImages[i].size.ToString();
                    pictureBoxLeft.Image = Images.loadBitmap(RSF.repeatedImages[i].path,
                        RSF.repeatedImages[i].extension,
                        pictureBoxLeft.Size.Width,
                        pictureBoxLeft.Size.Height);
                }
            }
            catch (IOException)
            {
                noFile(RSF.repeatedImages[i].filename + RSF.repeatedImages[i].extension, false, RSF.repeatedImages[i].notFoundGUI);
            }

            try
            {
                if (RSF.repeatedImages[i].repeatedNotFoundGUI)
                {
                    noFile(RSF.repeatedImages[i].repeatedWith, false, RSF.repeatedImages[i].repeatedNotFoundGUI);
                }
                else
                {
                    TextBoxRight.Text = RSF.repeatedImages[i].repeatedWith.ToString();
                    textBoxSizeRight.Text = RSF.repeatedImages[i].repeatedBigger.ToString();
                    pictureBoxRight.Image = Images.loadBitmap(RSF.repeatedImages[i].repeatedWithPath,
                        RSF.repeatedImages[i].extension,
                        pictureBoxRight.Size.Width,
                        pictureBoxRight.Size.Height);
                }
            }
            catch (IOException)
            {
                noFile(RSF.repeatedImages[i].repeatedWith, false, RSF.repeatedImages[i].repeatedNotFoundGUI);
            }
        }

        private void DeleteLeft_Click(object sender, System.EventArgs e)
        {
            int temp = i;
            i--;
            imageLoad();
            FileSystem.DeleteFile(RSF.repeatedImages[temp].path, UIOption.AllDialogs, RecycleOption.SendToRecycleBin);
        }

        private void DeleteRight_Click(object sender, System.EventArgs e)
        {
            int temp = i;
            i++;
            imageLoad();
            try
            {
                FileSystem.DeleteFile(RSF.repeatedImages[temp].repeatedWithPath, UIOption.AllDialogs, RecycleOption.SendToRecycleBin);
            }
            catch (System.Exception)
            {
                MessageBox.Show("File coudn't be removed");
            }
            
        } 

        private void noFile(string fileName, bool orginalFile, bool notFound)
        {
            MessageBox.Show("This file no longer exists" + fileName);
            if (orginalFile && !notFound)
            {
                pictureBoxLeft.Image = Properties.Resources.link_broken;
                RSF.repeatedImages[i].notFoundGUI = true;
            }
            if (!orginalFile && !notFound)
            {
                pictureBoxRight.Image = Properties.Resources.link_broken;
                RSF.repeatedImages[i].repeatedNotFoundGUI = true;
            }
        }
    }
}
