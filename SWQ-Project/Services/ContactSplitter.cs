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
            gender = ParseGender(salutation.Gender);

            // Get Lastname
            jsonString = File.ReadAllText("JSONs/Prefix.json");
            var lastnamePrefixes = JsonSerializer.Deserialize<List<string>>(jsonString);
            var lastname = GetLastname(lastnamePrefixes, completeContactModel.CompleteContact, words);

            // Get Firstname
            var lastnameRemoved = completeContactModel.CompleteContact.Replace(lastname, "");
            var firstname = lastnameRemoved.Trim().Split(' ')[^1].Replace(",", "");

            // Get Title
            var titleJsonString = File.ReadAllText("JSONs/Title.json");
            var salutationTitleJsonStrig = File.ReadAllText("JSONs/SalutationTitle.json");
            var allTitle = GetTitle(titleJsonString, completeContactModel.CompleteContact);
            var salutationTitle = GetSalutationTitle(salutationTitleJsonStrig, completeContactModel.CompleteContact, gender);
            if (!string.IsNullOrWhiteSpace(salutationTitle.Short) && !string.IsNullOrWhiteSpace(letterSalutation))
                letterSalutation += " " + salutationTitle.Short;
            if (gender == Gender.Unknown && salutationTitle.Gender != Gender.Unknown.ToString())
                gender = ParseGender(salutationTitle.Gender);

            //Create return object
            return new SplitContact
            {
                Firstname = firstname,
                Lastname = lastname,
                Title = allTitle,
                SalutationTitle = salutationTitle.Short,
                Salutation = salutation.Salutation,
                LetterSalutation = letterSalutation,
                Gender = gender
            };
        }

        /// <summary>
        /// Parse gender string to gender enum.
        /// </summary>
        /// <param name="gender">Gender string</param>
        /// <returns>gender enum</returns>
        private Gender ParseGender(string gender)
        {
            switch (gender)
            {
                case "Male":
                    return Gender.Male;
                    break;
                case "Female":
                    return Gender.Female;
                    break;
                default:
                    return Gender.Unknown;
            }
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
        /// 
        /// </summary>
        /// <param name="letterTitleJsonString">All valid salutaion titles.</param>
        /// <param name="title">All titles of inputted string.</param>
        /// <returns>Salutation title</returns>
        public SalutationTitleModel GetSalutationTitle(string letterTitleJsonString, string title, Gender gender)
        {
            var titles = JsonSerializer.Deserialize<List<SalutationTitleModel>>(letterTitleJsonString);

            foreach (var salutationTitle in titles)
            {
                if (gender == Gender.Unknown)
                {
                    if (title.Contains(salutationTitle.Title) || title.Contains(salutationTitle.Short))
                        return salutationTitle;
                }
                else if (salutationTitle.Gender == gender.ToString() && title.Contains(salutationTitle.Title) || title.Contains(salutationTitle.Short))
                    return salutationTitle;
            }
            return new SalutationTitleModel
            {
                Short = "",
                Title = "",
                Gender = ""
            };
        }

        /// <summary>
        /// Get all valid titles in entered order.
        /// </summary>
        /// <param name="titleJsonString">List of all valid title.</param>
        /// <param name="completeContact">Inputted string from user.</param>
        /// <returns>All valid title</returns>
        private string GetTitle(string titleJsonString, string completeContact)
        {
            //validate titles
            var contactTemp = completeContact;
            var allTitles = JsonSerializer.Deserialize<List<string>>(titleJsonString);
            List<string> titleList = new List<string>();
            foreach (string t in allTitles)
            {
                if (contactTemp.Contains(t))
                {
                    titleList.Add(t);
                    contactTemp = contactTemp.Replace(t, "");
                }
            }

            //reorder validated titles
            List<int> orderIndex = new List<int>();
            foreach (var tempTitle in titleList)
            {
                orderIndex.Add(completeContact.IndexOf(tempTitle));
            }
            int next = -1;
            List<string> titleInOrder = new List<string>();
            foreach (int z in orderIndex)
            {
                int temp = int.MaxValue;
                for (int index = 0; index < titleList.Count; index++)
                {
                    if (orderIndex[index] < temp)
                    {
                        temp = orderIndex[index];
                        next = index;
                    }
                }
                titleInOrder.Add(titleList[next]);
                titleList.RemoveAt(next);
            }

            //output in right order
            string title = "";
            foreach (string t in titleInOrder)
            {
                if (title.Count() != 0)
                    title += " " + t;
                else
                    title += t;
            }
            return title;
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