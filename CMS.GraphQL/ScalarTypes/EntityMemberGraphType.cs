using System;
using GraphQL.Language.AST;
using GraphQL.Types;

namespace CMS.Base.GraphQL.ScalarTypes
{
    public class EntityMemberGraphType : ScalarGraphType
    {
        public override object Serialize(object value)
        {
            if (value is DateTime time)
                return time.ToString("O");
            return value;
        }

        public override object ParseValue(object value)
        {
            return value.ToString();
        }

        public override object ParseLiteral(IValue value)
        {
            return value.ToString();
        }
    }
}