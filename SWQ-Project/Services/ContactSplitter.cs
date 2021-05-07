using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using SWQ_Project.Models;

namespace SWQ_Project.Services
{
    public class ContactSplitter : IContactSpillter
    {
        public SplitContact split(CompleteContactModel completeContactModel )
        {
            string jsonString = File.ReadAllText("JSONs/Salutations.json");
            var salutations = JsonSerializer.Deserialize<List<SalutationModel>>(jsonString);

            return new SplitContact
            {
                Firstname = "",
                Lastname = "",
                Title = "",
                Salutation = "",
                LetterSalutation = "",
            };
        }

        public SalutationModel GetSalutation(List<SalutationModel> salutations, string toCheck)
        {
            foreach (var salutation in salutations)
            {
                if (toCheck.Equals(salutation.Salutation))
                {
                    return salutation;
                }
            }

            return null;
        }
    }
}