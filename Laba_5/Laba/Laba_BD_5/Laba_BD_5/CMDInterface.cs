using BisnesLogic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Laba_BD_5
{

    public class CMDInterface
    {
        public static string path = @"Data Source= DESKTOP-J7VJ20H\SQLEXPRESS;Initial Catalog=Airlines;Integrated Security=True";
        private static Logic Logic = new Logic();
        private static List<string> table = new List<string>();
        static int stop = 1;
        static void Main(string[] args)
        {

            using (SqlConnection conn = new SqlConnection(path))
            {
                Logic logic = new Logic(conn, path);
                Logic = logic;
                try
                {
                    Console.WriteLine("База данных подключена!\n*Нажмите любую клавишу*\n");
                    Console.ReadKey();
                    Console.Clear();
                    while (stop != 0)
                    {
                        Menu();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("База данных отключена!\n");
                    Console.WriteLine("Error: " + e);
                    Console.WriteLine(e.StackTrace);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        static List<string> flight = new List<string>();

        static void Menu()
        {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("                   Меню                    ");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("1) Данные о сотрудниках и должностях, на которых они работают.");
            Console.WriteLine("2) Информация о самолетах и пилотах, которые на них летали.");
            Console.WriteLine("3) Список рейсов, которые выполняются только внутри страны.");
            Console.WriteLine("4) Параметрический запрос для отображения информации о \n" +
                                    "сотрудниках, работающих на определенной должности.");
            Console.WriteLine("5) Параметрический запрос для отображения информации о самолётах заданного типа.");
            Console.WriteLine("6) Вычислить количество сделанных рейсов каждым пилотом.");
            Console.WriteLine("7) Перекрестный запрос с информацией об общей стоимости проданных билетов для различных рейсов на каждую дату.");
            Console.WriteLine("8) Добавление нового рейса");
            Console.WriteLine("9) Обновление рейса");
            Console.WriteLine("10) Удаление рейса");
            Console.WriteLine("0) Выход");

            int a;
            do
            {
                try
                {
                    a = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    a = -1;
                }
            }
            while (a < 0 || a > 10);

            switch (a)
            {
                case 0:
                    Console.WriteLine("Программа завершена!");
                    stop=0;
                    break;
                case 1:
                    table = Logic.GetEmployes();
                    for (int i = 0; i < 100; i++)
                    {
                        Console.WriteLine(table[i]);
                    }
                    break;
                case 2:
                    table = Logic.GetInfAboutPilots();
                    for (int i = 0; i < 100; i++)
                    {
                        Console.WriteLine(table[i]);
                    }
                    break;
                case 3:
                    table = Logic.GetFlightsWhichInCountry();
                    for (int i = 0; i < 100; i++)
                    {
                        Console.WriteLine(table[i]);
                    }
                    break;
                case 4:
                    Console.WriteLine("Input name of post");
                    string namePost = Console.ReadLine();
                    table = Logic.GetAllEmployesWhithConcretNameOfPost(namePost);
                    for (int i = 0; i < 100; i++)
                    {
                        Console.WriteLine(table[i]);
                    }
                    break;
                case 5:
                    Console.WriteLine("Input ID of type plane");
                    int IDPlane = Convert.ToInt32(Console.ReadLine());
                    table = Logic.GetAllPlanesOfOneType(IDPlane);
                    for (int i = 0; i < table.Count; i++)
                    {
                        Console.WriteLine(table[i]);
                    }
                    break;
                case 6:
                    table = Logic.GetCountOfAllFlightsOfAllPilots();
                    for (int i = 0; i < table.Count; i++)
                    {
                        Console.WriteLine(table[i]);
                    }
                    break;
                case 7:
                    table = Logic.GetAllInfOfSales();
                    for (int i = 0; i < table.Count; i++)
                    {
                        Console.WriteLine(table[i]);
                    }
                    break;
                case 8:
                    flight = new List<string>();
                    Console.WriteLine("Input Data for new flight\n");
                    Console.WriteLine("Input date \nExample: eeee-mm-dd");
                    flight.Add(Console.ReadLine()); 
                    Console.WriteLine("Input ID of arival/dispath");
                    flight.Add(Console.ReadLine());
                    Console.WriteLine("Input ID of plane");
                    flight.Add(Console.ReadLine());
                    Console.WriteLine("Input time of flight in ours");
                    flight.Add(Console.ReadLine());
                    Console.WriteLine("Input ID of post");
                    flight.Add(Console.ReadLine());
                    Console.WriteLine("Input ID of pilot");
                    flight.Add(Console.ReadLine());

                    Logic.ExecSetFlight(flight.ToArray());
                    break;
                case 9:
                    flight = new List<string>();
                    Console.WriteLine("Input Data for new flight\n");
                    Console.WriteLine("Input ID of flight which do you whant to update");
                    flight.Add(Console.ReadLine());
                    Console.WriteLine("Input date \nExample: eeee-mm-dd");
                    flight.Add(Console.ReadLine());
                    Console.WriteLine("Input ID of arival/dispath");
                    flight.Add(Console.ReadLine());
                    Console.WriteLine("Input ID of plane");
                    flight.Add(Console.ReadLine());
                    Console.WriteLine("Input time of flight in ours");
                    flight.Add(Console.ReadLine());
                    Console.WriteLine("Input ID of post");
                    flight.Add(Console.ReadLine());
                    Console.WriteLine("Input ID of pilot");
                    flight.Add(Console.ReadLine());

                    Logic.ExecUpdateFlights(flight.ToArray());
                    break;
                case 10:
                    Console.WriteLine("Input ID of flight which do you whant to delete");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Logic.ExecDeleteOneFlight(id);
                    break;
                default:
                    Console.Write("Error!");
                    break;
            }
        }
    }
}



