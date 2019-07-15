namespace Studio.Application.Clients.Queries.GetClientById
{
    using System;
    using System.Linq.Expressions;
    using Studio.Domain.Entities;

    public class ClientViewModel
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string VatNumber { get; set; }

        public string ManagerFirstName { get; set; }

        public string ManagerLastName { get; set; }

        public string Phone { get; set; }

        public static Expression<Func<Client, ClientViewModel>> Projection
        {
            get
            {
                return client => new ClientViewModel
                {
                    Id = client.Id,
                    CompanyName = client.CompanyName,
                    VatNumber = client.VatNumber,
                    ManagerFirstName = client.Manager.FirstName,
                    ManagerLastName = client.Manager.LastName,
                    Phone = client.Phone
                };
            }
        }

        public static ClientViewModel Create(Client client)
        {
            return Projection.Compile().Invoke(client);
        }
    }
}