using UnityEngine;
using Npgsql;
using System;
using System.Data;

public static class DatabaseManager
{
    private static string host = "localhost"; 
    private static string database = "KURS2.0";
    private static string username = "postgres";
    private static string password = "1"; 

    private static NpgsqlConnection connection;

  
    private static void OpenConnection()
    {
        string connectionString = $"Host={host};Username={username};Password={1};Database={database}";
        connection = new NpgsqlConnection(connectionString);
        try
        {
            connection.Open();
            Debug.Log("Connection opened successfully");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to open connection: {ex.Message}");
        }
    }

    // Метод для закрытия соединения
    private static void CloseConnection()
    {
        if (connection != null && connection.State == ConnectionState.Open)
        {
            connection.Close();
            Debug.Log("Connection closed successfully");
        }
    }
    public static void AddOrUpdatePlayerTotalPoints(int playerId, int totalPoints)
    {
        OpenConnection();
        string query = @"
            INSERT INTO PlayerTotalPoints (player_id, total_points)
            VALUES (@player_id, @total_points)
            ON CONFLICT (player_id) 
            DO UPDATE SET 
                total_points = EXCLUDED.total_points";

        using (var command = new NpgsqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("player_id", playerId);
            command.Parameters.AddWithValue("total_points", totalPoints);
            command.ExecuteNonQuery();
        }
        CloseConnection();
    }

    // Метод для получения данных о фруктах
    public static void GetFruitPoints()
    {
        OpenConnection();
        string query = "SELECT name, points FROM Fruits";

        using (var command = new NpgsqlCommand(query, connection))
        {
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string fruitName = reader.GetString(0);
                    int points = reader.GetInt32(1);
                    Debug.Log($"Fruit: {fruitName}, Points: {points}");
                    
                }
                
            }
        }
        CloseConnection();
    }
    // Метод для получения очков фрукта по его ID
    public static int GetFruitPointsById(int fruitId)
    {
        OpenConnection();
        string query = "SELECT points FROM Fruits WHERE fruit_id = @fruit_id";
        int points = 0;

        using (var command = new NpgsqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("fruit_id", fruitId);
            object result = command.ExecuteScalar();
            if (result != DBNull.Value)
            {
                points = Convert.ToInt32(result);
            }
        }
        CloseConnection();

        return points;
    }

    // Метод для получения очков игрока
    public static int GetPlayerPoints(int playerId)
    {
        OpenConnection();
        string query = "SELECT SUM(total_points) FROM PlayerFruitPoints WHERE player_id = @player_id";
        int totalPoints = 0;
        try
        {
            using (var command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("player_id", playerId);
                totalPoints = Convert.ToInt32(command.ExecuteScalar());
            }
        }
        catch
        {
            Debug.Log("error with database");
        }
        CloseConnection();

        return totalPoints;
    }

   
}