using MySqlConnector;

namespace SAT.Utils
{
    public static class DatabaseHelper
    {
        public static bool TestarConexao(string connectionString, out string erro)
        {
            erro = "";
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
                return false;
            }
        }
    }
}
