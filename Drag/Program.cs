using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Drag
{
    class Program
    {
        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };
        static void Main(string[] args)
        {
            var s = Directory.GetFiles(".");
            int count = 0;

            Console.WriteLine("Converting 0 of {0} Images.", s.Length -1);
            foreach (var s1 in s)
            {
                count++;
                try
                {
                    Console.Clear();
                    Console.WriteLine("Converting {0} of {1} Images.", count, s.Length-1);
                    if (ImageExtensions.Contains(Path.GetExtension(s1).ToUpperInvariant()))
                    {
                        var image = Image.FromFile(s1);
                        var newImage = ScaleImage(image, 800, 600);
                        newImage.Save(@"temp.jpg", ImageFormat.Jpeg);
                        image.Dispose();
                        newImage.Dispose();

                        File.Delete(s1);
                        File.Move(@"temp.jpg", s1);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Remove the non pictures from the folder.");
                }
            }
        }

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            return newImage;
        }
    }



}
