using System;
using GraphQL.Language.AST;
using GraphQL.Types;

namespace CMS.GraphQL.ScalarTypes
{
    public class TypeGraphType : ScalarGraphType
    {
        public override object Serialize(object value)
        {
            return (value as Type)?.Name;
        }

        public override object ParseValue(object value)
        {
            return Type.GetType(value.ToString());
        }

        public override object ParseLiteral(IValue value)
        {
            return Type.GetType(value.ToString());
        }
    }
}