using System.Linq.Expressions;

namespace CarRental.SharedKernel.PredicateBuilder
{
    class ParameterRebinder : ExpressionVisitor
    {
        readonly Dictionary<ParameterExpression, ParameterExpression> _map;

        private ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this._map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {

            if (_map.TryGetValue(node, out ParameterExpression replacement))
            {
                node = replacement;
            }

            return base.VisitParameter(node);
        }
    }
}
