using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QTask
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            dataGridView1.Columns["InWork"].CellTemplate = new CardCell();
            //dataGridView1.CellMouseDown += new DataGridViewCellMouseEventHandler(CellMouseDown);
            //dataGridView1.DragDrop += new DragEventHandler(DragDrop);
            //dataGridView1.DragEnter += new DragEventHandler(DragEnter);
        }
        private void CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dataGridView1.DoDragDrop(dataGridView1.Rows[e.RowIndex], DragDropEffects.Move);
            }
        }

        private void DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void DragDrop(object sender, DragEventArgs e)
        {
            Point clientPoint = dataGridView1.PointToClient(new Point(e.X, e.Y));
            DataGridView.HitTestInfo hit = dataGridView1.HitTest(clientPoint.X, clientPoint.Y);

            if (hit.Type == DataGridViewHitTestType.Cell)
            {
                DataGridViewRow rowToMove = (DataGridViewRow)e.Data.GetData(typeof(DataGridViewRow));
                dataGridView1.Rows.Remove(rowToMove);
                dataGridView1.Rows.Insert(hit.RowIndex, rowToMove);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "qTaskDataSet.Tasks". При необходимости она может быть перемещена или удалена.
            this.tasksTableAdapter.Fill(this.qTaskDataSet.Tasks);

        }
    }
    public class CardCell : DataGridViewTextBoxCell
    {
        protected override void Paint(Graphics graphics, Rectangle clipBounds,
                                      Rectangle cellBounds, int rowIndex,
                                      DataGridViewElementStates cellState, object value,
                                      object formattedValue, string errorText,
                                      DataGridViewCellStyle cellStyle,
                                      DataGridViewAdvancedBorderStyle advancedBorderStyle,
                                      DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState,
                       value, formattedValue, errorText, cellStyle,
                       advancedBorderStyle, paintParts);

            // Рисуем карточку
            // cardBounds определяет размеры карточки
            Rectangle cardBounds = new Rectangle(cellBounds.X + 5, cellBounds.Y + 5,
                                                 cellBounds.Width - 10, cellBounds.Height - 10);

            // Определяем поля для рисования
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            format.LineAlignment = StringAlignment.Center;
            Rectangle taskIdBounds = new Rectangle(cardBounds.X + 5, cardBounds.Y + 5,
                                                    cardBounds.Width - 10, 20);
            Rectangle titleBounds = new Rectangle(cardBounds.X + 5,
                                                   taskIdBounds.Bottom + 5,
                                                   cardBounds.Width - 10, 20);
            Rectangle startDateBounds = new Rectangle(cardBounds.X + 5,
                                                       titleBounds.Bottom + 5,
                                                       cardBounds.Width - 10, 20);
            Rectangle endDateBounds = new Rectangle(cardBounds.X + 5,
                                                     startDateBounds.Bottom + 5,
                                                     cardBounds.Width - 10, 20);
            Rectangle statusBounds = new Rectangle(cardBounds.X + 5,
                                                    endDateBounds.Bottom + 5,
                                                    cardBounds.Width - 10, 20);

            // Заполняем поля данными
            int taskId = 0;
            string title = "";
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MinValue;
            string status = "";

            /*if (value != null)
            {
                DataRow row = value as DataRow;
                taskId = (int)row["TaskId"];
                title = (string)row["Title"];
                startDate = (DateTime)row["Start_date"];
                endDate = (DateTime)row["End_date"];
                status = (string)row["Status"];
            }*/

            // Рисуем поля на карточке
            graphics.DrawString("Task Id: " + taskId.ToString(), cellStyle.Font,
                                 Brushes.Black, taskIdBounds, format);
            graphics.DrawString("Title: " + title, cellStyle.Font,
                                 Brushes.Black, titleBounds, format);
            graphics.DrawString("Start Date: " + startDate.ToShortDateString(),
                                 cellStyle.Font, Brushes.Black, startDateBounds, format);
            graphics.DrawString("End Date: " + endDate.ToShortDateString(), cellStyle.Font,
                                 Brushes.Black, endDateBounds, format);
            graphics.DrawString("Status: " + status, cellStyle.Font,
                                 Brushes.Black, statusBounds, format);
        }
    }
}
