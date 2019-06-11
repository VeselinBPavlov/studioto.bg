namespace Studio.Persistence.Context
{
    using System;
    
    using Microsoft.EntityFrameworkCore;

    using Application.Interfaces.Persistence;

    public class DbQueryRunner : IDbQueryRunner
    {
        public DbQueryRunner(StudioDbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public StudioDbContext Context { get; set; }

        public void RunQuery(string query, params object[] parameters)
        {
            this.Context.Database.ExecuteSqlCommand(query, parameters);
        }

        public void Dispose()
        {
            this.Context?.Dispose();
        }
    }
}
