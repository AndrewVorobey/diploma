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
            #region inicialization
            int namesLen = Data.teacher.Count;
            const int pairLen = 4;//Сколько пар в день отображать
            string[] daysNames = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница" };
            string[] times = { "9.30-10.30", "11.10–12.45", "13.45–15.20", "15.35–17.10", "17.25–19.00??" };
            object nr = 1;
            object nc = pairLen;
            object start = 0;
            object end = 0;
            const int dayWidth = 120;//ширина одного дня
            const int namesWidth = 95;//ширина имен.

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
            doc.PageSetup.Orientation=Word.WdOrientation.wdOrientLandscape;
            //Создание таблицы
            Word.Range cellRange;
            Word.Range tableLocation = doc.Range(ref start, ref end);
            doc.Tables.Add(tableLocation, namesLen + 2, 7, ref DefaultTableBehavior, ref AutoFitBehavior);
            Word.Table table = doc.Tables[1];
            table.Borders.Enable = 1;
            table.Rows.SetHeight(14f, Microsoft.Office.Interop.Word.WdRowHeightRule.wdRowHeightExactly); 

            //Формирование Шрифтов 
            Word.Font TextFont = new Word.Font();
            TextFont.Size = 8;
            TextFont.Bold = 0;
            table.Cell(1, 1).Width = namesWidth;
            cellRange = table.Cell(1, 1).Range;
            cellRange.Font = TextFont;
            #endregion

            formatTable(ref table, ref TextFont, pairLen, dayWidth, namesWidth, namesLen, cellRange);
            setBorders(ref table, pairLen);
            setDateToTable(ref doc, ref TextFont, pairLen, dayWidth, namesWidth, namesLen);

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
        private static void formatTable(ref Word.Table table, ref  Word.Font TextFont, int pairLen, int dayWidth, int namesWidth, int namesLen, Word.Range cellRange)
        {

            string[] daysNames = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница" };
            string[] times = { "9.30-10.30", "11.10–12.45", "13.45–15.20", "15.35–17.10", "17.25–19.00??" };
            object nr = 1;
            object nc = pairLen;
            //Прописываем дни недели. 
            for (int i = 0; i < 5; i++)
            {
                table.Cell(1, i + 2).Width = dayWidth;
                cellRange = table.Cell(1, i + 2).Range;
                cellRange.Text = daysNames[i];
            }
            table.Cell(1, 1 + 5 + 1).Width = namesWidth;

            //Делим дни на пары
            for (int i = 0; i < 5; i++)
                table.Cell(2, 2 + i * 4).Split(ref nr, ref nc);
            table.Cell(2, 1).Width = namesWidth;
            table.Cell(2, 1 + pairLen * 5 + 1).Width = namesWidth;

            TextFont.Size = 5;
            //Задаем размер пар, и подписываем время. 

            for (int i = 0; i < 5 * pairLen; i++)
            {
                table.Cell(2, i + 2).Width = dayWidth / pairLen;
                cellRange = table.Cell(2, i + 2).Range;
                cellRange.Font = TextFont;
                cellRange.Text = times[i % pairLen];
            }

            TextFont.Size = 6;
            //Делаем разметку под пары. 
            for (int i = 3; i < namesLen + 3; i++)
            {
                table.Cell(i, 1).Width = namesWidth;
                table.Cell(i, 1+5+1).Width = namesWidth;
                for (int k = 0; k < 5; k++)
                    table.Cell(i, 2 + k * 4).Split(ref nr, ref nc);

                for (int j = 0; j < 5 * pairLen; j++)
                    table.Cell(i, j + 2).Width = dayWidth / pairLen;

            }


        }
        private static void setBorders(ref Word.Table table, int pairLen)
        {
            //Первая строка
            for (int i = 0; i < 7; i++)
            {
                table.Cell(1, i).Borders[Word.WdBorderType.wdBorderRight].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
                table.Cell(1, i).Borders[Word.WdBorderType.wdBorderRight].LineWidth = Word.WdLineWidth.wdLineWidth100pt;
            }

            //остальные строки. 
            for (int j = 2; j <= table.Rows.Count; j++)
            {
                for (int i = 0; i < 5 * pairLen; i += pairLen)
                {
                    table.Cell(j, i + 2).Borders[Word.WdBorderType.wdBorderLeft].LineStyle = Word.WdLineStyle.wdLineStyleSingle;
                    table.Cell(j, i + 2).Borders[Word.WdBorderType.wdBorderLeft].LineWidth = Word.WdLineWidth.wdLineWidth100pt;
                }
                table.Cell(j, 5 * pairLen + 2).Borders[Word.WdBorderType.wdBorderLeft].LineWidth = Word.WdLineWidth.wdLineWidth100pt;
            }


        }
        private static void setDateToTable(ref Word.Document doc, ref  Word.Font TextFont, int pairLen, int dayWidth, int namesWidth, int namesLen)
        {
            Word.Table table = doc.Tables[1];
            Word.Range cellRange;
            //Заполнение созданной таблицы данными
            for (int j = 3; j < namesLen + 3; j++)
            {
            TextFont.Size = 10;
                cellRange = table.Cell(j, 1).Range;
                cellRange.Font = TextFont;
                cellRange.Text = Data.teacher[j - 3].name;
                cellRange = table.Cell(j, 1 + 5 * pairLen + 1).Range;
                cellRange.Font = TextFont;
                cellRange.Text = Data.teacher[j - 3].name;
                TextFont.Size = 5;
                for (int k = 2; k < 22; k++)
                {
                    cellRange.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceSingle;
                    object A = cellRange.ParagraphFormat;
                    cellRange = table.Cell(j, k).Range;
                    cellRange.Font = TextFont;
                    //cellRange.Rows.SetHeight(20, Word.WdRowHeightRule.wdRowHeightExactly); Установка высоты строки
                    cellRange.Text = Data.teacher[j - 3].lesson[(k - 2) / 4, (k - 2) % 4].ToString();
                    cellRange.ParagraphFormat.SpaceAfter = 0.0f;
                    cellRange.ParagraphFormat.LeftIndent = doc.Content.Application.CentimetersToPoints((float)-0.2);//Отступ отрицательный, что бы запись занимала ячейку полностью.
                    cellRange.ParagraphFormat.RightIndent = doc.Content.Application.CentimetersToPoints((float)-0.2);
                }
            }


        }

    }


}
