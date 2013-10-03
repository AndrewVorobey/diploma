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
        public void setSettings(string name, string IP, string UName, string Pass) 
        { 
            this.name = name;
            ip = IP; userName = UName; 
            userPass = Pass;
        }
        public override string ToString()
        {
            return "Database=" + name + ";Data Source=" + ip + ";User Id=" + userName + ";Password=" + userPass;// +"port=3306;";
        }
    }

    class Sql
    {
        static Sql()
        {
            settings.setSettings("local_schedule", "127.0.0.1", "Andrew", "1994Andrew");
        }
        static bool online;
        static MySqlConnection myConnection;
        static MySqlCommand myCommand;
        public static SqlAccount settings;
        public static string connet()
        {
            try
            {
                string Connect = settings.ToString();
                myConnection = new MySqlConnection(Connect);
                myConnection.Open();
                online = true;
                return "";
            }
            catch (Exception e)
            {
                online = false;
                return e.Message.ToString();
            }
        }

        //
        public static List<List<string>> sendRequest(string str)
        {
            List<List<string>> SqlAnswerd = new List<List<string>>(5);
            if (online)
                try
                {
                    myCommand = new MySqlCommand(str, myConnection);
                    MySqlDataReader MyDataReader = myCommand.ExecuteReader();
                    while (MyDataReader.Read())
                    {
                        SqlAnswerd.Add(new List<string>(5));
                        for (int i = 0; i < MyDataReader.FieldCount; i++)
                            SqlAnswerd[SqlAnswerd.Count - 1].Add(MyDataReader.GetString(i));//записываем результат
                    }
                    MyDataReader.Close();
                }
                catch (Exception e)
                {
                    // SqlAnswerd[SqlAnswerd.Count].Add(e.Message.ToString());//добавляется как последний элемент

                }
            return SqlAnswerd;
        }

        //Записывает расписание в БД
        public static void SetSchedule(int N)
        {
            if (!online) return;
            int idUser = seachId(new string(Data.teacher[N].name.ToCharArray()));
            byte chet;
            if (idUser != -1)
            {
                sendRequest("delete from _timeTable where id_user=" + idUser + ";");
                for (int i = 0; i < 5; i++)
                {
                    chet = 0;
                    SetFirstPar(idUser
                        , chet
                        , i + 1
                        , Data.teacher[N].lesson[0, i].ToString(chet)
                        , Data.teacher[N].lesson[1, i].ToString(chet)
                        , Data.teacher[N].lesson[2, i].ToString(chet)
                        , Data.teacher[N].lesson[3, i].ToString(chet)
                        , Data.teacher[N].lesson[4, i].ToString(chet));
                    chet = 1;
                    SetFirstPar(idUser
                        , chet
                        , i + 1
                        , Data.teacher[N].lesson[0, i].ToString(chet)
                        , Data.teacher[N].lesson[1, i].ToString(chet)
                        , Data.teacher[N].lesson[2, i].ToString(chet)
                        , Data.teacher[N].lesson[3, i].ToString(chet)
                        , Data.teacher[N].lesson[4, i].ToString(chet));
                }
            }
        }

        private static void SetFirstPar(int idUser, int chet, int pair, string mon, string tue, string wed, string thu, string fri)
        {
            if (mon == "" && tue == "" && wed == "" && thu == "" && fri == "") return;
            string request = "insert into _timeTable values(null"
                           + "," + idUser
                           + "," + chet
                           + "," + pair
                           + ",\"" + mon
                           + "\",\"" + tue
                           + "\",\"" + wed
                           + "\",\"" + thu
                           + "\",\"" + fri
                           + "\");";
            sendRequest(request);
        }

        private static int seachId(string name)
        {
            int id = -1;
            string reqestNameId = "select id,surname, name,secname from _users;";
            List<List<string>> answer = sendRequest(reqestNameId);

            for (int i = 0; i < answer.Count; i++)
                try
                {
                    if (Equals(name, answer[i][1], answer[i][2], answer[i][3]))
                        return Convert.ToInt32(answer[i][0]);
                }
                catch { }

            return id;
        }

        private static bool Equals(string name1, string surName, string name2, string secName)
        {
            try
            {
                if (name1.Contains(surName))
                    if (name1.Contains(name2[0] + "."))
                        if (name1.Contains(secName[0] + "."))
                            return true;
            }
            catch { }
            return false;
        }


        public static void read()
        {
            if (!online) return;

            DataMass.saveAndClear("SQL");
            string reqest = "select id_user,pair,iseven, mon, tue, wed, thu, fri from _timeTable order by id_user;";
            List<List<string>> mass=sendRequest(reqest);
            int id = -1;
            int N=-1;
            for (int i = 0; i < mass.Count; i++)
            {
                if (id.ToString() != mass[i][0])
                {
                    id = Convert.ToInt32(mass[i][0]);
                    string reqest2 = "select surname, name, secname from _users where id=" + id ;
                    List<List<string>> name = sendRequest(reqest2);
                    if (name.Count>0)
                    Data.teacher.Add(new formatTimeTable(name[0][0]+" " + name[0][1][0]+"."+name[0][2][0]+".", 6));
                    N++;
                    for(int j=0;j<6;j++)
                        for(int k=0;k<6;k++)
                            Data.teacher[N].lesson[j,k]=new Lesson(0);
                }

                string chet=(Convert.ToInt32(mass[i][2])+1).ToString();
                for(int j=3;j<3+5;j++)
                Data.teacher[N].lesson[j-3,Convert.ToInt32(mass[i][1])-1].fromString(chet+" "+mass[i][j]);
            }
        }
    }
}
