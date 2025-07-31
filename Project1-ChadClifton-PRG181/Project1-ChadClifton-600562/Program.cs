using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_ChadClifton_600562
{
    internal class Program
    {   //Creating global variables, and arrays that are used in multiple methods.
        private static int appNo = 1;
        private static char returnTo;
        private static int grantedCredit = 0;
        private static int deniedCredit = 0;

        /*
         * These arrays are the collections that the applicants' will be stored in. Setting the arrays to a large element number ensures that 
         * their is ample space for many applications. But that does mean there is a limit to how many people can apply for the credit at Lawry's store.
         * Therefore, this number may need to increase depending on how many customers will be applying.
        */
        private static string[] applicantNames = new string[1000];
        private static string[] applicantSurname = new string[1000];
        private static char[] employStatus = new char[1000];
        private static int[] yearsInJob = new int[1000];
        private static int[] yearsAtResidence = new int[1000];
        private static double[] salary = new double[1000];
        private static double[] debt = new double[1000];
        private static int[] numChild = new int[1000];

        static void CaptureDetails()
        {
            /*
             * A method that will capture the applicant’s details and store them in arrays.
             * • The data has been formatted appropriately to be saved.
             * • Users can input as many applicants’ data as they wish, until they press N for No when asked, or the maximum number of elements 
             * are filled.
             */

            char moreApp;

            Console.WriteLine("Capture Applicants Details\r\n");
            Console.WriteLine("Enter applicants details below:\r\n");
            
            do      //A Do - While loop is used to prompt the user for the details of the applicants. 
            {
                int index = appNo - 1;

                Console.WriteLine("Applicant {0}. ", appNo);
                Console.Write("First Name: ");
                applicantNames[index] = Console.ReadLine();
                Console.Write("Surname: ");
                applicantSurname[index] = Console.ReadLine();
                Console.Write("Employment status (Y for employed, N for unemployed): ");
                employStatus[index] = char.Parse(Console.ReadLine());
                if (employStatus[index] == 'Y') //If the applicant is not employed, there is no sense in asking how many hours they have worked. 
                {
                    Console.Write("Years in current job: ");
                    yearsInJob[index] = int.Parse(Console.ReadLine());
                }
                else
                {
                    yearsInJob[index] = 0;
                }
                Console.Write("Years at current residence: ");
                yearsAtResidence[index] = int.Parse(Console.ReadLine());
                Console.Write("Monthly salary: R ");
                salary[index] = double.Parse(Console.ReadLine());
                Console.Write("Amount of non-mortgage debt: R ");
                debt[index] = double.Parse(Console.ReadLine());
                Console.Write("Number of children: ");
                numChild[index] = int.Parse(Console.ReadLine());
                Console.WriteLine(" ");                    
                
                Console.Write("Do you want to capture any more applicants? Press N for No or Y for Yes: ");
                moreApp = char.Parse(Console.ReadLine());
                Console.WriteLine(" ");
                if (moreApp == 'N') /*If the user does not have any more applicants to enter,
                                      then the program will automatically take them back to the menu.
                                       But the global arrays and variables values will be stored for later use.*/
                { 
                    Console.Clear(); //Clears the console and returns the screen to black, allowing new information to be displayed at the top.
                    menu();
                    break;
                }
                else
                {
                    appNo++;   //Increases the appNo variable by 1. This variable is used to determine how many applicants there are.
                }
            }
            while(moreApp == 'Y');
        }

        static void CreditApplications()
        {
            /*
             * A method that uses the method and arrays above, to determine if the applicants qualify for credit or not.
             * • Credit is granted if the applicant:
             *  o Is employed and has worked in the same job for more than a year
             *  o Has lived in the same location for at least 2 years
             * • Credit is denied if the applicant:
             *  o Owes two months’ salary in non-mortgage debt
             *  o Has more than six children
             *  
             * It displays all the applicants that have been entered and their details. 
             * It also shows whether their credit was granted or denied.
            */

            Console.WriteLine("Credit Applications\r\n");

            for (int i = 0; i < appNo; i++)  //A for loop that is used to display and check all the applicants that have been entered.
                                             //Only those that have been entered.
            {
                Console.WriteLine("Applicant {0}. ", i+1);
                Console.WriteLine("Full name: {0} {1}", applicantNames[i], applicantSurname[i]);
                if (employStatus[i] == 'Y')
                {
                    Console.WriteLine("Employment status: Employed");
                }
                else
                {
                    Console.WriteLine("Employment status: Unemployed");
                }
                Console.WriteLine("Years in current job: {0}", yearsInJob[i]);                
                Console.WriteLine("Years at current residence: {0}", yearsAtResidence[i]);
                Console.WriteLine("Monthly salary: R {0}", salary[i]);                
                Console.WriteLine("Amount of non-mortgage debt: R {0}", debt[i]);          
                Console.WriteLine("Number of children: {0}", numChild[i]);              
                if (employStatus[i] == 'Y' && yearsInJob[i] > 2 && yearsAtResidence[i] > 2 && debt[i] < (salary[i] * 2) && numChild[i] <= 6)
                    //This if statement checks the array index value that is called, and checks it against the before mentioned requirements.
                {
                    Console.WriteLine("Credit: Granted"); //If the requirements are met, the grantedCredit counter increases by one.
                    grantedCredit++;

                }
                else
                {
                    Console.WriteLine("Credit: Denied"); //If the requirements are not met, the deniedCredit counter increases by one.
                    deniedCredit++;

                }
                Console.WriteLine(" ");
            }

            Console.Write("Would you like to return to the Menu? Y/N: "); //The user may return to the menu whenever they wish.
            returnTo = char.Parse(Console.ReadLine());
            if (returnTo == 'Y')
            {
                Console.Clear();
                menu();
            }
            else
            {
                Console.Clear();
                CreditApplications();
            }
        }

        static void CreditStats()
        {
            /*
             * • The method will display the credit requirements and qualification stats, namely:
             *  o The number of applicants that were granted credit, also the number of those whose credit was denied.
            */

            string rows;
            
            Console.WriteLine("Qualification Stats\r\n");

            rows = "The store grants credit if the applicant:\r\n o Is employed and has worked in the same job for more than a year\r\n o Has lived in the same location for at least 2 years\r\n* However, credit is denied if a person owes two months’ salary in non-mortgage debt or has more than six children\r\n";
            Console.WriteLine(rows);

            Console.WriteLine("Number of granted applications: {0}", grantedCredit); //Displays the amount of applications granted credit.
            Console.WriteLine("Number of denied applications: {0}\r\n", deniedCredit); //Displays the amount of applicants denied credit.

            Console.Write("Would you like to return to the Menu? Y/N: "); //The user may return to the menu whenever they wish.
            returnTo = char.Parse(Console.ReadLine());
            if (returnTo == 'Y')
            {
                Console.Clear();
                menu();
            }
            else
            {
                CreditStats();
            }
        }

        enum Menu
            //An enum that contains the values of the Menu. Used in the Switch case in the method names menu.
        {
            CaptureDetails = 1,
            CreditApplications,
            CreditStats,
            Exit,
        }

        static void menu()
        {
            /*
             * A method that, is a Menu, allows the user to choose what they want to do given the following options:
             * • Capture Details
             * • Check credit qualification
             * • Show qualification stats
             * • Exit the program
            */

            string rows;
            int chosen;

            Console.WriteLine("Lawry Store: Credit.\r\n");
            rows = "1. Capture Details\r\n2. Check credit Applications\r\n3. Show Qualification stats\r\n4. Exit";
            Console.WriteLine(rows);
            Console.Write("\r\nOption Chosen (1-4): ");
            chosen = int.Parse(Console.ReadLine()); //Records the option chosen and is used in the switch case below.
            Console.WriteLine(" ");

            switch (chosen) 
                /*
                 * The switch case loop is used to display the option that was chosen. 
                 * It checks the option above against the value from the Enum, and calls the corresponding method.
                 * Except for Case 4, which closes the program and default, which informs the user that they must try again.
                 */
            {   
                case (int)Menu.CaptureDetails:
                    Console.Clear();
                    CaptureDetails();
                    break;
                case (int)Menu.CreditApplications:
                    Console.Clear();
                    CreditApplications();
                    break;
                case (int)Menu.CreditStats:
                    Console.Clear();
                    CreditStats();
                    break;
                case (int)Menu.Exit:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Incorrect option chosen. Press ENTER to try again.");
                    Console.ReadLine();
                    Console.Clear();
                    menu();
                    break;
            }
        }
        static void Main(string[] args)
        {
            //The Main that calls the Menu method and allows for everything to be displayed.

            menu();
            Console.ReadLine(); //Holds the console so that everything can be displayed and interacted with.
        }
    }
}
