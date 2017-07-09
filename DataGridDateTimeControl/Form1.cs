using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GS_DataGridCalendarCell;

namespace DataGridDateTimeControl
{
    public partial class Form1 : Form
    {
      
        public Form1()
        {
            InitializeComponent();
         
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*
            CalendarColumn col = new CalendarColumn();
            


            DataGridViewColumn col2 = new DataGridViewColumn(new DataGridViewTextBoxCell());
            DataGridViewColumn col3 = new DataGridViewColumn(new DataGridViewTextBoxCell());
            DataGridViewColumn col4 = new DataGridViewColumn(new DataGridViewTextBoxCell());


            this.dataGridView2.Columns.Add(col);
            this.dataGridView2.Columns.Add(col2);
            this.dataGridView2.Columns.Add(col3);
            this.dataGridView2.Columns.Add(col4);

            this.dataGridView2.RowCount = 5;

           
            this.dataGridView2.Columns.Insert(3, new CalendarColumn());
            this.dataGridView2.Columns.RemoveAt(4);
            foreach (DataGridViewRow row in this.dataGridView2.Rows)
            {
                row.Cells[0].Value = DateTime.Now;
                row.Cells[3].Value = DateTime.Now;
            }

            */


            


            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn { DataType = typeof(string), ColumnName = "UserName" });
            dt.Columns.Add(new DataColumn { DataType = typeof(DateTime), ColumnName = "DOB" });
            dt.Columns.Add(new DataColumn { DataType = typeof(DateTime), ColumnName = "STARTDATE" });

            DataRow dr = dt.NewRow();
            dr["UserName"] = "ThuraTun";
            dr["DOB"] = DateTime.Parse("1987-11-27");
            dr["STARTDATE"] = DateTime.Parse("1987-11-27");
            dt.Rows.Add(dr);

            

            this.dataGridView2.DataSource = dt;
            this.dataGridView2.Columns.Insert(1, new GS_DataGridCalendarCell.CalendarColumn(CustomDTP_ValueChanged));
            this.dataGridView2.Columns[1].DataPropertyName = "DOB";
            this.dataGridView2.Columns.RemoveAt(2);


            
        }

        public void CustomDTP_ValueChanged(object sender , EventArgs e)
        {
            if(((DateTimePicker)sender).Value > DateTime.Now)
            {
                MessageBox.Show("Invalid DateTime");
               
            }

        }

    }
}
