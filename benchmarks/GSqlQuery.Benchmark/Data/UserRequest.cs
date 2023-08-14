using System;

namespace GSqlQuery.Benchmark.Data
{
    [Table("UserRequest")]
    internal class UserRequest : Entity<UserRequest>
    {
        [Column("UserId", Size = 16, IsPrimaryKey = true)]
        public int UserId { get; set; }

        [Column("RequestId", Size = 16, IsPrimaryKey = true)]
        public int RequestId { get; set; }

        public DateTime DateTime { get; set; }

        public UserRequest() { }

        public UserRequest(int userId, int requestId, DateTime dateTime)
        {
            UserId = userId;
            RequestId = requestId;
            DateTime = dateTime;
        }
    }
}