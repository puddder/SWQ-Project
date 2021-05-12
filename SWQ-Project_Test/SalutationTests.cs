using NUnit.Framework;
using SWQ_Project.Models;
using SWQ_Project.Services;

namespace SWQ_Project_Test
{
    public class SalutationTests
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
            Assert.True(contact.Gender == Gender.Female);
        }

        [Test]
        public void Test02()
        {
            string name = "Herr Dr. Sandro Gutmensch";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Salutation == "Herr");
            Assert.True(contact.Gender == Gender.Male);
        }

        [Test]
        public void Test03()
        {
            string name = "Professor Heinreich Freiherr vom Wald";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Salutation == "");
            Assert.True(contact.Gender == Gender.Male);
        }

        [Test]
        public void Test04()
        {
            string name = "Mrs. Doreen Faber";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Salutation == "Mrs");
            Assert.True(contact.Gender == Gender.Female);
        }

        [Test]
        public void Test05()
        {
            string name = "Mme. Charlotte Noir";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Salutation == "Mme");
            Assert.True(contact.Gender == Gender.Female);
        }


        [Test]
        public void Test06()
        {
            string name = "Estobar y Gonzales"; 
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Salutation == "");
            Assert.True(contact.Gender == Gender.Unknown);
        }


        [Test]
        public void Test07()
        {
            string name = "Frau Prof. Dr. rer. nat. Maria von Leuthäuser-Schnarrenberger";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Salutation == "Frau");
            Assert.True(contact.Gender == Gender.Female);
        }


        [Test]
        public void Test08()
        {
            string name = "Herr Dipl. Ing. Max von Müller";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Salutation == "Herr");
            Assert.True(contact.Gender == Gender.Male);
        }


        [Test]
        public void Test09()
        {
            string name = "Dr. Russwurm, Winfried";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Salutation == "");
            //Assert.True(contact.Gender == Gender.Unknown);
        }


        [Test]
        public void Test10()
        {
            string name = "Herr Dr.-Ing. Dr. rer. nat. Dr. h.c. mult. Paul Steffens";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Salutation == "Herr");
            Assert.True(contact.Gender == Gender.Male);
        }
    }
}
