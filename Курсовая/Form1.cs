using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Курсовая
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public TableLayoutPanel CreateArea(int rows, int columns, int CellSize)
        {
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.BackColor = Color.Yellow;
            tableLayoutPanel.ColumnCount = columns;
            tableLayoutPanel.Location = new Point(90, 34);
            tableLayoutPanel.Name = "tableLayoutPanel1";
            tableLayoutPanel.RowCount = rows;
            tableLayoutPanel.Size = new Size(0, 0);
            tableLayoutPanel.AutoSize = true;
            tableLayoutPanel.TabIndex = 0;
            tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;


            for (int i = 0; i < columns; i++)
            {
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, CellSize));
            }
            for (int i = 0; i < rows; i++)
            {
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, CellSize));
            }

            return tableLayoutPanel;

        }
        public PictureBox CreatePictureBox(int row, int column)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Name = $"{row}x{column}";
            pictureBox.BackColor = Color.Black;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Click += PictureBox_Click;

            pictureBox.BackgroundImageLayout = ImageLayout.Stretch;
            
            return pictureBox;
        }

        private void PictureBox_Click(object? sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox.BackgroundImage != null)
            {
                pictureBox.BackgroundImage = null;
            }
            else
            {
                string imagesFolderPath = Path.Combine(Application.StartupPath, "Images");
                string imagePath = Path.Combine(imagesFolderPath, "Light_bulb.png");
                pictureBox.BackgroundImage = Image.FromFile(imagePath);

                string[] mas = pictureBox.Name.Split('x');

                MarkCellByRow(Convert.ToInt32(mas[0]),pictureBox.Parent as TableLayoutPanel);
                MarkCellByColumn(Convert.ToInt32(mas[1]), pictureBox.Parent as TableLayoutPanel);
            }
            
        }
        private void MarkCellByColumn(int column, TableLayoutPanel tableLayoutPanel)
        {
            if (column >= 0 && column < tableLayoutPanel.ColumnCount)
            {
                for (int row = 0; row < tableLayoutPanel.RowCount; row++)
                {
                    var control = tableLayoutPanel.GetControlFromPosition(column, row);

                    if (control is PictureBox pictureBox)
                    {
                        pictureBox.BackColor = Color.Red;
                    }
                }
            }
            else
            {
                MessageBox.Show("Указан недопустимый столбец.");
            }
        }

        private void MarkCellByRow(int row, TableLayoutPanel tableLayoutPanel)
        {
            if (row >= 0 && row < tableLayoutPanel.RowCount)
            {
                for (int col = 0; col < tableLayoutPanel.ColumnCount; col++)
                {
                    var control = tableLayoutPanel.GetControlFromPosition(col, row);

                    if (control is PictureBox pictureBox)
                    {
                        pictureBox.BackColor = Color.Red;
                    }
                }
            }
            else
            {
                MessageBox.Show("Указан недопустимый ряд.");
            }
        }

        public void FillTableLayoutPanelWithPictureBoxes(TableLayoutPanel tableLayoutPanel, int rows, int columns)
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    PictureBox pictureBox = CreatePictureBox(row, col); // Створюємо новий PictureBox
                    tableLayoutPanel.Controls.Add(pictureBox, col, row); // Додаємо в TableLayoutPanel
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TableLayoutPanel Table = CreateArea(7, 7, 30);
            FillTableLayoutPanelWithPictureBoxes(Table, 7, 7);
            this.Controls.Add(Table);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
