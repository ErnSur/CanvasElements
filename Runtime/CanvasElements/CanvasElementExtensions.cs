namespace QuickEye.Utility
{
    public static class CanvasElementExtensions
    {
        public static T AddNewInitialized<T, TContext>(this Container<T> container, TContext context)
            where T : CanvasElement<TContext>
        {
            var item = container.AddNew();
            item.Initialize(context);
            return item;
        }
    }
}