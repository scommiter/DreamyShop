namespace DreamyShop.Common.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<TResult> GroupJoinWithDefault<TOuter, TInner, TKey, TResult>(
            this IEnumerable<TOuter> outer,
            IEnumerable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Func<TOuter, TInner, TResult> resultSelector)
        {
            return outer.GroupJoin(inner, outerKeySelector, innerKeySelector, (outerItem, innerItems) => new { outerItem, innerItems })
                .SelectMany(oi => oi.innerItems.DefaultIfEmpty(), (oi, ii) => resultSelector(oi.outerItem, ii));
        }
    }

}
