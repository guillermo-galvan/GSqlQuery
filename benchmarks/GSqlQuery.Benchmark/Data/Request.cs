namespace GSqlQuery.Benchmark.Data
{
    [Table("Request")]
    internal class Request : Entity<Request>
    {
        [Column("Id", Size = 16, IsAutoIncrementing = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        public string Url { get; set; }

        public Request() {}

        public Request(int id, string url)
        {
            Id = id;
            Url = url;
        }
    }
}
