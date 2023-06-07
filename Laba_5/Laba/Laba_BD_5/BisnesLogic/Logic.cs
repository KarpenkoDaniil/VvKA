using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BisnesLogic
{
    public class Logic
    {
        string path;
        SqlConnection conn;
        public Logic() { }
        public Logic(SqlConnection connection, string path)
        {
            this.conn = connection;
            this.path = path;
            SetDataOne();
            SetSecondDATA();
            SetTherdDATA();
            SetForthDATA();
        }

        public void AbsExec(string command)
        {
            using (SqlConnection connection = new SqlConnection(path))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand cmmnd = connection.CreateCommand();
                cmmnd.Transaction = transaction;

                cmmnd.CommandText = command;
                cmmnd.ExecuteNonQuery();

                transaction.Commit();
            }
        }

        private List<string> AbsTabel(string command)
        {
            List<string> list = new List<string>();
            string str = null;
            using (SqlConnection conn = new SqlConnection(path))
            {
                conn.Open();
                SqlCommand cmmnd = new SqlCommand(command, conn);

                SqlDataReader reader = cmmnd.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    int countOfCols = reader.FieldCount;
                    for (int i = 0; i < countOfCols; i++)
                    {
                        str = str + reader.GetName(i) + " ";
                    }
                    list.Add(str);

                    while (reader.Read())
                    {
                        string row = null;
                        for (int i = 0; i < countOfCols; i++)
                        {
                            row = row + Convert.ToString(reader.GetValue(i)) + " ";
                        }
                        list.Add(row);
                    }
                }

            }
            return list;
        }
        //1.	Данные о сотрудниках и должностях, на которых они работают. 
        public List<string> GetEmployes()
        {
            string command = "SELECT * FROM Employes INNER JOIN Posts ON Posts.PostID = Employes.PostID";
            return AbsTabel(command);
        }
        //2.	Информация о самолетах и пилотах, которые на них летали.
        public List<string> GetInfAboutPilots()
        {
            string command = "SELECT * FROM Employes, Flights WHERE Flights.PostID = Employes.PostID";
            return AbsTabel(command);
        }
        //3.	Список рейсов, которые выполняются только внутри страны.
        public List<string> GetFlightsWhichInCountry()
        {
            string country = "Беларусь";
            string command = "SELECT * FROM Flights, ArivalsDispatch WHERE Flights.ArivalDispatchID = ArivalsDispatch.ArivalDispatchID AND" +
                " ArivalsDispatch.ArivalCountry = \'" + country + "\' AND ArivalsDispatch.DispatchCountry = \'" + country + "\'";
            return AbsTabel(command);
        }
        //4.	Параметрический запрос для отображения информации о сотрудниках, работающих на определенной должности.
        public List<string> GetAllEmployesWhithConcretNameOfPost(string nameOfPost)
        {
            string command = "SELECT * FROM Employes, Posts WHERE Employes.PostID = Posts.PostID AND Posts.PostName = \'" + nameOfPost + "\'";
            return AbsTabel(command);
        }

        //5.	Параметрический запрос для отображения информации о самолётах заданного типа.
        public List<string> GetAllPlanesOfOneType(int IDPlane)
        {
            string command = "SELECT * FROM Planes WHERE Planes.PlaneID = " + IDPlane;
            return AbsTabel(command);
        }
        //6.	Вычислить количество сделанных рейсов каждым пилотом.
        public List<string> GetCountOfAllFlightsOfAllPilots()
        {
            string comand = "SELECT Employes.Name, Employes.Surname, Employes.Midllename, Count(Flights.FlightID) as 'Count of Flights' " +
                            "FROM Employes, Flights " +
                            "WHERE Employes.EmployesID = Flights.EmployesID " +
                            "GROUP BY Employes.[Name], Employes.Surname, Employes.Midllename";
            return AbsTabel(comand);
        }

        //7.	Перекрестный запрос с информацией об общей стоимости проданных билетов для различных рейсов на каждую дату.
        public List<string> GetAllInfOfSales()
        {
            string comand = "SELECT Flights.DateOfFlight, Flights.FlightID, SUM(PlaneTickets.Price) AS AllPrice " +
                            "FROM PlaneTickets JOIN Flights ON PlaneTickets.FlightID = Flights.FlightID " +
                            "GROUP BY Flights.DateOfFlight, Flights.FlightID;";
            return AbsTabel(comand);
        }

        //8
        public void ExecSetFlight(string[] flight)
        {
            string command = "Exec FlightsInsert " + "\'" + flight[0] + "\'" + ", " + flight[1] + ", "  + flight[2] + ", " + flight[3] + ", " + flight[4] + ", " + flight[5];
            AbsExec(command);
        }
        //9 @FlightID int, @DateOfFlight date,@ArivalDispatchID int,@PlaneID int, @FlightTime float, @PostID int, @EmployesID int
        public void ExecUpdateFlights(string[] flight)
        {
            string command = "Exec UpdateFlights " + flight[0] + ", \'" + flight[1] + "\', " + flight[2] + ", " + flight[3] + ", " + flight[4] + ", " + flight[5] + ", " + flight[6];
            AbsExec(command);
        }
        //10
        public void ExecDeleteOneFlight(int id)
        {
            string delete = "DELETE FROM PlaneTickets \r\nWHERE FlightID = " + id;
            AbsTabel(delete);
            delete = "UPDATE PlaneTickets \r\nSET FlightID = NULL \r\nWHERE FlightID = " + id;
            string command = "Exec DeleteFlights " + id;
            AbsExec(command);
        }


        void SetDataOne()
        {
            using (SqlConnection connection = new SqlConnection(path))
            {
                List<string> posts = new List<string> { "Кассир", "Младший-кассир", "Старший кассир", "Пилот", "Старший-пилот", "Младший-пилот", "Стюардеса", "Охрана", "Уборщица" }; //post
                List<string> salary = new List<string> { "10000", "21312", "312312", "123", "1241", "4124124", "4124124", "1241242" };    //post

                List<string> name = new List<string> { "Даниил", "Александр", "Владислав", "Егор", "Артем", "Богдан", "Владимир" }; //Emploeys
                List<string> surname = new List<string> { "Иванов", "Смирнов", "Кузнецов", "Попов", "Васильев", "Петров", "Соколов", "Михайлов", "Новиков", "Федоров" };
                List<string> midellname = new List<string> { "Александрович", "Борисович", "Владимирович", "Геннадьевич", "Дмитриевич", "Евгеньевич", "Игоревич", "Константинович", "Михайлович", "Сергеевич" };
                string[] age = new string[70];
                for (int i = 1; i < age.Length; i++)
                {
                    age[i - 1] = i.ToString();
                }
                List<string> pasportDATA = new List<string> { "HB124124", "HB124124", "HB14214", "HB311231", "HB34345", "HB12312", "HB342342", "HB234234", "HB4353643", "HB123123" }; //Employes

                List<string> typePlanes = new List<string> { "Военный", "Гражданский", "Грузоперевозочный", "Медецинский", "Спасательный" }; //TypePlanes
                List<string> lim = new List<string> { "2т и 10 чел", "120т и 10 чел", "22т и 3 чел", "10т и 120 чел", "32т и 5 чел", "20т и 100 чел" };

                List<string> planeName = new List<string> { "Самолет1", "Самолет2", "Самолет3", "Самолет4" }; //Planes   
                List<string> planeCapacity = new List<string> { "40", "50", "70", "100", "125", "150" };
                List<string> loadCopacity = new List<string> { "15", "15", "16", "20", "100" };
                //typePlane Id 1-100
                //tehcnilcalSpecs
                List<string> dateOfCreate = new List<string> { "2000-03-22", "1986-01-01", "1986-01-01", "1986-01-02", "1976-04-07", "1996-11-21" };
                //flightHours
                List<string> dateOfLastRepair = new List<string> { "2000-03-22", "1986-01-01", "1986-01-01", "1986-01-02", "1976-04-07", "1996-11-21" }; //Planes

                List<string> ArivalCountry = new List<string> { "Белорусь", "Россия" };//ArivalsDispatch
                List<string> DispatchCountry = new List<string> { "Белорусь", "Белорусь", "Белорусь", "Германия", "Россия", "Германия", "Чехия", };
                List<string> ArivalAirName = new List<string> { "Городской" };
                List<string> DispatchAirName = new List<string> { "Сельский", "Крепостной", "WestEnd", "Doechland" };//ArivalsDispatch

                List<string> dateOfFlights = new List<string> { "2021-01-01", "2022-01-01", "2023-01-01", "2022-12-31", "2021-12-31", "2021-12-31" };
                //Flights
                //PlaneID
                //FlightTime
                //PostID
                //ArivalDispatchID
                //EmployesID

                //tikets
                //name
                //surname
                //midellname
                //PasportDATA
                //FlightID
                List<string> price = new List<string> { "123123", "123", "3124", "31231", "2131", "32131" };
                //place in plane

                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                SqlCommand command = connection.CreateCommand();
                command.Transaction = transaction;
                Random rd = new Random();
                try //используя хранимые процедуры
                {
                    for (int i = 1; i < 100; i++)
                    {
                        command.CommandText = $"EXEC InsertPost '{posts[rd.Next(1, posts.Count)]}', '{salary[rd.Next(salary.Count)]}', {rd.Next(100)}, {rd.Next(1, 100)}";
                        command.ExecuteNonQuery();
                        command.CommandText = $"EXEC TypeOfPlanesInsert '{typePlanes[rd.Next(1, typePlanes.Count)]}', '{lim[rd.Next(1, lim.Count)]}', {rd.Next(1, 100)}";
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                }
            }
        }
        void SetSecondDATA()
        {
            using (SqlConnection connection = new SqlConnection(path))
            {
                List<string> posts = new List<string> { "Кассир", "Младший-кассир", "Старший кассир", "Пилот", "Старший-пилот", "Младший-пилот", "Стюардеса", "Охрана", "Уборщица" }; //post
                List<string> salary = new List<string> { "10000", "21312", "312312", "123", "1241", "4124124", "4124124", "1241242" };    //post

                List<string> name = new List<string> { "Даниил", "Александр", "Владислав", "Егор", "Артем", "Богдан", "Владимир" }; //Emploeys
                List<string> surname = new List<string> { "Иванов", "Смирнов", "Кузнецов", "Попов", "Васильев", "Петров", "Соколов", "Михайлов", "Новиков", "Федоров" };
                List<string> midellname = new List<string> { "Александрович", "Борисович", "Владимирович", "Геннадьевич", "Дмитриевич", "Евгеньевич", "Игоревич", "Константинович", "Михайлович", "Сергеевич" };
                string[] age = new string[70];
                for (int i = 1; i < age.Length; i++)
                {
                    age[i - 1] = i.ToString();
                }
                List<string> pasportDATA = new List<string> { "HB124124", "HB124124", "HB14214", "HB311231", "HB34345", "HB12312", "HB342342", "HB234234", "HB4353643", "HB123123" }; //Employes

                List<string> typePlanes = new List<string> { "Военный", "Гражданский", "Грузоперевозочный", "Медецинский", "Спасательный" }; //TypePlanes
                List<string> lim = new List<string> { "2т и 10 чел", "120т и 10 чел", "22т и 3 чел", "10т и 120 чел", "32т и 5 чел", "20т и 100 чел" };

                List<string> planeName = new List<string> { "Самолет1", "Самолет2", "Самолет3", "Самолет4" }; //Planes   
                List<string> planeCapacity = new List<string> { "40", "50", "70", "100", "125", "150" };
                List<string> loadCopacity = new List<string> { "15", "15", "16", "20", "100" };
                //typePlane Id 1-100
                //tehcnilcalSpecs
                List<string> dateOfCreate = new List<string> { "2000-03-22", "1986-01-01", "1986-01-01", "1986-01-02", "1976-04-07", "1996-11-21" };
                //flightHours
                List<string> dateOfLastRepair = new List<string> { "2000-03-22", "1986-01-01", "1986-01-01", "1986-01-02", "1976-04-07", "1996-11-21" }; //Planes

                List<string> dateOfFlights = new List<string> { "2021-01-01", "2022-01-01", "2023-01-01", "2022-12-31", "2021-12-31", "2021-12-31" };

                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                SqlCommand command = connection.CreateCommand();
                command.Transaction = transaction;
                Random rd = new Random();
                try //используя хранимые процедуры
                {
                    for (int i = 1; i < 100; i++)
                    {
                        command.CommandText = $"EXEC InsertInformationEmployes '{name[rd.Next(1, name.Count)]}', '{surname[rd.Next(1, surname.Count)]}', '{midellname[rd.Next(1, midellname.Count)]}', {rd.Next(1, 70)}," +
                            $" '{pasportDATA[rd.Next(1, pasportDATA.Count)]}', {rd.Next(1, 100)}";//2
                        command.ExecuteNonQuery();
                        command.CommandText = $"EXEC PlanesInsert '{planeName[rd.Next(1, planeName.Count)]}', '{planeCapacity[rd.Next(1, planeCapacity.Count)]}', '{loadCopacity[rd.Next(1, loadCopacity.Count)]}', " +
                            $"{rd.Next(1, 100)}, 'spec1', '{dateOfCreate[rd.Next(1, dateOfCreate.Count)]}', {rd.Next(1, 24)}, '{dateOfCreate[rd.Next(1, dateOfCreate.Count)]}'"; //2
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                }
            }
        }

        void SetTherdDATA()
        {
            using (SqlConnection connection = new SqlConnection(path))
            {
                List<string> dateOfFlights = new List<string> { "2021-01-01", "2022-01-01", "2023-01-01", "2022-12-31", "2021-12-31", "2021-12-31" };

                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                SqlCommand command = connection.CreateCommand();
                command.Transaction = transaction;
                Random rd = new Random();
                try //используя хранимые процедуры
                {
                    for (int i = 1; i < 100; i++)
                    {
                        command.CommandText = $"EXEC FlightsInsert '{dateOfFlights[rd.Next(1, dateOfFlights.Count)]}', {rd.Next(1, 7)}, {rd.Next(1, 24)}, " +
                            $"{rd.Next(1, 100)}, {rd.Next(1,100)}, {rd.Next(1, 3)}";//2
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                }
            }
        }

        void SetForthDATA()
        {
            using (SqlConnection connection = new SqlConnection(path))
            {
                List<string> name = new List<string> { "Даниил", "Александр", "Владислав", "Егор", "Артем", "Богдан", "Владимир" }; //Emploeys
                List<string> surname = new List<string> { "Иванов", "Смирнов", "Кузнецов", "Попов", "Васильев", "Петров", "Соколов", "Михайлов", "Новиков", "Федоров" };
                List<string> midellname = new List<string> { "Александрович", "Борисович", "Владимирович", "Геннадьевич", "Дмитриевич", "Евгеньевич", "Игоревич", "Константинович", "Михайлович", "Сергеевич" };
                string[] age = new string[70];
                for (int i = 1; i < age.Length; i++)
                {
                    age[i - 1] = i.ToString();
                }
                List<string> pasportDATA = new List<string> { "HB124124", "HB124124", "HB14214", "HB311231", "HB34345", "HB12312", "HB342342", "HB234234", "HB4353643", "HB123123" }; //Employes

                //typePlane Id 1-100
                //tehcnilcalSpecs
                List<string> dateOfCreate = new List<string> { "2000-03-22", "1986-01-01", "1986-01-01", "1986-01-02", "1976-04-07", "1996-11-21" };
                //flightHours
                List<string> dateOfLastRepair = new List<string> { "2000-03-22", "1986-01-01", "1986-01-01", "1986-01-02", "1976-04-07", "1996-11-21" }; //Planes

                List<string> dateOfFlights = new List<string> { "2021-01-01", "2022-01-01", "2023-01-01", "2022-12-31", "2021-12-31", "2021-12-31" };

                List<string> price = new List<string> { "123123", "123", "3124", "31231", "2131", "32131" };
                //place in plane

                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                SqlCommand command = connection.CreateCommand();
                command.Transaction = transaction;
                Random rd = new Random();
                try //используя хранимые процедуры
                {
                    for (int i = 1; i < 100; i++)
                    {
                        command.CommandText = $"EXEC InsertPlaneTickets '{name[rd.Next(1, name.Count)]}', '{surname[rd.Next(1, surname.Count)]}', '{midellname[rd.Next(1, midellname.Count)]}', " +
                            $"'{pasportDATA[rd.Next(1, pasportDATA.Count)]}', {rd.Next(1, 100)}, '{price[rd.Next(1, price.Count)]}', {rd.Next(1, 100)}";
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();

                    Console.WriteLine("Данные добавлены в базу данных");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                }
            }
        }
    }
}
