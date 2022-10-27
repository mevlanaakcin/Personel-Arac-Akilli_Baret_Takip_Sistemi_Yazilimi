using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; //dosya işlemleri için özellikle resim kullanmak için kullandık
using System.IO.Ports;
using System.Data.OleDb;  // veri tabanı kütüphanesi eklendi

namespace helmet_tracking_system
{
    public partial class yukleniyor : Form
    {
        public yukleniyor()
        {
            InitializeComponent();
        }

        public string mesaj = "";
        public int step;
        private void yukleniyor_Load(object sender, EventArgs e)
        {

        }
    }
}
