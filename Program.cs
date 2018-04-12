using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FilterField
{
    class Program
    {
        static void Main(string[] args)
        {
            var mdl = new ModelA();
            mdl.Name = "Aykut";
            mdl.Surname = "Asil";
            mdl.Age = 26;

            var jsonObj = JsonConvert.SerializeObject(mdl);
            Console.WriteLine(jsonObj); // {"Name":"Aykut","Surname":"Asil","Age":26}

            var filterString = FilterFields(jsonObj);
            Console.WriteLine(filterString); // {"Surname":"Asil","Age":26}

            Console.ReadLine();
        }


        private static String FilterFields(String json)
        {
            var obj = JObject.Parse(json);

            var properties = typeof(FilterFieldsModel).GetProperties();

            foreach (var info in properties)
            {
                var field = obj.Children<JProperty>().FirstOrDefault(p => p.Name == info.Name);
                if (field != null)
                    field.Remove();
            }

            var str = JsonConvert.SerializeObject(obj);
            return str;
        }
    }

    class ModelA
    {
        public String Name { get; set; }
        public String Surname { get; set; }
        public int Age { get; set; }
    }

    class FilterFieldsModel
    {
        public String Name { get; set; }
    }
}

