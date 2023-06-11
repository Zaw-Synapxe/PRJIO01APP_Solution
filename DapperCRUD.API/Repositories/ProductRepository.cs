﻿using Dapper;
using DapperCRUD.API.Domain;
using System.Data;

namespace DapperCRUD.API.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(IConfiguration configuration)
            : base(configuration)
        { }

        public async Task<List<Product>> GetAllAsync()
        {
            try
            {
                var query = "SELECT * FROM Products";
                using (var connection = CreateConnection())
                {
                    return (await connection.QueryAsync<Product>(query)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            try
            {
                var query = "SELECT * FROM Products WHERE Id = @Id";

                var parameters = new DynamicParameters();
                parameters.Add("Id", id, DbType.Int64);

                using (var connection = CreateConnection())
                {
                    return (await connection.QueryFirstOrDefaultAsync<Product>(query, parameters));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<int> CreateAsync(Product entity)
        {
            try
            {
                var query = "INSERT INTO Products (Name, Price, Quantity) VALUES (@Name, @Price, @Quantity)";

                var parameters = new DynamicParameters();
                parameters.Add("Name", entity.Name, DbType.String);
                parameters.Add("Price", entity.Price, DbType.Decimal);
                parameters.Add("Quantity", entity.Quantity, DbType.Int32);
                parameters.Add("CreatedOn", entity.CreatedOn, DbType.DateTime);
                parameters.Add("CreatedBy", entity.CreatedBy, DbType.String);

                using (var connection = CreateConnection())
                {
                    return (await connection.ExecuteAsync(query, parameters));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<int> UpdateAsync(Product entity)
        {
            try
            {
                var query = "UPDATE Products SET Name = @Name, Price = @Price, Quantity = @Quantity WHERE Id = @Id";

                var parameters = new DynamicParameters();
                parameters.Add("Name", entity.Name, DbType.String);
                parameters.Add("Price", entity.Price, DbType.Decimal);
                parameters.Add("Quantity", entity.Quantity, DbType.Int32);
                parameters.Add("Id", entity.Id, DbType.Int64);

                using (var connection = CreateConnection())
                {
                    return (await connection.ExecuteAsync(query, parameters));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<int> DeleteAsync(Product entity)
        {
            try
            {
                var query = "DELETE FROM Products WHERE Id = @Id";

                var parameters = new DynamicParameters();
                parameters.Add("Id", entity.Id, DbType.Int64);

                using (var connection = CreateConnection())
                {
                    return (await connection.ExecuteAsync(query, parameters));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<List<Product>> GetAllProductByNameAsync(string prodname)
        {
            try
            {
                var query = "SELECT * FROM Products Where [Name] Like '%" + prodname + "%'";
                using (var connection = CreateConnection())
                {
                    return (await connection.QueryAsync<Product>(query)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
