using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Npgsql;

namespace simple_npgsql_test
{
    public class SimpleCypherTest
    {
                public void matchTest()
        {
            using (var conn = new NpgsqlConnection("Host=127.0.0.1;Port=5432;Username=test;Password=test;Database=test"))
            {
                try
                {
                    conn.Open();
                    var cmd = new NpgsqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SET graph_path=test";
                    cmd.CommandType = CommandType.Text;

                    cmd.ExecuteNonQuery();

                    var cmd2 = new NpgsqlCommand();
                    cmd2.Connection = conn;
                    cmd2.CommandText = "MATCH (n:TEST123)-[r:has_rel]->(m:TEST123) " +
                                       " WHERE n.id=1 " +
                                       " RETURN n.id,type(r),m.id";
                    cmd2.CommandType = CommandType.Text;

                    using (var dr = cmd2.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string txt = dr.GetString(0);
                            string txt2 = dr.GetString(1);
                            string txt3 = dr.GetString(2);
                            Console.WriteLine("MATCH result => 1st ID: " + txt + ", Rel_Type: " + txt2 + ", 2nd ID: " + txt3);
                        }


                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error!!!!!!!");
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }

            }

        }

        public void createTest()
        {
            using (var conn = new NpgsqlConnection("Host=127.0.0.1;Port=5432;Username=test;Password=test;Database=test"))
            {
                try
                {
                    conn.Open();
                    //Console.WriteLine("I am here#1");
                    var cmd = new NpgsqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SET graph_path TO test";
                    cmd.CommandType = CommandType.Text;
                    //Console.WriteLine("I am here#2");

                    cmd.ExecuteNonQuery();
                    //Console.WriteLine("I am here#3");

                    var cmd2 = new NpgsqlCommand();
                    cmd2.Connection = conn;
                    cmd2.CommandText = "CREATE VLABEL TEST123";

                    var tmp_num = cmd2.ExecuteNonQuery();
                    
                    var cmd3 = new NpgsqlCommand();
                    cmd3.Connection = conn;
                    for (int i = 0; i < 10; i++)
                    {
                        cmd3.CommandText = "CREATE (:TEST123 {id:" + i + ",text:'test123-" + i + "'})";
                        cmd3.ExecuteNonQuery();
                    }
                    
                    var cmd4 = new NpgsqlCommand();
                    cmd4.Connection = conn;
                    cmd4.CommandText = "CREATE ELABEL has_rel";
                    cmd4.CommandType = CommandType.Text;
                    cmd4.ExecuteNonQuery();
                    
                    var cmd5 = new NpgsqlCommand();
                    cmd5.Connection = conn;
                    cmd5.CommandText = "MATCH (a:TEST123 {id:1}), (b:TEST123 {id:2}) CREATE (a)-[:has_rel]->(b)";
                    cmd5.CommandType = CommandType.Text;
                    cmd5.ExecuteNonQuery();

                    var cmd6 = new NpgsqlCommand();
                    cmd6.Connection = conn;
                    cmd6.CommandText = "MATCH (n:TEST123) RETURN count(n)";
                    cmd6.CommandType = CommandType.Text;
                    using (var dr = cmd6.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int cnt = Int32.Parse(dr.GetString(0));
                            if (cnt == 10)
                            {
                                Console.WriteLine("CREATE operation has been successfully executed!");
                            }
                        }


                    }
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error!!!!!!!");
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }

            }

        }

        public void deleteTest()
        {
            using (var conn = new NpgsqlConnection("Host=127.0.0.1;Port=5432;Username=test;Password=test;Database=test"))
            {
                try
                {
                    conn.Open();
                    var cmd = new NpgsqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SET graph_path=test";
                    cmd.CommandType = CommandType.Text;

                    cmd.ExecuteNonQuery();

                    var cmd2 = new NpgsqlCommand();
                    cmd2.Connection = conn;
                    cmd2.CommandText = "MATCH (n:TEST123) DETACH DELETE n";

                    cmd2.ExecuteNonQuery();

                    var cmd3 = new NpgsqlCommand();
                    cmd3.Connection = conn;
                    cmd3.CommandText = "MATCH (n:TEST123) RETURN count(n)";
                    cmd3.CommandType = CommandType.Text;
                    using (var dr = cmd3.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int cnt = Int32.Parse(dr.GetString(0));
                            if (cnt == 0)
                            {
                                Console.WriteLine("DELETE operation has been successfully executed!");
                            }
                        }


                    }

                    var cmd4 = new NpgsqlCommand();
                    cmd4.Connection = conn;
                    cmd4.CommandText = "DROP VLABEL TEST123 CASCADE";

                    cmd4.ExecuteNonQuery();
                    
                    var cmd5 = new NpgsqlCommand();
                    cmd5.Connection = conn;
                    cmd5.CommandText = "DROP ELABEL has_rel";

                    cmd5.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error!!!!!!!");
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }

            }

        }
    }
}