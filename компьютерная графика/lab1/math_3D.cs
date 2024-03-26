using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    class math_3D
    {
        
        /// <summary>
        /// Multiplication Matrix
        /// </summary>
        /// <param name="m1">first matrix</param>
        /// <param name="m2">second matrix</param>
        /// <param name="rez">result after operation</param>
        public void multipleMatrixnxm(float[,] m1, float[,] m2, float[,] rez)
        {
            int i, j, r;
            int firstMxRow = 0;
            int firstMxCol = 0;
            int secondMxRow = 0;
            int secondMxCol = 0;
            int rezMxRow = 0;
            int rezMxCol = 0;

            firstMxRow = m1.GetLength(0);
            firstMxCol = m1.GetLength(1);
            secondMxRow = m2.GetLength(0);
            secondMxCol = m2.GetLength(1);
            rezMxRow = rez.GetLength(0);
            rezMxCol = rez.GetLength(1);

            for (i = 0; i < rezMxRow; i++)
            {
                for (j = 0; j < rezMxCol; j++)
                {
                    rez[i, j] = 0;
                }
            }

            if (firstMxCol == secondMxRow)
            {
                for (i = 0; i < firstMxRow; i++)
                {
                    for (j = 0; j < secondMxCol; j++)
                    {
                        for (r = 0; r < firstMxCol; r++)
                        {
                            rez[i, j] = rez[i, j] + m1[i, r] * m2[r, j];
                        }
                    }
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("The firdt matrix Columns count and the second" +
                    "matrix Rows count must be equals", "Important");
                return;
            }


        } //end

        /// <summary>
        /// From homogeneous To Cartesian coordinates
        /// </summary>
        /// <param name="endCord">coordinates after transformation</param>
        /// <param name="decCord">Result, coordinates in the Cartesian system</param>
        public void homegToDec(float[,] endCord, float[,] decCord)
        {

            //из однородной системы координат переходим в декартовую систему
            //нормирование координат трансформированного объекта
            int mi = 0;
            int mj = 0;

            int firstMxRow = endCord.GetLength(0);     //количество строк первой матрицы
            int firstMxCol = endCord.GetLength(1);     //количество столбцов первой матрицы


            for (mi = 0; mi < firstMxRow; mi++)
                for (mj = 0; mj < firstMxCol; mj++)
                {
                    if (endCord[mi, 3] != 1)
                    {
                        if (endCord[mi, 3] == 0)
                        {
                            endCord[mi, 3] = 0.01f;
                        }
                        else
                        {
                            decCord[mi, mj] = endCord[mi, mj] / endCord[mi, 3];
                        }
                    }
                    else
                    {
                        decCord[mi, mj] = endCord[mi, mj];
                    }
                }
        } //end

        /// <summary>
        /// convert Cartesian coordinates to Display cord
        /// </summary>
        /// <param name="mDisp">display coordinate matrix</param>
        /// <param name="mDek">Cartesian-Decart coordinate matrix</param>
        /// <param name="dgvDisp">data grid view for display coordinates</param>
        /// <param name="W">Canvas Width</param>
        /// <param name="H">Canvas Height</param>
        /// <param name="mmw">Canvas Width unit</param>
        /// <param name="mmh">Canvas Height unit</param>
        public void calculateDispCord(int[,] mDisp, float[,] mDek, 
                                        int W, int H, float mmw, float mmh)
        {
            int i, j;
            int mDekerk = mDek.GetLength(0);
            int mDeklayn = mDek.GetLength(1);

            for (i = 0; i < mDekerk; i++)
            {
                for (j = 0; j < mDeklayn - 1; j++)
                {
                    if (j == 0)
                    {
                        mDisp[i, 0] = (int)(W / 2 + (mmw * mDek[i, j]));
                    }
                    if (j == 1)
                    {
                        mDisp[i, 1] = (int)(H / 2 - (mmh * mDek[i, j]));
                    }
                }
            }
        } //end 

        /// <summary>
        /// Fill datagridview with float data
        /// </summary>
        /// <param name="m">matrix for fill</param>
        /// <param name="dgv">datagridview for fill</param>
        public void fillMatrixmxn(float[,] m, System.Windows.Forms.DataGridView dgv)
        {
            int i, j;
            int arajinmatricaerk = 0;
            int arajinmatricalayn = 0;
            arajinmatricaerk = m.GetLength(0);
            arajinmatricalayn = m.GetLength(1);
            dgv.RowCount = arajinmatricaerk;
            if (arajinmatricaerk > dgv.RowCount)
            {
                System.Windows.Forms.MessageBox.Show("datagride Rows count must be greater  " +
                    "than matrix rows count", "Important");
                return;
            }
            for (i = 0; i < arajinmatricaerk; i++)
            {
                for (j = 0; j < arajinmatricalayn; j++)
                {
                    dgv.Rows[i].Cells[j].Value = m[i, j];

                }
            }
        } //end

        /// <summary>
        /// Fill datagridview with int data
        /// </summary>
        /// <param name="m">matrix for fill</param>
        /// <param name="dgv">datagridview for fill</param>
        public void fillMatrixmxn(int[,] m, System.Windows.Forms.DataGridView dgv)
        {
            int i, j;
            int arajinmatricaerk = 0;
            int arajinmatricalayn = 0;
            arajinmatricaerk = m.GetLength(0);
            arajinmatricalayn = m.GetLength(1);
            dgv.RowCount = arajinmatricaerk;
            if (arajinmatricaerk > dgv.RowCount)
            {
                System.Windows.Forms.MessageBox.Show("datagride Rows count must be greater  " +
                    "than matrix rows count", "Important");
                return;
            }
            for (i = 0; i < arajinmatricaerk; i++)
            {
                for (j = 0; j < arajinmatricalayn; j++)
                {
                    dgv.Rows[i].Cells[j].Value = m[i, j];

                }
            }
        } //end
        //
        public void createGeneralTransfMx(Single[,] mxRY, Single[,] mxRX, Single[,] mxOrtZ,
            Single[,] mxGT)
        {
            float[,] tempM = new float[4, 4];
            multipleMatrixnxm(mxRY, mxRX, tempM);
            multipleMatrixnxm(tempM, mxOrtZ, mxGT);
        }

        public void matrixTransformation(Single[,] mxAxStCord, Single[,] mxGenTr, Single[,] mxAxEndCord)
        {
            multipleMatrixnxm(mxAxStCord, mxGenTr, mxAxEndCord);

        }

        public void matrixTransformationSurfase(Single[,] surfaceXOY, Single[,] surfaceXOz, Single[,] surfaceYOZ,
                                    Single[,] matrixGenTr, Single[,] surfaceXOYEnd,
                                    Single[,] surfaceXOZEnd, Single[,] surfaceYOZEnd)
        {
            multipleMatrixnxm(surfaceXOY, matrixGenTr, surfaceXOYEnd);
            multipleMatrixnxm(surfaceXOz, matrixGenTr, surfaceXOZEnd);
            multipleMatrixnxm(surfaceYOZ, matrixGenTr, surfaceYOZEnd);
        }

        public void matrixFgTransformation(Single[,] matrixTransf, Single[,] mxScaleFig, Single[,] matrixGenTr,
                                           Single[,] matrixFgGenTr, Single[,] matrixFgStartCord, Single[,] matrixFgEndCord)
        {
            float[,] tempM = new float[4, 4];
            float[,] mxRotYX = new float[4, 4];
            float[,] mxRotYXZ = new float[4, 4];
            float[,] mxRotYXZper = new float[4, 4];
            float[,] matrixVid = new float[4, 4];


            /*multipleMatrixnxm(mxFgRotY, mxFgRotX, mxRotYX);
            multipleMatrixnxm(mxRotYX, mxFgRotZ, mxRotYXZ);
            multipleMatrixnxm(mxRotYXZ, matrixTransf, mxRotYXZper);*/
            multipleMatrixnxm(matrixTransf, mxScaleFig, tempM);
            multipleMatrixnxm(tempM, matrixGenTr, matrixFgGenTr);

            multipleMatrixnxm(matrixFgStartCord, matrixFgGenTr, matrixFgEndCord);

        }

        public void matrixAxonomTransf(Single[,] matrixRotX, Single[,] matrixRotY, Single[,] matrixOrtZ,
                                           Single[,] matrixFgGenPrTr, Single[,] matrixFgStartCord, Single[,] matrixFgEndCord)
        {
            float[,] tempM = new float[4, 4];
            float[,] mxRotYX = new float[4, 4];
            float[,] mxRotYXZ = new float[4, 4];
            float[,] mxRotYXZper = new float[4, 4];
            float[,] matrixVid = new float[4, 4];


            /*multipleMatrixnxm(mxFgRotY, mxFgRotX, mxRotYX);
            multipleMatrixnxm(mxRotYX, mxFgRotZ, mxRotYXZ);
            multipleMatrixnxm(mxRotYXZ, matrixTransf, mxRotYXZper);*/
            multipleMatrixnxm(matrixRotX, matrixRotY, mxRotYX);
            multipleMatrixnxm(mxRotYX, matrixOrtZ, matrixFgGenPrTr);

            multipleMatrixnxm(matrixFgStartCord, matrixFgGenPrTr, matrixFgEndCord);

        }

    }
}
