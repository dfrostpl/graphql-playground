using System;
using GraphQL.Language.AST;
using GraphQL.Types;

namespace CMS.Base.GraphQL.ScalarTypes
{
    public class GuidGraphType : ScalarGraphType
    {
        public GuidGraphType()
        {
            Name = "Guid";
            Description = "Globally Unique Identifier.";
        }

        public override object Serialize(object value)
        {
            return value?.ToString();
        }

        public override object ParseValue(object value)
        {
            var guid = value?.ToString().Replace("\"", "").Replace("\'", "");
            return string.IsNullOrWhiteSpace(guid) ? null : Guid.Parse(guid) as Guid?;
        }

        public override object ParseLiteral(IValue value)
        {
            if (value is StringValue str)
                return ParseValue(str.Value);
            return null;
        }
    }
}