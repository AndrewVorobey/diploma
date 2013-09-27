using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
using System.Text.RegularExpressions;





namespace Диплом
{

    class WordFile
    {
        static timeTable[] f1 = new timeTable[35];
        public Word.Selection sln;
        private Object miss = Type.Missing;
        public Word.Range rng;
        [STAThread]
        //выводит первое число из файла.
        static int FindInt(string s, int i)
        {
            int[] matches = Regex.Matches(s, "\\d+")
                .Cast<Match>()
                .Select(x => int.Parse(x.Value))
                .ToArray();
            foreach (int match in matches) ;
            if (matches.Length > i)
                return matches[i];
            else return -1;
        }
        static string FindName(string s)
        {
           // const string patternGroup = @"(A|А|ТФ|ЭЛ)" + @"(\s?-?–?\s?(\d{1,2}\,?-?–?){1,5}\s?–?-?\s?\d{2})";//А-14-11
            Regex regex = new Regex(@"\s([А-Я])([а-я]*)\s([А-Я])\.\s?([А-Я])\.?");//");
            return  regex.Match(s).Value;
            
        }

        public static void ReadFromFile(Object filename)
        {
            String[] Names = new String[34] { "Александров А.А.", "Амосов А.А.", "Амосова О.А.", "Ахметшин А.А", "Бредихин Р.Н.", "Булычева О.Н.", " Вестфальский А.Е.", "Горелов В.А.", "Горицкий Ю.А.", "Григорьев В.П.", "Дубинский Ю.А.", "Дубовицкая Н.В.", "Жилейкин Я.М.", "Заславский А.А.", "Злотник А.А.", "Зубков П.В.", "Зубов В.С.", "Игнатьева Н.У.", "Ишмухаметов А.З.", "Казенкин К.О.", "Кирсанов М.Н.", "Князев А.В.", "Крупин Г.В.", "Кубышин С.Ю.", "Ляшенко Л.И.", "Мамонтов А.И.", "Макаров П.В.", "Мещанинов Д.Г.", "Набебин А.А.", "Перескоков А.В.", "Титов Д.А.", "Фролов А.Б.", "Черепова М.Ф.", "Шевченко И.В." };

            for (int i = 0; i < 35; i++)
            {
                f1[i] = new timeTable();
            }

            Word.Application app = new Word.ApplicationClass();
            Word.Document doc = new Word.DocumentClass();

            Object confirmConversions = Type.Missing;
            Object readOnly = true;// Type.Missing;
            Object addToRecentFiles = Type.Missing;
            Object passwordDocument = Type.Missing;
            Object passwordTemplate = Type.Missing;
            Object revert = Type.Missing;
            Object writePasswordDocument = Type.Missing;
            Object writePasswordTemplate = Type.Missing;
            Object format = Type.Missing;
            Object encoding = Type.Missing;
            Object visible = Type.Missing;
            Object openConflictDocument = Type.Missing;
            Object openAndRepair = Type.Missing;
            Object documentDirection = Type.Missing;
            Object noEncodingDialog = Type.Missing;
            string f = "";
            doc = app.Documents.Open(ref filename, ref confirmConversions, ref readOnly, ref addToRecentFiles,
            ref passwordDocument, ref passwordTemplate, ref revert, ref writePasswordDocument, ref writePasswordTemplate,
            ref format, ref encoding, ref visible, ref openConflictDocument, ref openAndRepair, ref documentDirection, ref noEncodingDialog);
            if (doc.Tables.Count == 0)
            {
                //Err. TODOk
            }
            else
            {

                //Очистка входных данных
                Data.teacher.Clear();

                for (int i = 1; i <= doc.Tables.Count; i++)
                {
                    //loadStatus.progress.StatusBarPlass(i, doc.Tables.Count);
                    
                formatTimeTable buf=new formatTimeTable("",6);
                    Word.Table t = doc.Tables[i];
                    for (int k = 2; k <= t.Columns.Count; k++)
                    {
                        for (int j = 2; j <= t.Rows.Count; j++)
                        {
                            Lesson A=new Lesson(0);
                            string str = t.Cell(j, k).Range.Text;
                            A.fromString(str);
                            if(j<6 && k<7)
                            buf.lesson[k - 2, j - 2] = A;
                        }

                    }

                    Data.teacher.Add(buf);
 
                  
                }
            }
            //считывания имен
            int StrI = 0;
            string txt;
            for (int i = 1; i <= doc.Paragraphs.Count; i++)
            {
                txt=doc.Paragraphs[i].Range.Text;
               string name= FindName(txt);
               if (name != "")
               {
                   Data.teacher[StrI++].name = name;
                   i += 39;
               }
            }
            Object saveChanges = Word.WdSaveOptions.wdSaveChanges;
            Object originalFormat = Type.Missing;
            Object routeDocument = Type.Missing;
            app.Quit(ref saveChanges, ref originalFormat, ref routeDocument);

            return;
        }

