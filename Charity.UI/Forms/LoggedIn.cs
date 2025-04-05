using Charity.Domain;
using Charity.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Charity.Service.Observer;

namespace Charity
{

    public partial class LoggedIn : Form, IObserver
    {
        private User loggedUser;
        private IAppService service;
        private CazCaritabil selectedCase;
        private Donator selectedDonor;

        public LoggedIn(User user, IAppService service)
        {
            loggedUser = user;
            this.service = service;
            InitializeComponent();
        }

        private void LoggedIn_Load(object sender, EventArgs e)
        {
            UserLabel.Text = "User: " + loggedUser.Username;
            //populate the Name and Suma columns
            PopulateCaseTable();
            PopulateDonorTable();

        }
        
        void IObserver.Update()
        {
            //update the tables
            PopulateCaseTable();
            PopulateDonorTable();
        }

        private void CazuriCaritabileView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //get the edited row
            DataGridViewRow row = CazuriCaritabileView.Rows[e.RowIndex];
            //check if the caz is in the list or is a new one
            if (e.RowIndex < service.GetAllCazuri().ToList().Count)
            {
                //update the caz
                CazCaritabil caz = service.GetAllCazuri().ToList()[e.RowIndex];
                service.UpdateCazCaritabil(caz.Id, row.Cells[0].Value.ToString(), double.Parse(row.Cells[1].Value.ToString()));
            }
            else
            {
                //check if all fields are filled
                if (row.Cells[0].Value == null || row.Cells[1].Value == null)
                {
                    return;
                }
                else
                    service.AddCazCaritabil(row.Cells[0].Value.ToString(), double.Parse(row.Cells[1].Value.ToString()));
            }

        }

        private void PopulateCaseTable()
        {
            //clear the table
            CazuriCaritabileView.Rows.Clear();
            //populate the Name and Suma columns
            foreach (CazCaritabil caz in service.GetAllCazuri())
            {
                CazuriCaritabileView.Rows.Add(caz.Nume, caz.SumaAdunata);
            }
        }

        private void PopulateDonorTable()
        {
            DonatorTable.Rows.Clear();
            //populate the DonorName, DonorAdress and Phone columns
            foreach (Donator donor in service.GetDonators(""))
            {
                DonatorTable.Rows.Add(donor.Nume, donor.Adresa, donor.NumarTelefon);
            }
        }

        private async void SearchBar_TextChanged(object sender, EventArgs e)
            {
                // Add a delay of 500 milliseconds
                await Task.Delay(250);
            
                DonatorTable.Rows.Clear();
                foreach (Donator donor in service.GetDonators(SearchBar.Text))
                {
                    DonatorTable.Rows.Add(donor.Nume, donor.Adresa, donor.NumarTelefon);
                }
            }

        private void DonatorTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //get the edited row
            DataGridViewRow row = DonatorTable.Rows[e.RowIndex];
            //check if the donor is in the list or is a new one
            if (e.RowIndex < service.GetDonators(SearchBar.Text).ToList().Count)
            {
                //update the donor
                Donator donor = service.GetDonators(SearchBar.Text).ToList()[e.RowIndex];
                service.UpdateDonator(donor.Id, row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString());
            }
            else
            {
                //check if all fields are filled
                if (row.Cells[0].Value == null || row.Cells[1].Value == null || row.Cells[2].Value == null)
                {
                    return;
                }
                else
                    service.AddDonator(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString());
            }
        }

        private void CazuriCaritabileView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // check if the selected caz has all the fields filled
            if (e.RowIndex >= service.GetAllCazuri().ToList().Count)
            {
                return;
            }
            selectedCase = service.GetAllCazuri().ToList()[e.RowIndex];
        }

        private void DonatorTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // check if the selected donor has all the fields filled
            if (e.RowIndex >= service.GetDonators(SearchBar.Text).ToList().Count)
            {
                return;
            }
            selectedDonor = service.GetDonators(SearchBar.Text).ToList()[e.RowIndex];
        }

        private void Donate_Click(object sender, EventArgs e)
        {
            //open a message box with the selected caz and donor
            if (selectedCase != null && selectedDonor != null)
            {
                MessageBox.Show("Donation made by " + selectedDonor.Nume + " to " + selectedCase.Nume);
                //create a new Donatie
                service.addDonation(selectedDonor, selectedCase, double.Parse(Amount.Text));
                //update the tables
                PopulateCaseTable();
            }
        }
    }
}
