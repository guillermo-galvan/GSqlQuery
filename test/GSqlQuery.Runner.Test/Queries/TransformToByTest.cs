using GSqlQuery.Runner.Test.Models;
using GSqlQuery.Runner.Transforms;
using System;
using Xunit;

namespace GSqlQuery.Runner.Test.Queries
{
    public class TransformToByTest
    {
        [Theory]
        [InlineData(1, "Name", 1, true)]
        [InlineData(2, "Name1", 1, false)]
        [InlineData(3, "", 1, true)]
        [InlineData(4, null, 1, false)]
        [InlineData(5, "Test", 1, true)]
        [InlineData(6, "Test 1", 1, false)]
        public void Should_generate_object_by_constructor(int id, string name, int increaseDays, bool isTest)
        {
            DateTime create = DateTime.Now.AddDays(increaseDays);
            TransformToByConstructor<Test1> transformToBy = new TransformToByConstructor<Test1>(4);

            transformToBy.SetValue(0, nameof(Test1.Id), id);
            transformToBy.SetValue(1, nameof(Test1.Name), name);
            transformToBy.SetValue(2, nameof(Test1.Create), create);
            transformToBy.SetValue(3, nameof(Test1.IsTest), isTest);

            var result = transformToBy.Generate();

            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(name, result.Name);
            Assert.Equal(create, result.Create);
            Assert.Equal(isTest, result.IsTest);
        }

        [Theory]
        [InlineData(1, "Name", 1, true)]
        [InlineData(2, "Name1", 1, false)]
        [InlineData(3, "", 1, true)]
        [InlineData(4, null, 1, false)]
        [InlineData(5, "Test", 1, true)]
        [InlineData(6, "Test 1", 1, false)]
        public void Should_generate_object(int id, string name, int increaseDays, bool isTest)
        {
            DateTime create = DateTime.Now.AddDays(increaseDays);
            TransformToByField<Test4> transformToBy = new TransformToByField<Test4>(4);

            transformToBy.SetValue(0, nameof(Test4.Ids), id);
            transformToBy.SetValue(1, nameof(Test4.Names), name);
            transformToBy.SetValue(2, nameof(Test4.Creates), create);
            transformToBy.SetValue(3, nameof(Test4.IsTests), isTest);

            var result = transformToBy.Generate();

            Assert.NotNull(result);
            Assert.Equal(id, result.Ids);
            Assert.Equal(name, result.Names);
            Assert.Equal(create, result.Creates);
            Assert.Equal(isTest, result.IsTests);
        }
    }
}
