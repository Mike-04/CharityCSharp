namespace Charity
{
    partial class LoggedIn
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            UserLabel = new Label();
            CazuriCaritabileView = new DataGridView();
            Nume = new DataGridViewTextBoxColumn();
            Suma = new DataGridViewTextBoxColumn();
            DonatorTable = new DataGridView();
            DonorName = new DataGridViewTextBoxColumn();
            DonorAdress = new DataGridViewTextBoxColumn();
            Phone = new DataGridViewTextBoxColumn();
            Donate = new Button();
            CharityLabel = new Label();
            Amount = new TextBox();
            SearchBar = new TextBox();
            ((System.ComponentModel.ISupportInitialize)CazuriCaritabileView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DonatorTable).BeginInit();
            SuspendLayout();
            // 
            // UserLabel
            // 
            UserLabel.AutoSize = true;
            UserLabel.Location = new Point(12, 11);
            UserLabel.Name = "UserLabel";
            UserLabel.Size = new Size(41, 20);
            UserLabel.TabIndex = 0;
            UserLabel.Text = "User:";
            // 
            // CazuriCaritabileView
            // 
            CazuriCaritabileView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            CazuriCaritabileView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            CazuriCaritabileView.Columns.AddRange(new DataGridViewColumn[] { Nume, Suma });
            CazuriCaritabileView.Location = new Point(12, 34);
            CazuriCaritabileView.Name = "CazuriCaritabileView";
            CazuriCaritabileView.RowHeadersWidth = 51;
            CazuriCaritabileView.ScrollBars = ScrollBars.Vertical;
            CazuriCaritabileView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            CazuriCaritabileView.Size = new Size(413, 505);
            CazuriCaritabileView.TabIndex = 1;
            CazuriCaritabileView.CellClick += CazuriCaritabileView_CellClick;
            CazuriCaritabileView.CellEndEdit += CazuriCaritabileView_CellEndEdit;
            // 
            // Nume
            // 
            Nume.HeaderText = "Nume";
            Nume.MinimumWidth = 6;
            Nume.Name = "Nume";
            // 
            // Suma
            // 
            Suma.HeaderText = "Suma";
            Suma.MinimumWidth = 6;
            Suma.Name = "Suma";
            // 
            // DonatorTable
            // 
            DonatorTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DonatorTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DonatorTable.Columns.AddRange(new DataGridViewColumn[] { DonorName, DonorAdress, Phone });
            DonatorTable.Location = new Point(431, 93);
            DonatorTable.Name = "DonatorTable";
            DonatorTable.RowHeadersWidth = 51;
            DonatorTable.Size = new Size(414, 446);
            DonatorTable.TabIndex = 2;
            DonatorTable.CellClick += DonatorTable_CellClick;
            DonatorTable.CellEndEdit += DonatorTable_CellEndEdit;
            // 
            // DonorName
            // 
            DonorName.HeaderText = "Nume";
            DonorName.MinimumWidth = 6;
            DonorName.Name = "DonorName";
            // 
            // DonorAdress
            // 
            DonorAdress.HeaderText = "Address";
            DonorAdress.MinimumWidth = 6;
            DonorAdress.Name = "DonorAdress";
            // 
            // Phone
            // 
            Phone.HeaderText = "Telefon";
            Phone.MinimumWidth = 6;
            Phone.Name = "Phone";
            // 
            // Donate
            // 
            Donate.Location = new Point(751, 30);
            Donate.Name = "Donate";
            Donate.Size = new Size(94, 33);
            Donate.TabIndex = 3;
            Donate.Text = "Donate";
            Donate.UseVisualStyleBackColor = true;
            Donate.Click += Donate_Click;
            // 
            // CharityLabel
            // 
            CharityLabel.AutoSize = true;
            CharityLabel.Location = new Point(439, 37);
            CharityLabel.Name = "CharityLabel";
            CharityLabel.Size = new Size(55, 20);
            CharityLabel.TabIndex = 4;
            CharityLabel.Text = "Charity";
            // 
            // Amount
            // 
            Amount.Location = new Point(511, 33);
            Amount.Name = "Amount";
            Amount.PlaceholderText = "Enter Amount";
            Amount.Size = new Size(234, 27);
            Amount.TabIndex = 5;
            // 
            // SearchBar
            // 
            SearchBar.Location = new Point(431, 64);
            SearchBar.Name = "SearchBar";
            SearchBar.PlaceholderText = "Search";
            SearchBar.Size = new Size(414, 27);
            SearchBar.TabIndex = 6;
            SearchBar.TextChanged += SearchBar_TextChanged;
            // 
            // LoggedIn
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(853, 551);
            Controls.Add(SearchBar);
            Controls.Add(Amount);
            Controls.Add(CharityLabel);
            Controls.Add(Donate);
            Controls.Add(DonatorTable);
            Controls.Add(CazuriCaritabileView);
            Controls.Add(UserLabel);
            Name = "LoggedIn";
            Text = "Logged In";
            Load += LoggedIn_Load;
            ((System.ComponentModel.ISupportInitialize)CazuriCaritabileView).EndInit();
            ((System.ComponentModel.ISupportInitialize)DonatorTable).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label UserLabel;
        private DataGridView CazuriCaritabileView;
        private DataGridViewTextBoxColumn Nume;
        private DataGridViewTextBoxColumn Suma;
        private DataGridView DonatorTable;
        private Button Donate;
        private Label CharityLabel;
        private TextBox Amount;
        private TextBox SearchBar;
        private DataGridViewTextBoxColumn DonorName;
        private DataGridViewTextBoxColumn DonorAdress;
        private DataGridViewTextBoxColumn Phone;
    }
}