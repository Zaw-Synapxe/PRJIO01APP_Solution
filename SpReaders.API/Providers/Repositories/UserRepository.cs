using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SpReaders.API.Contracts.DTO;
using SpReaders.API.Models;
using System.Data;

namespace SpReaders.API.Providers.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext context;

        public UserRepository(AppDbContext _context)
        {
            _context = context;
        }

        public async Task<int> GetOrCreateUserAsync(UserProfile id)
        {
            if (id != null)
            {
                // create parameters to pass to the exec Stored Procedure statement
                // and bind the parameters with the values passed
                var emailAddressParam = new SqlParameter("@emailAddress", id.EmailAddress);
                var passwordParam = new SqlParameter("@passwordHash", id.PasswordHash);

                // run the statement to exec Stored Procedure inside the database
                // and capture the return values
                var users = await context.UserProfiles
                    .FromSqlRaw(
                        "exec sp_GetUsers @emailAddress, @passwordHash",
                        emailAddressParam,
                        passwordParam
                    )
                    .ToListAsync();

                /*
                // The above Stored Procedure does the same job
                // as what the below code does

                var users = context
                    .UserProfiles
                    .Where(x => x.EmailAddress == id.EmailAddress
                        && x.PasswordHash == id.PasswordHash).ToList();
                    */

                if (users.Count > 0)
                {
                    return users.First().UserProfileId;
                }
                else
                {
                    // Create a New User with the passed Details
                }

                return 0;
            }

            return -1;
        }

        //public async Task<int> GetOrCreateUserAsync(UserProfile id)
        //{
        //    if (id != null)
        //    {
        //        // create parameters to pass to the exec Stored Procedure statement
        //        // and bind the parameters with the values passed
        //        var emailAddressParam = new SqlParameter("@emailAddress", id.EmailAddress);
        //        var passwordParam = new SqlParameter("@passwordHash", id.PasswordHash);

        //        // run the statement to exec Stored Procedure inside the database
        //        // and capture the return values
        //        var users = context.UserProfiles
        //            .FromSqlRaw(
        //                "exec sp_GetUsers @emailAddress, @passwordHash",
        //                emailAddressParam,
        //                passwordParam
        //            )
        //            .ToList();

        //        if (users.Count > 0)
        //        {
        //            return users.First().Id;
        //        }
        //        else
        //        {
        //            /*
        //                // The above Stored Procedure does the same job
        //                // as what the below code does
        //                var user = id;
        //                await context.UserProfiles.AddAsync(user);
        //                await context.SaveChangesAsync();
        //                return user.Id;
        //            */

        //            // the emailAddress and passwordHash params are reused
        //            // from their previous declaration in the method

        //            // define SqlParameters for the other two params to be passed
        //            var oidProviderParam = new SqlParameter("@oidProvider", id.OIdProvider);
        //            var oidParam = new SqlParameter("@oid", string.IsNullOrEmpty(id.OId) ? "" : id.OId);

        //            // define the output parameter that needs to be retained
        //            // for the Id created when the Stored Procedure executes
        //            // the INSERT command
        //            var userIdParam = new SqlParameter("@Id", SqlDbType.Int);

        //            // the direction defines what kind of parameter we're passing
        //            // it can be one of:
        //            // Input
        //            // Output
        //            // InputOutput -- which does pass a value to Stored Procedure and retains a new state
        //            userIdParam.Direction = ParameterDirection.Output;

        //            // we can also use context.Database.ExecuteSqlCommand() or awaitable ExecuteSqlCommandAsync()
        //            // which also produces the same result - but the method is now marked obsolete
        //            // so we use ExecuteSqlRawAsync() instead

        //            // we're using the awaitable version since GetOrCreateUserAsync() method is marked async
        //            await context.Database.ExecuteSqlRawAsync(
        //                "exec sp_CreateUser @emailAddress, @passwordHash, @oidProvider, @oid, @Id out",
        //                emailAddressParam,
        //                passwordParam,
        //                oidProviderParam,
        //                oidParam,
        //                userIdParam
        //            );

        //            // the userIdParam which represents the Output param
        //            // now holds the Id of the new user and is an Object type
        //            // so we convert it to an Integer and send
        //            return Convert.ToInt32(userIdParam.Value);
        //        }

        //        return id;
        //    }

        //    return -1;
        //}

        public async Task<List<UserRolesDTO>> GetUserRolesByProfileId(int userProfileId)
        {
            // you must not CLOSE this connection, as it is used by Entity Framework Core
            //SqlConnection dbConnection = (SqlConnection)context.Database.GetDbConnection();

            //using (SqlCommand cmd = new SqlCommand("sp_GetUserRoles", dbConnection))
            //{
            //    SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            //    adapt.SelectCommand.CommandType = CommandType.StoredProcedure;
            //    adapt.SelectCommand.Parameters.Add(new SqlParameter("@ProfileId", SqlDbType.Int));
            //    adapt.SelectCommand.Parameters["@ProfileId"].Value = userProfileId;

            //    // fill the data table - no need to explicitly call `conn.Open()` -
            //    // the SqlDataAdapter automatically does this (and closes the connection, too)
            //    DataTable dt = new DataTable();
            //    adapt.Fill(dt);

            //    var userRolesResult = new List<UserRolesDTO>();

            //    if (dt.Rows.Count > 0)
            //    {
            //        foreach (DataRow row in dt.Rows)
            //        {
            //            var UserProfileId = row["UserProfileId"].ToString();
            //            var UserRoleId = row["UserRoleId"].ToString();
            //            var RoleName = row["RoleName"].ToString();
            //            var CreatedOn = row["CreatedOn"].ToString();

            //            userRolesResult.Add(
            //                new UserRolesDTO
            //                {
            //                    UserProfileId = Convert.ToInt32(UserProfileId),
            //                    UserRoleId = Convert.ToInt32(UserRoleId),
            //                    CreatedOn = Convert.ToDateTime(CreatedOn),
            //                    RoleName = RoleName
            //                }
            //            );
            //        }
            //    }

            //    return userRolesResult;
            //}

            string connectionString = GetConnectionString();

            using (SqlConnection awConnection = new SqlConnection(connectionString))
            {
                await awConnection.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("sp_GetUserRoles", awConnection))
                {
                    SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                    adapt.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapt.SelectCommand.Parameters.Add(new SqlParameter("@ProfileId", SqlDbType.Int));
                    adapt.SelectCommand.Parameters["@ProfileId"].Value = userProfileId;

                    // fill the data table - no need to explicitly call `conn.Open()` -
                    // the SqlDataAdapter automatically does this (and closes the connection, too)
                    DataTable dt = new DataTable();
                    adapt.Fill(dt);

                    var userRolesResult = new List<UserRolesDTO>();

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            var UserProfileId = row["UserProfileId"].ToString();
                            var UserRoleId = row["UserRoleId"].ToString();
                            var RoleName = row["RoleName"].ToString();
                            var CreatedOn = row["CreatedOn"].ToString();

                            userRolesResult.Add(
                                new UserRolesDTO
                                {
                                    UserProfileId = Convert.ToInt32(UserProfileId),
                                    UserRoleId = Convert.ToInt32(UserRoleId),
                                    CreatedOn = Convert.ToDateTime(CreatedOn),
                                    RoleName = RoleName
                                }
                            );
                        }
                    }

                    return userRolesResult;
                }


            }

        }

        private static string GetConnectionString()
        {
            // To avoid storing the connection string in your code, you can retrieve it from a configuration file.
            return "Data Source=(local);Integrated Security=SSPI;Initial Catalog=AdventureWorks;MultipleActiveResultSets=True";
        }


        // https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/asynchronous-programming
    }
}
