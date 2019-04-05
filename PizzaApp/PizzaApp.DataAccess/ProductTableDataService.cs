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
    public class ProductTableDataService
    {
        private readonly string _connectionString;
        private readonly string _ownerName;
        private readonly DbProviderFactory _providerFactory;


        public ProductTableDataService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["mainAppConnectionString"].ConnectionString;
            _ownerName = ConfigurationManager.AppSettings["ownerName"];
            _providerFactory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["mainAppConnectionString"].ProviderName);
        }
        public List<Product> GetAll()
        {
            var data = new List<Product>();

            using (var connection = _providerFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.ConnectionString = _connectionString;
                    connection.Open();
                    command.CommandText = "select * from Products";

                    var dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        //int id = (int)dataReader["Id"];
                        string name = dataReader["Name"].ToString();
                        string description = dataReader["Description"].ToString();
                        int price = (int)dataReader["Price"];
                        data.Add(new Product { Name = name, Description = description, Price = price });
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

        public void Add(Product product)
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
                    command.CommandText = $"insert into Products values(@name, @description, @price)";

                    DbParameter nameParameter = command.CreateParameter();
                    nameParameter.ParameterName = "@name";
                    nameParameter.DbType = System.Data.DbType.String;
                    nameParameter.Value = product.Name;

                    DbParameter descriptionParameter = command.CreateParameter();
                    descriptionParameter.ParameterName = "@description";
                    descriptionParameter.DbType = System.Data.DbType.String;
                    descriptionParameter.Value = product.Description;

                    DbParameter priceParameter = command.CreateParameter();
                    priceParameter.ParameterName = "@price";
                    priceParameter.DbType = System.Data.DbType.Int32;
                    priceParameter.Value = product.Price;

                    command.Parameters.AddRange(new DbParameter[] { nameParameter, descriptionParameter, priceParameter });
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
