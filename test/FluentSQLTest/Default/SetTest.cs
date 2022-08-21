using FluentSQL.Default;
using FluentSQL.Models;
using FluentSQLTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQLTest.Default
{
    public class SetTest
    {
        private readonly ConnectionOptions _connectionOptions;

        public SetTest()
        {
            _connectionOptions = new ConnectionOptions(new FluentSQL.Default.Statements());
        }

        [Fact]
        public void Should_initializes_a_new_instance_of_the_Set_class()
        {
            Set<Test1> test = new (new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                _connectionOptions, null);

            Assert.NotNull(test);
            Assert.NotNull(test.ColumnValues);
            Assert.NotEmpty(test.ColumnValues);
            Assert.Equal(3, test.ColumnValues.Count);
        }

        [Fact]
        public void Should_initializes_a_new_instance_of_the_Set_class_2()
        {
            Test1 model = new(1, null, DateTime.Now, true);
            Set<Test1> test = new(model,new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                _connectionOptions);

            Assert.NotNull(test);
            Assert.NotNull(test.ColumnValues);
            Assert.NotEmpty(test.ColumnValues);
            Assert.Equal(3, test.ColumnValues.Count);
        }

        [Fact]
        public void Should_add_a_new_column_value_with_set_value()
        {
            Set<Test1> test = new(new ClassOptions(typeof(Test1)), new List<string> {nameof(Test1.Name)},
               _connectionOptions, null);

            test.Add(x => x.Id, 1).Add(x => x.Create, DateTime.Now);

            Assert.NotNull(test.ColumnValues);
            Assert.NotEmpty(test.ColumnValues);
            Assert.Equal(3, test.ColumnValues.Count);
        }

        [Fact]
        public void Should_add_a_new_column_value_with_property()
        {
            Test1 model = new(1, null, DateTime.Now, true);
            Set<Test1> test = new(model, new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Name) },
               _connectionOptions);

            test.Add(x => x.Id).Add(x => x.Create);

            Assert.NotNull(test.ColumnValues);
            Assert.NotEmpty(test.ColumnValues);
            Assert.Equal(3, test.ColumnValues.Count);
        }

        [Fact]
        public void TThrow_exception_if_any_null_parameters_are_passed()
        {
            Assert.Throws<ArgumentNullException>(() => new Set<Test1>(null, new List<string> { nameof(Test1.Id) }, _connectionOptions, null));
            Assert.Throws<ArgumentNullException>(() => new Set<Test1>(new ClassOptions(typeof(Test1)), null, _connectionOptions, null));
            Assert.Throws<ArgumentNullException>(() => new Set<Test1>(new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Id) }, null, null));
            Assert.Throws<InvalidOperationException>(() => new Set<Test1>(new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Id) }, _connectionOptions, null).Add(x => x.Name));
        }

        [Fact]
        public void TThrow_exception_if_any_null_parameters_are_passed2()
        {
            Test1 model = new(1, null, DateTime.Now, true);
            Assert.Throws<ArgumentNullException>(() => new Set<Test1>(null,new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Id) }, _connectionOptions));
            Assert.Throws<ArgumentNullException>(() => new Set<Test1>(model, null, new List<string> { nameof(Test1.Id) }, _connectionOptions));
            Assert.Throws<ArgumentNullException>(() => new Set<Test1>(model, new ClassOptions(typeof(Test1)), null, _connectionOptions));
            Assert.Throws<ArgumentNullException>(() => new Set<Test1>(model, new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Id) }, null));
        }

        [Fact]
        public void Should_generate_the_query()
        {
            Test1 model = new(1, null, DateTime.Now, true);
            Set<Test1> test = new(model, new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
               _connectionOptions);
            var query =test.Add(x => x.Id).Add(x => x.Create).Build();
            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.ConnectionOptions);
            Assert.NotNull(query.ConnectionOptions.Statements);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
        }

        [Fact]
        public void Should_generate_the_query2()
        {
            Set<Test1> test = new(new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Name) },_connectionOptions, null);
            var query = test.Add(x => x.Id, 1).Add(x => x.Create, DateTime.Now).Build();
            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.ConnectionOptions);
            Assert.NotNull(query.ConnectionOptions.Statements);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
        }


        [Fact]
        public void Should_get_the_where_query()
        {
            Test1 model = new(1, null, DateTime.Now, true);
            Set<Test1> test = new(model, new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
               _connectionOptions);
            var where = test.Add(x => x.Id).Add(x => x.Create).Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_get_the_where_query2()
        {
            Set<Test1> test = new(new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Name) }, _connectionOptions, null);
            var where = test.Add(x => x.Id, 1).Add(x => x.Create, DateTime.Now).Where();
            Assert.NotNull(where);
        }
    }
}
