namespace FluentEnsure.Sdk
{
    internal abstract class ContextContainerBase<T> : IContextContainer<T>
    {
        public T Context { get; }

        protected ContextContainerBase(T context)
        {
            Context = context;
        }
    }
}
