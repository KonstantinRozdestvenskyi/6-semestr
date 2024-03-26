using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace lab1
{
    class graph_3D
    {
        public void drawAxis(int[,] matrEkr, System.Drawing.Graphics gr)
        {
            System.Drawing.Pen pendr = new System.Drawing.Pen(new System.Drawing.SolidBrush(System.Drawing.Color.Red), 3);
            gr.DrawLine(pendr, matrEkr[0, 0], matrEkr[0, 1], matrEkr[1, 0], matrEkr[1, 1]);
            gr.DrawLine(pendr, matrEkr[2, 0], matrEkr[2, 1], matrEkr[3, 0], matrEkr[3, 1]);
            gr.DrawLine(pendr, matrEkr[4, 0], matrEkr[4, 1], matrEkr[5, 0], matrEkr[5, 1]);

        }

        public void writeAxisLeter(int[,] matrEkr, System.Drawing .Graphics gr)
        {
            System.Drawing.Font tf = new System.Drawing.Font("Arial", 8);
            gr.DrawString("X", tf, System.Drawing.Brushes.Red, matrEkr[1, 0], matrEkr[1, 1]);
            gr.DrawString("Y", tf, System.Drawing.Brushes.Red, matrEkr[3, 0], matrEkr[3, 1]);
            gr.DrawString("Z", tf, System.Drawing.Brushes.Red, matrEkr[5, 0], matrEkr[5, 1]);

        }
        public void DrawSurface(int[,] surfCord, System.Drawing .Graphics gr)
        {
            System.Drawing.Pen pendr = new System.Drawing.Pen(new System.Drawing.SolidBrush(System.Drawing.Color.Yellow ), 1);
            gr.DrawLine(pendr, surfCord[0, 0], surfCord[0, 1], surfCord[1, 0], surfCord[1, 1]);
            gr.DrawLine(pendr, surfCord[1, 0], surfCord[1, 1], surfCord[2, 0], surfCord[2, 1]);
            gr.DrawLine(pendr, surfCord[2, 0], surfCord[2, 1], surfCord[3, 0], surfCord[3, 1]);
            gr.DrawLine(pendr, surfCord[3, 0], surfCord[3, 1], surfCord[0, 0], surfCord[0, 1]);
        }

        /// <summary>
        /// Fill XOY, YOZ, XOZ Surfaces
        /// </summary>
        /// <param name="planeCord">Кординаты вершин плоскостей</param>
        /// <param name="gr">Графический контекст</param>
        public void fillColorShort(int[,] planeCord, System.Drawing.Graphics gr)
        {
            System.Drawing.Point[] pts ={new System.Drawing.Point(planeCord[0,0], planeCord[0,1]),
                         new System.Drawing.Point(planeCord[1,0], planeCord[1,1]),
                         new System.Drawing.Point(planeCord[2,0], planeCord[2,1]),
                         new System.Drawing.Point(planeCord[3,0], planeCord[3,1])};
            GraphicsPath g_path = new GraphicsPath();
            g_path.AddClosedCurve(pts, 0.01f);
            SolidBrush TransBrush = new SolidBrush(Color.FromArgb(50, 0, 0, 100));
            gr.FillPath(TransBrush, g_path);

        }

        public void DrawFigure(int[,] FgCd, System.Drawing.Graphics gr)
        {
            Pen figPen = new Pen(new SolidBrush(Color.Blue), 3);
            gr.DrawLine(figPen, FgCd[0, 0], FgCd[0, 1], FgCd[1, 0], FgCd[1, 1]);
            gr.DrawLine(figPen, FgCd[1, 0], FgCd[1, 1], FgCd[2, 0], FgCd[2, 1]);
            gr.DrawLine(figPen, FgCd[2, 0], FgCd[2, 1], FgCd[3, 0], FgCd[3, 1]);
            gr.DrawLine(figPen, FgCd[3, 0], FgCd[3, 1], FgCd[0, 0], FgCd[0, 1]);

            gr.DrawLine(figPen, FgCd[4, 0], FgCd[4, 1], FgCd[5, 0], FgCd[5, 1]);
            gr.DrawLine(figPen, FgCd[5, 0], FgCd[5, 1], FgCd[6, 0], FgCd[6, 1]);
            gr.DrawLine(figPen, FgCd[6, 0], FgCd[6, 1], FgCd[7, 0], FgCd[7, 1]);
            gr.DrawLine(figPen, FgCd[7, 0], FgCd[7, 1], FgCd[4, 0], FgCd[4, 1]);

            gr.DrawLine(figPen, FgCd[0, 0], FgCd[0, 1], FgCd[4, 0], FgCd[4, 1]);
            gr.DrawLine(figPen, FgCd[1, 0], FgCd[1, 1], FgCd[5, 0], FgCd[5, 1]);
            gr.DrawLine(figPen, FgCd[2, 0], FgCd[2, 1], FgCd[6, 0], FgCd[6, 1]);
            gr.DrawLine(figPen, FgCd[3, 0], FgCd[3, 1], FgCd[7, 0], FgCd[7, 1]);

        }

        public void writeFgLeters(int[,] matrEkr, Graphics gr)
        {
            Font tf = new Font("Arial", 10);
            gr.DrawString("A", tf, Brushes.Red, matrEkr[0, 0], matrEkr[0, 1]);
            gr.DrawString("B", tf, Brushes.Red, matrEkr[1, 0], matrEkr[1, 1]);
            gr.DrawString("C", tf, Brushes.Red, matrEkr[2, 0], matrEkr[2, 1]);
            gr.DrawString("D", tf, Brushes.Red, matrEkr[3, 0], matrEkr[3, 1]);

            gr.DrawString("E", tf, Brushes.Red, matrEkr[4, 0], matrEkr[4, 1]);
            gr.DrawString("F", tf, Brushes.Red, matrEkr[5, 0], matrEkr[5, 1]);
            gr.DrawString("G", tf, Brushes.Red, matrEkr[6, 0], matrEkr[6, 1]);
            gr.DrawString("H", tf, Brushes.Red, matrEkr[7, 0], matrEkr[7, 1]);
        }

        public void DrawProjection(int[,] prCord, Graphics gr)
        {
            Pen prPen = new Pen(new SolidBrush(Color.DarkMagenta), 1);
            gr.DrawLine(prPen, prCord[0, 0], prCord[0, 1], prCord[1, 0], prCord[1, 1]);
            gr.DrawLine(prPen, prCord[1, 0], prCord[1, 1], prCord[2, 0], prCord[2, 1]);
            gr.DrawLine(prPen, prCord[2, 0], prCord[2, 1], prCord[3, 0], prCord[3, 1]);
            gr.DrawLine(prPen, prCord[3, 0], prCord[3, 1], prCord[0, 0], prCord[0, 1]);

            gr.DrawLine(prPen, prCord[4, 0], prCord[4, 1], prCord[5, 0], prCord[5, 1]);
            gr.DrawLine(prPen, prCord[5, 0], prCord[5, 1], prCord[6, 0], prCord[6, 1]);
            gr.DrawLine(prPen, prCord[6, 0], prCord[6, 1], prCord[7, 0], prCord[7, 1]);
            gr.DrawLine(prPen, prCord[7, 0], prCord[7, 1], prCord[4, 0], prCord[4, 1]);

            gr.DrawLine(prPen, prCord[0, 0], prCord[0, 1], prCord[4, 0], prCord[4, 1]);
            gr.DrawLine(prPen, prCord[1, 0], prCord[1, 1], prCord[5, 0], prCord[5, 1]);
            gr.DrawLine(prPen, prCord[2, 0], prCord[2, 1], prCord[6, 0], prCord[6, 1]);
            gr.DrawLine(prPen, prCord[3, 0], prCord[3, 1], prCord[7, 0], prCord[7, 1]);
        }

        public void ShowLuchi(int[,] mxFigDispCord, int[,] mxPr, Graphics gr)
        {
            Pen lfPen = new Pen(new SolidBrush(Color.DarkOrange), 1);
            lfPen.DashStyle = DashStyle.Dash;
            gr.DrawLine(lfPen, mxFigDispCord[0, 0], mxFigDispCord[0, 1], mxPr[0, 0], mxPr[0, 1]);
            gr.DrawLine(lfPen, mxFigDispCord[1, 0], mxFigDispCord[1, 1], mxPr[1, 0], mxPr[1, 1]);
            gr.DrawLine(lfPen, mxFigDispCord[2, 0], mxFigDispCord[2, 1], mxPr[2, 0], mxPr[2, 1]);
            gr.DrawLine(lfPen, mxFigDispCord[3, 0], mxFigDispCord[3, 1], mxPr[3, 0], mxPr[3, 1]);

            gr.DrawLine(lfPen, mxFigDispCord[4, 0], mxFigDispCord[4, 1], mxPr[4, 0], mxPr[4, 1]);
            gr.DrawLine(lfPen, mxFigDispCord[5, 0], mxFigDispCord[5, 1], mxPr[5, 0], mxPr[5, 1]);
            gr.DrawLine(lfPen, mxFigDispCord[6, 0], mxFigDispCord[6, 1], mxPr[6, 0], mxPr[6, 1]);
            gr.DrawLine(lfPen, mxFigDispCord[7, 0], mxFigDispCord[7, 1], mxPr[7, 0], mxPr[7, 1]);


        }
    }
}
