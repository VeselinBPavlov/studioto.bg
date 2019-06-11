namespace Studio.Application.Interfaces.Persistence
{
    public interface IDbQueryRunner
    {
        void RunQuery(string query, params object[] parameters);
    }
}
