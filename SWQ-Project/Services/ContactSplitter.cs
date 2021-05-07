using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SWQ_Project.Models;

namespace SWQ_Project.Services
{
    public class ContactSplitter : IContactSpillter
    {
        /// <summary>
        /// Split inputstring into individual parts of the contact
        /// </summary>
        /// <param name="completeContactModel">inputstring</param>
        /// <returns>Object with the individual parts oft the contact</returns>
        public SplitContact Split(CompleteContactModel completeContactModel )
        {
            string jsonString = File.ReadAllText("JSONs/Salutations.json");
            var salutations = JsonSerializer.Deserialize<List<SalutationModel>>(jsonString);
            var words = completeContactModel.CompleteContact.Split(' ');

            var salutation = GetSalutation(salutations, words[0]);
            Gender gender = Gender.Unknown;
            string letterSalutation = salutation.LetterSalutation;
            switch (salutation.Gender)
            {
                case "Male":
                    gender = Gender.Male;
                    break;
                case "Female":
                    gender = Gender.Female;
                    break;
            }

            return new SplitContact
            {
                Firstname = "",
                Lastname = "",
                Title = "",
                Salutation = salutation.Salutation,
                LetterSalutation = letterSalutation,
                Gender = gender
            };
        }

        /// <summary>
        /// Check if word is a salutation
        /// </summary>
        /// <param name="salutations">List of salutations (get from json)</param>
        /// <param name="toCheck">word to check</param>
        /// <returns>SalutationModel: Language, Salutation, Gender, LetterSalutation</returns>
        public SalutationModel GetSalutation(List<SalutationModel> salutations, string toCheck)
        {
            foreach (var salutation in salutations)
            {
                if (toCheck.ToLower().Equals(salutation.Salutation.ToLower()))
                {
                    return salutation;
                }
            }
            return new SalutationModel()
            {
                Gender = "none",
                Language = "",
                Salutation = "",
                LetterSalutation = ""
            };
        }
    }
}