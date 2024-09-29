namespace ERC.Hub.Business.Common.Utilities
{
    public interface IPasswordHasher
    {
        bool Validate(string hashed, string password);
        string Hash(string password);
    }
}
