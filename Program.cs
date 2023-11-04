using Microsoft.Data.SqlClient;

namespace sqltest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                /**
                builder.DataSource = "";
                builder.UserID = "";
                builder.Password = "";
                builder.InitialCatalog = "";
                **/

                Console.WriteLine("Enter DataSource (Server Name):");
                builder.DataSource = Console.ReadLine();
                Console.WriteLine("Enter User Id:");
                builder.UserID = Console.ReadLine();
                Console.WriteLine("Enter Password:");
                builder.Password = Console.ReadLine();
                Console.WriteLine("Enter DatabaseName:");
                builder.InitialCatalog = Console.ReadLine();


                builder.Encrypt = false;

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    bool run = true;
                    while (run)
                    {
                        run = EnterCommand(connection);
                    }
                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("\nDone. Press enter.");
            Console.ReadLine();
        }

        public static bool EnterCommand(SqlConnection connection)
        {
            Console.WriteLine("\n=========================================\n");
            Console.WriteLine("\nWelcome To The Simple SQL Database UI");
            Console.WriteLine("To start please select from the following options:\n");
            Console.WriteLine("(1) FIND HIGH LEVEL PLAYERS\n(2) FIND ACTIVITY BY DESTINATION\n(3) FIND QUEST REWARD (WEAPON)\n(4) FIND QUEST BY GOAL\n(5) FIND QUEST BY DIFFICULTY\n(6) RUN PURE SQL QUERY\n(7) Quit");
            Console.WriteLine("\nEnter Option Choice by Number or Name: ");

            //String sql = "SELECT * FROM Player";
            String input = Console.ReadLine() + "";
            String sql = "";
            if (input.Equals("Quit") || input.Equals("7"))
            {
                return false;
            }
            else if (input.Equals("FIND HIGH LEVEL PLAYERS") || input.Equals("1"))
            {
                sql = "SELECT * FROM PLAYER WHERE PLAYER.Level > 10;";
            }
            else if (input.Equals("FIND ACTIVITY BY DESTINATION") || input.Equals("2"))
            {
                sql = "SELECT * FROM ACTIVITY WHERE ACTIVITY.Destination =";
                Console.WriteLine("PLEASE ENTER DESTINATION [TYPE: CHAR | LENGTH LIMIT: 30]");
                sql = sql + "'" + Console.ReadLine() + "';";
            }
            else if (input.Equals("FIND QUEST REWARD (WEAPON)") || input.Equals("3"))
            {
                sql = "SELECT * FROM QUEST WHERE QUEST.Weapon =";
                Console.WriteLine("PLEASE ENTER WEAPON NAME [TYPE: CHAR | LENGTH LIMIT: 30]");
                sql = sql + "'" + Console.ReadLine() + "';";
            }
            else if (input.Equals("FIND QUEST BY GOAL") || input.Equals("4"))
            {
                sql = "SELECT * FROM QUEST WHERE QUEST.Goal = ";
                Console.WriteLine("PLEASE ENTER GOAL NAME [TYPE: CHAR | LENGTH LIMIT: 30]");
                sql = sql + "'" + Console.ReadLine() + "';";
            }
            else if (input.Equals("FIND QUEST BY DIFFICULTY") || input.Equals("5"))
            {
                sql = "SELECT * FROM QUEST WHERE QUEST.Level = ";
                Console.WriteLine("PLEASE ENTER DIFFICULTY [TYPE: INTEGER]");
                sql = sql + "'" + Console.ReadLine() + "';";
            }
            else if (input.Equals("RUN PURE SQL QUERY") || input.Equals("6"))
            {
                sql = Console.ReadLine() + "";
            }

            Console.WriteLine("\nRESULT\n");

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    for (int x = 0; x < reader.FieldCount; x++)
                    {
                        Console.Write(reader.GetName(x) + " | ");
                    }
                    Console.WriteLine();
                    while (reader.Read())
                    {
                        //Console.WriteLine("{0} {1} {2}", reader.GetString(0), reader.GetString(1), reader.GetInt32(2));
                        //Console.WriteLine(reader.FieldCount);
                        for (int x = 0; x < reader.FieldCount; x++)
                        {
                            Console.Write((reader[x].ToString() + "").Trim() + " | ");
                        }
                        Console.WriteLine();
                    }
                }
            }
            return true;
        }
    }
}