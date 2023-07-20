using Dreamy.Domain;
using Dreamy.Repository.Generic;
using Dreamy.Repository.Utilities;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using static Dapper.SqlMapper;

namespace Dreamy.Repository.Authen
{
    public class AuthRepository : GenericRepository<User>, IAuthRepository
    {
        private readonly IDbConnection db;
        public AuthRepository(IDbConnection _db) : base(_db)
        {
            db = _db;
        }

        public async Task Register(User userRegister)
        {
            string insertQuery = "INSERT INTO Users (FullName, GenderType, Dob, Avatar, Email, Phone, IdentityID, Address, Occupation, Country, Password, StoredSalt, StatusID, DateCreated, DateUpdated) " +
                "VALUES (@FullName, @GenderType, @Dob, @Avatar, @Email, @Phone, @IdentityID, @Address, @Occupation, @Country, @Password, @StoredSalt, @StatusID, @DateCreated, @DateUpdated)";
            using (SqlConnection _con = new SqlConnection(@"Server=.;Database=DREMY;Trusted_Connection=True;"))
            {
                using (SqlCommand _cmd = new SqlCommand(insertQuery, _con))
                {
                    _cmd.Parameters.AddWithValue("@FullName", userRegister.FullName);
                    _cmd.Parameters.AddWithValue("@GenderType", userRegister.GenderType == true ? 0 : 1);
                    _cmd.Parameters.AddWithValue("@Dob", userRegister.Dob);
                    _cmd.Parameters.AddWithValue("@Avatar", (object)userRegister.Avatar ?? DBNull.Value);
                    _cmd.Parameters.AddWithValue("@Email", userRegister.Email);
                    _cmd.Parameters.AddWithValue("@Phone", userRegister.Phone);
                    _cmd.Parameters.AddWithValue("@IdentityID", (object)userRegister.IdentityID ?? DBNull.Value);
                    _cmd.Parameters.AddWithValue("@Address", (object)userRegister.Address ?? DBNull.Value);
                    _cmd.Parameters.AddWithValue("@Occupation", (object)userRegister.Occupation ?? DBNull.Value);
                    _cmd.Parameters.AddWithValue("@Country", (object)userRegister.Country ?? DBNull.Value);
                    _cmd.Parameters.AddWithValue("@Password", userRegister.Password);
                    SqlParameter param = _cmd.Parameters.Add("@StoredSalt", SqlDbType.VarBinary);
                    param.Value = userRegister.StoredSalt;
                    _cmd.Parameters.AddWithValue("@StatusID", 1);
                    _cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                    _cmd.Parameters.AddWithValue("@DateUpdated", DateTime.Now);
                    _con.Open();
                    _cmd.ExecuteNonQuery();
                    _con.Close();
                }
            }
        }
    }
}
