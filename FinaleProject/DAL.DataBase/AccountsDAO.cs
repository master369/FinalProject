using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entities;
using DAL.Interfaces;
using System.Configuration;
using System.Data.SqlClient;
using log4net;

namespace DAL.DataBase
{
   public class AccountsDAO : IAccountsDAO
    {
        readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string _connectionString;
        private Dictionary<int, string> _roleContainer;

        public AccountsDAO()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            _roleContainer = new Dictionary<int, string>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))//сохранили все возможные роли
                {
                    var command = new SqlCommand("SELECT [Id], [Name] FROM dbo.[Roles]", connection);
                    connection.Open();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        _roleContainer[(int)reader["Id"]] = (string)reader["Name"];
                    }
                }
            }
            catch (Exception)
            {
                logger.Error("Ошибка при получении всех возможных ролей из базы данных!");
            }

        }
        public bool Add(Account user)
        {
            if (Get(user.Name) != null) return false;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var commandSelect = new SqlCommand("SELECT [Login], [Password] FROM dbo.[Accounts] WHERE [Login] = @Login", connection);
                    commandSelect.Parameters.AddWithValue("@Login", user.Name);
                    connection.Open();
                    var reader = commandSelect.ExecuteReader();

                    if (reader.Read() && reader["Login"] == user.Name) return false;
                    connection.Close();

                    var commandInsert = new SqlCommand("INSERT INTO dbo.[Accounts] ([Login], [Password]) VALUES (@Login, @Password) ", connection);
                    commandInsert.Parameters.AddWithValue("@Login", user.Name);
                    commandInsert.Parameters.AddWithValue("@Password", user.Password);
                    connection.Open();
                    var result = commandInsert.ExecuteNonQuery();
                    return true;

                }
            }
            catch (Exception)
            {
                logger.Error("Ошибка при добавлении пользователя в базу данных!");
            }
            return false;
        }

        public bool AddRole(string name, string role)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    //ПРОВЕРКА ЕСТЬ ЛИ ТАКОЙ ПОЛЬЗОВАТЕЛЬ В БД
                    #region FindUser

                    var command = new SqlCommand("SELECT [Login], [Password] FROM dbo.[Accounts] WHERE [Login] = @Login", connection);
                    command.Parameters.AddWithValue("@Login", name);
                    connection.Open();
                    var reader_ = command.ExecuteReader();

                    if (!reader_.Read()) return false;//если такого пользователя нет в БД
                    connection.Close();

                    #endregion

                    //ПРОВЕРКА ЕСЛИ ЛИ У ЭТОГО ПОЛЬЗОВАТЕЛЯ ТАКАЯ РОЛЬ В БД
                    #region Find_Role_For_User

                    var commandSelect = new SqlCommand("SELECT [AccountLogin], [RoleId] FROM dbo.[AccountsWithRoles] WHERE [AccountLogin] = @Login", connection);
                    commandSelect.Parameters.AddWithValue("@Login", name);
                    connection.Open();
                    var reader = commandSelect.ExecuteReader();
                    var roles = new List<int>();//контейнер для ролей у пользователя из БД
                    while (reader.Read())
                    {
                        roles.Add((int)reader["RoleId"]);
                    }
                    if (roles.Contains(_roleContainer.FirstOrDefault(x => x.Value == role).Key))
                        return false;//если такая роль уже есть у пользователя
                    connection.Close();

                    #endregion

                    //ДОБАВЛЕНИЕ РОЛИ ПОЛЬЗОВАТЕЛЮ
                    #region Add_Role

                    var commandInsert = new SqlCommand("INSERT INTO dbo.[AccountsWithRoles] ([AccountLogin], [RoleId]) VALUES (@Login, @Role) ", connection);
                    commandInsert.Parameters.AddWithValue("@Login", name);
                    commandInsert.Parameters.AddWithValue("@Role", _roleContainer.FirstOrDefault(x => x.Value == role).Key);
                    connection.Open();
                    var result = commandInsert.ExecuteNonQuery();
                    return true;

                    #endregion

                }
            }
            catch (Exception)
            {
                logger.Error("Ошибка при добавлении роли пользователю в базу данных!");
            }

            return false;
        }

        public void DeleteRole(string name, string role)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("DELETE FROM dbo.[AccountsWithRoles] WHERE [AccountLogin] = @Login And [RoleId] = @Role ", connection);
                    command.Parameters.AddWithValue("@Login", name);
                    command.Parameters.AddWithValue("@Role", _roleContainer.FirstOrDefault(x => x.Value == role).Key);
                    connection.Open();

                    var result = command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                logger.Error("Ошибка при удалении роли пользователя из базы данных!");
            }

        }

        public Account Get(string name)
        {
            Account ent = null;
            try
            {
                using (var connection = new SqlConnection(_connectionString))//запоминаем только пользователей с паролями
                {
                    var command = new SqlCommand("SELECT [Login], [Password] FROM dbo.[Accounts] WHERE [Login] = @Login", connection);
                    command.Parameters.AddWithValue("@Login", name);
                    connection.Open();
                    var reader = command.ExecuteReader();

                    if (!reader.Read()) return null;

                    ent = new Account((string)reader["Login"], (string)reader["Password"], new List<string>());
                }

                using (var connection = new SqlConnection(_connectionString))//для каждому пользователю присваиваем роли из БД
                {
                    var command = new SqlCommand("SELECT [AccountLogin], [Role_Id] FROM dbo.[AccountsWithRoles] WHERE [AccountLogin] = @Login", connection);
                    command.Parameters.AddWithValue("@Login", ent.Name);
                    connection.Open();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ent.Roles.Add(_roleContainer[(int)reader["Role_Id"]]);
                    }
                }
            }
            catch (Exception)
            {
                logger.Error("Ошибка при попытке получить данные пользователя из базы данных!");
            }


            return ent;

        }

        public IEnumerable<Account> GetAll()
        {
            List<Account> accounts = new List<Account>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("SELECT [Login] FROM dbo.[Accounts]", connection);
                    connection.Open();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        accounts.Add(new Account((string)reader["Login"], new List<string>()));
                    }
                }


                foreach (var item in accounts)
                {
                    using (var connection = new SqlConnection(_connectionString))//для каждому пользователю присваиваем роли из БД
                    {
                        var command = new SqlCommand("SELECT [AccountLogin], [RoleId] FROM dbo.[AccountsWithRoles] WHERE [AccountLogin] = @Login", connection);
                        command.Parameters.AddWithValue("@Login", item.Name);
                        connection.Open();
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            item.Roles.Add(_roleContainer[(int)reader["RoleId"]]);
                        }
                    }
                }
            }
            catch (Exception)
            {
                logger.Error("Ошибка при получении всех пользователей из базы данных!");
            }


            foreach (var item in accounts)
            {
                yield return item;
            }

        }


        public void EditPassword(string Login, string OldPassword, string NewPassword)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("UPDATE dbo.[Accounts] SET [Password] = @Password WHERE [Login] = @Login ", connection);
                    command.Parameters.AddWithValue("@Login", Login);
                    command.Parameters.AddWithValue("@Password", NewPassword);
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                logger.Error("Ошибка при редактировании роли пользователя в базе данных!");
            }

        }

        public bool IsUserInRole(string login, string roleName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmdReadRelationship = connection.CreateCommand();
                cmdReadRelationship.CommandText = @"SELECT Id FROM dbo.AccountRole WHERE ( 
                                                    Id_account=(SELECT Id FROM dbo.Accounts WHERE dbo.Accounts.Login = @Login) 
                                                    AND
                                                    Id_role=(SELECT Id FROM dbo.Roles WHERE dbo.Roles.Name = @Name)
                                                    )";
                cmdReadRelationship.Parameters.AddWithValue("@Login", login);
                cmdReadRelationship.Parameters.AddWithValue("@Name", roleName);
                connection.Open();
                var reader = cmdReadRelationship.ExecuteScalar();
                if (reader == null)
                {
                    return false;
                }
                return true;
            }
        }

        public bool IsUserRegistrated(string login)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmdReadUsers = connection.CreateCommand();
                cmdReadUsers.CommandText = @"SELECT Id FROM dbo.Accounts WHERE Login=@Login";
                cmdReadUsers.Parameters.AddWithValue("@Login", login);
                connection.Open();
                var reader = cmdReadUsers.ExecuteScalar();
                if (reader == null)
                {
                    return false;
                }
                return true;
            }
        }

        public bool IsUserRegistrated(string login, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmdReadUsers = connection.CreateCommand();
                cmdReadUsers.CommandText = @"SELECT Id FROM dbo.Accounts WHERE Login=@Login AND Password=@Password";
                cmdReadUsers.Parameters.AddWithValue("@Login", login);
                cmdReadUsers.Parameters.AddWithValue("@Password", password);
                connection.Open();
                var reader = cmdReadUsers.ExecuteScalar();
                if (reader == null)
                {
                    return false;
                }
                return true;
            }
        }

        public string[] GetRolesForUser(string name)
        {
            throw new NotImplementedException();
        }
    }
}
