using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cw
{
    public partial class Form1 : Form
    {
        private BindingList<Country> countryList;
        private BindingList<City> cityList;
        private BindingList<SportType> sportTypeList;
        private BindingList<Member> memberList;
        private BindingList<Olympiad> olympiadList;
        private BindingList<SportTypeOlympiad> typeOlympiadList;
        public Form1()
        {
            InitializeComponent();
            countryList = new BindingList<Country>();
            cityList = new BindingList<City>();
            sportTypeList = new BindingList<SportType>();
            memberList = new BindingList<Member>();
            olympiadList = new BindingList<Olympiad>();
            typeOlympiadList = new BindingList<SportTypeOlympiad>();
            /*using (OlympiadDBContext db = new OlympiadDBContext())
            {
                db.SportTypes.Add(new SportType() { Name = "type1" });
                db.SportTypes.Add(new SportType() { Name = "type2" });
                db.SportTypes.Add(new SportType() { Name = "type3" });
                db.SportTypes.Add(new SportType() { Name = "type4" });
                db.SportTypes.Add(new SportType() { Name = "type5" });
                db.SaveChanges();
                db.Countries.Add(new Country() { Name = "country1" });
                db.Countries.Add(new Country() { Name = "country2" });
                db.Countries.Add(new Country() { Name = "country3" });
                db.Countries.Add(new Country() { Name = "country4" });
                db.Countries.Add(new Country() { Name = "country5" });
                db.SaveChanges();
                db.Cities.Add(new City() { Name = "city1", CountryId = 1 });
                db.Cities.Add(new City() { Name = "city2", CountryId = 2 });
                db.Cities.Add(new City() { Name = "city3", CountryId = 3 });
                db.Cities.Add(new City() { Name = "city4", CountryId = 4 });
                db.Cities.Add(new City() { Name = "city5", CountryId = 5 });
                db.SaveChanges();
                db.Members.Add(new Member() { Name = "name1", Surname = "surname1", Patronymic = "patr1", CountryId = 1, SportTypeId = 1, BusyPlace = 1, Birthday = new DateTime(1991, 1, 20) });
                db.Members.Add(new Member() { Name = "name2", Surname = "surname2", Patronymic = "patr2", CountryId = 2, SportTypeId = 2, BusyPlace = 2, Birthday = new DateTime(1992, 1, 20) });
                db.Members.Add(new Member() { Name = "name3", Surname = "surname3", Patronymic = "patr3", CountryId = 3, SportTypeId = 3, BusyPlace = 3, Birthday = new DateTime(1993, 1, 20) });
                db.Members.Add(new Member() { Name = "name4", Surname = "surname4", Patronymic = "patr4", CountryId = 4, SportTypeId = 4, BusyPlace = 4, Birthday = new DateTime(1994, 1, 20) });
                db.Members.Add(new Member() { Name = "name5", Surname = "surname5", Patronymic = "patr5", CountryId = 5, SportTypeId = 5, BusyPlace = 5, Birthday = new DateTime(1995, 1, 20) });
                db.SaveChanges();
                db.Olympiads.Add(new Olympiad() { CityId = 1, IsSummerly = true, Year = 2001 });
                db.Olympiads.Add(new Olympiad() { CityId = 2, IsSummerly = true, Year = 2002 });
                db.Olympiads.Add(new Olympiad() { CityId = 3, IsSummerly = false, Year = 2003 });
                db.Olympiads.Add(new Olympiad() { CityId = 4, IsSummerly = true, Year = 2004 });
                db.Olympiads.Add(new Olympiad() { CityId = 5, IsSummerly = false, Year = 2005 });
                db.SaveChanges();
                db.SportTypeOlympiads.Add(new SportTypeOlympiad() { SportTypeId = 1, OlympiadId = 1 });
                db.SportTypeOlympiads.Add(new SportTypeOlympiad() { SportTypeId = 2, OlympiadId = 2 });
                db.SportTypeOlympiads.Add(new SportTypeOlympiad() { SportTypeId = 3, OlympiadId = 3 });
                db.SportTypeOlympiads.Add(new SportTypeOlympiad() { SportTypeId = 4, OlympiadId = 4 });
                db.SportTypeOlympiads.Add(new SportTypeOlympiad() { SportTypeId = 5, OlympiadId = 5 });
                db.SaveChanges();
            }*/
            InfoRefresh();
            comboBox1.Items.AddRange(new object[]
            {
                "Countries",
                "Cities",
                "SportTypes",
                "Members",
                "Olympiads",
                "SportTypeOlympiads"
            });
            comboBox2.Items.AddRange(new object[]
            {
                "Таблица медального зачета",
                "Медалисты разных видов спорта",
                "Страна, собравшая больше всего золотых медалей",
                "Спортсмен, заработавший больше всего золотых медалей",
                "Страна, которая чаще всего была хозяйкой олимпиады",
                "Состав команды определенной страны",
                "Статистика выступления страны"
            });
            comboBox1.SelectedIndex = 0;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedItem != null)
                {
                    using (OlympiadDBContext db = new OlympiadDBContext())
                    {
                        if (comboBox1.SelectedItem.ToString() == "Countries")
                        {
                            dataGridView1.DataSource = countryList;
                            button5.Enabled = false;
                            button5.Visible = false;
                            button6.Enabled = false;
                            button6.Visible = false;
                        }
                        if (comboBox1.SelectedItem.ToString() == "Cities")
                        {
                            dataGridView1.DataSource = cityList;
                            dataGridView1.Columns["Country"].ReadOnly = true;
                            button5.Enabled = false;
                            button5.Visible = false;
                            button6.Enabled = false;
                            button6.Visible = false;
                        }
                        if (comboBox1.SelectedItem.ToString() == "SportTypes")
                        {
                            dataGridView1.DataSource = sportTypeList;
                            button5.Enabled = false;
                            button5.Visible = false;
                            button6.Enabled = false;
                            button6.Visible = false;
                        }
                        if (comboBox1.SelectedItem.ToString() == "Members")
                        {
                            dataGridView1.DataSource = memberList;
                            dataGridView1.Columns["Country"].ReadOnly = true;
                            dataGridView1.Columns["SportType"].ReadOnly = true;
                            button5.Enabled = true;
                            button5.Visible = true;
                            button6.Enabled = true;
                            button6.Visible = true;
                        }
                        if (comboBox1.SelectedItem.ToString() == "Olympiads")
                        {
                            dataGridView1.DataSource = olympiadList;
                            dataGridView1.Columns["City"].ReadOnly = true;
                            button5.Enabled = false;
                            button5.Visible = false;
                            button6.Enabled = false;
                            button6.Visible = false;
                        }
                        if (comboBox1.SelectedItem.ToString() == "SportTypeOlympiads")
                        {
                            dataGridView1.DataSource = typeOlympiadList;
                            dataGridView1.Columns["SportType"].ReadOnly = true;
                            dataGridView1.Columns["Olympiad"].ReadOnly = true;
                            button5.Enabled = false;
                            button5.Visible = false;
                            button6.Enabled = false;
                            button6.Visible = false;
                        }
                        dataGridView1.Columns["Id"].ReadOnly = true;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!");
                InfoRefresh();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (OlympiadDBContext db = new OlympiadDBContext())
                {
                    
                    if (comboBox1.SelectedItem.ToString() == "Countries")
                    {
                        List<Country> countries = countryList.Where(x => x.Id == 0).ToList();
                        foreach (var item in countries)
                        {
                            if (db.Countries.Select(x => x.Name).Contains(item.Name))
                                throw new Exception("This country already exists!");
                            db.Countries.Add(new Country() { Name = item.Name });
                            db.SaveChanges();
                        }
                        InfoRefresh();
                        dataGridView1.DataSource = countryList;
                        dataGridView1.Refresh();
                    }
                    if (comboBox1.SelectedItem.ToString() == "Cities")
                    {
                        List<City> cities = cityList.Where(x => x.Id == 0).ToList();
                        foreach (var item in cities)
                        {
                            if (db.Cities.Where(x => x.Name == item.Name && x.CountryId == item.CountryId).ToList().Count() != 0)
                                throw new Exception("City of this country already exists!");
                            db.Cities.Add(new City() { Name = item.Name, CountryId = item.CountryId });
                            db.SaveChanges();
                        }
                        InfoRefresh();
                        dataGridView1.DataSource = cityList;
                        dataGridView1.Refresh();
                    }
                    if (comboBox1.SelectedItem.ToString() == "SportTypes")
                    {
                        List<SportType> sportTypes = sportTypeList.Where(x => x.Id == 0).ToList();
                        foreach (var item in sportTypes)
                        {
                            if (db.SportTypes.Select(x => x.Name).Contains(item.Name))
                                throw new Exception("This type already exists!");
                            db.SportTypes.Add(new SportType() { Name = item.Name });
                            db.SaveChanges();
                        }
                        InfoRefresh();
                        dataGridView1.DataSource = sportTypeList;
                        dataGridView1.Refresh();
                    }
                    if (comboBox1.SelectedItem.ToString() == "Members")
                    {
                        List<Member> members = memberList.Where(x => x.Id == 0).ToList();
                        foreach (var member in members)
                        {
                            db.Members.Add(new Member() { Name = member.Name, Birthday = member.Birthday, CountryId = member.CountryId, BusyPlace = member.BusyPlace, Patronymic = member.Patronymic, Surname = member.Surname, SportTypeId = member.SportTypeId });
                            db.SaveChanges();
                        }
                        InfoRefresh();
                        dataGridView1.DataSource = memberList;
                        dataGridView1.Refresh();
                    }
                    if (comboBox1.SelectedItem.ToString() == "Olympiads")
                    {
                        List<Olympiad> olympiads = olympiadList.Where(x => x.Id == 0).ToList();
                        foreach (var item in olympiads)
                        {
                            if (db.Olympiads.Select(x => x.Year).Contains(item.Year))
                                throw new Exception("Olympiad already is added!");
                            db.Olympiads.Add(new Olympiad() { CityId = item.CityId, IsSummerly = item.IsSummerly, Year = item.Year });
                            db.SaveChanges();
                        }
                        InfoRefresh();
                        dataGridView1.DataSource = olympiadList;
                        dataGridView1.Refresh();
                    }
                    if (comboBox1.SelectedItem.ToString() == "SportTypeOlympiads")
                    {
                        List<SportTypeOlympiad> sportTypeOlympiads = typeOlympiadList.Where(x => x.Id == 0).ToList();
                        foreach (var item in sportTypeOlympiads)
                        {
                            if (db.SportTypeOlympiads.Where(x => x.SportTypeId == item.SportTypeId && x.OlympiadId == item.OlympiadId).ToList().Count != 0)
                                throw new Exception("Pair is already added!");
                            db.SportTypeOlympiads.Add(new SportTypeOlympiad() { OlympiadId = item.OlympiadId, SportTypeId = item.SportTypeId });
                            db.SaveChanges();
                        }
                        InfoRefresh();
                        dataGridView1.DataSource = typeOlympiadList;
                        dataGridView1.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!");
                InfoRefresh();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (OlympiadDBContext db = new OlympiadDBContext())
                {
                    if (dataGridView1.SelectedCells.Count != 0)
                    {
                        List<int> indexes = new List<int>();
                        for (int i = 0; i < dataGridView1.SelectedCells.Count; i++)
                        {
                            int index = int.Parse(dataGridView1.Rows[dataGridView1.SelectedCells[i].RowIndex].Cells["Id"].Value.ToString());
                            if(!indexes.Contains(index))
                            {
                                indexes.Add(index);
                            }
                        }
                        int a = -1;
                        foreach (int index in indexes)
                        {
                            if (comboBox1.SelectedItem.ToString() == "Countries")
                            {
                                Warning warning = new Warning(db.Countries.Where(x => x.Id == index).First().Name);

                                if (a == 0 || a == -1)
                                {
                                    DialogResult dr = warning.ShowDialog();
                                    if (dr == DialogResult.Yes)
                                        a = 1;
                                    if (dr == DialogResult.Cancel)
                                        a = -1;
                                    if (dr == DialogResult.OK)
                                        a = 0;
                                    if (dr == DialogResult.No)
                                        break;
                                }

                                if (a == 0 || a == 1)
                                {
                                    List<City> c = db.Cities.Where(x => x.CountryId == index).ToList();
                                    foreach (var item in c)
                                    {
                                        List<Olympiad> o = db.Olympiads.Where(x => x.CityId == item.Id).ToList();
                                        foreach (var item2 in o)
                                        {
                                            List<SportTypeOlympiad> sto = db.SportTypeOlympiads.Where(x => x.OlympiadId == item2.Id).ToList();
                                            foreach (var item3 in sto)
                                            {
                                                db.SportTypeOlympiads.Remove(item3);
                                                db.SaveChanges();
                                            }
                                            db.Olympiads.Remove(item2);
                                            db.SaveChanges();
                                        }
                                        db.Cities.Remove(item);
                                        db.SaveChanges();
                                    }
                                    List<Member> m = db.Members.Where(x => x.CountryId == index).ToList();
                                    foreach(var item in m)
                                    {
                                        db.Members.Remove(item);
                                        db.SaveChanges();
                                    }
                                    db.Countries.Remove(db.Countries.Where(x => x.Id == index).First());
                                    db.SaveChanges();
                                    dataGridView1.DataSource = countryList;
                                }
                            }
                            if (comboBox1.SelectedItem.ToString() == "Cities")
                            {
                                Warning warning = new Warning(db.Cities.Where(x => x.Id == index).First().Name);

                                if (a == 0 || a == -1)
                                {
                                    DialogResult dr = warning.ShowDialog();
                                    if (dr == DialogResult.Yes)
                                        a = 1;
                                    if (dr == DialogResult.Cancel)
                                        a = -1;
                                    if (dr == DialogResult.OK)
                                        a = 0;
                                    if (dr == DialogResult.No)
                                        break;
                                }

                                if (a == 0 || a == 1)
                                {
                                    List<Olympiad> o = db.Olympiads.Where(x => x.CityId == index).ToList();
                                    foreach (var item2 in o)
                                    {
                                        List<SportTypeOlympiad> sto = db.SportTypeOlympiads.Where(x => x.OlympiadId == item2.Id).ToList();
                                        foreach (var item3 in sto)
                                        {
                                            db.SportTypeOlympiads.Remove(item3);
                                        }
                                        db.Olympiads.Remove(item2);
                                    }
                                    db.Cities.Remove(db.Cities.Where(x => x.Id == index).First());
                                    db.SaveChanges();
                                    dataGridView1.DataSource = cityList;
                                }
                            }
                            if (comboBox1.SelectedItem.ToString() == "SportTypes")
                            {
                                Warning warning = new Warning(db.SportTypes.Where(x => x.Id == index).First().Name);

                                if (a == 0 || a == -1)
                                {
                                    DialogResult dr = warning.ShowDialog();
                                    if (dr == DialogResult.Yes)
                                        a = 1;
                                    if (dr == DialogResult.Cancel)
                                        a = -1;
                                    if (dr == DialogResult.OK)
                                        a = 0;
                                    if (dr == DialogResult.No)
                                        break;
                                }

                                if (a == 0 || a == 1)
                                {
                                    List <SportTypeOlympiad> sto = db.SportTypeOlympiads.Where(x => x.SportTypeId == index).ToList();
                                    foreach (var item in sto)
                                    {
                                        db.SportTypeOlympiads.Remove(item);
                                    }
                                    List<Member> m = db.Members.Where(x => x.SportTypeId == index).ToList();
                                    foreach(var item in m)
                                    {
                                        db.Members.Remove(item);
                                        db.SaveChanges();
                                    }
                                    db.SportTypes.Remove(db.SportTypes.Where(x => x.Id == index).First());
                                    db.SaveChanges();
                                    dataGridView1.DataSource = sportTypeList;
                                }

                            }
                            if (comboBox1.SelectedItem.ToString() == "Members")
                            {
                                Warning warning = new Warning(db.Members.Where(x => x.Id == index).First().Name);

                                if (a == 0 || a == -1)
                                {
                                    DialogResult dr = warning.ShowDialog();
                                    if (dr == DialogResult.Yes)
                                        a = 1;
                                    if (dr == DialogResult.Cancel)
                                        a = -1;
                                    if (dr == DialogResult.OK)
                                        a = 0;
                                    if (dr == DialogResult.No)
                                        break;
                                }

                                if (a == 0 || a == 1)
                                {
                                    db.Members.Remove(db.Members.First(x => x.Id == index));
                                    db.SaveChanges();
                                    dataGridView1.DataSource = memberList;
                                }
                            }
                            if (comboBox1.SelectedItem.ToString() == "Olympiads")
                            {
                                Warning warning = new Warning("Olympiad of " + db.Olympiads.Where(x => x.Id == index).First().Year.ToString());

                                if (a == 0 || a == -1)
                                {
                                    DialogResult dr = warning.ShowDialog();
                                    if (dr == DialogResult.Yes)
                                        a = 1;
                                    if (dr == DialogResult.Cancel)
                                        a = -1;
                                    if (dr == DialogResult.OK)
                                        a = 0;
                                    if (dr == DialogResult.No)
                                        break;
                                }

                                if (a == 0 || a == 1)
                                {
                                    List<SportTypeOlympiad> sto = db.SportTypeOlympiads.Where(x => x.OlympiadId == index).ToList();
                                    foreach (var item3 in sto)
                                    {
                                        db.SportTypeOlympiads.Remove(item3);
                                        db.SaveChanges();
                                    }
                                    db.Olympiads.Remove(db.Olympiads.Where(x => x.Id == index).First());
                                    db.SaveChanges();
                                    dataGridView1.DataSource = olympiadList;
                                }
                            }
                            if (comboBox1.SelectedItem.ToString() == "SportTypeOlympiads")
                            {
                                db.SportTypeOlympiads.Remove(db.SportTypeOlympiads.Where(x => x.Id == index).First());
                                db.SaveChanges();
                                dataGridView1.DataSource = typeOlympiadList;
                            }
                        }
                    }
                    InfoRefresh();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!");
                InfoRefresh();
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (OlympiadDBContext db = new OlympiadDBContext())
                {
                    int index = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
                    if (comboBox1.SelectedItem.ToString() == "Countries")
                    {
                        Country country = countryList.First(x => x.Id == index);
                        if (db.Countries.Select(x => x.Name).Contains(country.Name))
                            throw new Exception("This country already exists!");
                        db.Countries.First(x => x.Id == index).Name = country.Name;
                    }
                    if (comboBox1.SelectedItem.ToString() == "Cities")
                    {
                        City city = cityList.First(x => x.Id == index);
                        if (db.Cities.Where(x => x.Name == city.Name && x.CountryId == city.CountryId).ToList().Count() != 0)
                            throw new Exception("City of this country already exists!");
                        db.Cities.First(x => x.Id == index).Name = city.Name;
                        db.Cities.First(x => x.Id == index).CountryId = city.CountryId;
                    }
                    if (comboBox1.SelectedItem.ToString() == "SportTypes")
                    {
                        SportType type = sportTypeList.First(x => x.Id == index);
                        if (db.SportTypes.Select(x => x.Name).Contains(type.Name))
                            throw new Exception("This type already exists!");
                        db.SportTypes.First(x => x.Id == index).Name = type.Name;
                    }
                    if (comboBox1.SelectedItem.ToString() == "Members")
                    {
                        Member member = memberList.First(x => x.Id == index);
                        db.Members.First(x => x.Id == index).Name = member.Name;
                        db.Members.First(x => x.Id == index).Surname = member.Surname;
                        db.Members.First(x => x.Id == index).Patronymic = member.Patronymic;
                        db.Members.First(x => x.Id == index).CountryId = member.CountryId;
                        db.Members.First(x => x.Id == index).SportTypeId = member.SportTypeId;
                        db.Members.First(x => x.Id == index).Birthday = member.Birthday;
                        db.Members.First(x => x.Id == index).BusyPlace = member.BusyPlace;
                    }
                    if (comboBox1.SelectedItem.ToString() == "Olympiads")
                    {
                        Olympiad olympiad = olympiadList.First(x => x.Id == index);
                        if (db.Olympiads.Select(x => x.Year).Contains(olympiad.Year))
                            throw new Exception("Olympiad already is added!");
                        db.Olympiads.First(x => x.Id == index).Year = olympiad.Year;
                        db.Olympiads.First(x => x.Id == index).IsSummerly = olympiad.IsSummerly;
                        db.Olympiads.First(x => x.Id == index).CityId = olympiad.CityId;
                    }
                    if (comboBox1.SelectedItem.ToString() == "SportTypeOlympiads")
                    {
                        SportTypeOlympiad typeOlympiad = typeOlympiadList.First(x => x.Id == index);
                        if (db.SportTypeOlympiads.Where(x => x.SportTypeId == typeOlympiad.SportTypeId && x.OlympiadId == typeOlympiad.OlympiadId).ToList().Count != 0)
                            throw new Exception("Pair is already added!");
                        db.SportTypeOlympiads.First(x => x.Id == index).SportTypeId = typeOlympiad.SportTypeId;
                        db.SportTypeOlympiads.First(x => x.Id == index).OlympiadId = typeOlympiad.OlympiadId;
                    }
                    db.SaveChanges();
                    InfoRefresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!");
                InfoRefresh();
            }
        }
        public void InfoRefresh()
        {
            using (OlympiadDBContext db = new OlympiadDBContext())
            {
                countryList.Clear();
                cityList.Clear();
                sportTypeList.Clear();
                memberList.Clear();
                olympiadList.Clear();
                typeOlympiadList.Clear();
                int index = -1;
                for (int i = 0; i < db.Countries.Count(); i++)
                {
                    index = db.Countries.ToList().ElementAt(i).Id;
                    countryList.Add(db.Countries.First(x => x.Id == index));
                }

                for (int i = 0; i < db.Cities.Count(); i++)
                {
                    index = db.Cities.ToList().ElementAt(i).Id;
                    cityList.Add(db.Cities.First(x => x.Id == index));
                }

                for (int i = 0; i < db.SportTypes.Count(); i++)
                {
                    index = db.SportTypes.ToList().ElementAt(i).Id;
                    sportTypeList.Add(db.SportTypes.First(x => x.Id == index));
                }

                for (int i = 0; i < db.Members.Count(); i++)
                {
                    index = db.Members.ToList().ElementAt(i).Id;
                    memberList.Add(db.Members.First(x => x.Id == index));
                }

                for (int i = 0; i < db.Olympiads.Count(); i++)
                {
                    index = db.Olympiads.ToList().ElementAt(i).Id;
                    olympiadList.Add(db.Olympiads.First(x => x.Id == index));
                }

                for (int i = 0; i < db.SportTypeOlympiads.Count(); i++)
                {
                    index = db.SportTypeOlympiads.ToList().ElementAt(i).Id;
                    typeOlympiadList.Add(db.SportTypeOlympiads.First(x => x.Id == index));
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                InfoRefresh();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedCells.Count != 0)
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (OlympiadDBContext db = new OlympiadDBContext())
                        {
                            int index = int.Parse(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["Id"].Value.ToString());
                            db.Members.First(x => x.Id == index).Image = CreateCopy(openFileDialog.FileName);
                            db.SaveChanges();
                            InfoRefresh();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!");
                InfoRefresh();
            }
        }

        private byte[] CreateCopy(string fileName)
        {
            Image img = Image.FromFile(fileName);
            int maxWidth = 300, maxHeight = 300;
            double ratioX = (double)maxWidth / img.Width;
            double ratioY = (double)maxHeight / img.Height;
            double ratio = Math.Min(ratioX, ratioY);
            int newWidth = (int)(img.Width * ratio);
            int newHeight = (int)(img.Height * ratio);
            Image mi = new Bitmap(newWidth, newHeight);
            Graphics g = Graphics.FromImage(mi);
            g.DrawImage(img, 0, 0, newWidth, newHeight);
            MemoryStream ms = new MemoryStream();
            mi.Save(ms, ImageFormat.Jpeg);
            ms.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            BinaryReader br = new BinaryReader(ms);
            byte[] buf = br.ReadBytes((int)ms.Length);
            return buf;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedCells.Count != 0)
                {
                    using (OlympiadDBContext db = new OlympiadDBContext())
                    {
                        int index = int.Parse(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["Id"].Value.ToString());
                        if (db.Members.First(x => x.Id == index).Image == null)
                            return;
                        Picture picture = new Picture(db.Members.First(x => x.Id == index).Image);
                        picture.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!");
                InfoRefresh();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox2.SelectedIndex == 0)
            {
                View1 view = new View1();
                view.Show();
            }
        }
    }
}
