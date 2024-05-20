using UnityEngine;
using Npgsql;
using System;
using System.Data;

public class DatabaseManager : MonoBehaviour
{
    private string host = "localhost"; // ����� ������� PostgreSQL
    private string database = "your_database_name"; // ��� ���� ������
    private string username = "your_username"; // ��� ������������ PostgreSQL
    private string password = "your_password"; // ������ ������������ PostgreSQL

    private NpgsqlConnection connection;

    // ����� ��� �������� ����������
    private void OpenConnection()
    {
        string connectionString = $"Host={host};Username={username};Password={password};Database={database}";
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

    // ����� ��� �������� ����������
    private void CloseConnection()
    {
        if (connection != null && connection.State == ConnectionState.Open)
        {
            connection.Close();
            Debug.Log("Connection closed successfully");
        }
    }

    // ����� ��� ��������� ������ � �������
    public void GetFruitPoints()
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

    // ����� ��� ��������� ����� ������
    public int GetPlayerPoints(int playerId)
    {
        OpenConnection();
        string query = "SELECT SUM(total_points) FROM PlayerFruitPoints WHERE player_id = @player_id";
        int totalPoints = 0;

        using (var command = new NpgsqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("player_id", playerId);
            totalPoints = Convert.ToInt32(command.ExecuteScalar());
        }
        CloseConnection();

        return totalPoints;
    }

    // ������ ������ �������
    private void Start()
    {
        GetFruitPoints();
        int playerPoints = GetPlayerPoints(1); // ��������� ����� ��� ������ � ID 1
        Debug.Log($"Total points for player 1: {playerPoints}");
    }
}