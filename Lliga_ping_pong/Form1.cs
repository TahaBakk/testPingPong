using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lliga_ping_pong
{
    public partial class Form1 : Form
    {

        //ArrayList partidos = new ArrayList();
        public List<string> partidos = new List<string>();
        public List<string> resultados = new List<string>(); //Para poder almacenar en el firebase mas facilmente los datos
        public List<string> list = new List<string>();
        public string nombre, apellido;
        public ListView listView3;

        int count = 0;

        public Form1()
        {
            InitializeComponent();

            //propiedades
            listView1.View = View.Details;
            listView1.FullRowSelect = true;

            listView2.View = View.Details;
            listView2.FullRowSelect = true;

            //Añadiendo las columnas
            listView1.Columns.Add("Nombre", 150);
            listView1.Columns.Add("Apellido", 150);

            listView2.Columns.Add("Jugador", 150);
            listView2.Columns.Add("Puntos", 150);

        }

        //añadir

        public void Anadir(string nombre, string apellido)
        {
            //añadir jugadores
            if (!string.IsNullOrWhiteSpace(nombre) && !string.IsNullOrWhiteSpace(apellido))
            {
                string[] fila = { nombre, apellido };
                ListViewItem item = new ListViewItem(fila);
                listView1.Items.Add(item);//esto es para añadirla al listView
            }
            else
            {
                MessageBox.Show("Rellene todos los campos");
            }



        }

        public void Actualizar()
        {
            listView1.SelectedItems[0].SubItems[0].Text = textBox1.Text;
            listView1.SelectedItems[0].SubItems[1].Text = textBox2.Text;

            //limpiamos los campos despues de actualizar
            textBox1.Text = "";
            textBox2.Text = "";
        }

        public void Eliminar()
        {
            if (MessageBox.Show("Seguro que lo quieres eliminar","ELIMINAR",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning)==DialogResult.OK)
            {
                listView1.Items.RemoveAt(listView1.SelectedIndices[0]);

                //limpiamos los campos despues de actualizar
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        public void button1_Click(object sender, System.EventArgs e)//añadir
        {
            Anadir(textBox1.Text, textBox2.Text);

            //limpiamos los campos despues de añadir
            textBox1.Text = "";
            textBox2.Text = "";

        }

        public void button2_Click(object sender, System.EventArgs e)//eliminar
        {
            Eliminar();
        }

        public void button3_Click(object sender, System.EventArgs e)//actualizar
        {
            Actualizar();
        }

        public void button4_Click(object sender, System.EventArgs e)//limpiar
        {
            listView1.Items.Clear();

            //limpiamos los campos despues de añadir
            textBox1.Text = "";
            textBox2.Text = "";
        }

        public void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = listView1.SelectedItems[0].SubItems[0].Text;
            textBox2.Text = listView1.SelectedItems[0].SubItems[1].Text;
        }

        public void button5_Click(object sender, System.EventArgs e)//empezar liga
        {
            panel1.Visible = false;
            panel2.Visible = true;

            List<string> list = new List<string>();
 
            // recoremos el listview1
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                //añadir jugadores al listview
                string[] fila = { listView1.Items[i].SubItems[0].Text + " " + listView1.Items[i].SubItems[1].Text, "0" };
                ListViewItem item = new ListViewItem(fila);
                listView2.Items.Add(item);//esto es para añadirla al listView

                //añadir jugador a mi arraylist
                string jugador = listView1.Items[i].SubItems[0].Text + " " + listView1.Items[i].SubItems[1].Text;
                list.Add(jugador);
            }

            GestorPartidos(list);//le pasamos todos los jugadores para montar los partidos

        }

        public void GestorPartidos(List<string> list)
        {

            for (int i = 0; i < list.Count-1; i++)
            {
                int j = 1;
                for (; j < list.Count; j++)
                {
                    string p1 = list[i];
                    string p2 = list[j];
                    if (!p1.Equals(p2))
                    {
                        string p3 = p1 + "," + p2;
                        partidos.Add(p3);
                    }
                   
                }
                j++;
            }
            EmpezarPartido();
        }

        public void EmpezarPartido()
        {
            if (count < partidos.Count)
            {
                //Los que jugaran primero
                string partido = partidos[count];
                string[] primerPartido = partido.Split(',');

                //primera ronda
                string p1 = primerPartido[0];//jugador 1
                string p2 = primerPartido[1];//jugador 2

                textBox5.Text = p1;
                textBox6.Text = p2;

                if ((count + 1) < partidos.Count)
                {
                    //Los que jugaran despues
                    string partido2 = partidos[count + 1];
                    string[] primerPartido2 = partido2.Split(',');

                    //segunda ronda
                    string p3 = primerPartido2[0];//jugador 3
                    string p4 = primerPartido2[1];//jugador 4

                    textBox7.Text = p3;
                    textBox8.Text = p4;
                }
                else
                {
                    label5.Visible = false;
                    label7.Visible = false;
                    textBox7.Visible = false;
                    textBox8.Visible = false;
                    label8.Visible = true;
                }
               

                count++;
            }
            else
            {
                MessageBox.Show("LIGA FINALIZADA");
                //limpiamos los campos despues de actualizar
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                //Que me cargue el resultado final en un nuevo panel
            }
        }

        public void Form1_Load(object sender, System.EventArgs e)
        {

        }

        public void button6_Click(object sender, System.EventArgs e)//Finalizar partido
        {
            if(!string.IsNullOrWhiteSpace(textBox3.Text) || !string.IsNullOrWhiteSpace(textBox4.Text))
            {
                int jugador1 = Int32.Parse(textBox3.Text);
                int jugador2 = Int32.Parse(textBox4.Text);
                //En el listview, subimos 3 puntos al ganador
                if (jugador1 > jugador2)
                {
                    Ganador(textBox5.Text);
                }else
                {
                    Ganador(textBox6.Text);
                }
                string resultado = "P" + count + "," + textBox5.Text + "," + textBox6.Text + "," + textBox3.Text + "," + textBox4.Text;
                resultados.Add(resultado);

                EmpezarPartido();
            }
            else
            {
                MessageBox.Show("Introduce bien el resultado");
            }
        }

        public void Ganador(string gndr)
        {
            string ganador = gndr;
            for (int i = 0; i < listView2.Items.Count; i++)
            {
                //añadir jugadores al listview
                if (listView2.Items[i].SubItems[0].Text == ganador)
                {
                    int puntos = Int32.Parse(listView2.Items[i].SubItems[1].Text);
                    puntos += 3;
                    listView2.Items[i].SubItems[1].Text = Convert.ToString(puntos);
                }
            }
        }

        public void button7_Click(object sender, EventArgs e)//cancelar liga
        {
            //Eliminamos todos los datos sobre la liga
            panel2.Visible = false;
            panel1.Visible = true;
            listView2.Items.Clear();
            partidos.Clear();
            resultados.Clear();
        }


        //LIGA


    }
}
