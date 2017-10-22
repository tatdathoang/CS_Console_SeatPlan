/* 
 * Program.cs
 * This program helps user to manage a seat plan with 4 rows and 4 columns.  
 * User can add/remove customer to/from seat plan or automatically fill up the seat plan. 
 * User can display the whole seat plan.
 * 
 * Revision history:
 * Hoang Huu Tat Dat, 2017/08/01: Created
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDatHoangP1
{
    class Program
    {
        //List of the seats 
        public static Seat[,] listSeat = new Seat[4, 4];
        //Store user's choice
        static int answer = -1;

        static void Main(string[] args)
        {
            InitializeSeatList();
            PrintSeatMap();
            do
            {
                answer = PrintMenu();
                Console.WriteLine("\r\n--------------------------------");
                switch (answer)
                {
                    case 1:
                        {
                            if(HasSeatAvailable())
                                AddCustomerToSeat();
                            else
                            {
                                Console.WriteLine("We are sorry, there is no seat available at the moment!");
                                Console.WriteLine("-----------------DONE---------------\r\n");
                            }
                            break;
                        }
                    case 2:
                        {
                            RemoveCustomer();
                            break;
                        }
                    case 3:
                        {
                            PrintSeatMap();
                            break;
                        }
                    case 4:
                        {
                            TestLoad();
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            } while (answer != 5);

            Console.ReadLine();
        }

        /// <summary>
        /// Initialize the seat list
        /// </summary>
        /// 
        private static void InitializeSeatList()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    listSeat[i, j] = new Seat();
                }
            }
        }

        /// <summary>
        /// Print out the menu
        /// </summary>
        /// <returns>User's selection</returns>
        /// 
        public static int PrintMenu()
        {
            int answer = -1;
            do
            {
                Console.Write("Please choose one of these option:" +
                                "\r\n1. Add a customer to the seating plan" +
                                "\r\n2. Remove a customer from the seating plan" +
                                "\r\n3. Display the entire seating plan" +
                                "\r\n4. \"Test Load\" names to the seating plan" +
                                "\r\n5. Exit the Application" + 
                                "\r\nKey in your choice: ");
                answer = ValidateSelection(Console.ReadKey().KeyChar.ToString());
                if (answer == -1)
                    Console.WriteLine("\r\n------------WARNING------------------------" +
                        "\r\nOnly 1 or 2 or 3 or 4 or 5 is accepted. Try again!" +
                        "\r\n-------------------------------");
            } while (answer == -1);
            return answer;
        }

        /// <summary>
        /// Print out the seat list
        /// </summary>
        /// 
        public static void PrintSeatMap()
        {
            Console.WriteLine("\r\nHoang Huu Tat Dat's seat plan:");
            Console.WriteLine("\r\n**********************************************************");
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (!listSeat[i, j].IsAvailable)
                        Console.Write("[" + listSeat[i, j].CustomerName + "]\t");
                    else
                        Console.Write("[Seat {0}-{1}]\t", i, j);
                }
                Console.WriteLine("");
            }
            Console.WriteLine("**********************************************************");
        }

        /// <summary>
        /// Validate if user's choice is valid or not
        /// </summary>
        /// <param name="answer">User's choice</param>
        /// <returns>Validated user's choice</returns>
        public static int ValidateSelection(string answer)
        {
            try
            {
                int result = int.Parse(answer.ToString());
                if (result == 1 || result == 2 || result == 3 || result == 4 || result == 5)
                    return result;
            }
            catch
            {
                return -1;
            }
            return -1;
        }

        /// <summary>
        /// Add new customer to a seat
        /// </summary>
        /// 
        public static void AddCustomerToSeat()
        {
            String customerName = "";
            do
            {
                Console.Write("Please type in customer's name: (Please be noticed that only first 8 characters are storaged): ");
                customerName = Console.ReadLine();
                if(customerName=="")
                    Console.WriteLine("\r\n------------WARNING------------------------" +
                        "\r\nCustomer's name can not be blank. Try again!" +
                        "\r\n-------------------------------");

            } while (customerName == "");

            Console.WriteLine("Here is the seat plan. The ones without name are available");
            PrintSeatMap();
            int selectedRow, selectedColumn;
            do
            {
                GetSeatCoordinate(out selectedRow, out selectedColumn);
                if (!listSeat[selectedRow, selectedColumn].IsAvailable)
                    Console.WriteLine("\r\nIt looks like the seat you chose is not available at the moment. Please try another seat!");
                else
                {
                    listSeat[selectedRow, selectedColumn].CustomerName = customerName;
                    listSeat[selectedRow, selectedColumn].IsAvailable = false;
                    break;
                }
            } while (!listSeat[selectedRow, selectedColumn].IsAvailable);
            Console.WriteLine("\r\n{0} have been added to the seat plan at seat {1},{2}", listSeat[selectedRow, selectedColumn].CustomerName.ToUpper(), selectedRow, selectedColumn);
            Console.WriteLine("-----------------DONE---------------\r\n");
        }

        /// <summary>
        /// Check if there is any available seat in the seat list
        /// </summary>
        /// <returns>True if there is at least one seat available. False if there is no seat left</returns>
        private static bool HasSeatAvailable()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (listSeat[i, j].IsAvailable)
                        return true;
            return false;
        }
        
        /// <summary>
        /// Get the row and column of seat from user
        /// </summary>
        /// <param name="row">Seat's row</param>
        /// <param name="column">Seat's column</param>
        /// 
        private static void GetSeatCoordinate(out int row, out int column)
        {
            int temp;
            row = -1;
            column = -1;
            do
            {
                Console.Write("Please type in your prefered seat's row: ");
                temp = ValidateSeatCoordinate(Console.ReadKey().KeyChar.ToString());
                if (temp != -1)
                    row = temp;
                else
                    Console.WriteLine("\r\n------------------------WARNING------------------------" +
                        "\r\nOnly values less than 4 are accepted. Try again!" +
                        "\r\n-------------------------------");
            } while (temp == -1);

            do
            {
                Console.Write("\r\nPlease type in your prefered seat's column: ");
                temp = ValidateSeatCoordinate(Console.ReadKey().KeyChar.ToString());
                if (temp != -1)
                    column = temp;
                else
                    Console.WriteLine("\r\n------------------------WARNING------------------------" +
                        "\r\nOnly values less than 4 are accepted. Try again!" +
                        "\r\n-------------------------------");
            } while (temp == -1);
        }

        /// <summary>
        /// Validate if the seat coordinate was entered correctly
        /// </summary>
        /// <param name="answer">Seat row or column</param>
        /// <returns>Validated row/column. -1 if not valid</returns>
        /// 
        private static int ValidateSeatCoordinate(string answer)
        {
            try
            {
                int result = int.Parse(answer.ToString());
                if (result<4)
                    return result;
            }
            catch
            {
                return -1;
            }
            return -1;
        }

        /// <summary>
        /// Remove a customer from the seat plan
        /// </summary>
        private static void RemoveCustomer()
        {
            int temp;
            do
            {
                Console.Write("\r\nYou want to remove customer by NAME or by SEAT? (enter 0 for NAME, 1 for SEAT): ");
                temp = ValidateRemoveSelection(Console.ReadKey().KeyChar.ToString());
                if (temp != -1)
                {
                    if (temp == 0)
                        RemoveCustomerByName();
                    else
                        RemoveCustomerBySeat();
                }
                else
                    Console.WriteLine("\r\n------------------------WARNING------------------------" +
                        "\r\nOnly 0 or 1 is accepted. Try again!" +
                        "\r\n-------------------------------");
            } while (temp == -1);
        }

        /// <summary>
        /// Validate if user's choice for RemoveCustomer function is valid or not
        /// </summary>
        /// <param name="answer">User's choice</param>
        /// <returns>Validated user's choice. Return -1 if not valid</returns>
        public static int ValidateRemoveSelection(string answer)
        {
            try
            {
                int result = int.Parse(answer.ToString());
                if (result == 1 || result == 0)
                    return result;
            }
            catch
            {
                return -1;
            }
            return -1;
        }

        /// <summary>
        /// Remove customer from seat list by customer's name
        /// </summary>
        /// 
        private static void RemoveCustomerByName()
        {
            bool removed = false;
            do
            {
                Console.Write("\r\nPlease type in customer's name: ");
                string name = Console.ReadLine();
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                    {
                        if (listSeat[i, j].CustomerName.Trim().ToLower() == name.ToLower())
                        {
                            listSeat[i, j].CustomerName = "";
                            listSeat[i, j].IsAvailable = true;
                            Console.WriteLine("Customer {0} has been removed from the seat plan", name);
                            Console.WriteLine("-----------------DONE---------------\r\n");
                            removed = true;
                            return;
                        }
                    }
                Console.WriteLine("There is no customer name {0} in the seat plan. Please try again!", name);
            } while (removed != true);
        }

        /// <summary>
        /// Remove customer from seat list by seat's coordinate
        /// </summary>
        /// 
        private static void RemoveCustomerBySeat()
        {
            int row, column;
            Console.WriteLine("");
            GetSeatCoordinate(out row, out column);
            listSeat[row, column].CustomerName = "";
            listSeat[row, column].IsAvailable = true;
            Console.WriteLine("\r\nSeat {0},{1} has been made available", row,column);
            Console.WriteLine("-----------------DONE---------------\r\n");
        }

        /// <summary>
        /// Automatic fill up all seat in seat plan.
        /// </summary>
        /// 
        private static void TestLoad()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    listSeat[i, j].CustomerName = "Test Value";
                    listSeat[i, j].IsAvailable = false;
                }

            Console.WriteLine("All seats are filled");
            Console.WriteLine("-----------------DONE---------------\r\n");
        }
    }
}
