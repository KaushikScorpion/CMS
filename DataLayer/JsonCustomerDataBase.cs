using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.IO;
using DataLayer.CustomData;
using DataLayer.Interfaces;

namespace DataLayer
{
    public class JsonCustomerDataBaseManager
    {
        #region FileIO
        public List<CustomerDataObject> ReadFile(string FileName)
        {
            FileName = "c:\\" + FileName;
            IEnumerable<CustomerDataObject> s;
            var serializer = new JsonSerializer();
            if (new FileInfo(FileName).Length == 0)
            {
                return new List<CustomerDataObject>();
            }
            using (var re = File.OpenText(FileName))
            using (var reader = new JsonTextReader(re))
            {

                s = serializer.Deserialize<CustomerDataObject[]>(reader);
            }
            return s.ToList<CustomerDataObject>();
        }

        public void WriteFile(List<CustomerDataObject> CustomerList, string FileName)
        {
            var serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(FileName))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, CustomerList);

            }
        }
        #endregion

        #region HelperRoutines

        public Retrieve_message ValidateSearchCriteria(Retrieve_message Criteria)
        {
            if (Criteria.Start_index < 1)
                Criteria.Start_index = 1;
            return Criteria;
        }
        public bool IsNull(CustomerDataObject Record)
        {

            if (Record.Emailid == null || Record.Gender == null || Record.CustomerName == null || Record.Phone == 0 || Record.Address == null || Record.City == null)
                return true;
            else
                return false;

        }

        public int IdAssigner(List<CustomerDataObject> CustomerList, CustomerDataObject Record)
        {
            if (CustomerList.Count == 0)
                return 2500;
            else
                return CustomerList.Last<CustomerDataObject>().Id + 1;
        }

        #endregion


        public List<CustomerDataObject> InsertIntoList(List<CustomerDataObject> CustomerList, CustomerDataObject Record)
        {


            try
            {
                Record.Id = IdAssigner(CustomerList, Record);
                if (IsNull(Record))
                {
                    throw new Exception("Required Field Can't be Null");
                }
                if (CustomerList.Exists(delegate (CustomerDataObject Obj) { return Obj.Emailid == Record.Emailid; }))
                {
                    throw new Exception("Duplicate Email");
                }
                if (CustomerList.Exists(delegate (CustomerDataObject Obj) { return Obj.Phone == Record.Phone; }))
                {
                    throw new Exception("Duplicate Phone");
                }
                CustomerList.Add(Record);
                return CustomerList;
            }
            catch
            {
                throw;
            }

        }

        public List<CustomerDataObject> DeleteFromList(List<CustomerDataObject> CustomerList, CustomerDataObject Record)
        {
            try
            {
                Record = CustomerList.Find(delegate (CustomerDataObject Obj) { return Obj.Id == Record.Id && Obj.Adminid == Record.Adminid; });
                if (!CustomerList.Remove(Record))
                    throw new Exception("Unauthorized. No Such CustomerDataObject.");
                return CustomerList;
            }
            catch
            {
                throw;
            }
        }

        public List<CustomerDataObject> UpdateList(List<CustomerDataObject> CustomerList, CustomerDataObject Record)
        {
            try
            {
                int RecordIndex = CustomerList.FindIndex(delegate (CustomerDataObject Obj) { return Obj.Id == Record.Id && Obj.Adminid == Record.Adminid; });
                if (RecordIndex < 0 || RecordIndex >= CustomerList.Count)
                    throw new Exception("Unauthorized. No Such CustomerDataObject.");
                CustomerDataObject[] TempArray = new CustomerDataObject[CustomerList.Count];

                CustomerDataObject CurrentRecord = CustomerList.Find(delegate (CustomerDataObject Obj) { return Obj.Id == Record.Id && Obj.Adminid == Record.Adminid; });
                List<CustomerDataObject> Testlist = CustomerList.SkipWhile(cc => cc.Emailid == CurrentRecord.Emailid).ToList<CustomerDataObject>();
                if (CustomerList.Exists(delegate (CustomerDataObject Obj) { return Obj.Emailid == Record.Emailid; }))
                {
                    if (CustomerList.Find(delegate (CustomerDataObject Obj) { return Obj.Emailid == Record.Emailid; }).Id != Record.Id)
                        throw new Exception("Duplicate Email");
                }
                if (CustomerList.Exists(delegate (CustomerDataObject Obj) { return Obj.Phone == Record.Phone; }))
                {
                    if (CustomerList.Find(delegate (CustomerDataObject Obj) { return Obj.Phone == Record.Phone; }).Id != Record.Id)
                        throw new Exception("Duplicate Phone");
                }

                CustomerList[RecordIndex] = Record;
                return CustomerList;
            }
            catch
            {
                throw;
            }
        }

        public List<CustomerDataObject> RetrieveFromList(List<CustomerDataObject> CustomerList, Retrieve_message Criteria)
        {
            try
            {


                Criteria = ValidateSearchCriteria(Criteria);
                List<CustomerDataObject> FilteredList;
                FilteredList = CustomerList.FindAll(delegate (CustomerDataObject Obj) { return Obj.CustomerName.Contains(Criteria.Searchvalue) || Obj.Emailid == Criteria.Searchvalue || Obj.Gender.Contains(Criteria.Searchvalue); });
                if (Criteria.End_index > FilteredList.Count)
                {
                    Criteria.End_index = FilteredList.Count;
                }
                FilteredList = FilteredList.GetRange((Criteria.Start_index - 1), Criteria.End_index - Criteria.Start_index + 1);

                switch (Criteria.Sortparam)
                {
                    case "Name": FilteredList = FilteredList.OrderBy(cc => cc.CustomerName).ToList<CustomerDataObject>(); break;
                    case "Gender": FilteredList = FilteredList.OrderBy(cc => cc.Gender).ToList<CustomerDataObject>(); break;
                    case "Emailid": FilteredList = FilteredList.OrderBy(cc => cc.Emailid).ToList<CustomerDataObject>(); break;
                }
                return FilteredList;
            }
            catch
            {
                throw;
            }
        }
    }
    public class JsonCustomerDataBase:ICustomerDataBase
    {
      
        public List<CustomerDataObject> GetDetailsAll(Retrieve_message Criteria)
        {
            try {
                JsonCustomerDataBaseManager JsonDBM = new JsonCustomerDataBaseManager();
                List<CustomerDataObject> CustomerList = JsonDBM.ReadFile("DB.json");
                CustomerList = JsonDBM.RetrieveFromList(CustomerList, Criteria);
                return CustomerList;
            }
            catch 
            {
                throw;
            }
        }
        public int DeleteDetails(Retrieve_message Criteria)
        {
            try {
                JsonCustomerDataBaseManager JsonDBM = new JsonCustomerDataBaseManager();
                List<CustomerDataObject> CustomerList = JsonDBM.ReadFile("DB.json");
                CustomerDataObject Record = new CustomerDataObject();
                Record.Id = Criteria.id;
                Record.Adminid = Criteria.Adminid;
                CustomerList = JsonDBM.DeleteFromList(CustomerList, Record);
                return 1;
            }
            catch
            {
                throw;
            }
        }
        public int SetDetails(CustomerDataObject Record)
        {
            try
            {
                JsonCustomerDataBaseManager JsonDBM = new JsonCustomerDataBaseManager();
                List<CustomerDataObject> CustomerList = JsonDBM.ReadFile("DB.json");
                CustomerList = JsonDBM.InsertIntoList(CustomerList, Record);
                return 1;
            }
            catch
            {
                throw;
            }


        }
        public int UpdateDetails(CustomerDataObject Record)
        {
            try
            {
                JsonCustomerDataBaseManager JsonDBM = new JsonCustomerDataBaseManager();
                List<CustomerDataObject> CustomerList = JsonDBM.ReadFile("DB.json");
                CustomerList = JsonDBM.UpdateList(CustomerList, Record);
                return 1;
            }
            catch
            {
                throw;
            }
        }

    }
}
