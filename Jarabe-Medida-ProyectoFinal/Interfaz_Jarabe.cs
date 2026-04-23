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
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse (qué tan redondo)
            int nHeightEllipse // height of ellipse (qué tan redondo)
        );
    }
}
