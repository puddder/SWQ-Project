using SWQ_Project.Models;

namespace SWQ_Project.Services
{
    public interface IContactSpillter
    {
        public SplitContact Split(CompleteContactModel model);
        public void CreateSalutation(SalutationModel model);
    }
}