using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace LogoKaresz
{
    public partial class Form1 : Form
    {
        const string path = "./image.png";
        bool run = false;

        void FELADAT()
        {
            if (run) return;
            else run = true;

            using (new Frissítés(false))
            using (new Rajzol(true))
            {
                Color[] data;
                int width;

                using (var bitmap = new Bitmap(path))
                {
                    width = bitmap.Width;

                    var rect = new Rectangle(0, 0, width, bitmap.Height);
                    var bmpData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);

                    int byteCount = Math.Abs(bmpData.Stride) * bitmap.Height;
                    var bytes = new byte[byteCount];

                    System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, bytes, 0, byteCount);
                    bitmap.UnlockBits(bmpData);

                    data = new Color[byteCount >> 2];

                    int j = 0;
                    for (int i = 0; i < byteCount - 3; i += 4) {
                        data[j++] = Color.FromArgb(bytes[i + 3], bytes[i + 2], bytes[i + 1], bytes[i]);
                    }
                }

                int x = 1, y = 1;
                for (int i = 0; i < data.Length; i++) {
                    if (i != 0) {
                        if (i % width == 0) {
                            x++;
                            y = 1;
                        } else y++;
                    }
                    Teleport(y, x);
                    Tollszín(data[i]);
                    using (new Átmenetileg(Előre, -1)) ;
                }
            }
        }
    }
}