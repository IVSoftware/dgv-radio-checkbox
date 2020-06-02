using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dgv_radio_checkbox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (!DesignMode)    // We only want this behavior at runtime
            {
                // Create the binding list
                BindingList<QuestionaireItem> testdata = new BindingList<QuestionaireItem>();
                // And add 5 example items to it
                for (int i = 0; i < 5; i++) testdata.Add(new QuestionaireItem());
                // Now make this list the DataSource of the DGV.
                dataGridView1.DataSource = testdata;

                // This just formats the column widths a little bit
                dataGridView1.Columns["Question"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns["Maybe"].Width =
                dataGridView1.Columns["Yes"].Width =
                dataGridView1.Columns["No"].Width = 40;

                // And this subscribes to the event (one of them anyway...)
                // that will fire when the checkbox is changed
                dataGridView1.CurrentCellDirtyStateChanged += DataGridView1_CurrentCellDirtyStateChanged;
            }
        }

        private void DataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // The cell will be considered "dirty" or modified so Commit first.
            dataGridView1.EndEdit(DataGridViewDataErrorContexts.Commit);
            // Get the QuestionaireItem that is bound to the row
            QuestionaireItem item = (QuestionaireItem)
                dataGridView1
                .Rows[dataGridView1.CurrentCell.RowIndex]
                .DataBoundItem;
            // Now see which column changed:
            switch (dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name)
            {
                case "Yes":
                    item.No = false;        // i.e. "unchecked"
                    item.Maybe = false;
                    break;
                case "No":
                    item.Yes = false;       
                    item.Maybe = false;
                    break;
                case "Maybe":
                    item.Yes = false;
                    item.No = false;
                    break;
            }
            dataGridView1.Refresh();    // Update the binding list to the display
        }
    }
    class QuestionaireItem
    {
        public string Question { get; internal set; } = "Question " + _count++;
        public bool Yes { get; set; }
        public bool No { get; set; }
        public bool Maybe { get; set; }

        static int _count = 1;
    }
}
