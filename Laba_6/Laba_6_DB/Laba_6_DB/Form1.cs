using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Laba_6_DB
{
    public partial class Form1 : Form
    {
        int oldIndex;
        ShowTables showTables = new ShowTables(@"Data Source= DESKTOP-J7VJ20H\SQLEXPRESS;Initial Catalog=Shop;Integrated Security=True");
        System.Timers.Timer timer = new System.Timers.Timer();
        System.Timers.Timer chekerOfFilter = new System.Timers.Timer();
        public Form1()
        {
            InitializeComponent();
            InitializetionMainTable();

            ChoseTable.SelectedIndex = 4;
            timer = new System.Timers.Timer(500);
            timer.Enabled = true;
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (ChoseTable.InvokeRequired)
            {
                ChoseTable.Invoke(new Action(() =>
                {
                    int index = ChoseTable.SelectedIndex;
                    // действия с полученным индексом
                    if (oldIndex != index)
                    {
                        oldIndex = index;
                        FillWFATable(index);
                    }
                }));
            }
            else
            {
                int index = ChoseTable.SelectedIndex;
                // действия с полученным индексом
                if (oldIndex != index)
                {
                    oldIndex = index;
                    FillWFATable(index);
                }
            }

        }

        private void DeleteFilter()
        {
            int j = 0;
            int k = 0;
            int g = 0;
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i].Name == "chekBox" + j)
                {
                    this.Controls.RemoveAt(i);
                    j++;
                    i--;
                }
            }

            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i].Name == "textBox" + k)
                {
                    this.Controls.RemoveAt(i);
                    k++;
                    i--;
                }
            }

            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i].Name == "label" + g)
                {
                    this.Controls.RemoveAt(i);
                    g++;
                    i--;
                }
            }
        }

        private void CreateChekBoxs(List<string> columsNames)
        {
            DeleteFilter();

            for (int i = 0; i < columsNames.Count; i++)
            {
                System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox();
                textBox.Name = "textBox" + i;
                textBox.Location = new System.Drawing.Point(30, 60 + 50 * i);
                this.Controls.Add(textBox);
            }

            for (int i = 0; i < columsNames.Count; i++)
            {
                Label label = new Label();
                label.Name = "label" + i;
                label.Text = columsNames[i];
                label.Location = new System.Drawing.Point(50, 40 + 50 * i);
                this.Controls.Add(label);
            }

            for (int i = 0; i < columsNames.Count; i++)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Name = "chekBox" + i;
                checkBox.Checked = true;
                checkBox.Location = new System.Drawing.Point(30, 40 + 50 * i);
                this.Controls.Add(checkBox);
            }
        }
        List<string> columnsName;
        private void ShowTable(DataTable dataTable)
        {
            AirlinesGridView.DataSource = dataTable;
            AirlinesGridView.AutoGenerateColumns = true;
            List<string> columnsName = new List<string>();
            foreach (DataGridViewColumn column in AirlinesGridView.Columns)
            {
                columnsName.Add(column.Name);
            }
            this.columnsName = columnsName;
            CreateChekBoxs(columnsName);
        }

        private void FillWFATable(int index)
        {
            DataTable dt = new DataTable();
            switch (index)
            {
                case 0:
                    dt = showTables.ShowArivalsDispatchTable();
                    ShowTable(dt);
                    break;
                case 1:
                    dt = showTables.ShowEmployesTable();
                    ShowTable(dt);
                    break;
                case 2:
                    dt = showTables.ShowFlightsTable();
                    ShowTable(dt);
                    break;
                case 3:
                    dt = showTables.ShowPlanesTable();
                    ShowTable(dt);
                    break;
                case 4:
                    dt = showTables.ShowTkitTable();
                    ShowTable(dt);
                    break;
                case 5:
                    dt = showTables.ShowPostsTable();
                    ShowTable(dt);
                    break;
                case 6:
                    dt = showTables.ShowTypeOfPlanesTable();
                    ShowTable(dt);
                    break;
            }
        }

        private void InitializetionMainTable()
        {
            DataTable dt = showTables.ShowTkitTable();
            AirlinesGridView.DataSource = dt;
            AirlinesGridView.AutoGenerateColumns = true;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = AirlinesGridView.CurrentRow;
            DataGridViewCell cell = row.Cells[0];
            int rowIndex = Convert.ToInt32(cell.Value);
            DeleterRow deleter = new DeleterRow(@"Data Source= DESKTOP-J7VJ20H\SQLEXPRESS;Initial Catalog=shop;Integrated Security=True");
            DeleteFromTable(deleter, rowIndex);
            FillWFATable(oldIndex);
        }

        private void DeleteFromTable(DeleterRow deleterRow, int rowIndex)
        {
            switch (oldIndex)
            {
                case 0:
                    deleterRow.DeleteFormArivalsDispatch(rowIndex);
                    break;
                case 1:
                    deleterRow.DeleteFormEmployes(rowIndex);
                    break;
                case 2:
                    deleterRow.DeleteFormFlights(rowIndex);
                    break;
                case 3:
                    deleterRow.DeleteFormPlanes(rowIndex);
                    break;
                case 4:
                    deleterRow.DeleteFormPlaneTickets(rowIndex);
                    break;
                case 5:
                    deleterRow.DeleteFormPosts(rowIndex);
                    break;
               case 6:
                    deleterRow.DeleteFormTypeOfPlanes(rowIndex);
                    break;
            }
        }

        private void TurnOfTools()
        {
            AirlinesGridView.Width = AirlinesGridView.Width / 2;
            AirlinesGridView.Location = new System.Drawing.Point(AirlinesGridView.Location.X + AirlinesGridView.Width, AirlinesGridView.Location.Y);
            UpdateButton.Visible = false;
            DeleteButton.Visible = false;
            CreateButton.Visible = false;
            ChoseTable.Visible = false;
            DeleteFilter();
        }

        private void TurnOnTools()
        {
            AirlinesGridView.Width = AirlinesGridView.Width * 2;
            AirlinesGridView.Location = new System.Drawing.Point(AirlinesGridView.Location.X - AirlinesGridView.Width / 2, AirlinesGridView.Location.Y);
            UpdateButton.Visible = true;
            DeleteButton.Visible = true;
            CreateButton.Visible = true;
            ChoseTable.Visible = true;
            FillWFATable(oldIndex);
        }

        int count;
        List<string> nameOfColumns = new List<string>();
        List<string> data = new List<string>();

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            TurnOfTools();
            List<string> columnsName = new List<string>();
            foreach (DataGridViewColumn column in AirlinesGridView.Columns)
            {
                columnsName.Add(column.Name);
            }
            count = columnsName.Count;
            nameOfColumns = columnsName;
            for (int i = 0; i < columnsName.Count; i++)
            {
                Label label = new Label();
                label.Name = "label" + i;
                label.Text = columnsName[i];
                label.Visible = true;
                label.Location = new System.Drawing.Point(30, 30 + 50 * i);
                this.Controls.Add(label);
            }
            for (int i = 0; i < columnsName.Count; i++)
            {
                System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox();
                textBox.Name = "text" + i;
                textBox.Visible = true;
                textBox.Location = new System.Drawing.Point(30, 55 + 50 * i);
                this.Controls.Add(textBox);
            }

            System.Windows.Forms.Button button = new System.Windows.Forms.Button();
            button.Location = new System.Drawing.Point(30, 75 + 50 * columnsName.Count);
            button.Name = "button1";
            button.Text = "Confirm";
            button.Click += Button_Click;
            this.Controls.Add(button);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            int k = 0, j = 0;
            UpdateTable updateTable = new UpdateTable(@"Data Source= DESKTOP-J7VJ20H\SQLEXPRESS;Initial Catalog=Airlines;Integrated Security=True");
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i].Name == "text" + j)
                {
                    data.Add(this.Controls[i].Text);
                    j++;
                }
            }
            j = 0;
            updateTable.UpdateTables(data, ChoseTable.Text, nameOfColumns);

            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i].Name == "label" + k)
                {
                    this.Controls.RemoveAt(i);
                    k++;
                    i = 0;
                }
                if (this.Controls[i].Name == "text" + j)
                {
                    this.Controls.RemoveAt(i);
                    j++;
                    i = 0;
                }
                if (this.Controls[i].Name == "button1")
                {
                    this.Controls.RemoveAt(i);
                }
            }

            TurnOnTools();
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            TurnOfTools();
            List<string> columnsName = new List<string>();
            foreach (DataGridViewColumn column in AirlinesGridView.Columns)
            {
                columnsName.Add(column.Name);
            }
            count = columnsName.Count;
            nameOfColumns = columnsName;
            for (int i = 1; i < columnsName.Count; i++)
            {
                Label label = new Label();
                label.Name = "label" + i;
                label.Text = columnsName[i];
                label.Visible = true;
                label.Location = new System.Drawing.Point(30, 30 + 50 * i);
                this.Controls.Add(label);
            }
            for (int i = 1; i < columnsName.Count; i++)
            {
                System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox();
                textBox.Name = "text" + i;
                textBox.Visible = true;
                textBox.Location = new System.Drawing.Point(30, 55 + 50 * i);
                this.Controls.Add(textBox);
            }

            System.Windows.Forms.Button button = new System.Windows.Forms.Button();
            button.Location = new System.Drawing.Point(30, 75 + 50 * columnsName.Count);
            button.Name = "button1";
            button.Text = "Confirm";
            button.Click += Button_Click1; ;
            this.Controls.Add(button);
        }

        private void Button_Click1(object sender, EventArgs e)
        {
            int k = 1, j = 1;
            CreaterOfRows createrOfRows = new CreaterOfRows(@"Data Source= DESKTOP-J7VJ20H\SQLEXPRESS;Initial Catalog=Airlines;Integrated Security=True");
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i].Name == "text" + j)
                {
                    data.Add(this.Controls[i].Text);
                    j++;
                }
            }
            j = 1;
            createrOfRows.CreateRowTable(data, ChoseTable.Text, nameOfColumns);

            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i].Name == "label" + k)
                {
                    this.Controls.RemoveAt(i);
                    k++;
                    i = 0;
                }
                if (this.Controls[i].Name == "text" + j)
                {
                    this.Controls.RemoveAt(i);
                    j++;
                    i = 0;
                }
                if (this.Controls[i].Name == "button1")
                {
                    this.Controls.RemoveAt(i);
                }
            }

            TurnOnTools();
        }

        private void FilterButton_Click(object sender, EventArgs e)
        {
            Filter filter = new Filter(@"Data Source= DESKTOP-J7VJ20H\SQLEXPRESS;Initial Catalog=Airlines;Integrated Security=True");

            List<string> columnsName = new List<string>();
            foreach (string column in this.columnsName)
            {
                columnsName.Add(column);
            }

            int h = 0;
            for (int k = 0; k < Controls.Count; k++)
            {
                if (Controls[k] is CheckBox checkBox)
                {
                    if (!checkBox.Checked)
                    {
                        columnsName[h] = "";
                        h++;
                    }
                }
            }


            string[] wordFilter = new string[columnsName.Count];
            for (int i = 0; i < wordFilter.Length; i++)
            {
                wordFilter[i] = "";
            }

            int j = 0;

            for (int i = 0; i < this.Controls.Count && wordFilter.Length != 0; i++)
            {
                if (this.Controls[i].Name == "textBox" + j)
                {
                    wordFilter[j] = this.Controls[i].Text;
                    j++;
                }

            }

            if (ChoseTable.InvokeRequired)
            {
                ChoseTable.Invoke(new Action(() =>
                {
                    AirlinesGridView.Rows.Clear();
                    AirlinesGridView.DataSource = filter.FilterTable(columnsName, wordFilter, ChoseTable.Text); ;
                    AirlinesGridView.AutoGenerateColumns = true;
                }));
            }
            else
            {
                AirlinesGridView.DataSource = filter.FilterTable(columnsName, wordFilter, ChoseTable.Text);
                AirlinesGridView.AutoGenerateColumns = true;
            }
        }
    }
}
