using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Models;

namespace SMART
{
    class Program
    {

        //static string str = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=c:\users\parisa\documents\visual studio 2015\Projects\SMART\Smart.mdf; Integrated Security = True";
        //static string str = ConfigurationManager.ConnectionStrings["SmartEntities"].ConnectionString;
        //static SqlConnection con = new SqlConnection(str);
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
        static void GenerateDataForPowerOn(CancellationToken token)
        {
            //con.Open();
            bool run = true;
            while (run)
            {
                try
                {
                    SmartEntities db = new SmartEntities();
                    Thread.Sleep(3000);
                    token.ThrowIfCancellationRequested();
                    int i = random.Next(1, 10);
                    PowerIsOn = (i == 5) ? "ON" : "OFF";
                    //string query = "INSERT INTO Alarm(PowerIsOn) VALUES (' " + PowerIsOn + " ')";
                    //SqlCommand insert = new SqlCommand(query, con);
                    //insert.ExecuteNonQuery();

                    var alarm = new Alarm
                    {
                        PowerIsOn = PowerIsOn
                    };
                    db.Alarm.Add(alarm);
                    db.SaveChanges();

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
            //con.Dispose();
        }
    }
}

