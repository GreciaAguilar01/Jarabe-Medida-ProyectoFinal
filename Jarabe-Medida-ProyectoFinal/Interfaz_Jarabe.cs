using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Jarabe_Medida_ProyectoFinal
{
    public partial class Interfaz_Jarabe : Form
    {
        public Interfaz_Jarabe()
        {
            InitializeComponent();
        }

        private void Interfaz_Jarabe_Load(object sender, EventArgs e)
        {
            PanelIMC.Visible = false;
            PanelIMC.BackColor = Color.FromArgb(80, 255, 255, 255);
            PanelCalcuJarabe.BackColor = Color.FromArgb(80, 255, 255, 255);
            PanelCalcuJarabe.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, PanelCalcuJarabe.Width, PanelCalcuJarabe.Height, 25, 25));
            PanelIMC.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, PanelIMC.Width, PanelIMC.Height, 25, 25));

            float tamTitulo = this.Width / 45f;
            if (tamTitulo < 14) tamTitulo = 14;

            label1.Font = new Font(label1.Font.FontFamily, tamTitulo, FontStyle.Bold);
            label1.Width = this.Width; // Ocupa todo el ancho para que no se corte
            label1.Height = (int)(tamTitulo * 2.5);
            label1.Left = 0;
            label1.Top = 10;

            float tamSub = this.Width / 60f;
            if (tamSub < 10) tamSub = 10;

            label2.Font = new Font(label2.Font.FontFamily, tamSub, FontStyle.Regular);
            label2.Width = this.Width; // Ocupa todo el ancho
            label2.Height = (int)(tamSub * 2.5);
            label2.Left = 0;
            label2.Top = label1.Bottom + 5; // Se pega abajo del titulo
        }


        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     
            int nTopRect,      
            int nRightRect,    
            int nBottomRect,  
            int nWidthEllipse, 
            int nHeightEllipse 
        );

        private void BtnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                double[,] A = new double[5, 5];
                double[] b = new double[5];

                A[0, 0] = double.Parse(tbm1j1.Text); A[0, 1] = double.Parse(tbm1j2.Text); A[0, 2] = double.Parse(tbm1j3.Text); 
                A[0, 3] = double.Parse(tbm1j4.Text); A[0, 4] = double.Parse(tbm1j5.Text);
                A[1, 0] = double.Parse(tbm2j1.Text); A[1, 1] = double.Parse(tbm2j2.Text); A[1, 2] = double.Parse(tbm2j3.Text); 
                A[1, 3] = double.Parse(tbm2j4.Text); A[1, 4] = double.Parse(tbm2j5.Text);
                A[2, 0] = double.Parse(tbm3j1.Text); A[2, 1] = double.Parse(tbm3j2.Text); A[2, 2] = double.Parse(tbm3j3.Text); 
                A[2, 3] = double.Parse(tbm3j4.Text); A[2, 4] = double.Parse(tbm3j5.Text);
                A[3, 0] = double.Parse(tbm4j1.Text); A[3, 1] = double.Parse(tbm4j2.Text); A[3, 2] = double.Parse(tbm4j3.Text); 
                A[3, 3] = double.Parse(tbm4j4.Text); A[3, 4] = double.Parse(tbm4j5.Text);
                A[4, 0] = double.Parse(tbm5j1.Text); A[4, 1] = double.Parse(tbm5j2.Text); A[4, 2] = double.Parse(tbm5j3.Text); 
                A[4, 3] = double.Parse(tbm5j4.Text); A[4, 4] = double.Parse(tbm5j5.Text);
                
                b[0] = double.Parse(TbDR1.Text);
                b[1] = double.Parse(TbDR2.Text);
                b[2] = double.Parse(TbDR3.Text);
                b[3] = double.Parse(TbDR4.Text);
                b[4] = double.Parse(TbDR5.Text);

                
                double[] x = GaussSeidel(A, b);

                
                TbR1.Text = x[0].ToString("F4");
                TbR2.Text = x[1].ToString("F4");
                TbR3.Text = x[2].ToString("F4");
                TbR4.Text = x[3].ToString("F4");
                TbR5.Text = x[4].ToString("F4");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private double[] GaussSeidel(double[,] A, double[] b, int maxIter = 1000, double tol = 1e-6)
        {
            int n = 5;
            double[] x = new double[n];

            for (int iter = 0; iter < maxIter; iter++)
            {
                double maxError = 0;

                for (int i = 0; i < n; i++)
                {
                    double suma = b[i];

                    for (int j = 0; j < n; j++)
                        if (j != i)
                            suma -= A[i, j] * x[j];

                    double xNuevo = suma / A[i, i];
                    maxError = Math.Max(maxError, Math.Abs(xNuevo - x[i]));
                    x[i] = xNuevo;
                }

                if (maxError < tol)
                    break;
            }

            return x;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PanelIMC.Visible = !PanelIMC.Visible;
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {            
            for (int i = 1; i <= 5; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    string name = $"tbm{i}j{j}";
                    var ctrs = this.Controls.Find(name, true);
                    if (ctrs.Length > 0 && ctrs[0] is TextBox tb)
                        tb.Clear();
                }
            }

            for (int i = 1; i <= 5; i++)
            {
                string name = $"TbDR{i}";
                var ctrs = this.Controls.Find(name, true);
                if (ctrs.Length > 0 && ctrs[0] is TextBox tb)
                    tb.Clear();
            }

            for (int i = 1; i <= 5; i++)
            {
                string name = $"TbR{i}";
                var ctrs = this.Controls.Find(name, true);
                if (ctrs.Length > 0 && ctrs[0] is TextBox tb)
                    tb.Clear();
            }
        }
    }
}
