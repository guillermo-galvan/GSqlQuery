namespace GSqlQuery.Sqlite
{
    public class SqliteStatements : Statements
    {
        public override string Format => "\"{0}\"";

        public override string ValueAutoIncrementingQuery => "SELECT last_insert_rowid();";

        public override bool IncrudeTableNameInQuery => false;
    }
}