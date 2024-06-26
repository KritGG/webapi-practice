﻿using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace api.Models;

public partial class dbContext : DbContext
{
    private IDbContextTransaction _currentTransaction;
    public Task<int> ExecuteAsync(string sql, object param = null, CancellationToken token = default, bool isStore = false) => this.Database.GetDbConnection().ExecuteAsync(new CommandDefinition(sql, param, this._currentTransaction?.GetDbTransaction(), cancellationToken: token, commandType: isStore ? CommandType.StoredProcedure : CommandType.Text));

    public Task<T> ExecuteScalarAsync<T>(string sql, object param = null, CancellationToken token = default, bool isStore = false) => this.Database.GetDbConnection().ExecuteScalarAsync<T>(new CommandDefinition(sql, param, this._currentTransaction?.GetDbTransaction(), cancellationToken: token, commandType: isStore ? CommandType.StoredProcedure : CommandType.Text));

    public Task<T> QueryFirstAsync<T>(string sql, object param = null, CancellationToken token = default, bool isStore = false) => this.Database.GetDbConnection().QueryFirstAsync<T>(new CommandDefinition(sql, param, this._currentTransaction?.GetDbTransaction(), cancellationToken: token, commandType: isStore ? CommandType.StoredProcedure : CommandType.Text));

    public Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, CancellationToken token = default, bool isStore = false) => this.Database.GetDbConnection().QueryFirstOrDefaultAsync<T>(new CommandDefinition(sql, param, this._currentTransaction?.GetDbTransaction(), cancellationToken: token, commandType: isStore ? CommandType.StoredProcedure : CommandType.Text));

    public Task<T> QuerySingleAsync<T>(string sql, object param = null, CancellationToken token = default, bool isStore = false) => this.Database.GetDbConnection().QuerySingleAsync<T>(new CommandDefinition(sql, param, this._currentTransaction?.GetDbTransaction(), cancellationToken: token, commandType: isStore ? CommandType.StoredProcedure : CommandType.Text));

    public Task<T> QuerySingleOrDefaultAsync<T>(string sql, object param = null, CancellationToken token = default, bool isStore = false) => this.Database.GetDbConnection().QuerySingleOrDefaultAsync<T>(new CommandDefinition(sql, param, this._currentTransaction?.GetDbTransaction(), cancellationToken: token, commandType: isStore ? CommandType.StoredProcedure : CommandType.Text));

    public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, CancellationToken token = default, bool isStore = false) => this.Database.GetDbConnection().QueryAsync<T>(new CommandDefinition(sql, param, this._currentTransaction?.GetDbTransaction(), cancellationToken: token, commandType: isStore ? CommandType.StoredProcedure : CommandType.Text));

    public Task<T> GetParameterValue<T>(string group, string code, CancellationToken token) => this.Database.GetDbConnection().ExecuteScalarAsync<T>(new CommandDefinition("SELECT parameter_value FROM su_parameter WHERE parameter_group_code = @Group AND parameter_code = COALESCE(@Code,parameter_code)", new { Group = group, Code = code }, this._currentTransaction?.GetDbTransaction(), cancellationToken: token, commandType: CommandType.Text));

    public Task<IEnumerable<(string, T)>> GetParameterValues<T>(string group, CancellationToken token = default) => this.Database.GetDbConnection().QueryAsync<(string, T)>(new CommandDefinition("SELECT parameter_code,parameter_value FROM su.parameter WITH(NOLOCK) WHERE parameter_group_code = @Group", new { Group = group }, this._currentTransaction?.GetDbTransaction(), cancellationToken: token, commandType: CommandType.Text));

    public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, string splitOn = "Id", object param = null, CancellationToken token = default, bool isStore = false) => this.Database.GetDbConnection().QueryAsync<TFirst, TSecond, TReturn>(new CommandDefinition(sql, param, this._currentTransaction?.GetDbTransaction(), cancellationToken: token, commandType: isStore ? CommandType.StoredProcedure : CommandType.Text), map, splitOn);

    public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, string splitOn = "Id", object param = null, CancellationToken token = default, bool isStore = false) => this.Database.GetDbConnection().QueryAsync<TFirst, TSecond, TThird, TReturn>(new CommandDefinition(sql, param, this._currentTransaction?.GetDbTransaction(), cancellationToken: token, commandType: isStore ? CommandType.StoredProcedure : CommandType.Text), map, splitOn);
}