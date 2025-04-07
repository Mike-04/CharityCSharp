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
            UserLabel = new System.Windows.Forms.Label();
            CazuriCaritabileView = new System.Windows.Forms.DataGridView();
            Nume = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Suma = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DonatorTable = new System.Windows.Forms.DataGridView();
            DonorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DonorAdress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Donate = new System.Windows.Forms.Button();
            CharityLabel = new System.Windows.Forms.Label();
            Amount = new System.Windows.Forms.TextBox();
            SearchBar = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)CazuriCaritabileView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DonatorTable).BeginInit();
            SuspendLayout();
            // 
            // UserLabel
            // 
            UserLabel.AutoSize = true;
            UserLabel.Location = new System.Drawing.Point(12, 11);
            UserLabel.Name = "UserLabel";
            UserLabel.Size = new System.Drawing.Size(41, 20);
            UserLabel.TabIndex = 0;
            UserLabel.Text = "User:";
            // 
            // CazuriCaritabileView
            // 
            CazuriCaritabileView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            CazuriCaritabileView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            CazuriCaritabileView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { Nume, Suma });
            CazuriCaritabileView.Location = new System.Drawing.Point(12, 34);
            CazuriCaritabileView.Name = "CazuriCaritabileView";
            CazuriCaritabileView.RowHeadersWidth = 51;
            CazuriCaritabileView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            CazuriCaritabileView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            CazuriCaritabileView.Size = new System.Drawing.Size(413, 505);
            CazuriCaritabileView.TabIndex = 1;
            CazuriCaritabileView.CellClick += CazuriCaritabileView_CellClick;
            CazuriCaritabileView.CellEndEdit += CazuriCaritabileView_CellEndEdit;
            // 
            // Nume
            // 
            Nume.HeaderText = "Nume";
            Nume.MinimumWidth = 6;
            Nume.Name = "Nume";
            Nume.Width = 180;
            // 
            // Suma
            // 
            Suma.HeaderText = "Suma";
            Suma.MinimumWidth = 6;
            Suma.Name = "Suma";
            Suma.Width = 180;
            // 
            // DonatorTable
            // 
            DonatorTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            DonatorTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DonatorTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { DonorName, DonorAdress, Phone });
            DonatorTable.Location = new System.Drawing.Point(431, 93);
            DonatorTable.Name = "DonatorTable";
            DonatorTable.RowHeadersWidth = 51;
            DonatorTable.Size = new System.Drawing.Size(414, 446);
            DonatorTable.TabIndex = 2;
            DonatorTable.CellClick += DonatorTable_CellClick;
            DonatorTable.CellEndEdit += DonatorTable_CellEndEdit;
            // 
            // DonorName
            // 
            DonorName.HeaderText = "Nume";
            DonorName.MinimumWidth = 6;
            DonorName.Name = "DonorName";
            DonorName.Width = 120;
            // 
            // DonorAdress
            // 
            DonorAdress.HeaderText = "Address";
            DonorAdress.MinimumWidth = 6;
            DonorAdress.Name = "DonorAdress";
            DonorAdress.Width = 121;
            // 
            // Phone
            // 
            Phone.HeaderText = "Telefon";
            Phone.MinimumWidth = 6;
            Phone.Name = "Phone";
            Phone.Width = 120;
            // 
            // Donate
            // 
            Donate.Location = new System.Drawing.Point(751, 30);
            Donate.Name = "Donate";
            Donate.Size = new System.Drawing.Size(94, 33);
            Donate.TabIndex = 3;
            Donate.Text = "Donate";
            Donate.UseVisualStyleBackColor = true;
            Donate.Click += Donate_Click;
            // 
            // CharityLabel
            // 
            CharityLabel.AutoSize = true;
            CharityLabel.Location = new System.Drawing.Point(439, 37);
            CharityLabel.Name = "CharityLabel";
            CharityLabel.Size = new System.Drawing.Size(55, 20);
            CharityLabel.TabIndex = 4;
            CharityLabel.Text = "Charity";
            // 
            // Amount
            // 
            Amount.Location = new System.Drawing.Point(511, 33);
            Amount.Name = "Amount";
            Amount.PlaceholderText = "Enter Amount";
            Amount.Size = new System.Drawing.Size(234, 27);
            Amount.TabIndex = 5;
            // 
            // SearchBar
            // 
            SearchBar.Location = new System.Drawing.Point(431, 64);
            SearchBar.Name = "SearchBar";
            SearchBar.PlaceholderText = "Search";
            SearchBar.Size = new System.Drawing.Size(414, 27);
            SearchBar.TabIndex = 6;
            SearchBar.TextChanged += SearchBar_TextChanged;
            // 
            // LoggedIn
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(853, 551);
            Controls.Add(SearchBar);
            Controls.Add(Amount);
            Controls.Add(CharityLabel);
            Controls.Add(Donate);
            Controls.Add(DonatorTable);
            Controls.Add(CazuriCaritabileView);
            Controls.Add(UserLabel);
            Text = "Logged In";
            FormClosed += LoggedIn_FormClosed;
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