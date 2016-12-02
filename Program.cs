using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SMART
{
    class Program
    {

        static string str = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=c:\users\tens\documents\visual studio 2015\Projects\SMART\SmartData.mdf; Integrated Security = True";
        static SqlConnection con = new SqlConnection(str);
        static string PowerIsOn;
        static string AlarmSignal;
        static DateTime Time;

        static void Main(string[] args)
        {

            CancellationTokenSource cts = new CancellationTokenSource();
            Task t = new Task(() => GenerateDataForPowerOn(cts.Token));
            t.Start();


            try
            {
               
                //Task t = new Task(GenerateDataForPowerOn());
                //t.Start();

                //con.Open();

                Console.WriteLine("Is There Alarm Signal? Write Yes or No");
                AlarmSignal = Console.ReadLine();
                Time = DateTime.Now;
                string query = "INSERT INTO Alarm(PowerIsOn, AlarmSignal, Time) VALUES (' " + PowerIsOn + " ' ,' " + AlarmSignal + " ',  ' " + Time + " ')";
                SqlCommand insert = new SqlCommand(query, con);
                insert.ExecuteNonQuery();

                t.Wait();
                Console.WriteLine("stored in database");

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();

        }
        static void GenerateDataForPowerOn(CancellationToken token)
        {
            con.Open();
            while (true)
            {
                Thread.Sleep(30000);
                Console.WriteLine("Is power on? Write ON or Off");
                PowerIsOn = Console.ReadLine();
                string query = "INSERT INTO Alarm(PowerIsOn) VALUES (' " + PowerIsOn + " ')";
                SqlCommand insert = new SqlCommand(query, con);
                insert.ExecuteNonQuery();

            }

        }
    }
}
