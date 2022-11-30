namespace FluentSQL.DatabaseManagementTest.Default
{
    public class InsertQueryBuilderTest
    {
        private readonly IStatements _statements;

        public InsertQueryBuilderTest()
        {
            _statements = new FluentSQL.Default.Statements();
        }
    }
}
