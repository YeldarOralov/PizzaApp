using PizzaApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.DataAccess
{
    public class UsersTableDataService
    {
        private readonly string _connectionString;
        private readonly string _ownerName;
        private readonly DbProviderFactory _providerFactory;


        public UsersTableDataService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["mainAppConnectionString"].ConnectionString;
            _ownerName = ConfigurationManager.AppSettings["ownerName"];
            _providerFactory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["mainAppConnectionString"].ProviderName);
        }
        public List<User> GetAll()
        {
            var data = new List<User>();

            using (var connection = _providerFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.ConnectionString = _connectionString;
                    connection.Open();
                    command.CommandText = "select * from Users";

                    var dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        //int id = (int)dataReader["Id"];
                        string name = dataReader["Name"].ToString();
                        string phoneNumber = dataReader["PhoneNumber"].ToString();
                        string password = dataReader["Password"].ToString();
                        data.Add(new User { Name = name, PhoneNumber = phoneNumber, Password = password });
                    }
                    dataReader.Close();
                }
                catch (DbException exception)
                {
                    //ToDo obrabotka
                    throw;
                }
                catch (Exception exception)
                {
                    //ToDo obrabotka
                    throw;
                }
            }
            return data;
        }

        public void Add(User user)
        {
            using (var connection = _providerFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                DbTransaction transaction = null;
                try
                {
                    connection.ConnectionString = _connectionString;
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    command.Transaction = transaction;
                    command.CommandText = $"insert into Users values(@name, @phoneNumber, @password)";

                    DbParameter nameParameter = command.CreateParameter();
                    nameParameter.ParameterName = "@name";
                    nameParameter.DbType = System.Data.DbType.String;
                    nameParameter.Value = user.Name;

                    DbParameter phoneNumberParameter = command.CreateParameter();
                    phoneNumberParameter.ParameterName = "@phoneNumber";
                    phoneNumberParameter.DbType = System.Data.DbType.String;
                    phoneNumberParameter.Value = user.PhoneNumber;                    

                    DbParameter passwordParameter = command.CreateParameter();
                    passwordParameter.ParameterName = "@password";
                    passwordParameter.DbType = System.Data.DbType.String;
                    passwordParameter.Value = user.Password;

                    command.Parameters.AddRange(new DbParameter[] { nameParameter,phoneNumberParameter, passwordParameter });
                    var affectedRows = command.ExecuteNonQuery();

                    if (affectedRows < 1) throw new Exception("Вставка не удалась");

                    transaction.Commit();
                }
                catch (DbException exception)
                {
                    transaction?.Rollback();
                    //ToDo obrabotka
                    throw;
                }
                catch (Exception exception)
                {
                    transaction?.Rollback();
                    //ToDo obrabotka
                    throw;
                }
                finally
                {
                    transaction?.Dispose();
                }
            }
        }

        public void DeleteById(int id)
        {

        }

        public void Update(User user)
        {

        }

    }
}
