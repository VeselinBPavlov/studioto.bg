namespace Studio.Application.Clients.Queries.GetAllClients
{
    using System.Collections.Generic;

    public class ClientsListViewModel
    {
        public IList<ClientAllViewModel> Clients { get; set; } 
    }
}