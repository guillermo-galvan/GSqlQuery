namespace FluentSQL.Benchmarks.Data
{
    [Table("UserTest")]
    public class User : Entity<User>
    {
        [Column("Id", Size = 16, IsAutoIncrementing = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column("Name", Size = 80)]
        public string Name { get; set; }

        [Column("LastName", Size = 80)]
        public string LastName { get; set; }

        [Column("Email", Size = 80)]
        public string Email { get; set; }

        public bool IsActive { get; set; }

        public User() { }

        public User(int id, string name, string lastName, string email, bool isActive)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            Email = email;
            IsActive = isActive;
        }
    }
}
