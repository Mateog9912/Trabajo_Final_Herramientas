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
    public partial class InterfazVentas : Form


       
    {

        string nombre_cliente;
        public InterfazVentas(string nombre) //Aqui le estamos dando el parametro al constructor para que reciba strings
        {
            InitializeComponent();
            nombre_cliente = nombre;
        }

        public double Costo_de_Items() //Funcion para el costo de los items
        {
            double suma = 0;
            int i = 0;

            for (i = 0; i < dataGridView1.Rows.Count; i++)
            {
                suma = suma + Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value); //Con este for vamos a poder determinar la cantidad de columnas
            }
            return suma;
        }

        private void AddCost() //En esta funcion vamos a tener todas los calculos matematicos para calcular costor e impuestos
        {
            double tax, q;
            tax = 3.9;

            if (dataGridView1.Rows.Count > 0)
            {
                lblimpuesto.Text = String.Format("{0:c2}", (((Costo_de_Items() * tax) / 100)));
                lblsubtotal.Text = String.Format("{0:c2}", (Costo_de_Items()));
                q = ((Costo_de_Items() * tax) / 100); 
                lbltotal.Text = String.Format("{0:c2}", (Costo_de_Items() + q));
                lblbarcode.Text = Convert.ToString(q + Costo_de_Items());
            }
        }

        private void Cambio() //Funcion para dare uso al cambio
        {
            double tax, q, c;
            tax = 3.9;

            if (dataGridView1.Rows.Count > 0)
            {
                q = ((Costo_de_Items() * tax) / 100) * Costo_de_Items();
                c = Convert.ToInt32(lblcosto.Text);
                lbldevuelta.Text = String.Format("{0:c2}", c - q); //Operacion para la devuelta
            }
        }

        Bitmap bitmap;

        private void button27_Click(object sender, EventArgs e) //Metodo para que el imprimir lo que hay en la carta se pueda dar
        {
            try {
                
                int height = dataGridView1.Height;
                dataGridView1.Height = dataGridView1.RowCount * dataGridView1.RowTemplate.Height * 2;
                bitmap = new Bitmap(dataGridView1.Width, height);
                dataGridView1.DrawToBitmap(bitmap, new Rectangle(0, 0, dataGridView1.Width, dataGridView1.Height));
                printPreviewDialog1.PrintPreviewControl.Zoom = 1;
                printPreviewDialog1.ShowDialog();
                dataGridView1.Height = height;
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try 
            {
                e.Graphics.DrawImage(bitmap, 0, 0);
            }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
}

        private void button26_Click(object sender, EventArgs e) //Configuracion de el boton para que me reinicie todo el programa para agregar otros productos
        {
            try
            {
                lblbarcode.Text = "";
                lblcosto.Text = "0";
                lbldevuelta.Text = "";
                lblimpuesto.Text = "";
                lblsubtotal.Text = "";
                lbltotal.Text = "";
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                cbxpago.Text = "";


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InterfazVentas_Load(object sender, EventArgs e)
        {
            cbxpago.Items.Add("Efectivo");
            cbxpago.Items.Add("Visa");
            cbxpago.Items.Add("Master Card");
        }

        private void NumbersOnly(object sender, EventArgs e) //Metodo para dar uso al boton e ingresar los valores con los que se va a hacer el pago
        {
            Button b = (Button)sender;

            if (lblcosto.Text == "0")
            {
                lblcosto.Text = "";
                lblcosto.Text = b.Text;

                        
            }
            else if(b.Text == ".")
            {
                if (!lblcosto.Text.Contains(".")) //Si no hay punto decimal, entonces el valor del lblcosto se le va a subar el valor de b que es el sender del boton
                {
                    lblcosto.Text = lblcosto.Text + b.Text;
                }
                   
            }
            else
            {
                lblcosto.Text = lblcosto.Text + b.Text;
            }
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            lblcosto.Text = "0"; //Para dar uso al boton C, y poder limpiar lo que se muestra en el programa
        }

        private void btnpagar_Click(object sender, EventArgs e)
        {
            if (cbxpago.Text == "Efectivo")
            {
                Cambio(); //Llamado de la funcion cambio para calcular este
            }
            else
            {
                lbldevuelta.Text = "";
                lblcosto.Text = "0";
            }
            
        }

        private void btnremover_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row); //Para poder remover las columnas
            }
            AddCost(); //Llamado de la funcion para a;adir los costos
            if (cbxpago.Text == "Efectivo")
            {
                Cambio(); 
            }
            else
            {
                lbldevuelta.Text = "";
                lblcosto.Text = "0";
            }
        }

        

        private void btnhambur1_Click(object sender, EventArgs e)
        {
            double CostodeItem = 2.5;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Hamburguesa Sencilla"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * CostodeItem;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Hamburguesa Sencilla", "1", CostodeItem);
            AddCost();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            double CostodeItem = 5.0;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Cookie Dough"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * CostodeItem;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Cookie Dough", "1", CostodeItem);
            AddCost();
        }

        private void btnhambur2_Click(object sender, EventArgs e)
        {
            double CostodeItem = 5.0;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Hamburguesa Especial"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * CostodeItem;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Hamburguesa Especial", "1", CostodeItem);
            AddCost();
        }

        private void btnhambur3_Click(object sender, EventArgs e)
        {
            double CostodeItem = 7.5;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Hamburguesa Super"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * CostodeItem;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Hamburguesa Super", "1", CostodeItem);
            AddCost();
        }

        private void btnperro1_Click(object sender, EventArgs e)
        {
            double CostodeItem = 2.5;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Hamburguesa Sencillo"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * CostodeItem;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Hamburguesa Sencillo", "1", CostodeItem);
            AddCost();
        }

        private void btnperro2_Click(object sender, EventArgs e)
        {
            double CostodeItem = 5.0;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Perro Especial"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * CostodeItem;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Perro Especial", "1", CostodeItem);
            AddCost();
        }

        private void btnperro3_Click(object sender, EventArgs e)
        {
            double CostodeItem = 7.5;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Super Perro"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * CostodeItem;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Super Perro", "1", CostodeItem);
            AddCost();
        }

        private void btnpizza1_Click(object sender, EventArgs e)
        {
            double CostodeItem = 9.0;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Pizza Pepperoni"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * CostodeItem;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Pizza Pepperoni", "1", CostodeItem);
            AddCost();
        }

        private void btnpizza2_Click(object sender, EventArgs e)
        {
            double CostodeItem = 9.0;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Pizza Tres Carnes"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * CostodeItem;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("pizza Tres Carnes", "1", CostodeItem);
            AddCost();
        }

        private void btnpizza3_Click(object sender, EventArgs e)
        {
            double CostodeItem = 9.0;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Piza Napolitana"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * CostodeItem;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Pizza Napolitana", "1", CostodeItem);
            AddCost();
        }

        private void btnpapas1_Click(object sender, EventArgs e)
        {
            double CostodeItem = 4.0;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Salchipapa Pequena"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * CostodeItem;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Salchipapa Pequena", "1", CostodeItem);
            AddCost();
        }

        private void btnpapas2_Click(object sender, EventArgs e)
        {
            double CostodeItem = 6.0;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Salchipapa Mediana"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * CostodeItem;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Salchipapa Mediana", "1", CostodeItem);
            AddCost();
        }

        private void btnpapas3_Click(object sender, EventArgs e)
        {
            double CostodeItem = 8.0;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Salchipapa Grande"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * CostodeItem;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Salchipapa Grande", "1", CostodeItem);
            AddCost();
        }

        private void btngase1_Click(object sender, EventArgs e)
        {
            double CostodeItem = 3.0;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Coca Cola"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * CostodeItem;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Coca Cola", "1", CostodeItem);
            AddCost();
        }

        private void btngase2_Click(object sender, EventArgs e)
        {
            double CostodeItem = 3.0;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Premio"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * CostodeItem;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Premio", "1", CostodeItem);
            AddCost();
        }

        private void btngase3_Click(object sender, EventArgs e)
        {
            double CostodeItem = 3.0;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Naranjada"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * CostodeItem;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Naranjada", "1", CostodeItem);
            AddCost();
        }

        private void btnpostre1_Click(object sender, EventArgs e)
        {
            double CostodeItem = 5.0;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Tiramisu"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * CostodeItem;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Tiramisu", "1", CostodeItem);
            AddCost();
        }

        private void btnpostre2_Click(object sender, EventArgs e)
        {
            double CostodeItem = 5.0;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Brownie"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * CostodeItem;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Brownie", "1", CostodeItem);
            AddCost();
        }

        private void btnpostre3_Click(object sender, EventArgs e)
        {
            double CostodeItem = 5.0;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Brazo de Reina"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * CostodeItem;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Brazo de Reina", "1", CostodeItem);
            AddCost();
        }

        private void btnpostre4_Click(object sender, EventArgs e)
        {
            double CostodeItem = 5.0;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Maracuya"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * CostodeItem;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Maracuya", "1", CostodeItem);
            AddCost();
        }

        private void btnpostre5_Click(object sender, EventArgs e)
        {
            double CostodeItem = 5.0;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Cheese Cake"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * CostodeItem;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Cheese Cake", "1", CostodeItem);
            AddCost();
        }

        private void btnlicor1_Click(object sender, EventArgs e)
        {
            double CostodeItem = 10.0;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Jagger"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * CostodeItem;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Jagger", "1", CostodeItem);
            AddCost();
        }

        private void btnlicor_Click(object sender, EventArgs e)
        {
            double CostodeItem = 15.0;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Aguardiente"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * CostodeItem;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Aguardiente", "1", CostodeItem);
            AddCost();
        }

        private void btnlicor3_Click(object sender, EventArgs e)
        {
            double CostodeItem = 20.0;
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if ((bool)(row.Cells[0].Value = "Whisky"))
                {
                    row.Cells[1].Value = Double.Parse((string)row.Cells[1].Value + 1);
                    row.Cells[2].Value = Double.Parse((string)row.Cells[1].Value) * CostodeItem;
                    AddCost();
                }
            }
            dataGridView1.Rows.Add("Whisky", "1", CostodeItem);
            AddCost();
        }
    }
}
