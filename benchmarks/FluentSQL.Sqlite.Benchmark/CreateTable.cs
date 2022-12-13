﻿using FluentSQL.DatabaseManagement;
using FluentSQL.Sqlite.Benchmark.Entities;
using FluentSQL.SqliteTest.Data;
using Microsoft.Data.Sqlite;

namespace FluentSQL.Sqlite.Benchmark
{
    internal static class CreateTable
    {
        internal const string ConnectionString = "Data Source=benchmark.db";

        internal static SqliteConnectionOptions GetConnectionOptions()
        { 
            return new SqliteConnectionOptions(ConnectionString);
        }

        internal static void Create()
        {
            var tables = SqliteSchema.Select(GetConnectionOptions()).Build().Execute();
            if(tables != null && !tables.Any()) 
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();

               using var createCommand = connection.CreateCommand();
                createCommand.CommandText =
                    @"
                       CREATE TABLE ""test1"" (
	                        ""idTest1""	NUMERIC NOT NULL,
	                        ""Money""	TEXT,
	                        ""Nombre""	TEXT,
	                        ""GUID""	TEXT,
	                        ""URL""	TEXT
                        );
                    ";
                createCommand.ExecuteNonQuery();

                createCommand.CommandText =
                    @"
                       CREATE TABLE ""test2"" (
	                        ""Id""	INTEGER NOT NULL,
	                        ""Money""	TEXT,
	                        ""IsBool""	INTEGER,
	                        ""Time""	TEXT,
	                        PRIMARY KEY(""Id"" AUTOINCREMENT)
                        );
                    ";
                createCommand.ExecuteNonQuery();
                connection.Close();
            }
        }

        internal static void CreateData()
        {
            var connectionOptions = GetConnectionOptions();
            using var connection = connectionOptions.DatabaseManagment.GetConnection();
            using var transaction = connection.BeginTransaction();
            var batch = Execute.BatchExecuteFactory(connectionOptions);

            Test2 test = new() { IsBool = false, Money = 200m, Time = DateTime.Now };
            for (int i = 0; i < 1000; i++)
            {
                test.IsBool = (i % 2) == 0;
                batch.Add(sb => test.Insert(sb).Build());
            }

            int result = batch.Execute(transaction.Connection);

            transaction.Commit();
            connection.Close();
        }
    }
}