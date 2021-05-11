using NUnit.Framework;
using SWQ_Project.Models;
using SWQ_Project.Services;
using SWQ_Project.Controllers;

namespace SWQ_Project_Test
{
    public class FirstnameTests
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
            Assert.True(contact.Firstname == "Sandra");
        }

        [Test]
        public void Test02()
        {
            string name = "Herr Dr. Sandro Gutmensch";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Firstname == "Sandro");
        }

        [Test]
        public void Test03()
        {
            string name = "Professor Heinreich Freiherr vom Wald";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Firstname == "Heinreich");
        }

        [Test]
        public void Test04()
        {
            string name = "Mrs. Doreen Faber";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Firstname == "Doreen");
        }

        [Test]
        public void Test05()
        {
            string name = "Mme. Charlotte Noir";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Firstname == "Charlotte");
        }


        [Test]
        public void Test06()
        {
            string name = "Estobar y Gonzales"; //Was passiert mit dem "y"?
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Firstname == "Estobar");
        }


        [Test]
        public void Test07()
        {
            string name = "Frau Prof. Dr. rer. nat. Maria von Leuthäuser-Schnarrenberger";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Firstname == "Maria");
        }


        [Test]
        public void Test08()
        {
            string name = "Herr Dipl. Ing. Max von Müller";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Firstname == "Max");
        }


        [Test]
        public void Test09()
        {
            string name = "Dr. Russwurm, Winfried";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Firstname == "Russwurm");
        }


        [Test]
        public void Test10()
        {
            string name = "Herr Dr.-Ing. Dr. rer. nat. Dr. h.c. mult. Paul Steffens";
            var contact = contactSplitter.Split(new CompleteContactModel(name));
            Assert.True(contact.Firstname == "Paul");
        }
    }
}