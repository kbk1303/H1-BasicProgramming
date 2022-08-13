using System;
using System.Windows;
namespace GrundlæggendeStart
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Program.WriteWino();
            //Program.WriteLotto();
            //Program.WriteChristmasTree();
            Program.PasswordValidator.MainStart();
                
        }
        #region Wino
        //175134 175388 172818 142709 151437 152620 150979 152210 149450 154398 150160
        static void WriteWino()
        {
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("---------------- Wino -------------------------------------");
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine();
            int[,] data =
                { { 2009,175314 }, { 2010, 175388}, {2011, 172818 }, { 2012, 142709 }, {2013, 151437 }, {2014, 152620 }, { 2015, 150979 }, { 2016, 152210}, {2017, 149450 }, { 2018, 154398}, {2019, 150160 } };
            int[] years = new int[11];
            int[] sales = new int[years.Length];
            const int MAX_SALES = 175388;
            const byte MAX_STARS = 100;
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {

                    if ((j) % 2 == 0)
                    {
                        years[i] = data[i, j];
                    }
                    else
                    {
                        sales[i] = data[i, j];
                    }
                }
            }
            Array.Sort(sales, years);
            Array.Reverse(sales);
            Array.Reverse(years);
            for (int i = 0; i < years.Length; i++)
            {
                double index = (double)sales[i] / (float)MAX_SALES;
                int stars = (int)(index * MAX_STARS);
                Console.Write($"year: {years[i]} ");
                do
                {
                    Console.Write("*");
                    stars--;
                } while (stars > 0);
                Console.WriteLine($" {sales[i]}");
            }
        }
        #endregion
        #region Lotto
        static void WriteLotto()
        {
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("---------------- Lotto ------------------------------------");
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine();
            sbyte[] availableLottoNumbers = new sbyte[36];
            for(int i = 0; i < availableLottoNumbers.Length; i++)
            {
                availableLottoNumbers[i] = (sbyte)(i + 1);
            }
            byte[] lottoNumbers = new byte[7];
            byte lottoNumbersIndex = 0;
            sbyte joker =  -1;
            Random rnd = new Random();
            do
            {
                int index = rnd.Next(0, 36);
                if (availableLottoNumbers[index] > 0)
                {
                    lottoNumbers[lottoNumbersIndex++] = (byte)availableLottoNumbers[index];
                    availableLottoNumbers[index] = -1;
                }
            } while (lottoNumbersIndex < lottoNumbers.Length);
            while(joker == -1)
            {
                joker = availableLottoNumbers[rnd.Next(0, 36)];
            }
            Array.Sort(lottoNumbers);
            for (int i = 0; i  < lottoNumbers.Length; i++)
            {
                Console.Write($"{lottoNumbers[i]} ");
                System.Threading.Thread.Sleep(2000);
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(joker);
            Console.ResetColor();
        }
        #endregion
        #region ChristmasTree
        static void WriteChristmasTree()
        {
            /**
             * So in order to understand the christmas tree I have chosen to se it as a 
             * two-dim array of chars, either containing a whitespace or a *
             * 
             * The dimension of the tree is 8 rows and 15 cols
             * 
             *         *
             *        ***
             *       *****
             *      *******
             *     *********
             *    ***********
             *   *************
             *  ***************
             */

            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("----------------Cristmas Tree------------------------------");
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine();

            const int ROWS = 8;
            const int COLS = 15;
            const int MIDDLE_STAR_POS = 8;
            byte radixFromMiddle = 0;
            char[,] christmasTree = new char[ROWS, COLS];
            for(int row = 0; row < ROWS; row++)
            {
                christmasTree[row, COLS % MIDDLE_STAR_POS] = '*';
                for (int radix = 0; radix <= radixFromMiddle; radix++)
                {
                    if (radix != 0)
                    {
                        christmasTree[row, (MIDDLE_STAR_POS - 1) - radix] = '*';
                        christmasTree[row, (MIDDLE_STAR_POS - 1) + radix] = '*';
                    }
                }
                radixFromMiddle++;
                /* we need to align the left (neagative) side of the tree by replacing the blanks with whitespaces
                 * in order to show the tree correct. Console.Write does not print blanks ('\0') */
                for (int fillSpaces = (MIDDLE_STAR_POS - 1) - radixFromMiddle; fillSpaces >= 0; fillSpaces--)
                {
                    christmasTree[row, fillSpaces] = ' ';
                }
            }
            
            bool isgreen = true;
            for (int row = 0; row < ROWS; row++)
            {
                isgreen = true;
                for (int col = 0; col < COLS; col++)
                {

                    if (row % 2 == 0)
                    {
                        if (christmasTree[row, col] == '*')
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write(christmasTree[row, col]);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write(christmasTree[row, col]);
                        }
                    }
                    else
                    {
                        if (christmasTree[row, col] == '*')
                        {
                            if (isgreen)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.Write(christmasTree[row, col]);
                                isgreen = !isgreen;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.Write(christmasTree[row, col]);
                                isgreen = !isgreen;
                            }
                        }
                        else
                        {
                            Console.Write(christmasTree[row, col]);
                        }
                    }
                }
                Console.WriteLine();
                Console.ResetColor();
            }
        }
        #endregion

        #region PasswordValidator - Inner Cass 
        internal static class PasswordValidator
        {
            const int PASSED = 1;
            const int DOWNGRADED = 2;
            const int FAILED = 3;
            internal static void MainStart()
            {
                do
                {
                    StartController();
                    Console.WriteLine("Do you want another try? press enter or any key to exit");
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.Key != ConsoleKey.Enter)
                    {
                        break;
                    }

                    
                } while (true);   
                
            }

            #region GUI
            static void WelcomeGUI()
            {
                Console.Clear();
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("|  Welcome to Password Validator     |");
                Console.WriteLine("-------------------------------------");
                Console.WriteLine();
                Console.WriteLine("Please enter your password below");
                Console.Write($"The password can have 3 stages. ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Green ");
                Console.ResetColor();
                Console.Write("for pass / ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Yellow ");
                Console.ResetColor();
                Console.Write("for downgraded / ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Red ");
                Console.ResetColor();
                Console.WriteLine("for not passed");
                Console.WriteLine("Please notice the following rules for the password to pass:");
                Console.WriteLine("Password must be between 12 - 64 characters long. Both included.");
                Console.WriteLine("Password must contain at least one UPPER and one lower letter.");
                Console.WriteLine("Password must contain at least one letter and one digit.");
                Console.WriteLine("Password must contain at least one special character.");
                Console.WriteLine("If the password does not pass these rules, you will be offered a chance to enter a new one.");
                Console.WriteLine();
                Console.WriteLine("Please note:");
                Console.Write("The password will be downgraded to ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Yellow ");
                Console.ResetColor();
                Console.Write("if there are at least 4 repeated characters like: ");
                Console.Write("AAAA or 1111 or $$$$ etc..");
                Console.WriteLine();
                Console.Write("The password will likewise be downgraded to ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Yellow ");
                Console.ResetColor();
                Console.Write("if there are at least 4 sequential digits like: ");
                Console.WriteLine("1234, 5678 etc..");
                Console.WriteLine();
                Console.Write("Please enter your password Here: ");
            }

            static void ShowResultGUI(int result)
            {
                switch(result)
                {
                    case FAILED:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Your password failed! :(");
                        Console.ResetColor();
                        break;
                    case DOWNGRADED:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Your password passed, but was downgraded :|");
                        Console.ResetColor();
                        break;
                    case PASSED:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Your password passed :)");
                        Console.ResetColor();
                        break;
                    default:
                        Console.WriteLine("Choice must of the following PASSED/DOWNGRADED/FAILED");
                        break;
                }
            }

            #endregion

            #region Controller
            static void StartController()
            {
                /* ask GUI to display welcome screen */
                WelcomeGUI();
                string input = InputFromUserModel();
                /* Does the password pass the required rules? */
                if(Between12And64Chars(input) && UpperLowerChars(input) && 
                    LetterDigitChars(input) && SpecialChar(input))
                {
                    //Should the password be downgraded?
                    if(HasRepeatedSquences(input) || HasSequentialDigits(input))
                    {
                        ShowResultGUI(DOWNGRADED);
                    }
                    else
                    {
                        ShowResultGUI(PASSED);
                    }
                }
                else
                {
                    ShowResultGUI(FAILED);
                }
            }
            #endregion
            static string InputFromUserModel()
            {
                return Console.ReadLine();
            }
            #region Model

            #endregion

            #region Validators
            //Chek for Sequential characters
            static bool HasRepeatedSquences(string input)
            {
                bool hasRepeated = false;
                for(int i = 0; i < input.Length -3; i++)
                {
                    if(input[i].Equals(input[i+1]) && input[i+1].Equals(input[i+2]) && input[i +2].Equals(input[i+3]))
                    {
                        hasRepeated = true;
                        break;
                    }
                }
                return hasRepeated;
            }

            static bool HasSequentialDigits(string input)
            {
                bool isSequentialDigits = false;
                for(int i = 0; i < input.Length -3;i++)
                {
                    if(Char.IsDigit(input[i]) && Char.IsDigit(input[i+1]) && Char.IsDigit(input[i + 2]) && Char.IsDigit(input[i + 3]))
                    {
                        //example 1 2 3 4
                        if((input[i] +1 == input[i+1]) && (input[i + 1] +1 == input[i + 2]) && input[i + 2] +1 == input[i + 3]) 
                        {
                            isSequentialDigits = true;
                            break;
                        }
                    }
                }
                return isSequentialDigits;
            }

            //Check for Special characters
            static bool SpecialChar(string input)
            {
                bool special = false;
                for(int i = 0; i < input.Length; i++)
                {
                    if(!special && !Char.IsLetterOrDigit(input[i]))
                    {
                        special = true;
                        break;
                    }
                }    
                return special;
            }
            //Check for letters and digits
            static bool LetterDigitChars(string input)
            {
                bool letter = false;
                bool digit = false;
                for (int i = 0; i < input.Length; i++)
                {
                    if (!letter && Char.IsLetter(input[i]))
                    {
                        letter = true;
                    }
                    if (!digit && Char.IsDigit(input[i]))
                    {
                        digit = true;
                    }
                    if(letter && digit)
                    {
                        break;
                    }
                }
                return letter && digit;
            }
            //Check for upper or lower chars in input
            static bool UpperLowerChars(string input)
            {
                bool upper = false;
                bool lower = false;

                for(int i = 0; i < input.Length; i++)
                {
                    if(!upper && Char.IsLetter(input[i]) && Char.IsUpper(input[i]))
                    {
                        upper = true;
                    }
                    if(!lower && Char.IsLetter(input[i]) && Char.IsLower(input[i]))
                    {
                        lower = true;
                    }
                    if(upper && lower)
                    {
                        break;
                    }
                }
                return upper && lower;
            }
            static bool Between12And64Chars(string input)
            {
                return input.Length >= 12 && input.Length <= 64;
            }
            #endregion
        }
#endregion
    }
}
