using System;

namespace GSqlQuery.Runner.Test.Models
{
    internal class Test1
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Create { get; set; }

        public bool IsTest { get; set; }

        public Test1()
        { }

        public Test1(int id, string name, DateTime create, bool isTest)
        {
            Id = id;
            Name = name;
            Create = create;
            IsTest = isTest;
        }
    }
}