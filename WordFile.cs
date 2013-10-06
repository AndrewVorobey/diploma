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
            return regex.Match(s).Value;

        }

        public static void ReadFromFile(Object filename)
        {
            // String[] Names = new String[34] { "Александров А.А.", "Амосов А.А.", "Амосова О.А.", "Ахметшин А.А", "Бредихин Р.Н.", "Булычева О.Н.", " Вестфальский А.Е.", "Горелов В.А.", "Горицкий Ю.А.", "Григорьев В.П.", "Дубинский Ю.А.", "Дубовицкая Н.В.", "Жилейкин Я.М.", "Заславский А.А.", "Злотник А.А.", "Зубков П.В.", "Зубов В.С.", "Игнатьева Н.У.", "Ишмухаметов А.З.", "Казенкин К.О.", "Кирсанов М.Н.", "Князев А.В.", "Крупин Г.В.", "Кубышин С.Ю.", "Ляшенко Л.И.", "Мамонтов А.И.", "Макаров П.В.", "Мещанинов Д.Г.", "Набебин А.А.", "Перескоков А.В.", "Титов Д.А.", "Фролов А.Б.", "Черепова М.Ф.", "Шевченко И.В." };


            Word.Application app = new Word.ApplicationClass();
            Word.Document doc = new Word.DocumentClass();

            Object confirmConversions = Type.Missing;
            Object readOnly = Type.Missing;
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

            doc = app.Documents.Open(ref filename, ref confirmConversions, ref readOnly, ref addToRecentFiles,
            ref passwordDocument, ref passwordTemplate, ref revert, ref writePasswordDocument, ref writePasswordTemplate,
            ref format, ref encoding, ref visible, ref openConflictDocument, ref openAndRepair, ref documentDirection, ref noEncodingDialog);
            if (doc.Tables.Count == 0)
            {
                //Err. TODOk
            }
            else
            {
                DataMass.saveAndClear(".doc");

                for (int i = 1; i <= doc.Tables.Count; i++)
                {
                    //loadStatus.progress.StatusBarPlass(i, doc.Tables.Count);

                    formatTimeTable buf = new formatTimeTable("", 6);
                    Word.Table t = doc.Tables[i];
                    for (int k = 2; k <= t.Columns.Count; k++)
                    {
                        for (int j = 2; j <= t.Rows.Count; j++)
                        {
                            string str = t.Cell(j, k).Range.Text;
                            Lesson A = new Lesson(0);
                            A.italy = (t.Cell(j, k).Range.Italic != -1);
                            A.fromString(str);
                            if (j <= 6 && k < 7)
                                buf.lesson[k - 2, j - 2] = A;
                            //Записано.

                            if (t.Rows.Count < 8)
                                A = new Lesson(0);
                            A.fromString("");
                            buf.lesson[k - 2, j - 1] = A;

                        }

                    }

                    Data.teacher.Add(buf);


                }
            }
            //считывания имен
            int StrI = 0;
            string txt;
            /*  for (int i = 1; i <= doc.Paragraphs.Count; i++)
              {
                  txt = doc.Paragraphs[i].Range.Text;
                  txt = FindName(txt);
                  if (txt != "")
                  {
                      Data.teacher[StrI++].name = txt;
                      i += 39;
                  }
                //  if (Data.teacher.Count == StrI) break;
              }
              */
            Object saveChanges = Word.WdSaveOptions.wdSaveChanges;
            Object originalFormat = Type.Missing;
            Object routeDocument = Type.Missing;
            app.Quit(ref saveChanges, ref originalFormat, ref routeDocument);

            return;
        }

        public static void CreateWordDoc(string name)
        {
            //"\f\r"-перенос на следующую строку. 

            #region inicialization
            int namesLen = Data.teacher.Count;
            string[] days = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница" };
            string[] times = { "9.30-10.30", "11.10–12.45", "13.45–15.20", "15.35–17.10", "17.25–19.00??" };
            const int pairLen = 4;//Сколько пар в день отображать
            object start = 0;
            object end = 0;
            object nr = 1;
            object nc = pairLen;
            int dayWidth = 160;
            int nameWidth = 100;

            Word.Application app = new Word.ApplicationClass();
            Word.Document doc = new Word.DocumentClass();
            Object template = Type.Missing;
            Object newTemplate = Type.Missing;
            Object documentType = Type.Missing;
            Object visible = Type.Missing;
            app.Documents.Add(ref template, ref newTemplate, ref documentType, ref visible);
            Object DefaultTableBehavior = Type.Missing;
            Object AutoFitBehavior = Type.Missing;
            //Формирование страницы
            doc.PageSetup.PageWidth = 1200;
            doc.PageSetup.PageHeight = 1584;
            //Создание таблицы
            Word.Range r1;
            Word.Range tableLocation = doc.Range(ref start, ref end);
            doc.Tables.Add(tableLocation, namesLen + 2, 7, ref DefaultTableBehavior, ref AutoFitBehavior);
            Word.Table t = doc.Tables[1];
           t.Borders.Enable = 1;

            //Формирование Шрифтов 
            Word.Font f = new Word.Font();
            f.Size = 8;
            f.Bold = 1;
            t.Cell(1, 1).Width = nameWidth;
            r1 = t.Cell(1, 1).Range;
            r1.Font = f;


            #endregion

            //Прописываем дни недели. 
            for (int i = 0; i < 5; i++)
            {
                t.Cell(1, i + 2).Width = dayWidth;
                r1 = t.Cell(1, i + 2).Range;
                r1.Text = days[i];
               // t.Cell(1, i + 2).Borders[Word.WdBorderType.wdBorderLeft].LineStyle = Word.WdLineStyle.wdLineStyleSingle;

                
                    t.Cell(1, i ).Borders[Word.WdBorderType.wdBorderRight].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
                    t.Cell(1, i).Borders[Word.WdBorderType.wdBorderRight].LineWidth = Word.WdLineWidth.wdLineWidth150pt;
            }

            t.Cell(1, 5).Borders[Word.WdBorderType.wdBorderRight].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            t.Cell(1, 5).Borders[Word.WdBorderType.wdBorderRight].LineWidth = Word.WdLineWidth.wdLineWidth150pt;
            t.Cell(1, 6).Borders[Word.WdBorderType.wdBorderRight].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
            t.Cell(1, 6).Borders[Word.WdBorderType.wdBorderRight].LineWidth = Word.WdLineWidth.wdLineWidth150pt;

            //Делим дни на пары
            for (int i = 0; i < 5; i++)
                t.Cell(2, 2 + i * 4).Split(ref nr, ref nc);
            t.Cell(2, 1).Width = nameWidth;
            
            f.Bold = 1;
            //Задаем размер пар, и подписываем время. 
           
            for (int i = 0; i < 5 * pairLen; i++)
            {
                t.Cell(2, i + 2).Width = dayWidth / pairLen;
                r1 = t.Cell(2, i + 2).Range;
                r1.Font = f;
                r1.Text = times[i % pairLen];

                if (i % pairLen == 0)
                {
                    t.Cell(2, i + 2).Borders[Word.WdBorderType.wdBorderLeft].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
                    t.Cell(2, i + 2).Borders[Word.WdBorderType.wdBorderLeft].LineWidth = Word.WdLineWidth.wdLineWidth150pt;
                }
            }

            //Делаем разметку под пары. 
            for (int i = 3; i < namesLen + 3; i++)
            {
                t.Cell(i, 1).Width = nameWidth;
                for (int k = 0; k < 5; k++)
                    t.Cell(i, 2 + k * 4).Split(ref nr, ref nc);

                for (int j = 0; j < 5 * pairLen; j++)
                {
                    t.Cell(i, j + 2).Width = dayWidth / pairLen;
                    if (j % pairLen == 0)
                    {
                        t.Cell(i, j + 2).Borders[Word.WdBorderType.wdBorderLeft].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
                        t.Cell(i, j + 2).Borders[Word.WdBorderType.wdBorderLeft].LineWidth = Word.WdLineWidth.wdLineWidth150pt;
                    }

                }

            }

            //Заполнение созданной таблицы данными
            for (int j = 3; j < namesLen + 3; j++)
            {
                f.Size = 10;
                r1 = t.Cell(j, 1).Range;
                r1.Font = f;
                r1.Text = Data.teacher[j - 3].name;
                r1 = t.Cell(j, 1 + 5 * pairLen + 1).Range;
                r1.Font = f;
                r1.Text = Data.teacher[j - 3].name;


                r1.Borders[Word.WdBorderType.wdBorderLeft].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
                r1.Borders[Word.WdBorderType.wdBorderLeft].LineWidth = Word.WdLineWidth.wdLineWidth150pt;

                f.Size = 5;
                for (int k = 2; k < 22; k++)
                {
                    r1.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceSingle;
                    object A = r1.ParagraphFormat;
                    if (k == 21)
                        f.Size = 5;
                    r1 = t.Cell(j, k).Range;
                    r1.Font = f;
                    r1.Rows.SetHeight(20, Word.WdRowHeightRule.wdRowHeightExactly);// = 30;
                    r1.Text = Data.teacher[j - 3].lesson[(k - 2) / 4, (k - 2) % 4].ToString();
                    //r1.ParagraphFormat.LineSpacing = 5f;
                    r1.ParagraphFormat.SpaceAfter = 0.7f;
                    r1.ParagraphFormat.LeftIndent=doc.Content.Application.CentimetersToPoints((float)-0.2);//Отступ отрицательный, что бы запись занимала ячейку полностью.
                    r1.ParagraphFormat.RightIndent = doc.Content.Application.CentimetersToPoints((float)-0.2);
                }
            }


          //Microsoft.Office.Interop.Word.WdRowHeightRule



            #region save
            Object fileName = name;//@"C:\Test\Форматированная_Таблица.doc";
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
            #endregion

        }
    }


}
