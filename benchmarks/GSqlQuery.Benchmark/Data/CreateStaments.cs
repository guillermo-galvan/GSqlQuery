namespace GSqlQuery.Benchmarks.Data
{
    public abstract class CreateStaments
    {
        protected readonly IStatements _statements;

        public CreateStaments()
        {
            _statements = new Statements();
        }
    }

}
