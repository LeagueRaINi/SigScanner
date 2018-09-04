namespace SigScanner
{
    public interface ISignature
    {
        int Offset { get; }

        byte[] GetPattern();
        string GetMask();
    }
}