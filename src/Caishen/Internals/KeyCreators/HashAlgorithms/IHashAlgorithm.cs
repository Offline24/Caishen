namespace Caishen.Internals.KeyCreators.HashAlgorithms
{
    internal interface IHashAlgorithm
    {
        byte[] CreateHash(byte[] bytes);
    }
}