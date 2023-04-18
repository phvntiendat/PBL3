using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PBL3.DTO;
using Excel = Microsoft.Office.Interop.Excel;


namespace PBL3.BLL
{
    internal class Provider
    {
        private static Provider _Instance;
        public static Provider Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new Provider();
                }
                return _Instance;
            }
            private set { }
        }
        private Provider()
        { }
        //--------------Account------------------
        public PBL3Entities database = new PBL3Entities();
        public Account currentUser = new Account();
        public Boolean isSignIn()
        {
            return (currentUser == null) ? false : true;
        }
        public Boolean isAdmin()
        {
            if (currentUser.Permission)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void fixData()
        {
            foreach (Citizen i in GetAll_Citizen())
            {
                string _newVCN = "";
                foreach (Registration j in GetRegistration_By_CMND(i.CMND_CCCD))
                {
                    if (j.Dose == 1)
                    {
                        _newVCN = j.vaccineName;
                    }
                }
                foreach (Registration j in GetRegistration_By_CMND(i.CMND_CCCD))
                {
                    if (j.Dose != 1)
                    {
                        j.vaccineName = _newVCN;
                    }
                }
            }
            database.SaveChanges();
        }
        //---------------Sync Changes-----------
        public void SyncFullNameFromCitizen()
        {
            foreach (Citizen i in GetAll_Citizen())
            {
                var x = database.Accounts.Find(i.CMND_CCCD);
                if (x.Permission == false) x.Fullname = i.fullName;
            }
            database.SaveChanges();
        }
        public async void SyncRegistration()
        {
            foreach (Registration i in database.Registrations)
            {
                if (i.State)
                {
                    database.Citizens.Find(i.CMND_CCCD).vaccination = i.Dose;
                }
            }
            await database.SaveChangesAsync();
        }
        //--------------Account Data------------------
        private List<Account> GettAll_Accounts()
        {
            return database.Accounts.ToList();
        }
        private List<Account> GetAccounts_By_Username(string _username)
        {
            return database.Accounts.Where(p => p.Username.Contains(_username)).ToList();
        }

        //private List<Account> GetAccounts_By_Permission(string _permission)
        //{
        //    switch (_permission)
        //    {
        //        case "All":
        //            return database.Accounts.ToList();
        //        default:
        //            if (_permission == "Admin")
        //            {
        //                return database.Accounts.Where(p => p.Permission == true).ToList();
        //            }
        //            else
        //            {
        //                return database.Accounts.Where(p => p.Permission == false).ToList();
        //            }
        //    }
        //}
        //--------------Citizen Data-------------
        public List<Citizen> GetAll_Citizen(string txt = "")
        {
            return database.Citizens.Where(p => p.fullName.Contains(txt)).ToList();
        }
        public Citizen GetCitizen_By_CMND(string CMND_CCCD)
        {
            return database.Citizens.Where(p => p.CMND_CCCD == CMND_CCCD).FirstOrDefault();
        }

        public bool CheckDuplicateCMND(string CMND_CCCD)
        {
            foreach (string i in database.Citizens.Select(p => p.CMND_CCCD).ToList())
            {
                if (CMND_CCCD == i)
                {
                    return false;
                }
            }
            return true;
        }
        public void ExecuteAdd(Citizen s, string CMND_CCCD)
        {
            database.Citizens.Add(s);
            Account newAccount = new Account(s.CMND_CCCD, s.fullName, s.CMND_CCCD, s.CMND_CCCD, false);
            database.Accounts.Add(newAccount);
            database.SaveChangesAsync();
        }
        public void ExecuteEdit(Citizen s, string CMND_CCCD)
        {
            var x = database.Citizens.Where(p => p.CMND_CCCD == CMND_CCCD).FirstOrDefault();
            x.CMND_CCCD = s.CMND_CCCD;
            x.fullName = s.fullName;
            x.phone = s.phone;
            x.address = s.address;
            x.gender = s.gender;
            x.birth = s.birth;
            x.vaccination = s.vaccination;
            //Task.WaitAll();
            SyncFullNameFromCitizen();
        }
        public void DeleteRegistration_BLL(string regisID)
        {
            Registration r = database.Registrations.Find(regisID);
            if (r.State)
            {
                if (r.Citizen.vaccination >= 1)
                {
                    //database.Citizens.Find(r.CMND_CCCD).vaccination--;
                    var x = database.Citizens.Where(p => p.CMND_CCCD == r.CMND_CCCD).FirstOrDefault();
                    x.vaccination--;
                    database.SaveChangesAsync();
                }
            }
            database.Registrations.Remove(r);
            database.SaveChangesAsync();
        }
        public void DeleteCitizen_BLL(string CMND)
        {
            try
            {
                var x = database.Citizens.Find(CMND);
                //database.Citizens.Remove(x);
                x.fullName = "";
                x.phone = "";
                x.address = "";
                x.gender = true;
                x.birth = DateTime.Now;
                x.vaccination = 0;
                database.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public void DeleteAccount_BLL(string CMND)
        {
            try
            {

                database.Accounts.Remove(database.Accounts.Find(CMND));
                database.Citizens.Remove(database.Citizens.Find(CMND));
                database.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        // Citizen Data Alternative View
        public List<CitizenDataAltView> CitizenFilteredViews(string txt, string Address, string Does)
        {
            if (Address == "All")
            {
                Address = "";
            }
            if (Does == "All")
            {
                Does = "";
            }
            List<CitizenDataAltView> data = new List<CitizenDataAltView>();
            foreach (Citizen i in GetAll_Citizen(txt))
            {
                if (i.fullName != "" && i.address.Contains(Address) && i.vaccination.ToString().Contains(Does))
                {
                    string convertedGender = "Male";
                    if (i.gender == false)
                    {
                        convertedGender = "Female";
                    }
                    data.Add(new CitizenDataAltView
                    {
                        fullName = i.fullName,
                        CMND_CCCD = i.CMND_CCCD,
                        gender = convertedGender,
                        birth = i.birth.ToString("MM/dd/yyyy"),
                        phone = i.phone,
                        address = i.address,
                        vaccination = i.vaccination.ToString() + " Doses Injected"
                    });
                }
            }
            return data;
        }
        //Registration Alternative View
        public List<Registration> RegistrationFilterViews(string _cmnd, string _vaccineName, string _state, string _dose)
        {
            int k = 0;
            if (_cmnd == "All") _cmnd = "";
            if (_vaccineName == "All") _vaccineName = "";
            if (_state == "All") _state = "";
            if (_dose == "All") { } else { k = Convert.ToInt32(_dose); }

            List<Registration> data = new List<Registration>();
            bool vaccinationState;
            if (_state == "")
            {
                foreach (Registration i in GetAll_Registration())
                {
                    if (i.CMND_CCCD.Contains(_cmnd) && i.vaccineName.Contains(_vaccineName))
                    {
                        data.Add(new Registration(i.regisId, i.CMND_CCCD, i.Dose, i.vaccineName, i.regisDay, i.State));
                    }
                    if (_dose != "All")
                    {
                        if (i.Dose == k) data.Add(i);

                    }
                }
            }
            else
            {
                if (_state == "Vaccinated")
                {
                    vaccinationState = true;
                }
                else if (_state == "Not vaccinated")
                {
                    vaccinationState = false;
                }
                else vaccinationState = false;
                foreach (Registration i in GetAll_Registration())
                {
                    if (i.CMND_CCCD.Contains(_cmnd) && i.vaccineName.Contains(_vaccineName) && i.State == vaccinationState)
                    {
                        data.Add(new Registration(i.regisId, i.CMND_CCCD, i.Dose, i.vaccineName, i.regisDay, i.State));
                    }
                    if (_dose != "All")
                    {
                        if (i.Dose == k) data.Add(i);

                    }
                }
            }

            data.Distinct();
            return data;
        }
        //Account Data Alternative View
        public List<AccountDataAltView> AccountFilteredViews(string _permission, string _username)
        {
            List<AccountDataAltView> data = new List<AccountDataAltView>();
            if (_permission == "All")
            {
                _permission = "";
            }
            if (_permission == "Admin")
            {
                _permission = "True";
            }
            if (_permission == "User")
            {
                _permission = "False";
            }

            foreach (Account i in GettAll_Accounts())
            {
                if (i.Permission.ToString().Contains(_permission) && i.Username.Contains(_username))
                {
                    data.Add(new AccountDataAltView
                    {
                        CMND_CCCD = i.CMND_CCCD,
                        Username = i.Username,
                        Password = i.Password,
                        Fullname = i.Fullname,
                        Permission = i.Permission
                    });
                }
            }
            //if (_username == "")
            //{

            //}
            //else
            //{
            //    foreach (AccountDataAltView i in data)
            //    {
            //        if (i.Username != _username)
            //        {
            //            data.Remove(i);
            //        }
            //    }
            //}
            return data;
        }
        // CBB Filler
        public List<String> GetCBB_AllVaccineName()
        {
            return database.Vaccines.Select(p => p.vaccineName).ToList();
        }
        public List<String> GetCBB_VaccineName()
        {
            return database.Registrations.Select(p => p.vaccineName).Distinct().ToList();
        }
        public List<String> GetCBB_VaccinationState()
        {
            List<string> stateList = new List<string>();
            foreach (var p in database.Registrations.Select(p => p.State))
            {
                if (p)
                {
                    stateList.Add("Vaccinated");
                }
                else
                {
                    stateList.Add("Not vaccinated");
                }
            }
            return stateList.Distinct().ToList();
        }
        public List<string> GetCBB_Permission()
        {
            List<string> roleList = new List<string>();
            foreach (var p in database.Accounts.Select(p => p.Permission))
            {
                if (p)
                {
                    roleList.Add("Admin");
                }
                else
                {
                    roleList.Add("User");
                }
            }
            return roleList.Distinct().ToList();
        }
        public List<string> GetCBB_Address()
        {
            return database.Citizens.Select(p => p.address).Distinct().ToList();
        }
        public List<string> GetCBB_Does()
        {
            return database.Citizens.Select(p => p.vaccination.ToString()).Distinct().ToList();
        }
        public List<Registration> Sort_BLL(string sortType, bool sortDirection, string txtSearch, string vaccineName, string vaccinationState, string dose)
        {
            List<Registration> rawdata = RegistrationFilterViews(txtSearch, vaccineName, vaccinationState, dose);
            switch (sortType)
            {
                case "Registration ID":
                    rawdata = sortDirection ? rawdata.OrderBy(p => p.regisId).ToList() : rawdata.OrderByDescending(p => p.regisId).ToList();
                    break;
                case "CMND / CCCD":
                    rawdata = sortDirection ? rawdata.OrderBy(p => p.CMND_CCCD).ToList() : rawdata.OrderByDescending(p => p.CMND_CCCD).ToList();
                    break;
                case "Does":
                    rawdata = sortDirection ? rawdata.OrderBy(p => p.Dose).ToList() : rawdata.OrderByDescending(p => p.Dose).ToList();
                    break;
                case "Registration Date":
                    rawdata = sortDirection ? rawdata.OrderBy(p => p.regisDay).ToList() : rawdata.OrderByDescending(p => p.regisDay).ToList();
                    break;
                case "State":
                    rawdata = sortDirection ? rawdata.OrderBy(p => p.State).ToList() : rawdata.OrderByDescending(p => p.State).ToList();
                    break;
            }
            return rawdata;
        }
        public List<CitizenDataAltView> Sort_BLL(string txt, string Address, string Does, int SortIndex, bool SortingDirection)
        {
            List<Citizen> data = new List<Citizen>();
            if (SortingDirection == true)
            {
                if (SortIndex == 0)
                {
                    var x = database.Citizens.Where(p => p.fullName.Contains(txt))
                        .OrderBy(p => p.fullName);
                    data = x.ToList();
                }
                if (SortIndex == 1)
                {
                    var x = database.Citizens.Where(p => p.fullName.Contains(txt)).ToList()
                        .OrderBy(o => int.Parse(o.CMND_CCCD));
                    data = x.ToList();


                }
                if (SortIndex == 2)
                {
                    var x = database.Citizens.Where(p => p.fullName.Contains(txt))
                        .OrderBy(p => p.birth);
                    data = x.ToList();
                }
                if (SortIndex == 3)
                {
                    var x = database.Citizens.Where(p => p.fullName.Contains(txt))
                        .OrderBy(p => p.vaccination);
                    data = x.ToList();
                }
            }
            else
            {
                if (SortIndex == 0)
                {
                    var x = database.Citizens.Where(p => p.fullName.Contains(txt))
                        .OrderByDescending(p => p.fullName);
                    data = x.ToList();
                }
                if (SortIndex == 1)
                {
                    var x = database.Citizens.Where(p => p.fullName.Contains(txt)).ToList()
                        .OrderByDescending(o => int.Parse(o.CMND_CCCD));
                    data = x.ToList();
                }
                if (SortIndex == 2)
                {
                    var x = database.Citizens.Where(p => p.fullName.Contains(txt))
                        .OrderByDescending(p => p.birth);
                    data = x.ToList();
                }
                if (SortIndex == 3)
                {
                    var x = database.Citizens.Where(p => p.fullName.Contains(txt))
                        .OrderByDescending(p => p.vaccination);
                    data = x.ToList();
                }
            }

            List<CitizenDataAltView> data2 = new List<CitizenDataAltView>();
            if (Address == "All")
            {
                Address = "";
            }
            if (Does == "All")
            {
                Does = "";
            }
            foreach (Citizen i in data)
            {
                if (i.fullName != "" && i.address.Contains(Address) && i.vaccination.ToString().Contains(Does))
                {
                    string convertedGender = "Male";
                    if (i.gender == false)
                    {
                        convertedGender = "Female";
                    }
                    data2.Add(new CitizenDataAltView
                    {
                        fullName = i.fullName,
                        CMND_CCCD = i.CMND_CCCD,
                        gender = convertedGender,
                        birth = i.birth.ToString("MM/dd/yyyy"),
                        phone = i.phone,
                        address = i.address,
                        vaccination = i.vaccination.ToString() + " Doses Injected"
                    });
                }
            }
            return data2;
        }



        //--------------Vaccine Data-------------
        public List<Vaccine> GetAll_Vaccine(string txt = "")
        {
            return database.Vaccines.Where(p => p.vaccineName.Contains(txt)).ToList();
        }
        public Vaccine GetVaccine_By_Name(string name)
        {
            return database.Vaccines.Where(p => p.vaccineName == name).FirstOrDefault();
        }
        public void ExecuteAdd(Vaccine v, string name)
        {
            database.Vaccines.Add(v);
            database.SaveChanges();
        }
        public void ExecuteEdit(Vaccine v, string name)
        {
            var x = database.Vaccines.Where(p => p.vaccineName == name).FirstOrDefault();
            x.vaccineName = v.vaccineName;
            x.quantity = v.quantity;
            database.SaveChanges();
        }

        public bool CheckAddEdit_Vaccine(string name)
        {
            foreach (Vaccine i in database.Vaccines.ToList())
            {
                if (name == i.vaccineName)
                {
                    return false;
                }
            }
            return true;
        }
        public void Delete_BLL_Vaccine(string name)
        {
            try
            {
                var x = database.Vaccines.Find(name);
                database.Vaccines.Remove(x);
                database.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        //---------------------------------------
        // Vaccine Data Alternative View
        public List<VaccineDataAltView> VaccineFilteredViews(string txt, string search)
        {
            if (txt == "All")
            {
                txt = "";
            }
            List<VaccineDataAltView> data = new List<VaccineDataAltView>();
            foreach (Vaccine i in GetAll_Vaccine(txt))
            {
                if (i.vaccineName.Contains(txt) && i.quantity.ToString().Contains(search))
                {
                    data.Add(new VaccineDataAltView
                    {
                        vaccineName = i.vaccineName,
                        quantity = i.quantity
                    });
                }
            }
            return data;
        }
        // CBB Filler
        public List<string> GetCBB_Filter()
        {
            return database.Vaccines.Select(p => p.vaccineName).Distinct().ToList();
        }
        //Vaccine Data Alternative views
        public List<VaccineDataAltView> Sort_BLL(string txt, int SortIndex, bool SortingDirection)
        {
            List<Vaccine> data = new List<Vaccine>();

            if (SortingDirection == true)
            {
                if (SortIndex == 0)
                {
                    var x = database.Vaccines.Where(p => p.vaccineName.Contains(txt))
                        .OrderBy(p => p.vaccineName);
                    data = x.ToList();
                }
                if (SortIndex == 1)
                {
                    var x = database.Vaccines.Where(p => p.vaccineName.Contains(txt))
                        .OrderBy(p => p.quantity);
                    data = x.ToList();
                }
            }
            else
            {
                if (SortIndex == 0)
                {
                    var x = database.Vaccines.Where(p => p.vaccineName.Contains(txt))
                        .OrderByDescending(p => p.vaccineName);
                    data = x.ToList();
                }
                if (SortIndex == 1)
                {
                    var x = database.Vaccines.Where(p => p.vaccineName.Contains(txt))
                        .OrderByDescending(p => p.quantity);
                    data = x.ToList();
                }
            }
            List<VaccineDataAltView> data2 = new List<VaccineDataAltView>();
            foreach (Vaccine i in data)
            {
                data2.Add(new VaccineDataAltView
                {
                    vaccineName = i.vaccineName,
                    quantity = i.quantity
                });
            }
            return data2;
        }

        public bool CheckDuplicate_VaccineName(string name)
        {
            foreach (string i in database.Vaccines.Select(p => p.vaccineName).ToList())
            {
                if (name == i)
                {
                    return true;
                }
            }
            return false;

            //Vaccine v = new Vaccine();
            //v = database.Vaccines.Find(name);
            //if (v != null)
            //{
            //    return true;
            //}
            //return false;
        }


        // ----------------Registration-------------------
        public List<Registration> GetAll_Registration()
        {
            return database.Registrations.ToList();
        }
        public Registration GetRegistration_By_ID(string ID)
        {
            return database.Registrations.Find(ID);
        }
        public List<Registration> GetRegistration_By_CMND(string _cmnd)
        {
            return database.Registrations.Where(p => p.CMND_CCCD == _cmnd).ToList();
        }
        public void ExecuteEdit(Registration r)
        {
            var i = database.Registrations.Where(p => p.regisId == r.regisId).FirstOrDefault();
            i.vaccineName = r.vaccineName;
            i.State = r.State;
            database.SaveChanges();

        }
        public void ExecuteAdd(Registration r)
        {
            database.Registrations.Add(r);
            database.SaveChangesAsync();
        }
        public void ChangingFullname(Account i, string fullname)
        {
            var x = database.Accounts.Find(i.CMND_CCCD);
            x.Fullname = fullname;
            if (i.Permission == false)
            {
                var y = database.Citizens.Find(i.CMND_CCCD);
                y.fullName = fullname;
            }
            database.SaveChangesAsync();
        }
        public int DoseCounter(int dose)
        {
            return database.Citizens.Count(p => p.vaccination == dose);
        }
        public int GenderCounter(bool gender)
        {
            return database.Citizens.Count(p => p.gender == gender);
        }
        public int AgeCounter(int Range1, int Range2)
        {
            List<int> AgeList = new List<int>();
            foreach (Citizen i in database.Citizens)
            {
                int age = (int)((DateTime.Now - i.birth).TotalDays / 365.242199);
                AgeList.Add(age);
            }
            int counter = 0;
            foreach (int age in AgeList)
            {
                if (age >= Range1 && age <= Range2)
                {
                    counter++;
                }
            }
            return counter;
        }

        //---------------Export Excel-------------------
        public void load_Excel_App()
        {
            try
            {
                Excel.Application xlexcel;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;
                xlexcel = new Excel.Application();
                xlexcel.Visible = true;
                xlWorkBook = xlexcel.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                Excel.Range CR = (Excel.Range)xlWorkSheet.Cells[1, 1];
                CR.Select();
                xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
                xlWorkSheet.Columns.AutoFit();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString(), "Error");
            }
        }
        public List<Registration> GetVaccinationInfoByCMND(string CMND)
        {
            return database.Registrations.Where(p => p.CMND_CCCD == CMND).ToList();
        }
        public string GetPreviousVaccineName(string CMND)
        {
            Registration a = database.Registrations.Where(p => p.CMND_CCCD == CMND).FirstOrDefault();
            if (a == null)
            {
                return "";
            }
            return a.vaccineName;
        }
    }
}
