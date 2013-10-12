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
        public Lesson(int N) {  italy= new bool();exist = new bool[2]; lection = new bool[2]; group = new string[2]; roomNomber = new string[2]; subject = new string[2]; bothWeek = true; }

        public bool[] exist;
        public bool[] lection;
        public string[] group;
        public string[] roomNomber;
        public string[] subject;
        public bool bothWeek;
        public bool italy;
        private static string facultets = @"(A|А|ТФ|ЭЛ|ЭР|С|C)";

        private static string houses = @"(A|А|Б|В|B|Г|Д|Ж|М|M|Н|С|C)";

        public void fromString(string str)
        {
            str = str.Replace("  ", "");
            if (str.Length < 3)//строка пуста.
            {
                exist[0] = exist[1] = false;
                return;
            }

            str= firstFormat(str);
  
            if (str[0] != '1')
            {
                if (str[0] == '2')//Только четные недели
                {
                    cutOne(str.Substring(2), 1);
                    bothWeek = false;
                }
                else //Сразу 2 недели
                    cutOne(new string(str.ToCharArray()), 0);
            }
            else//Тут точно есть не четная неделя.Надо определить, есть ли четная. 
            {
                bothWeek = false;
                str = str.Substring(1);//Удаляем единицу из начала
                str = str.Replace("\r2", "|");//разделяем строки (четная и не четная)
                string[] strMass = str.Split("|".ToCharArray());
                cutOne(strMass[0], 0);//фиксуруем первую
                //если есть вторая строка. 
                if (strMass.Length == 2)
                    cutOne(strMass[1], 1);

            }
            if (exist[0])//Если запись существует, удаляем лишние символы
                subjectFormated(0);
            if (exist[1])
                subjectFormated(1);
            if(subject[0]!=null)
                subject[0] = subject[0].Replace("*", "");
            if (subject[1] != null)
                subject[1] = subject[1].Replace("*", "");
            unionIfEquivalent();//Если первая и вторая недели эквивалентны

            if (exist[0]) if (subject[0] != subject[0].Replace("иссл", "")) { bothWeek = true; exist[1] = false; }
        }

        private string firstFormat(string str)
        {
            str = delStarBefore2(str);//Удаляем звездочку перед первым. 
            if (chekShortVar(str) != "")//ТФ-11 – 10 1 Ж 510  2 Ж 206-РАЗБИВАЕМ ЭТУ СТРОКУ
            {
                string substr = chekShortVar(str);
                int n = str.IndexOf(substr);
                string S = str.Remove(n, substr.Length);
                str = "1 " + S + getRoomNomber(substr) + "\r2 " + S + getRoomNomber(substr.Substring(getRoomNomber(substr).Length));

            }
            return str;
        }
        private static string delStarBefore1(string str)
        {
            string pattern1 = @"(\*\s*1)";//*    2
            string pattern2 = @"(\*\s*)";//*    
            Regex regex = new Regex(pattern1);
            string str1 = regex.Match(str).Value;
            if (str1 != "")
                return str.Replace(str1, "1");
            else return str;
        }
        private static string delStarBefore2(string str)
        {
            string pattern1 = @"(\*\s*2)";//*    2
            string pattern2 = @"(\*\s*)";//*    
            Regex regex = new Regex(pattern1);
            string str1 = regex.Match(str).Value;
            regex = new Regex(pattern2);
            string str2 = regex.Match(str1).Value;
            if (str2 != "")
                return str.Replace(str2, "\r");
            else return str;
        }

        private string chekShortVar(string str)// "... 1 М710 2 М711"
        {
            string pattern = @"(1\s*)" + houses + @"\s?–?-?\d{3}(\s?/\s?\d{1,3})?" + @"\s*" + @"2" + @"\s*" + houses + @"\s?–?-?\d{3}(\s?/\s?\d{1,3})?";
            Regex regex = new Regex(pattern);
            string str1 = regex.Match(str).Value;
            return str1;
        }

        private void cutOne(string str1, int N)
        {
            if (str1.Length > 3) exist[N] = true;
            string str = "" + str1;
            string s1;
            group[N] = getLectionGroup(str);
            str = str.Remove(0, str.IndexOf(group[N]) + group[N].Length);
            if ((roomNomber[N]= getRoomNomber(str)) != "") exist[N] = true;
            str = str.Remove(str.IndexOf(roomNomber[N]), roomNomber[N].Length);
            str = str.Replace("\r", "");
            str = str.Replace("\a", "");
            subject[N] = str;
            if (getGroup(group[N]).Length>5) lection[N] = false;
            else lection[N] = true;
        }

        public static string getLectionGroup(string str)
        {

            string pattern = facultets + @"(\s?-?–?\s?(\d{1,2}a?м?а?\,?-?–?){1,5})" + @"(\s?–?-?\s?\d{1,2}){1,2}";//А-14-11
            pattern = @"(" + pattern + @"\,)?" + pattern;//Если не распознает группы, стереть эту строку.
            Regex regex = new Regex(pattern);
            return regex.Match(str).Value;
        }

        public static string getGroup(string str)
        {

            string pattern = facultets + @"(\s?-?–?\s?(\d{1,2}a?м?а?-?–?)\d{2})";//А-14-11
            Regex regex = new Regex(pattern);
            return regex.Match(str).Value;
        }

        public static string getRoomNomber(string str)
        {
            string pattern = houses + @"\s?–?-?\d{3}(\s?/\s?\d{1,3})?a?а?";// М-711
            Regex regex = new Regex(pattern);
            return regex.Match(str).Value;
        }


        int findInStr(string A, string S)
        {
            return 0;
        }
        private void subjectFormated(int N)
        {
            subject[N] = subject[N].Replace("\r", "");
            subject[N] = subject[N].Replace("  ", "");
            subject[N] = subject[N].Replace("?", "") + " ";
        }

        override public string ToString()
        {
            string day = " ";
            if (bothWeek)
                day += group[0] + " " + subject[0] + " " + roomNomber[0];
            else
            {
                if (exist[0])
                    day += "1" + group[0] + " " + subject[0] + " " + roomNomber[0] + "\n";
                if (exist[1])
                    day += "2" + group[1] + " " + subject[1] + " " + roomNomber[1];
            }
            return day;
        }
        public string ToString(int N)
        {
            string day = "";
            if (bothWeek)
                day += group[0] + " " + subject[0] + " " + roomNomber[0];
            else
            {
                if (exist[0] && N == 0)
                    day += group[0] + " " + subject[0] + " " + roomNomber[0];
                if (exist[1] && N == 1)
                    day += group[1] + " " + subject[1] + " " + roomNomber[1];
            }
            return day;
        }

        public void unionIfEquivalent()
        {
           if(exist[0] && exist[1])
            if (group[0].Replace(" ", "") == group[1].Replace(" ", ""))
                if (subject[0].Replace(" ", "") == subject[1].Replace(" ", ""))
                    if (roomNomber[0].Replace(" ", "") == roomNomber[1].Replace(" ", ""))
                        bothWeek = true;
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
        public static List<formatTimeTable> teacher;// = new List<formatTimeTable>();
        public static string name;
        public static int selectNomber;
    }

    class DataMass
    {
        private static bool needToSave;
        static DataMass() { needToSave = false; }
        public static List<List<formatTimeTable>> massData = new List<List<formatTimeTable>>();
        public static List<string> massName = new List<string>();
        private static int nomber = 1;
        public static void saveAndClear(string name)
        {
            Data.teacher = new List<formatTimeTable>(35);
            Data.name = name;
            massData.Add(Data.teacher); massName.Add("" + nomber++ + " " + Data.name);
        }

        public static void setData(int N)
        {
            if (N < massData.Count)
                try
                {
                    Data.name = massName[N];
                    Data.teacher = massData[N];
                }
                catch { }
        }
        public static void pop(int N)
        {
            if (N < massData.Count)
                try
                {
                    massData.RemoveAt(N);
                    massName.RemoveAt(N);
                }
                catch { }
        }
    }
}
