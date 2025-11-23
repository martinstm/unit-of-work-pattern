using Dapper.Core.UnitOfWorkPattern;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Core.RepositoryPattern
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;

        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Defines the table name by default as the name of <see cref="T"/>.
        /// </summary>
        public virtual string TableName => typeof(T).Name;

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _unitOfWork.Connection.QueryAsync<T>($"SELECT * FROM {TableName}",
                transaction: _unitOfWork.Transaction);
        }

        public async Task<T> GetAsync(Guid id)
        {
            var result = await _unitOfWork.Connection.QuerySingleOrDefaultAsync<T>($"SELECT * FROM {TableName} WHERE Id=@Id",
                new { Id = id }, _unitOfWork.Transaction);

            if (result == null)
                throw new KeyNotFoundException($"{TableName} with id [{id}] could not be found.");

            return result;
        }

        public async Task InsertAsync(T t)
        {
            var insertQuery = GenerateInsertQuery();
            await _unitOfWork.Connection.ExecuteAsync(insertQuery, t, _unitOfWork.Transaction);
        }

        public async Task<int> InsertAsync(IEnumerable<T> list)
        {
            var inserted = 0;
            var query = GenerateInsertQuery();

            inserted += await _unitOfWork.Connection.ExecuteAsync(query, list, _unitOfWork.Transaction);

            return inserted;
        }

        public async Task UpdateAsync(T t)
        {
            var updateQuery = GenerateUpdateQuery();
            await _unitOfWork.Connection.ExecuteAsync(updateQuery, t, _unitOfWork.Transaction);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.Connection.ExecuteAsync($"DELETE FROM {TableName} WHERE Id=@Id",
                new { Id = id }, _unitOfWork.Transaction);
        }

        private IEnumerable<PropertyInfo> GetProperties => typeof(T).GetProperties();

        private string GenerateInsertQuery()
        {
            var insertQuery = new StringBuilder($"INSERT INTO {TableName} ");

            insertQuery.Append("(");

            var properties = GenerateListOfProperties(GetProperties);
            properties.ForEach(prop => { insertQuery.Append($"[{prop}],"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(") VALUES (");

            properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(")");

            return insertQuery.ToString();
        }

        private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            return (from prop in listOfProperties
                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
                    select prop.Name).ToList();
        }

        private string GenerateUpdateQuery()
        {
            var updateQuery = new StringBuilder($"UPDATE {TableName} SET ");
            var properties = GenerateListOfProperties(GetProperties);

            properties.ForEach(property =>
            {
                if (!property.Equals("Id"))
                {
                    updateQuery.Append($"{property}=@{property},");
                }
            });

            updateQuery.Remove(updateQuery.Length - 1, 1); //remove last comma
            updateQuery.Append(" WHERE Id=@Id");

            return updateQuery.ToString();
        }
    }
}
