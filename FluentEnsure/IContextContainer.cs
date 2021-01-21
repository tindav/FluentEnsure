namespace FluentEnsure
{
    public interface IContextContainer<out T>
    {
        T Context { get; }
    }
}
