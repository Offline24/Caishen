namespace Caishen.Internals.KeyCreators.ArgumentSerializers
{
    internal interface IArgumentSerializer
    {
        byte[] Serialize(object[] arguments);
    }
}