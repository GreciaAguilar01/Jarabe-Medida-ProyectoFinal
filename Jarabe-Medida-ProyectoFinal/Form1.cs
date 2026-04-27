using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jarabe_Medida_ProyectoFinal
{
    public partial class FormIniciar_Sesion : Form
    {
        public FormIniciar_Sesion()
        {
            InitializeComponent();
        }

        private void BtnEntrar_Click(object sender, EventArgs e)
        {

            // 1. Obtener los datos ingresados por el pediatra
            string usuario = TbUsuario.Text;
            string contrasena = TbContraseña.Text;

            // 2. Validar credenciales
            if (usuario == "tester" && contrasena == "1234")
            {
                MessageBox.Show("¡Bienvenido, Dr. " + usuario + "!", "Inicio de Sesión Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Interfaz_Jarabe ObjInterfaz_Jarabe = new Interfaz_Jarabe();

                ObjInterfaz_Jarabe.Show(); // mostrar interfaz_Jarabe

                this.Hide();
            }
            else
            {
                // Credenciales incorrectas
                MessageBox.Show("Usuario o contraseña incorrectos. Por favor, intente de nuevo.", "Error de Inicio de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);

                TbUsuario.Clear();
                TbContraseña.Clear();
                TbUsuario.Focus();
            }
        }
    }
}
