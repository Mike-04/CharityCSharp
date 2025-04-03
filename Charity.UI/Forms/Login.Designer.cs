namespace Charity
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
            LoginButton = new System.Windows.Forms.Button();
            LoginUsername = new System.Windows.Forms.TextBox();
            LoginPassword = new System.Windows.Forms.TextBox();
            RegisterPassword2 = new System.Windows.Forms.TextBox();
            RegisterUsername = new System.Windows.Forms.TextBox();
            RegisterButton = new System.Windows.Forms.Button();
            RegisterPassword1 = new System.Windows.Forms.TextBox();
            ErrorLoginLabel = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // LoginButton
            // 
            LoginButton.Location = new System.Drawing.Point(112, 90);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new System.Drawing.Size(94, 29);
            LoginButton.TabIndex = 0;
            LoginButton.Text = "Login";
            LoginButton.UseVisualStyleBackColor = true;
            LoginButton.Click += LoginButton_Click;
            // 
            // LoginUsername
            // 
            LoginUsername.Location = new System.Drawing.Point(60, 25);
            LoginUsername.Name = "LoginUsername";
            LoginUsername.PlaceholderText = "Username";
            LoginUsername.Size = new System.Drawing.Size(198, 23);
            LoginUsername.TabIndex = 1;
            // 
            // LoginPassword
            // 
            LoginPassword.Location = new System.Drawing.Point(60, 58);
            LoginPassword.Name = "LoginPassword";
            LoginPassword.PasswordChar = '*';
            LoginPassword.PlaceholderText = "Password";
            LoginPassword.Size = new System.Drawing.Size(198, 23);
            LoginPassword.TabIndex = 2;
            // 
            // RegisterPassword2
            // 
            RegisterPassword2.Location = new System.Drawing.Point(56, 198);
            RegisterPassword2.Name = "RegisterPassword2";
            RegisterPassword2.PasswordChar = '*';
            RegisterPassword2.PlaceholderText = "Confirm Password";
            RegisterPassword2.Size = new System.Drawing.Size(198, 23);
            RegisterPassword2.TabIndex = 5;
            // 
            // RegisterUsername
            // 
            RegisterUsername.Location = new System.Drawing.Point(56, 132);
            RegisterUsername.Name = "RegisterUsername";
            RegisterUsername.PlaceholderText = "Create Username";
            RegisterUsername.Size = new System.Drawing.Size(198, 23);
            RegisterUsername.TabIndex = 4;
            RegisterUsername.TextChanged += RegisterUsername_TextChanged;
            // 
            // RegisterButton
            // 
            RegisterButton.Location = new System.Drawing.Point(108, 231);
            RegisterButton.Name = "RegisterButton";
            RegisterButton.Size = new System.Drawing.Size(94, 29);
            RegisterButton.TabIndex = 3;
            RegisterButton.Text = "Register";
            RegisterButton.UseVisualStyleBackColor = true;
            RegisterButton.Click += RegisterButton_Click;
            // 
            // RegisterPassword1
            // 
            RegisterPassword1.Location = new System.Drawing.Point(56, 165);
            RegisterPassword1.Name = "RegisterPassword1";
            RegisterPassword1.PasswordChar = '*';
            RegisterPassword1.PlaceholderText = "Create Password";
            RegisterPassword1.Size = new System.Drawing.Size(198, 23);
            RegisterPassword1.TabIndex = 6;
            // 
            // ErrorLoginLabel
            // 
            ErrorLoginLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            ErrorLoginLabel.AutoSize = true;
            ErrorLoginLabel.Location = new System.Drawing.Point(85, 261);
            ErrorLoginLabel.Name = "ErrorLoginLabel";
            ErrorLoginLabel.Size = new System.Drawing.Size(109, 15);
            ErrorLoginLabel.TabIndex = 7;
            ErrorLoginLabel.Text = "Username available";
            ErrorLoginLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Login
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(311, 290);
            Controls.Add(ErrorLoginLabel);
            Controls.Add(RegisterPassword1);
            Controls.Add(RegisterPassword2);
            Controls.Add(RegisterUsername);
            Controls.Add(RegisterButton);
            Controls.Add(LoginPassword);
            Controls.Add(LoginUsername);
            Controls.Add(LoginButton);
            Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Text = "Login";
            Load += Login_Load;
            Resize += Login_Resize;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button LoginButton;
        private TextBox LoginUsername;
        private TextBox LoginPassword;
        private TextBox RegisterPassword2;
        private TextBox RegisterUsername;
        private Button RegisterButton;
        private TextBox RegisterPassword1;
        private Label ErrorLoginLabel;
    }
}
