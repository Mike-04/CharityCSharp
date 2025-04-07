using Charity.Domain;
using Charity.Service;

namespace Charity
{
    public partial class Login : Form
    {
        private IAppService service;
        public Login(IAppService service)
        {
            this.service = service;
            InitializeComponent();
        }

        private void CenterAllControlsHorizontally()
        {
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Left = (this.ClientSize.Width - ctrl.Width) / 2;
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            CenterAllControlsHorizontally();
        }

        private void RegisterUsername_TextChanged(object sender, EventArgs e)
        {
            //check if username is already taken
            // if (service.UserService != null)
            // {
            //     if (service.UserService.FindByUsername(RegisterUsername.Text) != null)
            //     {
            //         ErrorLoginLabel.Text = "Username already taken";
            //     }
            //     else
            //     {
            //         ErrorLoginLabel.Text = "Username available";
            //     }
            // }
            CenterAllControlsHorizontally();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
                try
                {
                    LoggedIn loggedin = new LoggedIn(service);
                    User user = service.Login(LoginUsername.Text, LoginPassword.Text,loggedin);
                    loggedin.SetUser(user);
                    loggedin.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {

        }

        private void Login_Resize(object sender, EventArgs e)
        {
            CenterAllControlsHorizontally();
        }
    }
}
