using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GSqlQuery.Cache
{
    internal class ExpressionComparer : IEqualityComparer<Expression>
    {
        public bool Equals(Expression x, Expression y)
        {
            return ExpressionEqualityComparer.Instance.Equals(x, y);
        }

        public int GetHashCode(Expression obj)
        {
            return ExpressionEqualityComparer.Instance.GetHashCode(obj);
        }
    }

    internal class ExpressionEqualityComparer : ExpressionVisitor
    {
        private static readonly Lazy<ExpressionEqualityComparer> _instance = new Lazy<ExpressionEqualityComparer>(() => new ExpressionEqualityComparer());

        public static ExpressionEqualityComparer Instance => _instance.Value;

        private ExpressionEqualityComparer() { }

        public bool Equals(Expression x, Expression y)
        {
            return Visit(x, y);
        }

        public int GetHashCode(Expression obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            return obj.ToString().GetHashCode();
        }

        private bool Visit(Expression x, Expression y)
        {
            if (x == y) return true;
            if (x == null || y == null) return false;
            if (x.NodeType != y.NodeType || x.Type != y.Type) return false;

            switch (x.NodeType)
            {
                case ExpressionType.Constant:
                    return EqualsConstant((ConstantExpression)x, (ConstantExpression)y);
                case ExpressionType.MemberAccess:
                    return EqualsMemberAccess((MemberExpression)x, (MemberExpression)y);
                case ExpressionType.Lambda:
                    return EqualsLambda((LambdaExpression)x, (LambdaExpression)y);
                case ExpressionType.Parameter:
                    return EqualsParameter((ParameterExpression)x, (ParameterExpression)y);
                case ExpressionType.New:
                    return EqualsNew((NewExpression)x, (NewExpression)y);
                case ExpressionType.Convert:
                    return EqualsConvert((UnaryExpression)x, (UnaryExpression)y);
                default:
                    throw new NotImplementedException($"Tipo de expresión no manejado: '{x.NodeType}'");
            }
        }

        private bool EqualsConstant(ConstantExpression x, ConstantExpression y)
        {
            return Equals(x.Value, y.Value);
        }

        private bool EqualsMemberAccess(MemberExpression x, MemberExpression y)
        {
            return x.Member == y.Member && Visit(x.Expression, y.Expression);
        }

        private bool EqualsLambda(LambdaExpression x, LambdaExpression y)
        {
            if (x.Parameters.Count != y.Parameters.Count)
            {
                return false;
            }

            for (int i = 0; i < x.Parameters.Count; i++)
            {
                if (!Visit(x.Parameters[i], y.Parameters[i]))
                {
                    return false;
                }
            }

            return Visit(x.Body, y.Body);
        }

        private bool EqualsParameter(ParameterExpression x, ParameterExpression y)
        {
            return x.Name == y.Name && x.Type == y.Type;
        }

        private bool EqualsNew(NewExpression x, NewExpression y)
        {
            if (x.Constructor != y.Constructor)
            {
                return false;
            }

            if (x.Arguments.Count != y.Arguments.Count)
            {
                return false;
            }

            for (int i = 0; i < x.Arguments.Count; i++)
            {
                if (!Visit(x.Arguments[i], y.Arguments[i]))
                {
                    return false;
                }
            }

            return true;
        }

        private bool EqualsConvert(UnaryExpression x, UnaryExpression y)
        {
            return x.Method == y.Method && Visit(x.Operand, y.Operand);
        }
    }
}