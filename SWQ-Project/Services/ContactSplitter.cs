using SWQ_Project.Models;

namespace SWQ_Project.Services
{
    public class ContactSplitter : IContactSpillter
    {
        public SplitContact split(CompleteContactModel completeContactModel )
        {
            return new SplitContact
            {
                Firstname = "",
                Lastname = "",
                Title = "",
                Salutation = "",
                LetterSalutation = "",
            };
        }
        
    }
}