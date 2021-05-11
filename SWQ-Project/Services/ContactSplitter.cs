using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
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
        public SplitContact Split(CompleteContactModel completeContactModel)
        {
            //Split Input into seperated words
            var words = completeContactModel.CompleteContact.Trim().Split(' ');

            //Get Salutation
            string jsonString = File.ReadAllText("JSONs/Salutations.json");
            var salutations = JsonSerializer.Deserialize<List<SalutationModel>>(jsonString);
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

            // Get Lastname
            jsonString = File.ReadAllText("JSONs/Prefix.json");
            var lastnamePrefixes = JsonSerializer.Deserialize<List<string>>(jsonString);
            var lastname = GetLastname(lastnamePrefixes, completeContactModel.CompleteContact, words);

            //Create return object
            return new SplitContact
            {
                Firstname = "",
                Lastname = lastname,
                Title = "",
                Salutation = salutation.Salutation,
                LetterSalutation = letterSalutation,
                Gender = gender
            };
        }

        /// <summary>
        /// Add new Salutation to JSON
        /// </summary>
        /// <param name="model">Salutation to add</param>
        public void CreateSalutation(SalutationModel model)
        {
            string jsonString = File.ReadAllText("JSONs/Salutations.json");
            var salutations = JsonSerializer.Deserialize<List<SalutationModel>>(jsonString);
            salutations.Add(model);
            jsonString = JsonSerializer.Serialize(salutations);
            File.WriteAllText("JSONs/Salutations.json", jsonString);
        }

        /// <summary>
        /// Get lastname
        /// </summary>
        /// <param name="lastnamePrefixes">List of prefixes for lastnames</param>
        /// <param name="fllName">Full name</param>
        /// <param name="words">Full name split into single words</param>
        /// <returns></returns>
        private string GetLastname(List<string> lastnamePrefixes, string fllName, string[] words)
        {
            if (lastnamePrefixes.Any(fllName.Contains))
            {
                var prefix = lastnamePrefixes.OrderByDescending(s => s.Length).ToList().Find(fllName.Contains);
                var prefixLength = prefix.Split(' ').Length;

                var lastName = string.Join(' ', words[(words.Length - prefixLength - 1)..]);
                return lastName;
            }

            return words[^1];
        }

        /// <summary>
        /// Check if word is a salutation
        /// </summary>
        /// <param name="salutations">List of salutations (get from json)</param>
        /// <param name="toCheck">word to check</param>
        /// <returns>SalutationModel: Language, Salutation, Gender, LetterSalutation</returns>
        private SalutationModel GetSalutation(List<SalutationModel> salutations, string toCheck)
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