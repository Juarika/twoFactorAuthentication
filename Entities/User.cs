using System.Data;

namespace twoFactorAuthentication.Entities;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public User()
    {
        DataAccess.cadenaConexion = @"Data Source=localhost;Initial Catalog=twoFactorDB;User=root;Password=123456;";
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