        public static void CreateWordDoc(string name)
        {
            Word.Application app = new Word.ApplicationClass();
            Word.Document doc = new Word.DocumentClass();

            Object template = Type.Missing;
            Object newTemplate = Type.Missing;
            Object documentType = Type.Missing;
            Object visible = Type.Missing;
            app.Documents.Add(ref template, ref newTemplate, ref documentType, ref visible);
            Object DefaultTableBehavior = Type.Missing;
            Object AutoFitBehavior = Type.Missing;
            object start = 0;
            object end = 0;
            object nr = 1;
            object nc = 4;
            //Формирование страницы
            doc.PageSetup.PageWidth = 1200;
            doc.PageSetup.PageHeight = 1584;
            //Создание таблицы
            Word.Range r1;
            Word.Range tableLocation = doc.Range(ref start, ref end);
            doc.Tables.Add(tableLocation, 35, 6, ref DefaultTableBehavior, ref AutoFitBehavior);
            Word.Table t = doc.Tables[1];

            //Формирование Шрифтов начало
            Word.Font f = new Word.Font();
            f.Size = 8;
            f.Bold = 1;
            t.Cell(1, 1).Width = 150;
            r1 = t.Cell(1, 1).Range;
            r1.Font = f;

            //Формирование Шрифтов конец

            t.Cell(1, 2).Width = 160;
            r1 = t.Cell(1, 2).Range;
            r1.Text = "Понедельник";

            t.Cell(1, 3).Width = 160;
            r1 = t.Cell(1, 3).Range;
            r1.Text = "Вторник";

            t.Cell(1, 4).Width = 160;
            r1 = t.Cell(1, 4).Range;
            r1.Text = "Среда";

            t.Cell(1, 5).Width = 160;
            r1 = t.Cell(1, 5).Range;
            r1.Text = "Четверг";

            t.Cell(1, 6).Width = 160;
            r1 = t.Cell(1, 6).Range;
            r1.Text = "Пятница";

            t.Cell(2, 2).Split(ref nr, ref nc);
            t.Cell(2, 6).Split(ref nr, ref nc);
            t.Cell(2, 10).Split(ref nr, ref nc);
            t.Cell(2, 14).Split(ref nr, ref nc);
            t.Cell(2, 18).Split(ref nr, ref nc);

            t.Cell(2, 1).Width = 150;



            t.Cell(2, 2).Width = 40;
            r1 = t.Cell(2, 2).Range;
            r1.Font = f;
            r1.Text = "9.30-10.30";
            t.Cell(2, 3).Width = 40;
            r1 = t.Cell(2, 3).Range;
            r1.Font = f;
            r1.Text = "11.10–12.45";
            t.Cell(2, 4).Width = 40;
            r1 = t.Cell(2, 4).Range;
            r1.Font = f;
            r1.Text = "13.45–15.20";
            t.Cell(2, 5).Width = 40;
            r1 = t.Cell(2, 5).Range;
            r1.Font = f;
            r1.Text = "15.35–17.10";
            t.Cell(2, 6).Width = 40;
            r1 = t.Cell(2, 6).Range;
            r1.Font = f;
            r1.Text = "9.30-10.30";
            t.Cell(2, 7).Width = 40;
            r1 = t.Cell(2, 7).Range;
            r1.Font = f;
            r1.Text = "11.10–12.45";
            t.Cell(2, 8).Width = 40;
            r1 = t.Cell(2, 8).Range;
            r1.Font = f;
            r1.Text = "13.45–15.20";
            t.Cell(2, 9).Width = 40;
            r1 = t.Cell(2, 9).Range;
            r1.Font = f;
            r1.Text = "15.35–17.10";
            t.Cell(2, 10).Width = 40;
            r1 = t.Cell(2, 10).Range;
            r1.Font = f;
            r1.Text = "9.30-10.30";

            t.Cell(2, 11).Width = 40;
            r1 = t.Cell(2, 11).Range;
            r1.Font = f;
            r1.Text = "11.10–12.45";
            t.Cell(2, 12).Width = 40;
            r1 = t.Cell(2, 12).Range;
            r1.Font = f;
            r1.Text = "13.45–15.20";
            t.Cell(2, 13).Width = 40;
            r1 = t.Cell(2, 13).Range;
            r1.Font = f;
            r1.Text = "15.35–17.10";
            t.Cell(2, 14).Width = 40;
            r1 = t.Cell(2, 14).Range;
            r1.Font = f;
            r1.Text = "9.30-10.30";

            t.Cell(2, 15).Width = 40;
            r1 = t.Cell(2, 15).Range;
            r1.Font = f;
            r1.Text = "11.10–12.45";

            t.Cell(2, 16).Width = 40;
            r1 = t.Cell(2, 16).Range;
            r1.Font = f;
            r1.Text = "13.45–15.20";

            t.Cell(2, 17).Width = 40;
            r1 = t.Cell(2, 17).Range;
            r1.Font = f;
            r1.Text = "15.35–17.10";

            t.Cell(2, 18).Width = 40;
            r1 = t.Cell(2, 18).Range;
            r1.Font = f;
            r1.Text = "15.35–17.10";

            t.Cell(2, 19).Width = 40;
            r1 = t.Cell(2, 19).Range;
            r1.Font = f;
            r1.Text = "15.35–17.10";

            t.Cell(2, 20).Width = 40;
            r1 = t.Cell(2, 20).Range;
            r1.Font = f;
            r1.Text = "15.35–17.10";

            t.Cell(2, 21).Width = 40;
            r1 = t.Cell(2, 21).Range;
            r1.Font = f;
            r1.Text = "15.35–17.10";
            //t.Cell(2, 22).Width = 40;
            //r1 = t.Cell(2, 22).Range;
            //r1.Font = f;
            //r1.Text = "15.35–17.10";
            f.Bold = 0;
            for (int i = 3; i < 36; i++)
            {
                t.Cell(i, 1).Width = 150;
                t.Cell(i, 2).Split(ref nr, ref nc);
                t.Cell(i, 6).Split(ref nr, ref nc);
                t.Cell(i, 10).Split(ref nr, ref nc);
                t.Cell(i, 14).Split(ref nr, ref nc);
                t.Cell(i, 18).Split(ref nr, ref nc);
                for (int j = 2; j <= 21; j++)
                {
                    t.Cell(i, j).Width = 40;

                }

            }

            //Заполнение созданной таблицы данными
            for (int j = 3; j < 36; j++)
            {
                int c1, c2, c3, c4, c5;
                r1 = t.Cell(j, 1).Range;
                r1.Font = f;
                r1.Text = f1[j - 3].Name;

                for (c1 = 2; c1 < 6; c1++)
                {
                    if ((f1[j - 3].Monday[4] == null) && (f1[j - 3].Monday[3] != null))
                    {
                        r1 = t.Cell(j, c1).Range;
                        r1.Font = f;
                        r1.Text = f1[j - 3].Monday[c1 - 2];

                    }
                    if (f1[j - 3].Monday[4] != null)
                    {
                        r1 = t.Cell(j, c1).Range;
                        r1.Font = f;
                        r1.Text = f1[j - 3].Monday[c1 - 1];

                    }
                    if ((f1[j - 3].Monday[4] == null) && (f1[j - 3].Monday[3] == null))
                    {
                        r1 = t.Cell(j, c1 + 1).Range;
                        r1.Font = f;
                        r1.Text = f1[j - 3].Monday[c1 - 2];

                    }


                }
                for (c2 = 6; c2 < 10; c2++)
                {
                    if ((f1[j - 3].Thursday[4] == null) && (f1[j - 3].Thursday[3] != null))
                    {
                        r1 = t.Cell(j, c2).Range;
                        r1.Font = f;
                        r1.Text = f1[j - 3].Thursday[c2 - 6];

                    }
                    if (f1[j - 3].Thursday[4] != null)
                    {
                        r1 = t.Cell(j, c2).Range;
                        r1.Font = f;
                        r1.Text = f1[j - 3].Thursday[c2 - 5];

                    }
                    if ((f1[j - 3].Thursday[4] == null) && (f1[j - 3].Thursday[3] == null))
                    {
                        r1 = t.Cell(j, c2 + 1).Range;
                        r1.Font = f;
                        r1.Text = f1[j - 3].Thursday[c2 - 6];

                    }


                }

                for (c3 = 10; c3 < 14; c3++)
                {
                    if ((f1[j - 3].Wednesday[4] == null) && (f1[j - 3].Wednesday[3] != null))
                    {
                        r1 = t.Cell(j, c3).Range;
                        r1.Font = f;
                        r1.Text = f1[j - 3].Wednesday[c3 - 10];

                    }
                    if ((f1[j - 3].Wednesday[4] != null) && (f1[j - 3].Wednesday[3] != null))
                    {
                        r1 = t.Cell(j, c3).Range;
                        r1.Font = f;
                        r1.Text = f1[j - 3].Wednesday[c3 - 9];

                    }
                    if ((f1[j - 3].Wednesday[4] == null) && (f1[j - 3].Wednesday[3] == null))
                    {
                        r1 = t.Cell(j, c3 + 1).Range;
                        r1.Font = f;
                        r1.Text = f1[j - 3].Wednesday[c3 - 10];


                    }

                }
                for (c4 = 14; c4 < 18; c4++)
                {
                    if ((f1[j - 3].Tuesday[4] == null) && (f1[j - 3].Tuesday[3] != null))
                    {
                        r1 = t.Cell(j, c4).Range;
                        r1.Font = f;
                        r1.Text = f1[j - 3].Tuesday[c4 - 14];

                    }
                    if ((f1[j - 3].Tuesday[4] != null) && (f1[j - 3].Tuesday[3] != null))
                    {
                        r1 = t.Cell(j, c4).Range;
                        r1.Font = f;
                        r1.Text = f1[j - 3].Tuesday[c4 - 13];

                    }
                    if ((f1[j - 3].Tuesday[4] == null) && (f1[j - 3].Tuesday[3] == null))
                    {
                        r1 = t.Cell(j, c4 + 1).Range;
                        r1.Font = f;
                        r1.Text = f1[j - 3].Tuesday[c4 - 14];


                    }



                }
                for (c5 = 18; c5 < 22; c5++)
                {
                    if ((f1[j - 3].Friday[4] == null) && (f1[j - 3].Friday[3] != null))
                    {
                        r1 = t.Cell(j, c5).Range;
                        r1.Font = f;
                        r1.Text = f1[j - 3].Friday[c5 - 18];

                    }
                    if ((f1[j - 3].Friday[4] != null) && (f1[j - 3].Friday[3] != null))
                    {
                        r1 = t.Cell(j, c5).Range;
                        r1.Font = f;
                        r1.Text = f1[j - 3].Friday[c5 - 17];

                    }
                    if ((f1[j - 3].Friday[4] == null) && (f1[j - 3].Friday[3] == null))
                    {
                        try
                        {
                            r1 = t.Cell(j, c5 + 1).Range;
                        }
                        catch { break; }
                        r1.Font = f;
                        r1.Text = f1[j - 3].Friday[c5 - 18];


                    }
                }


            }


            Object fileName = @"C:\Test\Форматированная_Таблица.doc";
            Object fileFormat = Type.Missing;
            Object lockComments = Type.Missing;
            Object password = Type.Missing;
            Object addToRecentFiles = Type.Missing;
            Object writePassword = Type.Missing;
            Object readOnlyRecommended = Type.Missing;
            Object embedTrueTypeFonts = Type.Missing;
            Object saveNativePictureFormat = Type.Missing;
            Object saveFormsData = Type.Missing;
            Object saveAsAOCELetter = Type.Missing;
            Object encoding = Type.Missing;
            Object insertLineBreaks = Type.Missing;
            Object allowSubstitutions = Type.Missing;
            Object lineEnding = Type.Missing;
            Object addBiDiMarks = Type.Missing;

            doc.SaveAs(ref fileName, ref fileFormat, ref lockComments,
                ref password, ref addToRecentFiles, ref writePassword,
                ref readOnlyRecommended, ref embedTrueTypeFonts,
                ref saveNativePictureFormat, ref saveFormsData,
                ref saveAsAOCELetter, ref encoding, ref insertLineBreaks,
                ref allowSubstitutions, ref lineEnding, ref addBiDiMarks);


            Object saveChanges = Word.WdSaveOptions.wdSaveChanges;
            Object originalFormat = Type.Missing;
            Object routeDocument = Type.Missing;
            app.Quit(ref saveChanges, ref originalFormat, ref routeDocument);


        }
    }

}
