namespace FluentSQL.MySql
{
    public class MySqlStatements : Default.Statements
    {
        public override string Format => "`{0}`";

        public override string ValueAutoIncrementingQuery => "SELECT LAST_INSERT_ID();";
    }
}
