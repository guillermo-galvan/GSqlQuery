using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using GSqlQuery.Extensions;

namespace GSqlQuery.Test.Helpers
{
    internal class ValidateColumns
    {
        private readonly List<string> _propertiesName;

        public int Count => _propertiesName.Count;

        public ValidateColumns(IEnumerable<MemberInfo> memberInfos)
        {
            _propertiesName = [];
            foreach (MemberInfo item in memberInfos)
            {
                _propertiesName.Add(item.Name);
            }
        }

        public void VerifyColumn(KeyValuePair<string, PropertyOptions> item)
        {
            Assert.Contains(item.Key, _propertiesName);
            Assert.NotNull(item.Value);
        }
    }

    internal class ValidateColumnsJoin
    {
        [Flags]
        private enum ValidateType
        {
            First = 0,
            Second = 1,
            Third = 2,
        }

        private readonly List<string> _firstTable;
        private readonly List<string> _secondTable;
        private readonly List<string> _thirdTable;
        private readonly ValidateType _validateType;

        public ValidateColumnsJoin(IFormats formats,ClassOptionsTupla<IEnumerable<MemberInfo>> memberInfoFirstable, ClassOptionsTupla<IEnumerable<MemberInfo>> memberInfoSecondTable, ClassOptionsTupla<IEnumerable<MemberInfo>> memberInfoThirdTable = null)
        {
            _firstTable = [];
            _secondTable = [];
            _thirdTable = [];
            _validateType = ValidateType.First | ValidateType.Second;

            foreach (var item in ExpressionExtension.GetPropertyQuery(memberInfoFirstable))
            {
                _firstTable.Add(formats.Format.Replace("{0}", $"{memberInfoFirstable.ClassOptions.Type.Name}_{item.Value.ColumnAttribute.Name}"));
            }

            foreach (var item in ExpressionExtension.GetPropertyQuery(memberInfoSecondTable))
            {
                _secondTable.Add(formats.Format.Replace("{0}", $"{memberInfoSecondTable.ClassOptions.Type.Name}_{item.Value.ColumnAttribute.Name}"));
            }

            if (memberInfoThirdTable != null && memberInfoThirdTable.MemberInfo.Any())
            {
                foreach (var item in ExpressionExtension.GetPropertyQuery(memberInfoThirdTable))
                {
                    _thirdTable.Add(formats.Format.Replace("{0}", $"{memberInfoThirdTable.ClassOptions.Type.Name}_{item.Value.ColumnAttribute.Name}"));
                }

                _validateType |= ValidateType.Third;
            }
        }

        public int Count => _firstTable.Count + _secondTable.Count + _thirdTable.Count;

        public void VerifyColumn(KeyValuePair<string, PropertyOptions> item)
        {
            bool isFind = false;

            isFind = _firstTable.Contains(item.Key);

            if (!isFind && (_validateType & ValidateType.Second) == ValidateType.Second)
            {
                isFind = _secondTable.Contains(item.Key);
            }

            if (!isFind && (_validateType & ValidateType.Third) == ValidateType.Third)
            {
                isFind = _thirdTable.Contains(item.Key);
            }

            Assert.True(isFind);
            Assert.NotNull(item.Value);
        }
    }
}
