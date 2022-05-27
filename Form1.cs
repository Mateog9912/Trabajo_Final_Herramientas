using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Trabajo_Final_Herramientas
{
    public partial class Inicio : Form
    {
        
        public Inicio()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btncontinuar.Enabled = false; //Esta linea de codigo nos permite deshabilitar el boton continuar justo apena se abre el programa
        }

        private void Salir_Click(object sender, EventArgs e) //Metodo para cerrar el programa
        {
            this.Close();
        }

        private void controlBotones() //Con este metodo nos aseguramos de que el ususario ingrese solo su nombre y no otro tipo de dato
        {
            if(nombre.Text.Trim() != String.Empty && nombre.Text.All(Char.IsLetter)) //No se puede solo poner nombre, ya que este solo hace referencia al objeto que es te tipo textbox
                //Para acceder al string que representa el texto dentro de la caja, ponemos la propiedad .Text que si es un string. 
                //Si queremos eliminar espacios adelante y atras si el usuario ingreso esos espacios, usamos .Trim
                //Luego hacemos la comparacion con String.Empty para saber si el dato ingresado es un string vacio para segurarse de que no lo sea
                //Y luego para comprobar si todo el string tiene SOLO letras, se usa Text.All(Char.IsLetter)
            {
                btncontinuar.Enabled=true; //Si las condiciones se cumplen, el valor del boton continuar se activa
                errorProvider1.SetError(nombre, ""); //En el componente nombre ponga un error vacio, y luego esta linea logra que desaparezca el icono y el mensaje de error
            }
            else 
            { 
            if (!(nombre.Text.All(Char.IsLetter))) //Aqui vamos a poder verificar cuando el nombre en su texto no tiene todas letras
                    //Cuando eso sucede, va a aparecer el error 
            {
                errorProvider1.SetError(nombre, "El nombre solo debe contener letras");
            }
            else //Si el error no es el de arriba, damos por hecho de que el otro error es que no se introdujo nada
            {
                errorProvider1.SetError(nombre, "Debe introducir su nombre");
            }
            btncontinuar.Enabled = false; //Si se llega a dar alguna de las condiciones que genere un error, pues se van a cumplir estas lineas de codigo
                //En donde vamos a desabilitar el boton continuar hasta que no se introduzca un valor correcto
            nombre.Focus();
            }
        }

        private void nombre_TextChanged(object sender, EventArgs e)
        {
            controlBotones();
        }

        private void btnlimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                nombre.Text = "";


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btncontinuar_Click(object sender, EventArgs e)
        {
            
            using (InterfazVentas ventas = new InterfazVentas(nombre.Text)) //Este porcion de codigo nos sirve para poder continuar al siguiente formulario cuando ya se ingreso el nombre

                //Lo que se esta haciendo basicamente es pasarle al formulario InterfazVentas el texto incluido dentro de el textbox llamado nombre
                //Estamos tomando lo que el usuario ingresa como nombre que previamente debio de pasar por las validaciones de solo tener texto, y ese texto se pasa al constructor del formulario InterfazVentas

                ventas.ShowDialog(); //Con el ShowDialog vamos a lograr mostrar la otra parte del formuilario.


        }

       
    }
}
