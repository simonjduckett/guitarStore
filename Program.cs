using System;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace GuitarStore
{
    class Program
    {
        static Connection connection = new Connection();
        static MySqlConnection Conn = new MySqlConnection(connection.connString);

        static void Main(string[] args)
        {
            Console.WriteLine("welcome to guitar store press enter to begin...");
            Start();

        }

        public static void Start()
        {
            Console.ReadLine();

            Console.WriteLine("Please make a selection:");
            Console.WriteLine("1. view stock");
            Console.WriteLine("2. add stock");
            Console.WriteLine("3. Update stock");
            Console.WriteLine("4. delete stock");
            Console.WriteLine("5. Search stock");

            var x = Console.Read();

            if (x == 49)
            { ShowStock(); }
            else if (x == 50)
            { AddStock(); }
            else if (x == 51)
            { UpdateStock(); }
            else if (x == 52)
            { DeleteStock(); }
            else if (x == 53)
            { SearchStock(); }
        }


        public static void ShowStock()
        {

            MySqlCommand command = Conn.CreateCommand();
            command.CommandText = "Select * from stock";


            try
            {
                Conn.Open();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            MySqlDataReader reader = command.ExecuteReader();


            while (reader.Read())
            {
                Console.WriteLine("---------------------------");
                Console.WriteLine("Product ID:  " + (reader["id"].ToString()));
                Console.WriteLine("Make:  " + (reader["make"].ToString()));
                Console.WriteLine("Model:  "+ (reader["model"].ToString()));
                Console.WriteLine("Stock:  " + (reader["amount"].ToString()));
                Console.WriteLine("---------------------------");
            }
            Conn.Close();

            Start();
  
        }

        public static void AddStock()
        {
            Console.Read();

            Console.WriteLine("enter the make");
            string make = Console.ReadLine();

            Console.WriteLine("enter the model");
            string model = Console.ReadLine();

            Console.WriteLine("enter the amount");
            string amount = Console.ReadLine();

            MySqlCommand command = Conn.CreateCommand();

            command.CommandText = "Insert into stock (make, model, amount) values (" + "'" + make + "'" + "," + "'" + model + "'" + "," + "'" + amount + "'" + ")";

            Conn.Open();

            command.ExecuteNonQuery();
            Conn.Close();

            Console.WriteLine("stock has been added");

            Start();
        }

        public static void UpdateStock()
        {
            Console.Read();

            Console.WriteLine("enter the prodct id");
            string id = Console.ReadLine();


            int idc = Int32.Parse(id);


            Console.WriteLine("enter the new amount");
            string amount = Console.ReadLine();

            int amountc = Int32.Parse(amount);

            MySqlCommand command = Conn.CreateCommand();


             
            command.CommandText = "Update stock SET amount ='" + amountc + "' WHERE id = '" +idc+"'";
            Conn.Open();

            command.ExecuteNonQuery();
            Conn.Close();

            Console.WriteLine("stock has been updated");

            Start();
        }

        public static void DeleteStock()
        {
            Console.Read();

            Console.WriteLine("enter the prodct id");
            string id = Console.ReadLine();


            int idc = Int32.Parse(id);

            MySqlCommand command = Conn.CreateCommand();


            command.CommandText = "DELETE FROM stock WHERE id = '" + idc + "'";
            Conn.Open();

            command.ExecuteNonQuery();
            Conn.Close();

            Console.WriteLine("stock has been deleted");

            Start();
        }

        public static void SearchStock()
        {
            Console.Read();

            Console.WriteLine("Please enter a search term");

            string searchterm = Console.ReadLine();

            if (searchterm == "")
            {
                Console.WriteLine("nothing entered!");
                SearchStock();
            }
            else
            {

                MySqlCommand command = Conn.CreateCommand();
                command.CommandText = "Select * from stock WHERE make LIKE '" + searchterm + "' OR model LIKE '" + searchterm + "'";


                try
                {
                    Conn.Open();
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                MySqlDataReader reader = command.ExecuteReader();

                var i = 0;
                while (reader.Read())
                {
                    Console.WriteLine("---------------------------");
                    Console.WriteLine("Product ID:  " + (reader["id"].ToString()));
                    Console.WriteLine("Make:  " + (reader["make"].ToString()));
                    Console.WriteLine("Model:  " + (reader["model"].ToString()));
                    Console.WriteLine("Stock:  " + (reader["amount"].ToString()));
                    Console.WriteLine("---------------------------");

                    i += 1;
                }

                if (i == 0)
                { Console.WriteLine("no results"); }
                else if (i == 1)
                { Console.WriteLine(i + " result"); }
                else if (i > 1)
                { Console.WriteLine(i + " results"); }


                Conn.Close();



                Start();
            }
        }


    }
}
