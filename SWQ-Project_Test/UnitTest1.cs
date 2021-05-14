using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using NUnit.Framework;
using SWQ_Project.Models;
using SWQ_Project.Services;
using SWQ_Project.Controllers;

namespace SWQ_Project_Test
{
    public class Tests
    {
        public ContactSplitter contactSplitter { get; set; }
        [SetUp]
        public void Setup()
        {
            contactSplitter = new ContactSplitter();
        }

        [Test]
        public void Test01()
        {
            string name = "Frau Sandra Berger";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Salutation == "Frau");
            Assert.True(contact.Title == "");
            Assert.True(contact.Firstname == "Sandra");
            Assert.True(contact.Lastname == "Berger");
            Assert.True(contact.LetterSalutation == "Sehr geehrte Frau");
            Assert.True(contact.Gender == Gender.Female);
        }

        [Test]
        public void Test02()
        {
            string name = "Herr Dr. Sandro Gutmensch";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Salutation == "Herr");
            Assert.True(contact.Title == "Dr.");
            Assert.True(contact.Firstname == "Sandro");
            Assert.True(contact.Lastname == "Gutmensch");
            Assert.True(contact.LetterSalutation == "Sehr geehrter Herr Dr.");
            Assert.True(contact.Gender == Gender.Male);
        }

        [Test]
        public void Test03()
        {
            string name = "Professor Heinreich Freiherr vom Wald";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Salutation == "");
            Assert.True(contact.Title == "Professor");
            Assert.True(contact.Firstname == "Heinreich");
            Assert.True(contact.Lastname == "Freiherr vom Wald");
            Assert.True(contact.LetterSalutation == "");
            Assert.True(contact.Gender == Gender.Male);
        }

        [Test]
        public void Test04()
        {
            string name = "Mrs. Doreen Faber";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Salutation == "Mrs."); 
            Assert.True(contact.Title == "");
            Assert.True(contact.Firstname == "Doreen");
            Assert.True(contact.Lastname == "Faber");
            Assert.True(contact.LetterSalutation == "Dear Mrs."); 
            Assert.True(contact.Gender == Gender.Female);
        }

        [Test]
        public void Test05()
        {
            string name = "Mme. Charlotte Noir";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Salutation == "Mme."); 
            Assert.True(contact.Title == "");
            Assert.True(contact.Firstname == "Charlotte");
            Assert.True(contact.Lastname == "Noir");
            Assert.True(contact.LetterSalutation == "Madame");
            Assert.True(contact.Gender == Gender.Female);
        }


        [Test]
        public void Test06()
        {
            string name = "Estobar y Gonzales";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Salutation == "");
            Assert.True(contact.Title == "");
            Assert.True(contact.Firstname == "Estobar");
            Assert.True(contact.Lastname == "y Gonzales");
            Assert.True(contact.LetterSalutation == "");
            Assert.True(contact.Gender == Gender.Unknown);
        }


        [Test]
        public void Test07()
        {
            string name = "Frau Prof. Dr. rer. nat. Maria von Leuthäuser-Schnarrenberger";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Salutation == "Frau");
            Assert.True(contact.Title == "Prof. Dr. rer. nat.");
            Assert.True(contact.Firstname == "Maria");
            Assert.True(contact.Lastname == "von Leuthäuser-Schnarrenberger");
            Assert.True(contact.LetterSalutation == "Sehr geehrte Frau Prof.");
            Assert.True(contact.Gender == Gender.Female);
        }


        [Test]
        public void Test08()
        {
            string name = "Herr Dipl. Ing. Max von Müller";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Salutation == "Herr");
            Assert.True(contact.Title == "Dipl. Ing.");
            Assert.True(contact.Firstname == "Max");
            Assert.True(contact.Lastname == "von Müller");
            Assert.True(contact.LetterSalutation == "Sehr geehrter Herr");
            Assert.True(contact.Gender == Gender.Male);
        }


        [Test]
        public void Test09()
        {
            string name = "Dr. Russwurm, Winfried";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Salutation == "");
            Assert.True(contact.Title == "Dr.");
            Assert.True(contact.Firstname == "Russwurm");
            Assert.True(contact.Lastname == "Winfried");
            Assert.True(contact.LetterSalutation == "");
            Assert.True(contact.Gender == Gender.Unknown);
        }


        [Test]
        public void Test10()
        {
            string name = "Herr Dr.-Ing. Dr. rer. nat. Dr. h.c. mult. Paul Steffens";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Salutation == "Herr");
            Assert.True(contact.Title == "Dr.-Ing. Dr. rer. nat. Dr. h.c. mult.");
            Assert.True(contact.Firstname == "Paul");
            Assert.True(contact.Lastname == "Steffens");
            Assert.True(contact.LetterSalutation == "Sehr geehrter Herr Dr.");
            Assert.True(contact.Gender == Gender.Male);
        }

        [Test]
        public void Test11()
        {
            //Create Title
            string title = "Testtitle";
            var create = contactSplitter.CreateTitle(title);
            Assert.True(create);

            //Check Title
            string name = "Herr Testtitle Max Mustermann";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Title.Equals(title));

            //Delete Title
            string jsonString = File.ReadAllText("JSONs/Title.json");
            var titles = JsonSerializer.Deserialize<List<string>>(jsonString);
            titles.RemoveAt(titles.Count-1);
            jsonString = JsonSerializer.Serialize(titles);
            File.WriteAllText("JSONs/Title.json", jsonString);
        }

        [Test]
        public void Test12()
        {
            //Create Title
            var salutation = new SalutationModel
            {
                Language = "German",
                Salutation = "Testsalutation",
                Gender = "Female",
                LetterSalutation = "Sehr geehrte Tessalutation"
            };

            var create = contactSplitter.CreateSalutation(salutation);
            Assert.True(create);

            //Check Title
            string name = "Testsalutation Karin Musterfrau";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Salutation.Equals(salutation.Salutation));

            //Delete Title
            string jsonString = File.ReadAllText("JSONs/Salutations.json");
            var salutations = JsonSerializer.Deserialize<List<SalutationModel>>(jsonString);
            salutations.RemoveAt(salutations.Count-1);
            jsonString = JsonSerializer.Serialize(salutations);
            File.WriteAllText("JSONs/Salutations.json", jsonString);
        }

    }
}