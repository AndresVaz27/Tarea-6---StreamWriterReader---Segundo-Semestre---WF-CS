using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tarea_2___Segundo_Semestre;

namespace Tarea_4___Arreglos___Segundo_Semestre___WF_CS
{
    public partial class Form1 : Form
    {
        string nombreArchivo;
        int cantidadContactos;
        int siguienteIndex = 0;
        Contacto[] contactos;
        public Form1()
        {
            InitializeComponent();
            nombreArchivo = "contactos.txt";
            if (!File.Exists(nombreArchivo))
            {
                File.Create(nombreArchivo).Dispose();
            }
            else
            {
                using (StreamReader sr = File.OpenText(nombreArchivo))
                {
                    string linea;
                    while ((linea = sr.ReadLine()) != null)
                    {
                        Contacto nuevo = new Contacto();
                        string[] datos = linea.Split(',');
                        nuevo.Nombre = datos[0];
                        nuevo.Edad = datos[1];
                        nuevo.Telefono = datos[2];
                        nuevo.Correo = datos[3];
                        tabla.Rows.Add(nuevo.Nombre,nuevo.edad,nuevo.Telefono,nuevo.Correo);
                    }
                    sr.Close();
                }
            }
        }
        // ESTABLECER TAMAÑO DEL ARREGLO.
        private void btnEstablecer_Click(object sender, EventArgs e)
        {
            try
            {
                cantidadContactos = int.Parse(txtArregloLenght.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Únicamente números enteros para establecer la cantidad de contactos por agregar, por favor");
            }
            contactos = new Contacto[cantidadContactos];
        }

        // AGREGAR UN NUEVO CONTACTO.
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (contactos == null) { MessageBox.Show("Establezca el número de contactos por agregar.");}
            else if (siguienteIndex >= contactos.Length)
            {
                MessageBox.Show("La cantidad de contactos por agregar se completó.");  
            }
            else
            {
                Contacto nuevo = new Contacto();
                nuevo.Nombre = txtNombre.Text;
                nuevo.Telefono = txtTelefono.Text;
                DateTime fechaNacimiento;
                if (DateTime.TryParseExact(txtFecha.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaNacimiento))
                {
                    nuevo.FechaNacimiento = fechaNacimiento;
                }
                else
                {
                    MessageBox.Show("La fecha de nacimiento debe tener el formato dd/MM/yyyy");
                    return;
                }
                nuevo.Correo = txtCorreo.Text;

                contactos[siguienteIndex] = nuevo;
                tabla.Rows.Add(nuevo.Nombre, nuevo.Edad, nuevo.Telefono, nuevo.Correo);
                using (StreamWriter sw = File.AppendText(nombreArchivo)) 
                {
                    sw.WriteLine($"{nuevo.Nombre},{nuevo.Edad},{nuevo.Telefono},{nuevo.Correo}");
                    sw.Close();
                }
                txtNombre.Clear();
                txtFecha.Clear();
                txtTelefono.Clear();
                txtCorreo.Clear();
                siguienteIndex++;
            }

        }
    }
}
