using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using WPFModel;
namespace WPFModelView
{
    public class PersonViewModel
    {
        public ObservableCollection<Person> Persons { get; set;}
        public ObservableCollection<string> CountryOptions { get; set; }  
        public int RowCount { get; set; }
        public PersonViewModel()
        {
            Persons = new ObservableCollection<Person>();
            CountryOptions = new ObservableCollection<string>();
            RowCount = 0;
            GetAllPersons(); // Populate the ObservableCollection here
            LoadCountryOptions(); 
        }
        private void LoadPersons()
        {            
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(basePath, "..\\..\\..\\..\\WPFModelView\\PersonsData.csv");

            Console.WriteLine(filePath);
            StreamReader reader = null;
            if (File.Exists(filePath))
            {
                reader = new StreamReader(File.OpenRead(filePath));
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var data = line.Split(',');
                    if (data[0] == "name") continue;
                    var p = new Person
                    {
                        Id = Guid.NewGuid(),
                        Name = data[0],
                        Country = data[1],
                        Address = data[2],
                        PostalZip = data[3],
                        Email = data[4],
                        Phone = data[5]

                    };
                    Persons.Add(p);
                }
            }
            else
            {
                Console.WriteLine("File doesn't exist");
            }

        }
        private ObservableCollection<string> LoadCountryOptions()
        { 
            var countryList = Persons.Select(x=>x.Country).Distinct().ToList();
            CountryOptions.Add("All");
            foreach (var i in countryList)
            {
                CountryOptions.Add(i);
            }
            return CountryOptions;
        }
        private ObservableCollection<Person> GetAllPersons()
        {
            LoadPersons();
            return Persons;
        }
        public void SortPersons(string sortingTag)
        {
            switch (sortingTag)
            {
                case "Name":
                    SortByName();
                    break;
                case "Country":
                    SortByCountry();
                    break;
                    // Add more cases for other sorting criteria if needed
            }
        }

        private void SortByName()
        {
            List<Person> sortedPersons = Persons.OrderBy(person => person.Name).ToList();
            UpdatePersonsCollection(sortedPersons);
        }

        private void SortByCountry()
        {
            List<Person> sortedPersons = Persons.OrderBy(person => person.Country).ToList();
            UpdatePersonsCollection(sortedPersons);
        }

        private void UpdatePersonsCollection(List<Person> sortedPersons)
        {
            Persons.Clear();
            foreach (var person in sortedPersons)
            {
                Persons.Add(person);
            }
            RowCount = Persons.Count / 4; 
        }



        public void ApplyCountryFilter(string selectedCountry)
        {
            if (selectedCountry == "All")
            {
                ResetFilter();
            }
            else
            {
                ResetFilter();
                List<Person> filteredPersons = Persons.Where(p => p.Country == selectedCountry).ToList();
                UpdatePersonsCollection(filteredPersons);      
            }
        }

        private void ResetFilter()
        {
            Persons.Clear();
            LoadPersons();            
        }
    }
}
