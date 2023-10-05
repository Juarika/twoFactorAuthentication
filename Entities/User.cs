using System.Data;

namespace twoFactorAuthentication.Entities;

public class User : BaseEntity
{
    public string UserName { get; set; }
    public string IdenNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>();
    public User()
    {
        DataAccess.cadenaConexion = @"Data Source=W11-EPV\SQLSERVER;Initial Catalog=EPV;Integrated Security=true;";
    }
    public void SetSecret(string email, string code)
    {
        DataAccess.ExecuteQuery($"UPDATE dbo.Usuarios SET TwoFactorSecret='{code}' WHERE email='{email}'");
    }
    public string GetSecret(string email)
    {
        DataTable dt = DataAccess.GetTmpDataTable($"SELECT TwoFactorSecret FROM dbo.Usuarios WHERE email='{email}'");
        if (dt.Rows.Count == 1)
        {
            return dt.Rows[0]["TwoFactorSecret"].ToString();
        }
        else
        {
            return "";
        }
    }
}