using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cw
{
    public partial class View1 : Form
    {
        public View1()
        {
            InitializeComponent();
            radioButton1.Checked = true;
            using(OlympiadDBContext db = new OlympiadDBContext())
            {
                comboBox1.Items.AddRange(db.Olympiads.Select(x => x.Year.ToString()).ToArray());
                comboBox1.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Visible = radioButton1.Checked;
            comboBox1.Enabled = radioButton1.Checked;

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ResultForYear(int year)
        {
            try
            {
                using (OlympiadDBContext db = new OlympiadDBContext())
                {
                    Olympiad olympiad = db.Olympiads.First(x => x.Year == year);
                    List<int> indexes = db.SportTypes.Join(db.SportTypeOlympiads.ToList(), x1 => x1.Id, y1 => y1.SportTypeId, (x1, y1) => new { y1.SportTypeId, y1.OlympiadId }).Where(x => x.OlympiadId == olympiad.Id).Select(x => x.SportTypeId).ToList();
                    List<SportType> sportTypes = db.SportTypes.Where(x => indexes.Contains(x.Id)).ToList();

                    var res = db.Olympiads.Join(db.SportTypeOlympiads, x => x.Id, y => y.OlympiadId, (x, y) => new { x.Year, y.SportTypeId, y.OlympiadId }).Where(x => x.OlympiadId == olympiad.Id).Join(db.SportTypes, x => x.SportTypeId, y => y.Id, (x, y) => new { x.SportTypeId, y.Name }).Join(db.Members, x => x.SportTypeId, y => y.SportTypeId, (x, y) => new { sportType = x.Name, y.BusyPlace, y.Name, y.Surname, y.Patronymic, y.CountryId }).Join(db.Countries, x => x.CountryId, y=>y.Id, (x,y) => new { sportType = x.Name, x.BusyPlace, x.Name, x.Surname, x.Patronymic, country = y.Name });

                    db.Members.GroupBy(x => x.Country);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                return;
            }
        }
    }
}
