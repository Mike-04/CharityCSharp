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

namespace Charity
{

    public partial class LoggedIn : Form
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

        private void CazuriCaritabileView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // //get the edited row
            // DataGridViewRow row = CazuriCaritabileView.Rows[e.RowIndex];
            // //check if the caz is in the list or is a new one
            // if (e.RowIndex < service.CazCaritabilService.GetAll().Count)
            // {
            //     //update the caz
            //     CazCaritabil caz = service.CazCaritabilService.GetAll()[e.RowIndex];
            //     service.CazCaritabilService.Update(caz.Id, row.Cells[0].Value.ToString(), double.Parse(row.Cells[1].Value.ToString()));
            // }
            // else
            // {
            //     //check if all fields are filled
            //     if (row.Cells[0].Value == null || row.Cells[1].Value == null)
            //     {
            //         return;
            //     }
            //     else
            //         service.CazCaritabilService.Add(row.Cells[0].Value.ToString(), double.Parse(row.Cells[1].Value.ToString()));
            // }

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
            // DonatorTable.Rows.Clear();
            // //populate the DonorName, DonorAdress and Phone columns
            // foreach (Donator donor in service.DonatorService.GetAll())
            // {
            //     DonatorTable.Rows.Add(donor.Nume, donor.Adresa, donor.NumarTelefon);
            // }
        }

        private void SearchBar_TextChanged(object sender, EventArgs e)
        {
            // //use findbyname function   
            // DonatorTable.Rows.Clear();
            // foreach (Donator donor in service.DonatorService.FindByName(SearchBar.Text))
            // {
            //     DonatorTable.Rows.Add(donor.Nume, donor.Adresa, donor.NumarTelefon);
            // }

        }

        private void DonatorTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // //get the edited row
            // DataGridViewRow row = DonatorTable.Rows[e.RowIndex];
            // //check if the donor is in the list or is a new one
            // if (e.RowIndex < service.DonatorService.GetAll().Count)
            // {
            //     //update the donor
            //     Donator donor = service.DonatorService.GetAll()[e.RowIndex];
            //     service.DonatorService.Update(donor.Id, row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString());
            // }
            // else
            // {
            //     //check if all fields are filled
            //     if (row.Cells[0].Value == null || row.Cells[1].Value == null || row.Cells[2].Value == null)
            //     {
            //         return;
            //     }
            //     else
            //         service.DonatorService.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString());
            // }
        }

        private void CazuriCaritabileView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // // check if the selected caz has all the fields filled
            // if (e.RowIndex >= service.CazCaritabilService.GetAll().Count)
            // {
            //     return;
            // }
            // selectedCase = service.CazCaritabilService.GetAll()[e.RowIndex];
        }

        private void DonatorTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // // check if the selected donor has all the fields filled
            // if (e.RowIndex >= service.DonatorService.GetAll().Count)
            // {
            //     return;
            // }
            // selectedDonor = service.DonatorService.GetAll()[e.RowIndex];
        }

        private void Donate_Click(object sender, EventArgs e)
        {
        //     //open a message box with the selected caz and donor
        //     if (selectedCase != null && selectedDonor != null)
        //     {
        //         MessageBox.Show("Donation made by " + selectedDonor.Nume + " to " + selectedCase.Nume);
        //         //create a new Donatie
        //         service.addDonation(selectedDonor, selectedCase, double.Parse(Amount.Text));
        //         //update the tables
        //         PopulateCaseTable();
        //     }
        }
    }
}
