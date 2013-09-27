using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Диплом
{
    struct SqlAccount
    {
        string name;
        string ip;
        string userName;
        string userPass;
        public void setSettings(string name, string IP, string UName, string Pass) { this.name = name; ip = IP; userName = UName; userPass = Pass; }
        public override string ToString()
        {
            //return "Database=" + name+";Data Source=" + ip + ";User Id=" + userName + ";Password=" + userPass;
            return "Database=" + name + ";Server=" + ip + ";Uid=" + userName + ";Pwd=" + userPass + "CharSet = cp1251; " + "port=3306;";
        }
    }

    class Sql
    {
        static List<string> SqlAnswerd;
        static MySqlConnection myConnection;
        static MySqlCommand myCommand;
        public static SqlAccount settings;
        public static string connet()
        {
            try
            {
                settings.setSettings("mathmodm_dbmathmodm", "217.174.103.225", "mathmodm_test", "$[)-o4*$%h#[");
                string Connect = settings.ToString();
                myConnection = new MySqlConnection(Connect);
                myConnection.Open();
                return "";
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }
        public static List<string> request(string str)
        {
            try
            {
                myCommand = new MySqlCommand(str, myConnection);
                MySqlDataReader MyDataReader = myCommand.ExecuteReader();
                SqlAnswerd.Clear();
                while (MyDataReader.Read())
                {
                    SqlAnswerd.Add(MyDataReader.GetString(0));//записываем результат
                    // int id = MyDataReader.GetInt32(1); //Получаем целое число
                }
                MyDataReader.Close();
            }
            catch (Exception e)
            {
                SqlAnswerd.Add(e.Message.ToString());

            }
            return SqlAnswerd;
        }

    }
}
