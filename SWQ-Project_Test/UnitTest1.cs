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
            Assert.True(contact.LetterSalutation == "");
            //Assert.True(contact.Gender == Gender.Female);
        }

        [Test]
        public void Test02()
        {
            string name = "Herr Dr. Sandro Gutmensch";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Salutation == "Herr"); //Herr oder Herr Dr.?
            Assert.True(contact.Title == "Dr.");
            Assert.True(contact.Firstname == "Sandro");
            Assert.True(contact.Lastname == "Gutmensch");
            Assert.True(contact.LetterSalutation == "");
            //Assert.True(contact.Gender == Gender.Male);
        }

        [Test]
        public void Test03()
        {
            string name = "Professor Heinreich Freiherr vom Wald";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Salutation == ""); //Herr oder Herr Professor?
            Assert.True(contact.Title == "Professor");
            Assert.True(contact.Firstname == "Heinreich");
            Assert.True(contact.Lastname == "Freiherr vom Wald"); //???
            Assert.True(contact.LetterSalutation == "");
            //Assert.True(contact.Gender == Gender.Unknown);
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
            Assert.True(contact.LetterSalutation == "");
            //Assert.True(contact.Gender == Gender.Female);
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
            Assert.True(contact.LetterSalutation == "");
            //Assert.True(contact.Gender == Gender.Female);
        }


        [Test]
        public void Test06()
        {
            string name = "Estobar y Gonzales"; //Was passiert mit dem "y"?
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Salutation == "");
            Assert.True(contact.Title == "");
            Assert.True(contact.Firstname == "Estobar");
            Assert.True(contact.Lastname == "Gonzales");
            Assert.True(contact.LetterSalutation == "");
            //Assert.True(contact.Gender == Gender.Unknown);
        }


        [Test]
        public void Test07()
        {
            string name = "Frau Prof. Dr. rer. nat. Maria von Leuth�userSchnarrenberger";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Salutation == "Frau Prof. Dr. rer. nat."); //Das oder nur Frau?
            Assert.True(contact.Title == "Prof. Dr. rer. nat.");
            Assert.True(contact.Firstname == "Maria");
            Assert.True(contact.Lastname == "von Leuth�user-Schnarrenberger");
            Assert.True(contact.LetterSalutation == "");
            //Assert.True(contact.Gender == Gender.Female);
        }


        [Test]
        public void Test08()
        {
            string name = "Herr Dipl. Ing. Max von M�ller";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Salutation == "Herr");
            Assert.True(contact.Title == "Dipl. Ing.");
            Assert.True(contact.Firstname == "Max");
            Assert.True(contact.Lastname == "von M�ller");
            Assert.True(contact.LetterSalutation == "");
            //Assert.True(contact.Gender == Gender.Female);
        }


        [Test]
        public void Test09()
        {
            string name = "Dr. Russwurm, Winfried";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Salutation == "");
            Assert.True(contact.Title == "Dr.");
            Assert.True(contact.Firstname == "Winfried");
            Assert.True(contact.Lastname == "Russwurm");
            Assert.True(contact.LetterSalutation == "");
            //Assert.True(contact.Gender == Gender.Unknown);
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
            Assert.True(contact.LetterSalutation == "");
            //Assert.True(contact.Gender == Gender.Male);
        }
    }
}