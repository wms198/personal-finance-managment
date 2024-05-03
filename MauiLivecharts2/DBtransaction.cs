using System;
using Accessibility;
using MySqlConnector;
namespace MauiLivecharts2;

public class Categoryresult
{
    public string name;
    public decimal value;

    public Categoryresult(string name, decimal value)
    {
        this.name = name;
        this.value = value;
    }
}

public class DBtransaction
{
    private static string connectionString = "Server=127.0.0.1;Database=accountBook;User=root;Password=;";
    private static MySqlConnection connection;
    public static void Verbinden()
    {
        try
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Verbindung erfolgreich!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            connection.Close();
            Console.WriteLine("close");
        }
    }

    public static List<Categoryresult> CatgoriesIncomes()
    {
        connection = new MySqlConnection(connectionString);
        connection.Open();
        string sql = "SELECT category, SUM(value) as value FROM transaction where value > 0 GROUP by category;";
        MySqlCommand command = new MySqlCommand(sql, connection);
        MySqlDataReader reader = command.ExecuteReader();
        List<Categoryresult> categories = new List<Categoryresult>();
        while (reader.Read())
        {
            string category = (string)reader["category"];
            decimal value = (decimal)reader["value"];
            categories.Add(new Categoryresult(category, value));
        }
        Console.WriteLine("Incomes Piechart");
        connection.Dispose();
        return categories;
    }
    public static List<Categoryresult> CatgoriesExpenses()
    {
        connection = new MySqlConnection(connectionString);
        connection.Open();
        Console.WriteLine("Connection is successful for Select");
        string sql = "SELECT category, sum(abs(value)) as value FROM transaction  where value <= 0 group by category;";
        MySqlCommand command = new MySqlCommand(sql, connection);
        MySqlDataReader reader = command.ExecuteReader();
        List<Categoryresult> categories = new List<Categoryresult>();
        while (reader.Read())
        {
            string category =(string)reader["category"];
            decimal value = (decimal)reader["value"];
            categories.Add(new Categoryresult(category, value));
        }
        connection.Dispose();
        return categories;
    }
    public static List<Grouped_list> SelectByDate()
    {
        connection = new MySqlConnection(connectionString);
        connection.Open();
        Console.WriteLine("Connection is successful for Select");
        string sql = "SELECT * FROM transaction order by date ASC;";
        MySqlCommand command = new MySqlCommand(sql, connection);
        MySqlDataReader reader = command.ExecuteReader();
        List<Transaction> push_dateList = new List<Transaction>();
        while (reader.Read())
        {
            int id = (int)reader["id"];
            string category =(string)reader["category"];
            decimal value = (decimal)reader["value"];
            DateTime date = (DateTime)reader["date"];
            string imageUrl = (string)reader["category"]+".png";
            Transaction push_date = new Transaction(id, category, value, date, imageUrl); 
            push_dateList.Add(push_date);
        }
        connection.Dispose();
        Console.WriteLine(push_dateList);
        var dict= push_dateList.GroupBy(o => o.Date)
            .ToDictionary(g => g.Key, g => g.ToList());
        List<Grouped_list> dateList = new List<Grouped_list>();
        foreach (KeyValuePair<DateTime, List<Transaction>> item in dict)
        {
            dateList.Add(new Grouped_list(item.Key,new List<Transaction>(item.Value)));
        }
        
        return dateList;
    }
    public static List<Transaction> SelectTransactions()
    {
        connection = new MySqlConnection(connectionString);
        connection.Open();
        Console.WriteLine("Connection is successful for Select");
        string sql = "SELECT * FROM transaction order by date desc;";
        MySqlCommand command = new MySqlCommand(sql, connection);
        MySqlDataReader reader = command.ExecuteReader();
        List<Transaction> transactionList = new List<Transaction>();
        while (reader.Read())
        {
            /*Console.WriteLine($"ID : {reader["id"]}");
            Console.WriteLine($"Category: {reader["category"]}");
            Console.WriteLine($"Price : {reader["value"]}");
            Console.WriteLine($"Date : {reader["date"]}");
            Console.WriteLine($"ImageUrl : {reader["category"]}.png");*/
            int id = (int)reader["id"];
            string category =(string)reader["category"];
            decimal value = (decimal)reader["value"];
            DateTime date = (DateTime)reader["date"];
            string imageUrl = (string)reader["category"]+".png";
            Transaction transaction = new Transaction(id, category, value, date, imageUrl);
            transactionList.Add(transaction);
        }
        connection.Dispose();
        return transactionList;
    }
    public static void InsertTransaction(Transaction transaction)
    {
        connection = new MySqlConnection(connectionString);
        connection.Open();
        Console.WriteLine("Verbindung erfolgreich for Insert!");
        Console.WriteLine(transaction.Category);
        Console.WriteLine(transaction.Value);
        Console.WriteLine(transaction.Date);
        Console.WriteLine(transaction.ImageUrl);
        string sql = "INSERT INTO transaction(category, value, date, imageUrl)" +
                     " VALUES (@category,@amout,@date, @imageUrl);";
        MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("category", transaction.Category);
        command.Parameters.AddWithValue("amout", transaction.Value);
        command.Parameters.AddWithValue("date", transaction.Date);
        command.Parameters.AddWithValue("imageUrl", transaction.ImageUrl);
        
        int rowsAffected = command.ExecuteNonQuery();

        if (rowsAffected > 0)
        {
            Console.WriteLine("Transaction has been added");
        }
        else
        {
            Console.WriteLine("Database did not add the transaction.");
        }
        connection.Dispose();
    }

    public static void DeleteTransaction(Transaction transaction)
    {
        connection = new MySqlConnection(connectionString);
        connection.Open();
        Console.WriteLine($"Id: {transaction.Id}");
        string sql = $"DELETE FROM transaction WHERE id = {transaction.Id}";
        MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("id", transaction.Id);
        int rowAffect = command.ExecuteNonQuery();
        if (rowAffect > 0)
        {
            Console.WriteLine("Item has been deleted.");
        }
        else
        {
            Console.WriteLine("Deleted failed");   
        }
        connection.Dispose();
    }

    public static void UpdateTransaction(Transaction transaction)
    {
        connection = new MySqlConnection(connectionString);
        connection.Open();
        Console.WriteLine(transaction.Id);
        Console.WriteLine(transaction.Value);
        Console.WriteLine(transaction.Date);
        string sql = "UPDATE transaction SET value= @value, date= @date " +
                     "WHERE id = @id;";
        MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("id", transaction.Id);
        command.Parameters.AddWithValue("value", transaction.Value);
        command.Parameters.AddWithValue("date", transaction.Date);
        int roeAffection = command.ExecuteNonQuery();
        if (roeAffection > 0)
        {
            Console.WriteLine("Update is successful");
        }else
        {
            Console.WriteLine("Update is failed.");
        }
        connection.Dispose();
    }
    
}