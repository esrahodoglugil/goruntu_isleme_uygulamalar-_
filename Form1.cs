using System.Reflection;

namespace goruntu_isleme_uygulamaları_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog std = new OpenFileDialog();
            std.ShowDialog();
            pictureBox1.ImageLocation = std.FileName;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void griDönüşümToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(pictureBox1.Image);
            Bitmap gri = GriDonusum(image);
            pictureBox2.Image = gri;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        static Bitmap GriDonusum(Bitmap GirisResmi)
        {
            int ResimYukseligi = GirisResmi.Height;
            int ResimGenisligi = GirisResmi.Width;
            Bitmap grayImage = new Bitmap(GirisResmi.Width, GirisResmi.Height);

            for (int i = 0; i < ResimGenisligi; i++)
            {
                for (int j = 0; j < ResimYukseligi; j++)
                {
                    Color OkunanRenk = GirisResmi.GetPixel(i, j);

                    int griResim = (int)(OkunanRenk.R * 0.3 + OkunanRenk.G * 0.6 + OkunanRenk.B * 0.1);

                    Color DonusenRenk = Color.FromArgb(griResim, griResim, griResim);
                    grayImage.SetPixel(i, j, DonusenRenk);

                }
            }

            return grayImage;
        }
        private void binaryDönüşümToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(pictureBox1.Image);
            Bitmap binary = binaryYap(image);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Image = binary;
        }
        private Bitmap binaryYap(Bitmap bmp)
        {
            int tmp = 0;
            Bitmap gri = GriDonusum(bmp);
            int esik = esikBul(gri);
            Color renk;
            for (int i = 0; i < gri.Height - 1; i++)
            {
                for (int j = 0; j < gri.Width - 1; j++)
                {
                    tmp = gri.GetPixel(j, i).G;
                    if (tmp < esik)
                    {
                        renk = Color.FromArgb(0, 0, 0);
                        gri.SetPixel(j, i, renk);
                    }
                    else
                    {
                        renk = Color.FromArgb(255, 255, 255);
                        gri.SetPixel(j, i, renk);
                    }
                }

            }
            return gri;
        }
        private int esikBul(Bitmap gri)
        {
            int enb = gri.GetPixel(0, 0).G;
            int enk = gri.GetPixel(0, 0).G;
            for (int i = 0; i < gri.Height - 1; i++)
            {
                for (int j = 0; j < gri.Width - 1; j++)
                {
                    if (enb > gri.GetPixel(j, i).G)
                        enb = gri.GetPixel(j, i).G;
                    if (enk < gri.GetPixel(j, i).G)
                        enk = gri.GetPixel(j, i).G;

                }
            }
            int a = enb;
            int b = enk;
            int esik = (a + b) / 2;
            return esik;
        }

        private void görüntüKırpmaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int x = 100;
            int y = 100;
            int width = 200;
            int height = 200;

            Bitmap image = new Bitmap(pictureBox1.Image);
            Bitmap kirp = resimKirp(image, new Rectangle(x, y, width, height));
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Image = kirp;
        }
        private Bitmap resimKirp(Bitmap image, Rectangle cropArea)
        {
            Bitmap kirp = image.Clone(cropArea, image.PixelFormat);
            return kirp;
        }

        private void rGBtoHSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(pictureBox1.Image);
            Bitmap hsv = new Bitmap(image.Width, image.Height);
            pictureBox2.Image = hsv;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;


            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    Color pixelColor = image.GetPixel(j, i);
                    Color hsvColor = RGBtoHSV(pixelColor);
                    hsv.SetPixel(j, i, hsvColor);
                }
            }
        }
        private Color RGBtoHSV(Color rgb)
        {
            float r = (float)rgb.R / 255;
            float g = (float)rgb.G / 255;
            float b = (float)rgb.B / 255;

            float max = Math.Max(r, Math.Max(g, b));
            float min = Math.Min(r, Math.Min(g, b));
            float delta = max - min;

            float h = 0, s = 0, v = max;

            if (delta != 0)
            {
                if (max == r)
                    h = (g - b) / delta;
                else if (max == g)
                    h = 2 + (b - r) / delta;
                else
                    h = 4 + (r - g) / delta;

                h *= 60;
                if (h < 0)
                    h += 360;

                s = delta / max;
            }

            return Color.FromArgb((int)(h / 360 * 255), (int)(s * 255), (int)(v * 255));
        }

        private void rGBtoCMYKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(pictureBox1.Image);
            Bitmap cmyk = new Bitmap(image.Width, image.Height);
            pictureBox2.Image = cmyk;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;


            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    Color pixelColor = image.GetPixel(j, i);
                    Color rgbcmyk = RGBtoCMYK(pixelColor);
                    cmyk.SetPixel(j, i, rgbcmyk);
                }
            }
        }
        private Color RGBtoCMYK(Color rgb)
        {
            float r = (float)rgb.R / 255;
            float g = (float)rgb.G / 255;
            float b = (float)rgb.B / 255;

            float k = 1 - Math.Max(r, Math.Max(g, b));

            if (k == 1)
            {
                return Color.FromArgb(0, 0, 0, 255);
            }

            float c = (1 - r - k) / (1 - k);
            float m = (1 - g - k) / (1 - k);
            float y = (1 - b - k) / (1 - k);

            return Color.FromArgb((int)(c * 255), (int)(m * 255), (int)(y * 255), (int)(k * 255));
        }
        
        private void eklemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            OpenFileDialog openFileDialog2 = new OpenFileDialog();
            openFileDialog1.Filter = "resim dosyaları| *.jpg; *.jpeg; *.png; *.gif; *.bmp;";
            openFileDialog2.Filter = "resim dosyaları| *.jpg; *.jpeg; *.png; *.gif; *.bmp;";
            openFileDialog1.Title = "ilk resim";
            openFileDialog2.Title = "ikinci resim";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Bitmap resim1 = new Bitmap(openFileDialog1.FileName);

                if (openFileDialog2.ShowDialog() == DialogResult.OK)
                {
                    Bitmap resim2 = new Bitmap(openFileDialog2.FileName);

                    Bitmap sonucResim = Toplama(resim1, resim2);
                    pictureBox2.Image = sonucResim;
                    pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }
        private Bitmap Toplama(Bitmap resim1, Bitmap resim2)
        {
            if (resim1.Width != resim2.Width || resim1.Height != resim2.Height)
            {
                throw new ArgumentException("Resimler aynı boyutta olmalıdır.");
            }

            Bitmap yeniResim = new Bitmap(resim1.Width, resim1.Height);

            for (int y = 0; y < resim1.Height; y++)
            {
                for (int x = 0; x < resim1.Width; x++)
                {
                    Color piksel1 = resim1.GetPixel(x, y);
                    Color piksel2 = resim2.GetPixel(x, y);

                    int yeniR = Math.Min(255, piksel1.R + piksel2.R);
                    int yeniG = Math.Min(255, piksel1.G + piksel2.G);
                    int yeniB = Math.Min(255, piksel1.B + piksel2.B);

                    Color yeniRenk = Color.FromArgb(yeniR, yeniG, yeniB);
                    yeniResim.SetPixel(x, y, yeniRenk);
                }
            }
            return yeniResim;
        }

        private void çarpmaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            OpenFileDialog openFileDialog2 = new OpenFileDialog();
            openFileDialog1.Filter = "resim dosyaları| *.jpg; *.jpeg; *.png; *.gif; *.bmp;";
            openFileDialog2.Filter = "resim dosyaları| *.jpg; *.jpeg; *.png; *.gif; *.bmp;";
            openFileDialog1.Title = "ilk resim";
            openFileDialog2.Title = "ikinci resim";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Bitmap resim1 = new Bitmap(openFileDialog1.FileName);

                if (openFileDialog2.ShowDialog() == DialogResult.OK)
                {
                    Bitmap resim2 = new Bitmap(openFileDialog2.FileName);

                    Bitmap sonucResim = Carpma(resim1, resim2);
                    pictureBox2.Image = sonucResim;
                    pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }
        private Bitmap Carpma(Bitmap resim1, Bitmap resim2)
        {
            if (resim1.Width != resim2.Width || resim1.Height != resim2.Height)
            {
                throw new ArgumentException("Resimler aynı boyutta olmalıdır.");
            }

            Bitmap yeniResim = new Bitmap(resim1.Width, resim1.Height);

            for (int y = 0; y < resim1.Height; y++)
            {
                for (int x = 0; x < resim1.Width; x++)
                {
                    Color piksel1 = resim1.GetPixel(x, y);
                    Color piksel2 = resim2.GetPixel(x, y);

                    int yeniR = (int)((piksel1.R / 255.0) * (piksel2.R / 255.0) * 255);
                    int yeniG = (int)((piksel1.G / 255.0) * (piksel2.G / 255.0) * 255);
                    int yeniB = (int)((piksel1.B / 255.0) * (piksel2.B / 255.0) * 255);

                    Color yeniRenk = Color.FromArgb(yeniR, yeniG, yeniB);
                    yeniResim.SetPixel(x, y, yeniRenk);
                }
            }

            return yeniResim;
        }

        private void kenarBulmaAlgoritmalarınınKullanımısobelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(pictureBox1.Image);
            Bitmap sobel = sobelYap(image);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Image = sobel;
        }
        private Bitmap sobelYap(Bitmap image)
        {
            Bitmap gri = GriDonusum(image);
            Bitmap buffer = new Bitmap(gri.Width, gri.Height);
            Color renk;
            int valx, valy, gradient;
            int[,] GX = new int[3, 3];
            int[,] GY = new int[3, 3];

            GX[0, 0] = -1; GX[0, 1] = 0; GX[0, 2] = 1;
            GX[1, 0] = -2; GX[1, 1] = 0; GX[1, 2] = 2;
            GX[2, 0] = -1; GX[2, 1] = 0; GX[2, 2] = 1;

            GY[0, 0] = -1; GY[0, 1] = -2; GY[0, 2] = -1;
            GY[1, 0] = 0; GY[1, 1] = 0; GY[1, 2] = 0;
            GY[2, 0] = 1; GY[2, 1] = 2; GY[2, 2] = 1;


            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    if (i == 0 || i == gri.Height - 1 || j == 0 || j == gri.Width - 1)
                    {
                        renk = Color.FromArgb(255, 255, 255);
                        buffer.SetPixel(j, i, renk);
                        valx = 0;
                        valy = 0;
                    }
                    else
                    {
                        valx = gri.GetPixel(j - 1, i - 1).R * GX[0, 0]
                            + gri.GetPixel(j, i - 1).R * GX[0, 1]
                            + gri.GetPixel(j + 1, i - 1).R * GX[0, 2]
                            + gri.GetPixel(j - 1, i).R * GX[1, 0]
                            + gri.GetPixel(j, i).R * GX[1, 1]
                            + gri.GetPixel(j + 1, i).R * GX[1, 2]
                            + gri.GetPixel(j - 1, i + 1).R * GX[2, 0]
                            + gri.GetPixel(j, i + 1).R * GX[2, 1]
                            + gri.GetPixel(j + 1, i + 1).R * GX[2, 2];

                        valy = gri.GetPixel(j - 1, i - 1).R * GY[0, 0]
                             + gri.GetPixel(j, i - 1).R * GY[0, 1]
                             + gri.GetPixel(j + 1, i - 1).R * GY[0, 2]
                             + gri.GetPixel(j - 1, i).R * GY[1, 0]
                             + gri.GetPixel(j, i).R * GY[1, 1]
                             + gri.GetPixel(j + 1, i).R * GY[1, 2]
                             + gri.GetPixel(j - 1, i + 1).R * GY[2, 0]
                             + gri.GetPixel(j, i + 1).R * GY[2, 1]
                             + gri.GetPixel(j + 1, i + 1).R * GY[2, 2];

                        gradient = (int)(Math.Abs(valx) + Math.Abs(valy));


                        if (gradient < 0)
                            gradient = 0;
                        if (gradient > 255)
                            gradient = 255;

                        renk = Color.FromArgb(gradient, gradient, gradient);
                        buffer.SetPixel(j, i, renk);


                    }
                }
            }
            return buffer;
        }

        private void konvolüsyonİşlemigaussToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(pictureBox1.Image);
            Bitmap gauss = gaussFiltresi(image);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Image = gauss;
        }
        private Bitmap gaussFiltresi(Bitmap image)
        {
            Color OkunanRenk;
            Bitmap GirisResmi, CikisResmi;
            GirisResmi = new Bitmap(pictureBox1.Image);
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int SablonBoyutu = 5;
            int ElemanSayisi = SablonBoyutu * SablonBoyutu;
            int x, y, i, j, toplamR, toplamG, toplamB, ortalamaR, ortalamaG, ortalamaB;
            int[] Matris = { 1, 4, 7, 4, 1, 4, 20, 33, 20, 4, 7, 33, 55, 33, 7, 4, 20, 33, 20, 4, 1, 4, 7, 4, 1 };
            int MatrisToplami = 1 + 4 + 7 + 4 + 1 + 4 + 20 + 33 + 20 + 4 + 7 + 33 + 55 + 33 + 7 + 4 + 20 + 33 + 20 + 4 + 1 + 4 + 7 + 4 + 1;
            for (x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
            {
                for (y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    toplamR = 0;
                    toplamG = 0;
                    toplamB = 0;
                    int k = 0;
                    for (i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                    {
                        for (j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                        {
                            OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                            toplamR = toplamR + OkunanRenk.R * Matris[k];
                            toplamG = toplamG + OkunanRenk.G * Matris[k];
                            toplamB = toplamB + OkunanRenk.B * Matris[k];
                            k++;
                        }
                    }
                    ortalamaR = toplamR / MatrisToplami;
                    ortalamaG = toplamG / MatrisToplami;
                    ortalamaB = toplamB / MatrisToplami;
                    CikisResmi.SetPixel(x, y, Color.FromArgb(ortalamaR, ortalamaG, ortalamaB));
                }
            }
            pictureBox2.Image = CikisResmi;
            return CikisResmi;

        }


        private void görüntüyeFiltreUygulanmasıBlurringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(pictureBox1.Image);
            int bulaniklikMiktari = 5;
            Bitmap bulanik = bulaniklastir(image, bulaniklikMiktari);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Image = bulanik;
        }
        private Bitmap bulaniklastir(Bitmap image, int bulaniklikMiktari)
        {
            Bitmap bulaniklastirilmisGörüntü = new Bitmap(image.Width, image.Height);

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color bulanikRenk = CalculateBlurredColor(image, x, y, bulaniklikMiktari);
                    bulaniklastirilmisGörüntü.SetPixel(x, y, bulanikRenk);
                }
            }

            return bulaniklastirilmisGörüntü;
        }

        static Color CalculateBlurredColor(Bitmap image, int x, int y, int bulaniklikMiktari)
        {
            int totalR = 0, totalG = 0, totalB = 0;
            int count = 0;

            for (int i = x - bulaniklikMiktari; i <= x + bulaniklikMiktari; i++)
            {
                for (int j = y - bulaniklikMiktari; j <= y + bulaniklikMiktari; j++)
                {
                    if (i >= 0 && i < image.Width && j >= 0 && j < image.Height)
                    {
                        Color pixelColor = image.GetPixel(i, j);
                        totalR += pixelColor.R;
                        totalG += pixelColor.G;
                        totalB += pixelColor.B;
                        count++;
                    }
                }
            }

            int avgR = totalR / count;
            int avgG = totalG / count;
            int avgB = totalB / count;

            return Color.FromArgb(avgR, avgG, avgB);
        }

        private void görüntüDöndürmeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(pictureBox1.Image);
            Bitmap gri = Dondurme(image);
            pictureBox2.Image = gri;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        public Bitmap Dondurme(Bitmap GirisResmi)
        {

            Bitmap CikisResmi = new Bitmap(GirisResmi.Width, GirisResmi.Height);

            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;

            float centerX = ResimGenisligi / 2f;
            float centerY = ResimYuksekligi / 2f;

            int donme_acisi = 90;

            float radyan = donme_acisi * ((float)Math.PI / 180f);

            for (int i = 0; i < ResimGenisligi; i++)
            {
                for (int j = 0; j < ResimYuksekligi; j++)
                {
                    float deltaX = i - centerX;
                    float deltaY = j - centerY;

                    int X = (int)(deltaX * Math.Cos(radyan) - deltaY * Math.Sin(radyan) + centerX);
                    int Y = (int)(deltaX * Math.Sin(radyan) + deltaY * Math.Cos(radyan) + centerY);

                    if (X >= 0 && X < CikisResmi.Width && Y >= 0 && Y < CikisResmi.Height)
                    {
                        CikisResmi.SetPixel(X, Y, GirisResmi.GetPixel(i, j));
                    }
                }
            }

            return CikisResmi;
        }

        private void görüntüUzaklaştırmaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(pictureBox1.Image);
            Bitmap gri = Goruntu_Uzaklastirma(image);
            pictureBox2.Image = gri;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        public Bitmap Goruntu_Uzaklastirma(Bitmap GirisResmi)
        {

            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;
            Bitmap CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

            int x2 = 0, y2 = 0;
            int KucultmeKatsayisi = 10;
            for (int x1 = 0; x1 < ResimGenisligi; x1 = x1 + KucultmeKatsayisi)
            {
                y2 = 0;
                for (int y1 = 0; y1 < ResimYuksekligi; y1 = y1 + KucultmeKatsayisi)
                {
                    Color OkunanRenk = GirisResmi.GetPixel(x1, y1);


                    CikisResmi.SetPixel(x2, y2, OkunanRenk);
                    y2++;
                }
                x2++;
            }
            return CikisResmi;
        }

        private void görüntüYaklaştırmaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(pictureBox1.Image);
            Bitmap gri = Goruntu_Yakinlastirma(image);
            pictureBox2.Image = gri;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        public Bitmap Goruntu_Yakinlastirma(Bitmap GirisResmi)
        {
            int ResimGenisligi = GirisResmi.Width;
            int ResimYuksekligi = GirisResmi.Height;

            int CikisGenisligi = ResimGenisligi * 2;
            int CikisYuksekligi = ResimYuksekligi * 2;
            Bitmap CikisResmi = new Bitmap(CikisGenisligi, CikisYuksekligi);

            int BuyutmeKatsayisi = 3;

            for (int x1 = 0; x1 < ResimGenisligi; x1++)
            {
                for (int y1 = 0; y1 < ResimYuksekligi; y1++)
                {
                    Color OkunanRenk = GirisResmi.GetPixel(x1, y1);

                    for (int i = 0; i < BuyutmeKatsayisi; i++)
                    {
                        for (int j = 0; j < BuyutmeKatsayisi; j++)
                        {
                            int x2 = x1 * BuyutmeKatsayisi + i;
                            int y2 = y1 * BuyutmeKatsayisi + j;

                            if (x2 < CikisResmi.Width && y2 < CikisResmi.Height)
                                CikisResmi.SetPixel(x2, y2, OkunanRenk);
                        }
                    }
                }
            }
            return CikisResmi;
        }

        private void germeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(pictureBox1.Image);
            Bitmap gri = HistogramGermesiYap(image);
            pictureBox2.Image = gri;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        public static Bitmap HistogramGermesiYap(Bitmap GirisResmi)
        {

            int ResimYuksekligi = GirisResmi.Height;
            int ResimGenisligi = GirisResmi.Width;
            Bitmap CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

            int minPixel = 255;
            int maxPixel = 0;

            for (int i = 0; i < ResimGenisligi; i++)
            {
                for (int j = 0; j < ResimYuksekligi; j++)
                {
                    Color pixelColor = GirisResmi.GetPixel(i, j);
                    int pixelDegisken = pixelColor.R;

                    if (pixelDegisken < minPixel)
                        minPixel = pixelDegisken;
                    if (pixelDegisken > maxPixel)
                        maxPixel = pixelDegisken;
                }
            }
            for (int i = 0; i < ResimGenisligi; i++)
            {
                for (int j = 0; j < ResimYuksekligi; j++)
                {
                    Color pixelColor = GirisResmi.GetPixel(i, j);
                    int pixelDegisken = pixelColor.R;

                    int yeniPixel = (pixelDegisken - minPixel) * 255 / (maxPixel - minPixel);

                    Color yeniPixelRengi = Color.FromArgb(yeniPixel, yeniPixel, yeniPixel);
                    CikisResmi.SetPixel(i, j, yeniPixelRengi);

                }
            }

            return CikisResmi;
        }

        private void parlaklıkArtırmaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(pictureBox1.Image);
            Bitmap gri = ParlaklikArttir(image);
            pictureBox2.Image = gri;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        public Bitmap ParlaklikArttir(Bitmap GirisResmi)
        {

            int ResimGenisligi = GirisResmi.Width;
            int ResimYukseligi = GirisResmi.Height;
            Bitmap CikisResmi = new Bitmap(ResimGenisligi, ResimYukseligi);

            int parlaklikArrtirma = 50;
            for (int x = 0; x < ResimGenisligi; x++)
            {
                for (int y = 0; y < ResimYukseligi; y++)
                {
                    Color OkunanRenk = GirisResmi.GetPixel(x, y);

                    int R1 = Math.Min(OkunanRenk.R + parlaklikArrtirma, 255);
                    int G1 = Math.Min(OkunanRenk.G + parlaklikArrtirma, 255);
                    int B1 = Math.Min(OkunanRenk.B + parlaklikArrtirma, 255);

                    Color DonusenRenk = Color.FromArgb(R1, G1, B1);
                    CikisResmi.SetPixel(x, y, DonusenRenk);

                }
            }
            return CikisResmi;
        }

        private void eşiklemeİşlemleriAdaptifEşiklemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(pictureBox1.Image);
            Bitmap gri = EşiklemeTers(image, 155);
            pictureBox2.Image = gri;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        public Bitmap EşiklemeTers(Bitmap girişResim, int eşiklemeDegeri)
        {
            int resimGenişliği = girişResim.Width;
            int resimYüksekliği = girişResim.Height;
            Bitmap çıkışResmi = new Bitmap(resimGenişliği, resimYüksekliği);

            for (int x = 0; x < resimGenişliği; x++)
            {
                for (int y = 0; y < resimYüksekliği; y++)
                {
                    Color okunanRenk = girişResim.GetPixel(x, y);

                    int yeniR = okunanRenk.R >= eşiklemeDegeri ? okunanRenk.R : 0;
                    int yeniG = okunanRenk.G >= eşiklemeDegeri ? okunanRenk.G : 0;
                    int yeniB = okunanRenk.B >= eşiklemeDegeri ? okunanRenk.B : 0;

                    Color donusenRenk = Color.FromArgb(yeniR, yeniG, yeniB);
                    çıkışResmi.SetPixel(x, y, donusenRenk);
                }
            }

            return çıkışResmi;
        }
        private void saltPepperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(pictureBox1.Image);
            Bitmap gri = GaussSaltPepper(image);
            pictureBox2.Image = gri;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        public static Bitmap GaussSaltPepper(Bitmap GirisResmi)
        {
            int ResimYuksekligi = GirisResmi.Height;
            int ResimGenisligi = GirisResmi.Width;

            Bitmap CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            Bitmap griResim = GriDonusum(GirisResmi);

            Random random = new Random();
            int[,] noiseMask = new int[griResim.Height, griResim.Width];

            for (int i = 0; i < griResim.Height; i++)
            {
                for (int j = 0; j < griResim.Width; j++)
                {
                    noiseMask[i, j] = random.Next(0, 21);
                }
            }

            for (int i = 0; i < griResim.Height; i++)
            {
                for (int j = 0; j < griResim.Width; j++)
                {
                    if (noiseMask[i, j] == 0)
                    {
                        griResim.SetPixel(j, i, Color.Black);
                    }
                    else if (noiseMask[i, j] == 20)
                    {
                        griResim.SetPixel(j, i, Color.White);
                    }
                }
            }

            CikisResmi = griResim;

            return CikisResmi;
        }

        private void genişletmeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(pictureBox1.Image);
            Bitmap gri = HistogramGenisleme(image);
            pictureBox2.Image = gri;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        public static Bitmap HistogramGenisleme(Bitmap GirisResmi)
        {
            int ResimYuksekligi = GirisResmi.Height;
            int ResimGenisligi = GirisResmi.Width;
            Bitmap CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

            int minPixel = 255;
            int maxPixel = 0;

            for (int x = 0; x < ResimGenisligi; x++)
            {
                for (int y = 0; y < ResimYuksekligi; y++)
                {
                    Color pixelColor = GirisResmi.GetPixel(x, y);
                    int red = pixelColor.R;
                    int green = pixelColor.G;
                    int blue = pixelColor.B;

                    minPixel = Math.Min(minPixel, Math.Min(red, Math.Min(green, blue)));
                    maxPixel = Math.Max(maxPixel, Math.Max(red, Math.Max(green, blue)));
                }
            }

            for (int i = 0; i < ResimGenisligi; i++)
            {
                for (int j = 0; j < ResimYuksekligi; j++)
                {
                    Color pixelColor = GirisResmi.GetPixel(i, j);
                    int pixelDegisken = pixelColor.R;

                    int yeniPixel = ((256 - 1) / (maxPixel - minPixel)) * (pixelDegisken - minPixel);

                    yeniPixel = Math.Min(255, Math.Max(0, yeniPixel));

                    Color yeniPixelRengi = Color.FromArgb(yeniPixel, yeniPixel, yeniPixel);
                    CikisResmi.SetPixel(i, j, yeniPixelRengi);
                }
            }



            return CikisResmi;

        }

        private void genişlemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(pictureBox1.Image);
            Bitmap gri = GenislemeIslemi(image);
            pictureBox2.Image = gri;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        public static Bitmap GenislemeIslemi(Bitmap GirisResmi)
        {
            int ResimYuksekligi = GirisResmi.Height;
            int ResimGenisligi = GirisResmi.Width;

            Bitmap CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int[,] yapiElemenai = {
                {1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1}
            };


            int elemWidth = yapiElemenai.GetLength(0);
            int elemHeight = yapiElemenai.GetLength(1);

            int elemCenterX = elemWidth / 2;
            int elemCenterY = elemHeight / 2;



            byte[,] imgData = new byte[ResimGenisligi, ResimYuksekligi];
            for (int y = 0; y < ResimYuksekligi; y++)
            {
                for (int x = 0; x < ResimGenisligi; x++)
                {
                    Color pixel = GirisResmi.GetPixel(x, y);
                    imgData[x, y] = (byte)((pixel.R + pixel.G + pixel.B) / 3 > 127 ? 1 : 0);
                }
            }

            for (int y = 0; y < ResimYuksekligi; y++)
            {
                for (int x = 0; x < ResimGenisligi; x++)
                {
                    bool shouldDilate = false;

                    for (int ky = -elemCenterY; ky <= elemCenterY; ky++)
                    {
                        for (int kx = -elemCenterX; kx <= elemCenterX; kx++)
                        {
                            int posX = x + kx;
                            int posY = y + ky;
                            if (posX >= 0 && posX < ResimGenisligi && posY >= 0 && posY < ResimYuksekligi)
                            {
                                if (yapiElemenai[ky + elemCenterY, kx + elemCenterX] == 1 && imgData[posX, posY] == 1)
                                {
                                    shouldDilate = true;
                                    break;
                                }
                            }
                        }
                        if (shouldDilate)
                            break;
                    }

                    CikisResmi.SetPixel(x, y, shouldDilate ? Color.White : Color.Black);
                }
            }
            return CikisResmi;
        }

        private void aşınmaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(pictureBox1.Image);
            Bitmap gri = AsinmaIslemi(image);
            pictureBox2.Image = gri;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        public static Bitmap AsinmaIslemi(Bitmap GirisResmi)
        {
            int ResimYuksekligi = GirisResmi.Height;
            int ResimGenisligi = GirisResmi.Width;

            Bitmap CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            int[,] yapiElemenai = {
                {1, 1, 1},
                {1, 1, 1},
                {1, 1, 1}

            };

            int elemWidth = yapiElemenai.GetLength(0);
            int elemHeight = yapiElemenai.GetLength(1);

            int elemCenterX = elemWidth / 2;
            int elemCenterY = elemHeight / 2;



            byte[,] imgData = new byte[ResimGenisligi, ResimYuksekligi];
            for (int y = 0; y < ResimYuksekligi; y++)
            {
                for (int x = 0; x < ResimGenisligi; x++)
                {
                    Color pixel = GirisResmi.GetPixel(x, y);
                    imgData[x, y] = (byte)((pixel.R + pixel.G + pixel.B) / 3 > 127 ? 1 : 0);
                }
            }

            for (int y = 0; y < ResimYuksekligi; y++)
            {
                for (int x = 0; x < ResimGenisligi; x++)
                {
                    bool shouldErode = true;

                    for (int ky = -elemCenterY; ky <= elemCenterY; ky++)
                    {
                        for (int kx = -elemCenterX; kx <= elemCenterX; kx++)
                        {
                            int posX = x + kx;
                            int posY = y + ky;
                            if (posX >= 0 && posX < ResimGenisligi && posY >= 0 && posY < ResimYuksekligi)
                            {
                                if (yapiElemenai[ky + elemCenterY, kx + elemCenterX] == 1 && imgData[posX, posY] == 0)
                                {
                                    shouldErode = false;
                                    break;
                                }
                            }
                        }
                        if (!shouldErode)
                            break;
                    }

                    CikisResmi.SetPixel(x, y, shouldErode ? Color.Black : Color.White);
                }
            }
            return CikisResmi;
        }
        private void açmaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(pictureBox1.Image);
            Bitmap gri = Acınım(image);
            pictureBox2.Image = gri;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        public static Bitmap Acınım(Bitmap GirisResmi)
        {
            int ResimYuksekligi = GirisResmi.Height;
            int ResimGenisligi = GirisResmi.Width;

            Bitmap CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);
            Bitmap erosion = AsinmaIslemi(GirisResmi);


            Bitmap dilation = GenislemeIslemi(erosion);

            for (int y = 0; y < ResimYuksekligi; y++)
            {
                for (int x = 0; x < ResimGenisligi; x++)
                {
                    CikisResmi.SetPixel(x, y, dilation.GetPixel(x, y));
                }
            }
            return CikisResmi;
        }

        private void kapamaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(pictureBox1.Image);
            Bitmap gri = Kapanım(image);
            pictureBox2.Image = gri;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        public static Bitmap Kapanım(Bitmap GirisResmi)
        {
            int ResimYuksekligi = GirisResmi.Height;
            int ResimGenisligi = GirisResmi.Width;

            Bitmap CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

            Bitmap dilation = GenislemeIslemi(GirisResmi);
            Bitmap erosion = AsinmaIslemi(dilation);

            for (int y = 0; y < ResimYuksekligi; y++)
            {
                for (int x = 0; x < ResimGenisligi; x++)
                {
                    CikisResmi.SetPixel(x, y, erosion.GetPixel(x, y));
                }
            }
            return CikisResmi;
        }

        private void meanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(pictureBox1.Image);
            Bitmap gri = OrtalamaFiltre(image);
            pictureBox2.Image = gri;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        public static Bitmap OrtalamaFiltre(Bitmap GirisResmi)
        {

            int ResimYuksekligi = GirisResmi.Height;
            int ResimGenisligi = GirisResmi.Width;

            Bitmap CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);


            int SablonBoyutu = 3;

            int yarimBoyut = (SablonBoyutu - 1) / 2;

            for (int x = yarimBoyut; x < ResimGenisligi - yarimBoyut; x++)
            {
                for (int y = yarimBoyut; y < ResimYuksekligi - yarimBoyut; y++)
                {
                    int toplamR = 0, toplamG = 0, toplamB = 0;

                    for (int i = -yarimBoyut; i <= yarimBoyut; i++)
                    {
                        for (int j = -yarimBoyut; j <= yarimBoyut; j++)
                        {
                            Color OkunanRenk = GirisResmi.GetPixel(x + i, y + j);

                            toplamR += OkunanRenk.R;
                            toplamG += OkunanRenk.G;
                            toplamB += OkunanRenk.B;
                        }
                    }

                    int ortalamaR = toplamR / (SablonBoyutu * SablonBoyutu);
                    int ortalamaG = toplamG / (SablonBoyutu * SablonBoyutu);
                    int ortalamaB = toplamB / (SablonBoyutu * SablonBoyutu);

                    CikisResmi.SetPixel(x, y, Color.FromArgb(ortalamaR, ortalamaG, ortalamaB));
                }
            }

            return CikisResmi;
        }

        private void medianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(pictureBox1.Image);
            Bitmap gri = OrtaFiltresi(image);
            pictureBox2.Image = gri;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        public static Bitmap OrtaFiltresi(Bitmap GirisResmi)
        {

            int ResimYuksekligi = GirisResmi.Height;
            int ResimGenisligi = GirisResmi.Width;

            Bitmap CikisResmi = new Bitmap(ResimGenisligi, ResimYuksekligi);

            int SablonBoyutu = 5;


            int ElemanSayisi = SablonBoyutu * SablonBoyutu;

            for (int x = (SablonBoyutu - 1) / 2; x < ResimGenisligi - (SablonBoyutu - 1) / 2; x++)
            {
                for (int y = (SablonBoyutu - 1) / 2; y < ResimYuksekligi - (SablonBoyutu - 1) / 2; y++)
                {
                    List<int> R = new List<int>();
                    List<int> G = new List<int>();
                    List<int> B = new List<int>();

                    for (int i = -((SablonBoyutu - 1) / 2); i <= (SablonBoyutu - 1) / 2; i++)
                    {
                        for (int j = -((SablonBoyutu - 1) / 2); j <= (SablonBoyutu - 1) / 2; j++)
                        {
                            Color OkunanRenk = GirisResmi.GetPixel(x + i, y + j);
                            R.Add(OkunanRenk.R);
                            G.Add(OkunanRenk.G);
                            B.Add(OkunanRenk.B);
                        }
                    }

                    R.Sort();
                    G.Sort();
                    B.Sort();

                    int medianIndex = (ElemanSayisi - 1) / 2;

                    Color medianColor = Color.FromArgb(R[medianIndex], G[medianIndex], B[medianIndex]);
                    CikisResmi.SetPixel(x, y, medianColor);
                }
            }
            return CikisResmi;
        }
    }

}


