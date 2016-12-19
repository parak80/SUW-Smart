using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SMART
{
    class Program
    {

        //static string str = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Smart;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        static string str = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Smart;Integrated Security=True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        static SqlConnection con = new SqlConnection(str);
        static string PowerIsOn;
        static string AlarmSignal;
        static DateTime Time;
        static Random random = new Random();

        static void Main(string[] args)
        {

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            Task t = new Task(() => GenerateDataForPowerOn(token));
            t.Start();
            Console.WriteLine("Task has started");

            try
            {
                Console.WriteLine("Enter any key to stop");
                Console.ReadKey();
                cts.Cancel();
                //t.Wait();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();

        }

        static public void Post(string value)
        {
            Time = DateTime.Now;
            using (SqlConnection connection = new SqlConnection(str))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    //command.CommandText = @"UPDATE Alarm SET PowerIsOn = @PowerIsOn WHERE Id = @AlarmId";
                    command.CommandText = "INSERT INTO Alarm(PowerIsOn, AlarmSignal, Time) VALUES (' " + PowerIsOn + " ' ,' " + AlarmSignal + " ', ' " + Time + " ')";
                    command.Parameters.AddWithValue("@PowerIsOn", value);
                    command.Parameters.AddWithValue("@AlarmId", value);
                    command.Parameters.AddWithValue("@Time", value);
                    try
                    {
                        connection.Open();
                        int recordsAffected = command.ExecuteNonQuery();
                        if (recordsAffected <= 0)
                        {
                            Console.WriteLine("No records effected");
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Exception:" + ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        static void GenerateDataForPowerOn(CancellationToken token)
        {
            bool run = true;
            while (run)
            {
                try
                {
                    Thread.Sleep(5000);
                    token.ThrowIfCancellationRequested();
                    int i = random.Next(1, 10);
                    PowerIsOn = (i == 5) ? "ON" : "OFF";
                    Console.WriteLine("PowerIsOn:" + PowerIsOn);
                    Post(PowerIsOn);

                    AlarmSignal = (i == 5) ? "OK" : "DANGER";
                    Console.WriteLine("AlarmSignal:" + AlarmSignal);
                    Post(AlarmSignal);

                    //Time = DateTime.Now;
                    //Console.WriteLine("TIME:" + Time);
                    Console.WriteLine("stored in database");
                }

                catch (AggregateException ae)
                {
                    foreach (Exception e in ae.InnerExceptions)
                    {
                        if (e is TaskCanceledException)
                            Console.WriteLine("Canceled: {0}",
                                              ((TaskCanceledException)e).Message);
                        else
                            Console.WriteLine("Exception: " + e.GetType().Name);
                    }
                    run = false;
                }

            }
        }
    }
}