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
    public partial class Form2 : Form
    {
        math_3D m3d = new math_3D();
        graph_3D g3d = new graph_3D();

        //матрицы 
        float[,] matrixAxStartCord;     //Для начальных координат осей
        float[,] matrixAxEndCord;       //для координат осей после трансформации
        float[,] matrixAxDecCord;       //однородные координаты
        int[,] matrixAxDispCord;      //для экранных координат
        float[,] matrixTransf;          //матрица трансформации
        float ml, mm, mn, ms;

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

        //параметры
        //размеры поля отображения
        int W, H;


        float cAx, mmh, mmw, hh, ww;

        //mmh и mmw единица деления осей т. и. размеы между 1 2 3 ... , по X и Y  осей
        //cAx количество цифер для деление и нумерации X, Y осей, для одной половины оси.
        // hh, ww  для расчета дальности от центера кординатной ситемы до конца по ситуации
        float degSt;  //для расчета единицы радиана



        float rotY;   //для угла поворота по оси Y 


        float rotX;   //для угла поворота по оси X 


        //Графика
        Graphics graph;


        Graphics gr;


        Bitmap fromBitmap;
        Font f;

        



        //логические
        bool IamStarted = false;


        public Form2()
        {
            InitializeComponent();
        }


        private void Form2_Load(object sender, EventArgs e)
        {
            W = pictureBox1.Width;
            H = pictureBox1.Height;
            cAx = 10;
            mmw = W / (2 * (cAx + 1));
            mmh = H / (2 * (cAx + 1));
            degSt = (float)Math.PI / 180;

            rotY = Form1.mgamma;
            rotX = Form1.mbetta;
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
            matrixFgEndCord = Form1.matrixPrEndCord;
            matrixFgDecCord = new float[8, 4];
            matrixFgDispCord = new int[8, 4];

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
            m3d.homegToDec(matrixFgEndCord, matrixFgDecCord);
            m3d.calculateDispCord(matrixFgDispCord, matrixFgDecCord, W, H, mmw, mmh);
        }

        private void Form2_Resize(object sender, EventArgs e)
        {
            if (IamStarted == true)
            {
                globalUpdate();
            }
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

        private void graphGeneral()
        {
            g3d.drawAxis(matrixAxDispCord, gr);
            g3d.writeAxisLeter(matrixAxDispCord, gr);


            //отображаем фигуру
            g3d.DrawFigure(matrixFgDispCord, gr);

        }
    }
}
