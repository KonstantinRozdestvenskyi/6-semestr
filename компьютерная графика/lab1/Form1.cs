using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab1
{
    public partial class Form1 : Form
    {
        math_3D m3d = new math_3D();
        graph_3D g3d = new graph_3D();
        Form2 frm;

        //матрицы 
        float[,] matrixAxStartCord;     //Для начальных координат осей
        public static float[,] matrixAxEndCord;       //для координат осей после трансформации
        float[,] matrixAxDecCord;       //однородные координаты
        int[,] matrixAxDispCord;      //для экранных координат
        float[,] matrixTransf;          //матрица трансформации
        float ml, mm, mn, ms;
        public static float mbetta, mgamma;


        //элементы матрицы трансформации
        /*
          a  b  c  p
          d  e  f  q
          g  h  i  r
          l  m  n  s 
         */
        float[,] matrixAxTrRotY;   //матрица поворота по оси Y
        float[,] matrixAxTrRotX;   //матрица поворота по оси X 
        float[,] matrixOrtogZ;  //Ортогональная проекция Z=0
        float[,] matrixGenTr;   //окончательная основная матрица трансформации
                
        //Фигура
        float[,] matrixFgGenTr; //для сохранения матрицы полной трансформации фигуры
        float[,] matrixFgStartCord;
        float[,] matrixFgEndCord;
        float[,] matrixFgDecCord;
        int[,] matrixFgDispCord;

        //Масштабирование фигуры
        float[,] mxScaleFig;

        //Аксонометрия
        public float betta, gamma;
        float[,] mxFgRotX;
        float[,] mxFgRotY;
        public static float[,] matrixPrEndCord;
        float[,] matrixFgPrGenTr;

        //параметры
        //размеры поля отображения
        int W, H;


        float cAx, mmh, mmw;

        //mmh и mmw единица деления осей т. и. размеы между 1 2 3 ... , по X и Y  осей
        //cAx количество цифер для деление и нумерации X, Y осей, для одной половины оси.
        // hh, ww  для расчета дальности от центера кординатной ситемы до конца по ситуации
        float degSt;  //для расчета единицы радиана

        

        int rotY;   //для угла поворота по оси Y 


        int rotX;   //для угла поворота по оси X 


        //Графика
        Graphics graph;


        Graphics gr;


        Bitmap fromBitmap;
        Font f;

        

        //логические
        bool IamStarted = false;
        //состояние выбора радиобутонов


        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            W = pictureBox1.Width;
            H = pictureBox1.Height;
            cAx = 10;
            mmw = W / (2 * (cAx + 1));
            mmh = H / (2 * (cAx + 1));
            degSt = (float)Math.PI / 180;

            rotY = 0;
            rotX = 0;
            ml = 0;
            mm = 0;
            mn = 0;
            ms = 1;

            matrixTransf = new float[4, 4] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { ml, mm, mn, 1 } };
            matrixAxStartCord = new float[6, 4] {{ -cAx, 0, 0, 1 }, { cAx, 0, 0, 1 }, { 0, -cAx, 0, 1 },
                                            { 0, cAx, 0, 1 }, { 0, 0, -cAx, 1 }, { 0, 0, cAx, 1 }};

            matrixAxTrRotY = new float[4, 4] { {(float)Math.Cos(degSt * rotY),0,(float)-Math.Sin(degSt * rotY),0},
                                        {0,1,0,0},
                                        {(float)Math.Sin(degSt * rotY),0,(float)Math.Cos(degSt * rotY),0},
                                        {0,0,0,1}};
            matrixAxTrRotX = new float[4, 4] { { 1, 0, 0, 0 },
                                        { 0, (float)Math.Cos(degSt * rotX), (float)Math.Sin(degSt * rotX), 0 },
                                        { 0, (float)-Math.Sin(degSt * rotX), (float)Math.Cos(degSt * rotX), 0 },
                                        { 0, 0, 0, 1 } };
            matrixOrtogZ = new float[4, 4] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 1 } };
            //redimation matrix
            matrixGenTr = new float[4, 4];
            matrixAxEndCord = new float[6, 4];
            matrixAxDecCord = new float[6, 4];
            matrixAxDispCord = new int[6, 4];

            //Фигура
            matrixFgGenTr = new float[4, 4];

            matrixFgStartCord = new float[8, 4] { { 0, 0, 0, 1 }, { 0, 4, 0, 1 }, { 4, 4, 0, 1 }, { 4, 0, 0, 1 }, { 0, 0, 4, 1 }, { 0, 4, 4, 1 }, { 4, 4, 4, 1 }, { 4, 0, 4, 1 } };
            matrixFgEndCord = new float[8, 4];
            matrixFgDecCord = new float[8, 4];
            matrixFgDispCord = new int[8, 4];

            //масштабирование фигуры
            mxScaleFig = new float[4, 4] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, ms } };

            //Аксенометрия
            betta = 0;
            gamma = 0;
            mxFgRotX = new float[4,4];
            mxFgRotY = new float[4, 4];
            matrixPrEndCord = new float[8, 4];
            matrixFgPrGenTr = new float[4, 4];

            initmxFgRotX();
            initmxFgRotY();

            //graphic
            f = new Font("Arial", 10);
            graph = pictureBox1.CreateGraphics();
            fromBitmap = new Bitmap(W, H);
            gr = Graphics.FromImage(fromBitmap);
            pictureBox1.Image = fromBitmap;
            //starting
            startGeneral();
            //logical
            IamStarted = true;

        }

        


        private void startGeneral()
        {
            mathGeneral();
            graphGeneral();
        }



        private void mathGeneral()
        {
            m3d.createGeneralTransfMx(matrixAxTrRotY, matrixAxTrRotX, matrixOrtogZ, matrixGenTr);
            m3d.matrixTransformation(matrixAxStartCord, matrixGenTr, matrixAxEndCord);
            m3d.homegToDec(matrixAxEndCord, matrixAxDecCord);
            m3d.calculateDispCord(matrixAxDispCord, matrixAxDecCord, W, H, mmw, mmh);


            //фигура
            m3d.matrixFgTransformation(matrixTransf, mxScaleFig, matrixGenTr, matrixFgGenTr, matrixFgStartCord, matrixFgEndCord);
            m3d.homegToDec(matrixFgEndCord, matrixFgDecCord);
            m3d.calculateDispCord(matrixFgDispCord, matrixFgDecCord, W, H, mmw, mmh);
        }

        private void updatePicBox()
        {
            gr.Clear(pictureBox1.BackColor);
            startGeneral();
            pictureBox1.Image = fromBitmap;

        }

        

        private void globalUpdate()
        {
            W = pictureBox1.Width;
            H = pictureBox1.Height;

            mmw = W / (2 * (cAx + 1));
            mmh = H / (2 * (cAx + 1));

            graph = pictureBox1.CreateGraphics();
            fromBitmap = new Bitmap(W, H);
            gr = Graphics.FromImage(fromBitmap);
            pictureBox1.Image = fromBitmap;
            startGeneral();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (IamStarted == true)
            {
                cAx = trackBar1.Value;
                textBox1.Text = cAx.ToString();
                globalUpdate();
            }
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            rotY = trackBar2.Value;
            textBox2.Text = rotY.ToString();

            matrixAxTrRotY[0, 0] = (float)Math.Cos(degSt * rotY); matrixAxTrRotY[0, 1] = 0;
            matrixAxTrRotY[0, 2] = (float)-Math.Sin(degSt * rotY); matrixAxTrRotY[0, 3] = 0;

            matrixAxTrRotY[1, 0] = 0; matrixAxTrRotY[1, 1] = 1; matrixAxTrRotY[1, 2] = 0; matrixAxTrRotY[1, 3] = 0;

            matrixAxTrRotY[2, 0] = (float)Math.Sin(degSt * rotY); matrixAxTrRotY[2, 1] = 0;
            matrixAxTrRotY[2, 2] = (float)Math.Cos(degSt * rotY); matrixAxTrRotY[2, 3] = 0;

            matrixAxTrRotY[3, 0] = 0; matrixAxTrRotY[3, 1] = 0; matrixAxTrRotY[3, 2] = 0; matrixAxTrRotY[3, 3] = 1;

            updatePicBox();
        }

        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            rotX = trackBar3.Value;
            textBox3.Text = rotX.ToString();

            matrixAxTrRotX[0, 0] = 1; matrixAxTrRotX[0, 1] = 0; matrixAxTrRotX[0, 2] = 0; matrixAxTrRotX[0, 3] = 0;

            matrixAxTrRotX[1, 0] = 0; matrixAxTrRotX[1, 1] = (float)Math.Cos(degSt * rotX);
            matrixAxTrRotX[1, 2] = (float)Math.Sin(degSt * rotX); matrixAxTrRotX[1, 3] = 0;

            matrixAxTrRotX[2, 0] = 0; matrixAxTrRotX[2, 1] = (float)-Math.Sin(degSt * rotX);
            matrixAxTrRotX[2, 2] = (float)Math.Cos(degSt * rotX); matrixAxTrRotX[2, 3] = 0;

            matrixAxTrRotX[3, 0] = 0; matrixAxTrRotX[3, 1] = 0; matrixAxTrRotX[3, 2] = 0; matrixAxTrRotX[3, 3] = 1;

            updatePicBox();
        }

        private void trackBar5_ValueChanged(object sender, EventArgs e)
        {
            float km = 0.1f;
            ms = km * trackBar5.Value;
            textBox5.Text = Convert.ToString(1 / ms);
            mxScaleFig[3, 3] = ms;
            if (IamStarted == true)
            {
                updatePicBox();
            }
        }

        private void trcbN_ValueChanged(object sender, EventArgs e)
        {
            mn = trcbN.Value;
            txtn.Text = mn.ToString();
            matrixTransf[3, 2] = mn;
            updatePicBox();

        }

        private void trcbM_ValueChanged(object sender, EventArgs e)
        {
            mm = trcbM.Value;
            txtm.Text = mm.ToString();
            matrixTransf[3, 1] = mm;
            updatePicBox();
        }

        private void trcbL_ValueChanged(object sender, EventArgs e)
        {
            ml = trcbL.Value;
            txtl.Text = ml.ToString();
            matrixTransf[3, 0] = ml;
            updatePicBox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == false && radioButton2.Checked == false)
            {
                MessageBox.Show("Выберите вид проекции", "Внимание", MessageBoxButtons.OKCancel);
            }
            else
            {
                m3d.matrixAxonomTransf(mxFgRotX, mxFgRotY, matrixOrtogZ, matrixFgPrGenTr, matrixFgEndCord, matrixPrEndCord);
                frm = new Form2();
                mgamma = gamma;
                mbetta = betta;
                if (radioButton1.Checked)
                {
                    frm.Text = "dimetric projection";
                }
                if (radioButton2.Checked)
                {
                    frm.Text = "izometric projection";
                }
                frm.Show();
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            betta = 20.7f;
            gamma = 22.2f;
            initmxFgRotX();
            initmxFgRotY();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            betta = 35.26f;
            gamma = 45f;
            initmxFgRotX();
            initmxFgRotY();
        }

        public void initmxFgRotX()
        {

            mxFgRotX = new float[4, 4] { { 1, 0, 0, 0 },
                { 0, Convert.ToSingle(Math.Cos(degSt *betta)), Convert.ToSingle(Math.Sin(degSt *betta)), 0 },
                { 0, Convert.ToSingle(-Math.Sin(degSt *betta)), Convert.ToSingle(Math.Cos(degSt *betta)), 0 }, { 0, 0, 0, 1 } };

        }

        public void initmxFgRotY()
        {
            mxFgRotY = new float[4, 4] {{Convert.ToSingle(Math.Cos(degSt * gamma)) ,0,Convert.ToSingle(-Math.Sin(degSt * gamma)) ,0},
            {0,1,0,0,},{Convert.ToSingle(Math.Sin(degSt * gamma)) , 0, Convert.ToSingle(Math.Cos(degSt * gamma) ), 0},{0, 0, 0, 1}};
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (IamStarted == true)
            {
                globalUpdate();
            }
        }
        private void graphGeneral()
        {
            g3d.drawAxis(matrixAxDispCord, gr);
            g3d.writeAxisLeter(matrixAxDispCord, gr);

            
            //отображаем фигуру
            g3d.DrawFigure(matrixFgDispCord, gr);

        }
    }
}
