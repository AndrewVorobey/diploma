using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Lesson(bool ex, bool lec, string gr, string rn, string sub) { exist = ex; lection = lec; grup = gr; roomNomber = rn; subject = sub; }
        bool exist;
        bool lection;
        string grup;
        string roomNomber;
        string subject;
        public void fromString(string str)
        {
            string[] faculty = { "A", "А", "ТФ", "ЭЛ" };
            int posBegin = 0;//TODO
            int posGrup = 0;//done
            int posSubject = 0;//TODO
            int posRoomNomber = 0;// done


            //free
            if (str.Length < 5 || str[posBegin] == '*') { this.exist = false; return; }
            else this.exist = true;

            //Если стоит четность недели
            if (str[posBegin] >= '0' && str[posBegin] <= '9')
            {
                //TODO
                //вычленнить четность недели и сделать рекурсивный вызов без этого символа
            }
            else// если без четности
            {
                posSubject = posBegin;
                for (int i = 0; i < faculty.Length; i++)//Определяем потоковые ли занятия, определяем 
                {
                    posGrup = str.IndexOf(faculty[i]);
                }
                posRoomNomber = str.IndexOf(' ', posGrup + 7) + 1;
                if (posRoomNomber - posGrup <= 9) lection = true;
                else lection = false;

                //subject = ;
                //  grup = ;
                //roomNomber = ;
                //


            }
        }
        int findInStr(string A, string S)
        {
            return 0;
        }
    }

    class formatTimeTable
    {
        const int days = 5;
        public formatTimeTable(string name, int maxLessonLen) { this.name = name; lesson = new Lesson[days, maxLessonLen]; }
        string name;
        Lesson[,] lesson;   
    }


    class Data
    {
       // List<formatTimeTable> 
            
    }
}
