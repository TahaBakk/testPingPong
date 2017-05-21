using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lliga_ping_pong.Tests
{
    [TestClass()]
    public class Form1Tests
    {
        [TestMethod()]
        public void AnadirTest()
        {
            Form1 form = new Form1();
            string nombre = form.nombre;
            string apellido = form.apellido;
            Assert.IsNull(nombre, apellido);
            
        }

        [TestMethod()]
        public void button4_ClickTest()
        {
            Form1 form = new Form1();

            ListView listview1 = form.listView3;
            listview1.Items.Add("taha","bakk");
            Assert.IsNotNull(listview1, "error no es null");
        }

        [TestMethod()]
        public void GestorPartidosTest()
        {
            //Assert.Fail();
            Form1 form = new Form1();
            List<string> lista = new List<string>();
            Assert.AreEqual(form.list, lista);
        }
    }
}