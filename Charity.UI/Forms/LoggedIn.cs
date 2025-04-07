using Charity.Domain;
using Charity.Service;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private List<CazCaritabil> cazuriCache;
        private List<Donator> donatoriCache;

        public LoggedIn(IAppService service)
        {
            this.service = service;
            cazuriCache = new List<CazCaritabil>();
            donatoriCache = new List<Donator>();
        }

        public void SetUser(User user)
        {
            this.loggedUser = user;
            InitializeComponent();
        }

        public async Task ReCacheAsync()
        {
            cazuriCache = await Task.Run(() => service.GetAllCazuri().ToList());
            donatoriCache = await Task.Run(() => service.GetDonators("").ToList());
            //print the cache
            Console.WriteLine("Cazuri cache:");
            foreach (CazCaritabil caz in cazuriCache)
            {
                Console.WriteLine(caz.Nume + " " + caz.SumaAdunata);
            }
            Console.WriteLine("Donatori cache:");
            foreach (Donator donor in donatoriCache)
            {
                Console.WriteLine(donor.Nume + " " + donor.Adresa + " " + donor.NumarTelefon);
            }
        }

        private async void LoggedIn_Load(object sender, EventArgs e)
        {
            await ReCacheAsync();
            UserLabel.Text = "User: " + loggedUser.Username;
            PopulateCaseTable();
            PopulateDonorTable();
        }

        async void IObserver.Update()
        {
            Console.WriteLine("Update called");
            await ReCacheAsync();
            PopulateCaseTable();
            PopulateDonorTable();
        }

        private void PopulateCaseTable()
        {
            CazuriCaritabileView.Rows.Clear();
            foreach (CazCaritabil caz in cazuriCache)
            {
                CazuriCaritabileView.Rows.Add(caz.Nume, caz.SumaAdunata);
            }
        }

        private void PopulateDonorTable()
        {
            DonatorTable.Rows.Clear();
            foreach (Donator donor in donatoriCache)
            {
                DonatorTable.Rows.Add(donor.Nume, donor.Adresa, donor.NumarTelefon);
            }
        }

        private async void SearchBar_TextChanged(object sender, EventArgs e)
        {
            await Task.Delay(250);
            DonatorTable.Rows.Clear();
            donatoriCache = await Task.Run(() => service.GetDonators(SearchBar.Text).ToList());
            foreach (Donator donor in donatoriCache)
            {
                DonatorTable.Rows.Add(donor.Nume, donor.Adresa, donor.NumarTelefon);
            }
        }

        private async void DonatorTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = DonatorTable.Rows[e.RowIndex];
            if (e.RowIndex < donatoriCache.Count)
            {
                Donator donor = donatoriCache[e.RowIndex];
                await Task.Run(() => service.UpdateDonator(donor.Id, row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString()));
            }
            else
            {
                if (row.Cells[0].Value == null || row.Cells[1].Value == null || row.Cells[2].Value == null)
                {
                    return;
                }
                else
                {
                    await Task.Run(() => service.AddDonator(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString()));
                }
            }
        }

        private async void CazuriCaritabileView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = CazuriCaritabileView.Rows[e.RowIndex];
            if (e.RowIndex < cazuriCache.Count)
            {
                CazCaritabil caz = cazuriCache[e.RowIndex];
                await Task.Run(() => service.UpdateCazCaritabil(caz.Id, row.Cells[0].Value.ToString(), double.Parse(row.Cells[1].Value.ToString())));
            }
            else
            {
                if (row.Cells[0].Value == null || row.Cells[1].Value == null)
                {
                    return;
                }
                else
                {
                    await Task.Run(() => service.AddCazCaritabil(row.Cells[0].Value.ToString(), double.Parse(row.Cells[1].Value.ToString())));
                }
            }
        }

        private void CazuriCaritabileView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= cazuriCache.Count)
            {
                return;
            }
            selectedCase = cazuriCache[e.RowIndex];
        }

        private void DonatorTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= donatoriCache.Count)
            {
                return;
            }
            selectedDonor = donatoriCache[e.RowIndex];
        }

        private async void Donate_Click(object sender, EventArgs e)
        {
            if (selectedCase != null && selectedDonor != null)
            {
                MessageBox.Show("Donation made by " + selectedDonor.Nume + " to " + selectedCase.Nume);
                await Task.Run(() => service.addDonation(selectedDonor, selectedCase, double.Parse(Amount.Text)));
                PopulateCaseTable();
            }
        }

        private void LoggedIn_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Handle form closed event if necessary
        }
    }
}