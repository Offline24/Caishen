namespace Caishen.Internals.KeyCreators
{
    public interface IKeyCreator
    {
        string CreateKey(object[] arguments);
    }
}