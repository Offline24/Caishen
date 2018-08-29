namespace Caishen.Internals.KeyCreators.StringEncoders
{
    internal interface IStringEncoder
    {
        string BytesToString(byte[] bytes);
    }
}