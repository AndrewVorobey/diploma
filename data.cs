using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Диплом
{

    class timeTable
    {
        public String Name;
        public String[] Monday;
        public String[] Tuesday;
        public String[] Wednesday;
        public String[] Thursday;
        public String[] Friday;
    }


    struct Lesson
    {
        public Lesson(int N) { exist = new bool[2]; lection = new bool[2]; grup = new string[2]; roomNomber = new string[2]; subject = new string[2]; bothWeek = true; }

        public bool[] exist;
        public bool[] lection;
        public string[] grup;
        public string[] roomNomber;
        public string[] subject;
        public bool bothWeek;
        private static string facultets = @"(A|А|ТФ|ЭЛ)";

        private static string houses = @"(A|А|Б|В|B|Г|Д|Ж|М|M|Н|С|C)";

        public void fromString(string str)
        {
            if (str == "\r\a")
            {
                exist[0] = exist[1] = false;
                return;//строка пуста.
            }
            if (chekShortVar(str) != "")//ТФ-11 – 10 1 Ж 510  2 Ж 206
            {
                string substr = chekShortVar(str);
                int n = str.IndexOf(substr);
                string S = str.Remove(n, substr.Length);
                str = "1 " + S + setRoomNomber(substr, 0) + "\r2 " + S + setRoomNomber(substr.Substring(setRoomNomber(substr, 0).Length), 0);

            }
            if (str[0] != '1')
            {
                if (str[0] == '2')//Только четные недели
                {
                    cutOne(str.Substring(2), 1);
                    bothWeek = false;
                }
                else
                    cutOne(new string(str.ToCharArray()), 0);
            }
            else
            {
                bothWeek = false;
                string newStr;
                str = str.Substring(1);//Удаляем единицу из начала
                str = str.Replace("\r2", "|");//разделяем строки
                string[] strMass = str.Split("|".ToCharArray());

                cutOne(strMass[0], 0);//фиксуруем первую
                //Вторая строка. 
                if (strMass.Length == 2)
                    cutOne(strMass[1], 1);

            }

        }

        private string chekShortVar(string str)// "... 1 М710 2 М711"
        {
            string pattern = @"(1\s*)" + houses + @"\s?–?-?\d{3}(\s?/\s?\d{1,3})?" + @"\s*" + @"2" + @"\s*" + houses + @"\s?–?-?\d{3}(\s?/\s?\d{1,3})?";//;
            Regex regex = new Regex(pattern);
            string str1 = regex.Match(str).Value;
            return str1;
        }

        private void cutOne(string str1, int N)
        {
            string str = "" + str1;
            string s1;
            s1 = setGroup(str, N);
            str = str.Remove(0, str.IndexOf(s1) + s1.Length);
            if ((s1 = setRoomNomber(str, N)) != "") exist[N] = true;
            str = str.Remove(str.IndexOf(s1), s1.Length);
            str = str.Replace("\r", "");
            str = str.Replace("\a", "");
            subject[N] = str;
        }

        private string setGroup(string str, int N)
        {

            string pattern = facultets + @"(\s?-?–?\s?(\d{1,2}a?м?а?\,?-?–?){1,5})" + @"(\s?–?-?\s?\d{1,2}){1,2}";//А-14-11
            pattern = @"(" + pattern + @"\,)?" + pattern;//Если не распознает группы, стереть эту строку.
            Regex regex = new Regex(pattern);
            return grup[N] = regex.Match(str).Value;
        }
        private string setRoomNomber(string str, int N)
        {
            string pattern = houses + @"\s?–?-?\d{3}(\s?/\s?\d{1,3})?a?а?";// М-711
            Regex regex = new Regex(pattern);
            return roomNomber[N] = regex.Match(str).Value;

        }

        int findInStr(string A, string S)
        {
            return 0;
        }
    }



    class formatTimeTable
    {
        const int days = 6;
        int maxLesson;
        public formatTimeTable(string name, int maxLesson) { this.name = name; lesson = new Lesson[days, maxLesson]; this.maxLesson = maxLesson; }
        public int nomber;
        public string name;
        public Lesson[,] lesson;
    }


    class Data
    {
        public static List<formatTimeTable> teacher = new List<formatTimeTable>();

    }
}
