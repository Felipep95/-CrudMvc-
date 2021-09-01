using X.PagedList;

namespace CrudMvc.Models.ViewModels
{
    public class ClientViewModel
    {
        public IPagedList<Client> Client { get; set; }
    }
}
