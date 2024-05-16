namespace ReportAndSolveAPI.Security
{
    public interface IHashGenerator
    {
        public string IncryptData(string data);

        public bool VerifyHash(string data, string hash);
    }
}
