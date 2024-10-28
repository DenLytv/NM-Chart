using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Chart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.chart1.Series[0].Points.Clear();
            this.chart2.Series[0].Points.Clear();
            this.chart3.Series[0].Points.Clear();
            this.chart4.Series[0].Points.Clear();
            this.chart5.Series[0].Points.Clear();
            this.chart6.Series[0].Points.Clear();
            this.chart7.Series[0].Points.Clear();
            this.chart8.Series[0].Points.Clear();
            this.chart9.Series[0].Points.Clear();
            this.chart10.Series[0].Points.Clear();

            this.chart4.ChartAreas[0].AxisX.Minimum = 1E-16;
            this.chart4.ChartAreas[0].AxisX.Maximum = 1;
            this.chart4.ChartAreas[0].AxisX.IsLogarithmic = true;
            this.chart4.ChartAreas[0].AxisX.Title = "h";
            this.chart4.ChartAreas[0].AxisY.Minimum = 1E-12;
            this.chart4.ChartAreas[0].AxisY.Maximum = 1000;
            this.chart4.ChartAreas[0].AxisY.IsLogarithmic = true;

            this.chart5.ChartAreas[0].AxisX.Minimum = 10;
            this.chart5.ChartAreas[0].AxisX.Maximum = 10000000;
            this.chart5.ChartAreas[0].AxisX.IsLogarithmic = true;
            this.chart5.ChartAreas[0].AxisX.Title = "N";
            this.chart5.ChartAreas[0].AxisY.Minimum = 1E-12;
            this.chart5.ChartAreas[0].AxisY.Maximum = 7900;
            this.chart5.ChartAreas[0].AxisY.IsLogarithmic = true;

            this.chart10.ChartAreas[0].AxisY.IsLogarithmic = true;

            Errors();
            Lagrange();
            DerivativeAndIntegral();
            Lab4();
            Lab5();
            Lab6();
        }

        private void DerivativeAndIntegral()
        {
            for (double i = 1; i >= 0.0000000000000001; i /= 10)
            {
                Derivative(0, i);
            }

            for (int i = 10; i <= 10000000; i *= 10)
            {
                Integral(0, 10000, i);
            }
        }

        private void Errors()
        {
            double a = 0.045;
            for (int m = 100; m < 10000; m += 100)
            {
                Summa(a, m);
            }
        }

        private void Lagrange()
        {
            Func<double, double> lagrange1 = X => 0.8 * X - 0.18;
            Func<double, double> lagrange2 = X => 20 * Math.Pow(X, 2) - 8 * X + 0.8;
            Func<double, double> lagrange3 = X => -40 * Math.Pow(X, 3) + 60 * Math.Pow(X, 2) - 18 * X + 0.4;
            Func<double, double> lagrange4 = X => 1566.66667 * Math.Pow(X, 4) - 1620 * Math.Pow(X, 3) + 504.33333 * Math.Pow(X, 2) - 39.8 * X + 0.4;

            double xmin = 0.0, xmax = 0.5;
            int points = 50;
            double h = (xmax - xmin) / points;
            double x, y1, y2, y3, y4;

            for (int i = 0; i < points + 1; i++)
            {
                x = xmin + i * h;
                y1 = lagrange1(x);
                y2 = lagrange2(x);
                y3 = lagrange3(x);
                y4 = lagrange4(x);

                this.chart3.Series[0].Points.AddXY(x, y1);
                this.chart3.Series[1].Points.AddXY(x, y2);
                this.chart3.Series[2].Points.AddXY(x, y3);
                this.chart3.Series[3].Points.AddXY(x, y4);
            }
        }
        private void Summa(double a, int n)
        {
            double s = 0;
            double Na = 0;
            for (int i = 0; i < n; i++)
            {
                s += a;
            }
            Na = n * a;
            this.chart1.Series[0].Points.AddXY(n, Math.Abs(Na - s));
            this.chart2.Series[0].Points.AddXY(n, Math.Abs(Na - s) / Na * 100);
        }

        private void Derivative(double x, double h)
        {
            double df_dx = -0.064;
            double d2 = (Fun(x + h) - Fun(x - h)) / (2 * h);
            double d4 = (-Fun(x + 2 * h) + 8 * Fun(x + h) - 8 * Fun(x - h) + Fun(x - 2 * h)) / (12 * h);
            this.chart4.Series[0].Points.AddXY(h, Math.Abs((df_dx - d2) / df_dx) * 100);
            this.chart4.Series[1].Points.AddXY(h, Math.Abs((df_dx - d4) / df_dx) * 100);
        }

        private void Integral(double u, double v, int n)
        {
            double s1 = 0;
            double s2 = 0;
            double h = (v - u) / n;
            double s = 2.5;

            for (int i = 1; i < n; i++)
            {
                s1 += Fun(u + i * h);
            }
            s1 = (s1 + (Fun(u) - Fun(v)) / 2) * h;

            for (int i = 1; i < n / 2; i++)
            {
                s2 += (4 * Fun(u + (2 * i - 1) * h) + 2.0 * Fun(u + (2 * i) * h));
            }
            s2 = (s2 + Fun(u) - Fun(v)) * h / 3;

            this.chart5.Series[0].Points.AddXY(n, Math.Abs((s - s1) / s) * 100);
            this.chart5.Series[1].Points.AddXY(n, Math.Abs((s - s2) / s) * 100);
        }

        private double Fun(double x)
        {
            return 0.4 * Math.Exp(-0.16 * x);
        }

        private void Lab4()
        {
            double[][] A = new double[][]
            {
                new double[] {1, 1, 1, 1, 1},
                new double[] {0, 2.1, -0.8, -0.6, -0.8},
                new double[] {-1, -0.3, 1.4, -0.5, -0.5},
                new double[] {-0.9, -0.6, -0.3, 2, -0.7},
                new double[] {-0.8, -0.8, 0, -0.1, 2.2}
            };


            double[] B = { 8, -0.16, -1.44, -0.8, 0.8 };
            double[] X0 = new double[5];
            double delta = 1e-10;

            Gauss(A, B);
            GaussSeidel(A, B, X0, delta);
        }

        private void Gauss(double[][] AA, double[] BB)
        {
            int n = BB.Length;
            double[][] A = new double[n][];
            double[] B = new double[n];

            for (int i = 0; i < n; i++)
            {
                A[i] = new double[n];
                Array.Copy(AA[i], A[i], n);
            }
            Array.Copy(BB, B, n);

            for (int k = 0; k < n - 1; k++)
            {
                double value = 0;
                int index = 0;
                for (int i = k; i < n; i++)
                {
                    if (Math.Abs(A[i][k]) > value)
                    {
                        value = Math.Abs(A[i][k]);
                        index = i;
                    }
                }

                var tempRow = A[k];
                A[k] = A[index];
                A[index] = tempRow;

                double tempValue = B[k];
                B[k] = B[index];
                B[index] = tempValue;

                if (A[k][k] == 0)
                {
                    Console.WriteLine("Error: A[k][k] == 0");
                    return;
                }

                for (int i = k + 1; i < n; i++)
                {
                    double m = A[i][k] / A[k][k];
                    for (int j = k; j < n; j++)
                    {
                        A[i][j] -= A[k][j] * m;
                    }
                    B[i] -= B[k] * m;
                }
            }

            double[] X = new double[n];
            X[n - 1] = B[n - 1] / A[n - 1][n - 1];
            for (int i = n - 2; i >= 0; i--)
            {
                X[i] = B[i];
                for (int j = i + 1; j < n; j++)
                {
                    X[i] -= A[i][j] * X[j];
                }
                X[i] /= A[i][i];
            }

            Array.Resize(ref X, 11);

            for (int i = n; i < X.Length; i++)
            {
                X[i] = 1.6;
            }


            for (int i = 0; i < X.Length; i++)
            {
                this.chart6.Series[5].Points.AddXY(i, X[i]);
            }

        }

        private void GaussSeidel(double[][] A, double[] B, double[] X0, double delta)
        {
            int dimension = X0.Length;
            double[] X = new double[dimension];
            Array.Copy(X0, X, dimension);

            Console.WriteLine(0 + " [" + string.Join(", ", X) + "]");


            for (int i = 0; i < 5; i++)
            {
                this.chart6.Series[i].Points.AddXY(0, X[i]);
            }


            for (int k = 1; k <= 10; k++)
            {
                for (int i = 0; i < dimension; i++)
                {
                    X[i] = B[i];
                    for (int j = 0; j < dimension; j++)
                    {
                        if (i != j)
                        {
                            X[i] -= A[i][j] * X[j];
                        }
                    }
                    X[i] /= A[i][i];
                }

                double epsMax = 0;
                double ksiMax = 0;
                for (int i = 0; i < dimension; i++)
                {
                    double eps = Math.Abs(X[i] - X0[i]);
                    double ksi = eps / (Math.Abs(X[i]) + delta);

                    if (epsMax < eps) epsMax = eps;
                    if (ksiMax < ksi) ksiMax = ksi;
                }

                Console.WriteLine(k + " [" + string.Join(", ", X) + "]");

                Array.Copy(X, X0, dimension);

                for (int j = 0; j < 5; j++)
                {
                    this.chart6.Series[j].Points.AddXY(k, X[j]);
                }

                if (epsMax < delta && ksiMax < delta)
                {
                    break;
                }
            }

        }



        private void Lab5()
        {
            Bisection(0, 0.35, 1e-10);
            Console.WriteLine();
            Newton(0, 1e-10);
        }

        private double Func(double x)
        {
            return x * x + 0.54 * x - 0.112;
        }
        private double DFun(double x)
        {
            return 2 * x + 0.54;
        }

        private void Bisection(double a, double b, double delta)
        {
            double fa = Func(a);
            int signa = Math.Sign(fa);
            double fb = Func(b);
            int signb = Math.Sign(fb);
            double eps0 = 0;
            double x2 = 0.16;

            for (int i = 0; i <= 10; i++)
            {
                double c = (a + b) / 2;
                double fc = Func(c);
                int signc = Math.Sign(fc);
                double eps = Math.Abs(a - b);

                this.chart7.Series[0].Points.AddXY(i, a);
                this.chart7.Series[1].Points.AddXY(i, b);
                this.chart7.Series[2].Points.AddXY(i, c);
                this.chart7.Series[3].Points.AddXY(i, x2);

                if (eps < delta)
                    break;

                if (signb * signc > 0)
                {
                    b = c;
                    signb = signc;
                }
                else
                {
                    a = c;
                    signa = signc;
                }

                eps0 = eps;
            }
        }

        private void Newton(double x, double delta)
        {
            double eps0 = 0;
            double x2 = 0.16;

            Console.WriteLine($"0 {x}");

            for (int i = 1; i < 20; i++)
            {
                double f = Func(x);
                double df = DFun(x);
                double x1 = x - f / df;
                double eps = Math.Abs(x1 - x);
                double ksi = 2 * eps / (Math.Abs(x1) + delta);

                this.chart8.Series[0].Points.AddXY(i, x1);
                this.chart8.Series[1].Points.AddXY(i, x2);

                if (eps < delta || ksi < delta)
                    break;

                eps0 = eps;
                x = x1;
            }
        }

        private void Lab6()
        {
            Euler(0.16, 0, 1, 20);

            Console.WriteLine("\nHeun Method:");
            Heun(0.16, 0, 1, 20);

            Console.WriteLine("\nRunge-Kutta 4th Order Method:");
            RungeKutta4(0.16, 0, 1, 20);
        }


        private double Fun6(double x, double y)
        {
            return -0.7 * y;
        }


        private void Euler(double y0, double x0, double x1, int n)
        {
            double y1 = y0;
            double x = x0;
            double h = (x1 - x0) / n;
            double y = y0;
            Console.WriteLine($"0 {y1} {x}");
            this.chart9.Series[0].Points.AddXY(x, y);
            this.chart9.Series[1].Points.AddXY(x, y1);


            for (int i = 1; i <= n; i++)
            {
                double k1 = Fun6(x, y1);
                y1 = y1 + k1 * h;
                x = x + h;

                y = 0.16 * Math.Exp(-0.7 * x);
                this.chart9.Series[0].Points.AddXY(x, y);
                this.chart9.Series[1].Points.AddXY(x, y1);

                this.chart10.Series[0].Points.AddXY(x, Math.Abs(y-y1));
            }
        }

        private void Heun(double y0, double x0, double x1, int n)
        {
            double y2 = y0;
            double x = x0;
            double h = (x1 - x0) / n;
            double y = 0;
            Console.WriteLine($"0 {y2} {x}");

            this.chart9.Series[2].Points.AddXY(x, y2);

            for (int i = 1; i <= n; i++)
            {
                double k1 = Fun6(x, y2);
                double k2 = Fun6(x + h, y2 + h * k1);
                y2 = y2 + (k1 + k2) * h / 2;
                x = x + h;

                y = 0.16 * Math.Exp(-0.7 * x);
                this.chart9.Series[2].Points.AddXY(x, y2);

                this.chart10.Series[1].Points.AddXY(x, Math.Abs(y - y2));
            }
        }

        private void RungeKutta4(double y0, double x0, double x1, int n)
        {
            double y4 = y0;
            double x = x0;
            double h = (x1 - x0) / n;
            double y = 0;
            this.chart9.Series[3].Points.AddXY(x, y4);

            for (int i = 1; i <= n; i++)
            {
                double k1 = Fun6(x, y4);
                double k2 = Fun6(x + h / 2, y4 + k1 * h / 2);
                double k3 = Fun6(x + h / 2, y4 + k2 * h / 2);
                double k4 = Fun6(x + h, y4 + k3 * h);
                y4 = y4 + (k1 + 2 * k2 + 2 * k3 + k4) * h / 6;
                x = x + h;


                y = 0.16 * Math.Exp(-0.7 * x);
                this.chart9.Series[3].Points.AddXY(x, y4);

                this.chart10.Series[2].Points.AddXY(x, Math.Abs(y - y4));
            }
        }
    }
}