using System;
using System.Collections.Generic;
using System.IO;
using FinchAPI;


namespace Finch_Project_One
{
    public enum Command
    {
        none,
        moveforward,
        movebackward,
        stopmotors,
        wait,
        turnright,
        soundon,
        soundoff,
        turnleft,
        ledon,
        ledoff,
        gettemperatures,
        getlightlevelright,
        getlightlevelleft,
        getlightlevelaverage,
        done,
    }
    // **************************************************
    //
    // Title: Finch Control - Alarm System
    // Description: Starter solution with the helper methods,
    //              opening and closing screens, and the menu
    // Application Type: Console
    // Author: Dallas, River
    // Dated Created: 2/15/2020
    // Last Modified: 4/3/2020 
    //
    // **************************************************

    class Program
    {
        //
        //Startup 
        //
        static void Main(string[] args)
        {
            LoginRegister();
            SetTheme();
            OpeningScreen();
            MenuDisplay();
            EndingDisplay();
        }
        static void LoginRegister()
        {
            DisplayScreenHeader("Menu: Login/Create Login");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Have you created a login yet? [ yes | no ]");
            if (Console.ReadLine().ToLower() == "yes")
            {
                Login();
            }
            else
            {
                RegisterUser();
                Login();
            }
        }
        static void RegisterUser()
        {
            string password;
            string username;
            DisplayScreenHeader("Register");
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Enter your new username:");
            username = Console.ReadLine();
            Console.Write("Enter your new password:");
            password = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine();
            WriteLoginInfoData(username, password);
            Console.WriteLine("The password and username that has been entered is now saved.");
            Console.WriteLine($"\tNew Username: {username}");
            Console.WriteLine($"\tNew Password: {password}");
            ContinuePrompt();
        }
        static void WriteLoginInfoData(string username, string password)
        {
            string loginInfoText;
            string datalocation = @"Data/Accounts.txt";
            loginInfoText = username + "," + password;
            File.AppendAllText(datalocation,"\n" +loginInfoText);
            string datalocationtwo = @"C:\Users\river\Desktop\Project_FinchControl.MenuStarter-master-master-master\Project_FinchControl\Data\Accounts.txt";
            File.AppendAllText(datalocationtwo, "\n" + loginInfoText);
        }
        static void Login()
        {
            bool validLogin;
            string username;
            string password;
            do
            {
                DisplayScreenHeader("Menu: Login");
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("Enter user name:");
                username = Console.ReadLine();
                Console.Write("Enter password:");
                password = Console.ReadLine();
                validLogin = ValidateLoginInfo(username, password);
                Console.WriteLine();
                Console.WriteLine();
                if (validLogin)
                {
                    Console.WriteLine("This was a successfull login");
                }
                else
                {
                    Console.WriteLine("This was an unsuccessfull login");
                    Console.WriteLine("Inputted username or password is incorrect");
                    Console.WriteLine("Try entering credentials again");
                }
                ContinuePrompt();
            } while (!validLogin);

        }
        static bool ValidateLoginInfo(string username, string password)
        {
            List<(string username, string password)> registeredUserLoginInfo = new List<(string username, string password)>();
            bool validuser = false;
            registeredUserLoginInfo = LoginInfo();
            foreach ((string username, string password) userLoginInfo in registeredUserLoginInfo)
            {
                if ((userLoginInfo.username == username) && (userLoginInfo.password == password))
                {
                    validuser = true;
                    break;
                }
                
            }
            return validuser;
        }

        static List<(string username, string password)> LoginInfo()
        {
            string datalocation = @"Data/Accounts.txt";
            string[] loginInfoArray;
            (string username, string password) loginInfoTuple;
            List<(string username, string password)> LoginInfo = new List<(string userName, string password)>();
            loginInfoArray = File.ReadAllLines(datalocation);
            foreach (string loginInfoText  in loginInfoArray)
            {
                loginInfoArray = loginInfoText.Split(',');
                loginInfoTuple.username = loginInfoArray[0];
                loginInfoTuple.password = loginInfoArray[1];
                LoginInfo.Add(loginInfoTuple);
            }
            return LoginInfo;

        }


        









        #region user interface
        //
        //Theme
        //
        static void SetTheme()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
        }
        //
        //Menu
        //
        static void MenuDisplay()
        {
            bool quitApplication = false;
            string menuChoice;
            Console.CursorVisible = true;
            Finch finchRobot = new Finch();
            do
            {
                DisplayScreenHeader("Central Menu");
                Console.WriteLine("\ta) Connect Finch Robot To Computer");
                Console.WriteLine();
                Console.WriteLine("\tb) Talent Show");
                Console.WriteLine();
                Console.WriteLine("\tc) Data Recorder");
                Console.WriteLine();
                Console.WriteLine("\td) Alarm System");
                Console.WriteLine();
                Console.WriteLine("\te) User Programming");
                Console.WriteLine();
                Console.WriteLine("\tf) Disconnect Finch Robot");
                Console.WriteLine();
                Console.WriteLine("\tq) Quit");
                Console.WriteLine();
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();
                //
                //User Choice Letter Select
                //
                switch (menuChoice)
                {
                    case "a":
                        ConnectingFinchRobot(finchRobot);
                        break;
                    case "b":
                        TalentShowMenu(finchRobot);
                        break;
                    case "c":
                        DataRecorderMenu(finchRobot);
                        break;
                    case "d":
                        LightAlarmMenu(finchRobot);
                        break;
                    case "e":
                        UserProgrammingMenu(finchRobot);
                        break;
                    case "f":
                        DisconnectFinchRobot(finchRobot);
                        break;
                    case "q":
                        DisconnectFinchRobot(finchRobot);
                        quitApplication = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Enter one letter to go to the corresponding menu.");
                        ContinuePrompt();
                        break;
                }
            } while (!quitApplication);
        }

        //
        //
        //User Programming Menu
        //
        //
        static void UserProgrammingMenu(Finch finchRobot)   
        {
            bool Menuquit = false;
            string menuChoice;
            //
            //Tuple
            //

            (int motorSpeed, double waitSeconds, int LightBrightness) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.waitSeconds = 0;
            commandParameters.LightBrightness = 0;
            //
            //List
            //

            List<Command> commands = new List<Command>();
            //
            //User Programming Menu
            //

            do
            {
                DisplayScreenHeader("Menu: User Programming");
                Console.WriteLine("\ta) Set User Command Parameters");
                Console.WriteLine();
                Console.WriteLine("\tb) Input Commands");
                Console.WriteLine();
                Console.WriteLine("\tc) Display Commands");
                Console.WriteLine();
                Console.WriteLine("\td) Execute Commands");
                Console.WriteLine();
                Console.WriteLine("\tq) Quit");
                Console.WriteLine();
                Console.WriteLine("\t\tEnter Choice");
                Console.WriteLine();
                menuChoice = Console.ReadLine().ToLower();
                //
                //Menu Choice Selection
                //
                switch (menuChoice)
                {
                    case "a":
                        commandParameters = UserProgrammingGetCommandParameters();
                        break;
                    case "b":
                        UserProgrammingGetFinchCommands(commands);
                        break;
                    case "c":
                        UserProgramFinchCommands(commands);
                        break;
                    case "d":
                        UserProgrammingExecuteFinchCommands(finchRobot, commands, commandParameters);
                        break;
                    case "q":
                        Menuquit = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("Enter one letter to go to the corresponding menu.");
                        ContinuePrompt();
                        break;
                }
            } while (!Menuquit);
        }

        static (int motorSpeed, double waitSeconds, int ledBrightness) UserProgrammingGetCommandParameters()
        {
            DisplayScreenHeader("Menu: Command Parameters");

            (int motorspeed, double waitSeconds, int ledBrightness) commandParameters;
            commandParameters.motorspeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waitSeconds = 0;
            //
            //
            //
            Console.WriteLine("\tEnter Desired Motor Speed Within The Parameters [1 - 255]:");
            int.TryParse(Console.ReadLine(), out commandParameters.motorspeed);
            while (commandParameters.motorspeed < 0 || commandParameters.motorspeed > 255 || commandParameters.motorspeed == 0)
            {
                Console.WriteLine("Please enter a correct value.");
                commandParameters.motorspeed = int.Parse(Console.ReadLine());

            }
            Console.WriteLine();
            Console.WriteLine("{0} is a correct value", commandParameters.motorspeed);
            Console.WriteLine();
            Console.WriteLine();
            //
            //
            //
            Console.WriteLine("\tEnter Desired LED Brightness Within The Parameters [1 - 255]:");
            int.TryParse(Console.ReadLine(), out commandParameters.ledBrightness);
            while (commandParameters.ledBrightness < 1 || commandParameters.ledBrightness > 255 || commandParameters.ledBrightness == 0)
            {
                Console.WriteLine("Please enter a correct value.");
                commandParameters.ledBrightness = int.Parse(Console.ReadLine());

            }
            Console.WriteLine();
            Console.WriteLine("{0} is a correct value", commandParameters.ledBrightness);
            Console.WriteLine();
            Console.WriteLine();
            //
            //
            //
            Console.WriteLine("\tEnter Desired Wait Command Duration In Seconds [0 - 10]:");
            double.TryParse(Console.ReadLine(), out commandParameters.waitSeconds);
            while (commandParameters.waitSeconds < 0 || commandParameters.waitSeconds > 10 || commandParameters.waitSeconds == 0)
            {
                Console.WriteLine("Please enter a correct value.");
                commandParameters.waitSeconds = double.Parse(Console.ReadLine());

            }
            Console.WriteLine();
            Console.WriteLine("{0} is a correct value", commandParameters.waitSeconds);
            Console.WriteLine();
            Console.WriteLine();
            //
            //
            //
            Console.WriteLine();
            Console.WriteLine($"\tUser Motor Speed: {commandParameters.motorspeed}");
            Console.WriteLine();
            Console.WriteLine($"\tUser LED Brightness: {commandParameters.ledBrightness}");
            Console.WriteLine();
            Console.WriteLine($"\tUser Wait Command Duration In Seconds: {commandParameters.waitSeconds}");

            DisplayMenuPrompt("Menu: User Programming");

            return commandParameters;

        }

        static void UserProgrammingGetFinchCommands(List<Command> commands)
        {
            Command command = Command.none;
            //
            //command list
            //
            int commandAmount = 1;
            Console.WriteLine();
            Console.WriteLine("\tAvailable Commands For User To Use:");
            Console.WriteLine();
            Console.WriteLine("\t-");
            foreach (string commandName in Enum.GetNames(typeof(Command)))
            {
                Console.Write($"---- {commandName.ToLower()} ----");

                if (commandAmount % 5 == 0) Console.Write("-\n\t-");

                commandAmount++;
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            while (command != Command.done)
            {
                Console.WriteLine();
                Console.WriteLine("\tEnter Desired Command:");
                Console.WriteLine();
                if (Enum.TryParse(Console.ReadLine().ToLower(), out command))
                {
                    commands.Add(command);
                }
                else
                {
                    Console.WriteLine("\t\t-------------------------------------");
                    Console.WriteLine("\t\t-------------------------------------");
                    Console.WriteLine("\t\t  Incorrect Value Try Entering Again ");
                    Console.WriteLine("\t\t-------------------------------------");
                    Console.WriteLine("\t\t-------------------------------------");
                }

            }
        }
        //
        //Display Commands
        //
        static void UserProgramFinchCommands(List<Command> commands)
        {
            DisplayScreenHeader("On Screen: Finch Robot Commands Display");
            foreach (Command command in commands)
            {
                Console.WriteLine();
                Console.WriteLine($"\t{command}");
            }
            DisplayMenuPrompt("Menu: User Programming");
        }
        //
        //Command Execution
        //
        static void UserProgrammingExecuteFinchCommands(Finch finchRobot, List<Command> commands, (int motorspeed, double waitSeconds, int ledbrightness) commandParameters)
        {
            int milliSecondsWait = (int)(1000 * commandParameters.waitSeconds);

            int motorSpeed = commandParameters.motorspeed;

            int ledBrightness = commandParameters.ledbrightness;

            string commandFeedback = "";

            const int MOTOR_TURNING_SPEED = 100;

            DisplayScreenHeader("Menu: Execute Finch Commands");

            Console.WriteLine();
            Console.WriteLine("\tThe Finch Robot can now execute the inputed commands provided by the user");
            Console.WriteLine();
            Console.WriteLine();
            ContinuePrompt();
            //
            //executing 
            //
            
            foreach (Command command in commands)
            {
                switch (command)
                {
                    case Command.none:
                        break;
                    case Command.ledoff:
                        finchRobot.setLED(0, 0, 0);
                        break;
                    case Command.ledon:
                        finchRobot.setLED(ledBrightness, ledBrightness, ledBrightness);
                        break;
                    case Command.soundon:
                        finchRobot.noteOn(800);
                        commandFeedback = Command.soundon.ToString();
                        break;
                    case Command.soundoff:
                        finchRobot.noteOff();
                        commandFeedback = Command.soundoff.ToString();
                        break;
                    case Command.stopmotors:
                        finchRobot.setMotors(0,0);
                        commandFeedback = Command.stopmotors.ToString();
                        break;
                    case Command.movebackward:
                        finchRobot.setMotors(-motorSpeed, -motorSpeed);
                        commandFeedback = Command.movebackward.ToString();
                        break;
                    case Command.moveforward:
                        finchRobot.setMotors(motorSpeed, motorSpeed);
                        commandFeedback = Command.moveforward.ToString();
                        break;
                    case Command.wait:
                        finchRobot.wait(milliSecondsWait);
                        commandFeedback = Command.wait.ToString();
                        break;
                    case Command.gettemperatures:
                        commandFeedback = $"Temperature: {finchRobot.getTemperature().ToString("n1")}\n";
                        break;
                    case Command.getlightlevelright:
                        commandFeedback = $"Light Level Left Sensor: {finchRobot.getLeftLightSensor().ToString("n1")}\n";
                        break;
                    case Command.getlightlevelleft:
                        commandFeedback = $"Light Level Right Sensor: {finchRobot.getRightLightSensor().ToString("n1")}\n";
                        break;
                    case Command.getlightlevelaverage:
                        int averageLightLevel;
                        averageLightLevel = ((finchRobot.getRightLightSensor() + finchRobot.getLeftLightSensor()) / 2);
                        commandFeedback = $"Average Light Level For Both Sensors: {averageLightLevel.ToString("n1")}\n";
                        break;
                    case Command.turnleft:
                        finchRobot.setMotors(-MOTOR_TURNING_SPEED, MOTOR_TURNING_SPEED);
                        commandFeedback = Command.turnleft.ToString();
                        break;
                    case Command.turnright:
                        finchRobot.setMotors(MOTOR_TURNING_SPEED, -MOTOR_TURNING_SPEED);
                        commandFeedback = Command.turnright.ToString();
                        break;
                    case Command.done:
                        commandFeedback = Command.done.ToString();
                        break;
                    default:
                        break;
                }
                Console.WriteLine($"\t{ commandFeedback}");
            }
            Console.WriteLine();
            Console.WriteLine("" + commandFeedback);
            ContinuePrompt();
        }

        #region Light Alarm Menu 
        //
        //Light Alarm Menu 
        //
        //
        //
        //
        //
        static void LightAlarmMenu(Finch finch)
        {
            bool quitMenu = false;
            string menuChoice;
            Console.CursorVisible = true;
            string sensorsMonitor = "";
            string rangeType = "";
            int minMaxThresholdValue = 0;
            int timeToMonitor = 0;

            do
            {
                DisplayScreenHeader("Menu: Light Alarm");
                //
                //user menu choise
                //                
                Console.WriteLine("\ta) Set Sensors to Monitor");
                Console.WriteLine("\tb) Set Range Type");
                Console.WriteLine("\tc) Set Max/Min Threshold Value");
                Console.WriteLine("\td) Set Time to Monitor");
                Console.WriteLine("\te) Set Alarm");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();
                switch (menuChoice)
                {
                    case "a":
                        sensorsMonitor = LightAlarmShowSetSensors();
                        break;
                    case "b":
                        rangeType = LightAlarmShowrangeType();
                        break;
                    case "c":
                        minMaxThresholdValue = LightAlarmSetMinMaxThresholdValue(rangeType, finch);
                        break;
                    case "d":
                        timeToMonitor = LightAlarmSetTimeToMonitor();
                        break;
                    case "e":
                        LightAlarmSetAlarm(finch, sensorsMonitor, rangeType, minMaxThresholdValue, timeToMonitor);
                        break;
                    case "q":
                        quitMenu = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Enter a letter to go to the corresponding menu.");
                        ContinuePrompt();
                        break;
                }

            } while (!quitMenu);
        }

        static void LightAlarmSetAlarm(Finch finch, string sensorsMonitor, string rangeType, int minMaxThresholdValue, int timeToMonitor)
        {
            int secondsElapsed = 0;
            int currentLightValue = 0;
            bool thresholdExceeded = false;
            DisplayScreenHeader("Set Alarm");
            Console.WriteLine($"Sensors to monitor {sensorsMonitor}");
            Console.WriteLine($"Range Type : {rangeType}");
            Console.WriteLine($"Min/Max Threshold value: {minMaxThresholdValue}");
            Console.WriteLine($"Time to monitor: {timeToMonitor}");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press space to begin the monitoring process");
            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine();



            while (secondsElapsed < timeToMonitor && !thresholdExceeded)
            {
                switch (sensorsMonitor)
                {
                    case "left":
                        currentLightValue = finch.getLeftLightSensor();
                        break;
                    case "right":
                        currentLightValue = finch.getRightLightSensor();
                        break;
                    case "both":
                        currentLightValue = (finch.getLeftLightSensor() + finch.getRightLightSensor()) / 2;
                        break;
                }
                switch (rangeType)
                {
                    case "minimum":
                        if (currentLightValue < minMaxThresholdValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;
                    case "maximum":
                        if (currentLightValue > minMaxThresholdValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;

                }
                DisplayScreenHeader("Set Alarm");
                Console.WriteLine($"Sensors to monitor {sensorsMonitor}");
                Console.WriteLine($"Range Type : {rangeType}");
                Console.WriteLine($"Min/Max Threshold value: {minMaxThresholdValue}");
                Console.WriteLine($"Time to monitor: {timeToMonitor}");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("The current light level is " + currentLightValue + ".");
                Console.WriteLine("The current time elapsed in seconds is " + secondsElapsed + ".");
                finch.wait(1000);
                Console.Clear();
                secondsElapsed++;


            }

            if (thresholdExceeded)
            {
                Console.WriteLine($"The {rangeType} threshold value that is {minMaxThresholdValue} was exceeded by the current light sensor value of {currentLightValue}.");
                finch.noteOn(500);
                finch.setLED(255, 0, 0);
                finch.wait(2000);
                finch.noteOff();
                finch.setLED(0, 0, 0);
            }
            else
            {
                Console.WriteLine($"The {rangeType} threshold value that is {minMaxThresholdValue} was never exceeded by the current light sensor value of {currentLightValue}.");
                finch.noteOn(500);
                finch.setLED(0, 255, 0);
                finch.wait(2000);
                finch.noteOn(0);
                finch.setLED(0, 0, 0);
            }

            DisplayMenuPrompt("Menu: Light Alarm");
        }






        static int LightAlarmSetTimeToMonitor()
            {
                int timeToMonitor;
            string userResponse;
            DisplayScreenHeader("Time To Monitor");
            Console.WriteLine();
            Console.WriteLine($"\tEnter the time to monotor;");
            userResponse = Console.ReadLine();
            int.TryParse(userResponse, out timeToMonitor);
            if (timeToMonitor >= 0 && timeToMonitor <= 250)
            {
                Console.WriteLine("You have entered the value of " + timeToMonitor + " as your time to monitor");
                ContinuePrompt();
                return timeToMonitor;
            }
            else
            {
                Console.WriteLine("You have entered an incorrect response, please retry by entering a number ranging from 0-250 here: ");
                userResponse = Console.ReadLine();
                int.TryParse(userResponse, out timeToMonitor);
                Console.WriteLine("You have entered the value of " + timeToMonitor + " as your time to monitor");
                ContinuePrompt();
                return timeToMonitor;
            }
        }






            static int LightAlarmSetMinMaxThresholdValue(string rangeType, Finch finchRobot)
        {
            int minMaxThresholdValue;
            string userResponse;
            DisplayScreenHeader("Min/Max Threshold Value");
            Console.WriteLine($"\tLeft Light Sensor Ambient Light Level Value: {finchRobot.getLeftLightSensor()}");
            Console.WriteLine($"\tLeft Light Sensor Ambient Light Level Value: {finchRobot.getRightLightSensor()}");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"\tEnter the {rangeType} light sensor light level value;");
            userResponse = Console.ReadLine();
            int.TryParse(userResponse, out minMaxThresholdValue);
            if (minMaxThresholdValue >= 0 && minMaxThresholdValue <= 250)
                {
                Console.WriteLine("You have entered the value of " + minMaxThresholdValue + " as your min/max threshold value."); 
                ContinuePrompt();
                return minMaxThresholdValue;
                }
                else
                {
                    Console.WriteLine("You have entered an incorrect response, please retry by entering a number ranging from 0-250 here: ");
                    userResponse = Console.ReadLine();
                    int.TryParse(userResponse, out minMaxThresholdValue);
                    Console.WriteLine("You have entered the value of " + minMaxThresholdValue + " as your min/max threshold value.");
                    ContinuePrompt();
                    return minMaxThresholdValue;
            }
        }
        static string LightAlarmShowSetSensors()
        {

            string sensorsToMonitor;
            DisplayScreenHeader("Sensors to Monitor");
            Console.Write("Select the desired sensor to monitor:");
            Console.Write("[right, left, both]");
            sensorsToMonitor = Console.ReadLine();
            if (sensorsToMonitor.ToLower() == "left" || sensorsToMonitor.ToLower() == "right" || sensorsToMonitor.ToLower() == "left")
            {
                Console.WriteLine("You have selected a correct option. The option you selected is " + sensorsToMonitor + " .");
                DisplayMenuPrompt("Menu: Light Alarm");
                return sensorsToMonitor;
            }
            else
            {
                Console.WriteLine("You have selected an incorrect correct option. Please try again");
                sensorsToMonitor = Console.ReadLine();
                if (sensorsToMonitor.ToLower() == "left" || sensorsToMonitor.ToLower() == "right" || sensorsToMonitor.ToLower() == "left")
                {
                    Console.WriteLine("You have selected a correct option. The option you selected is " + sensorsToMonitor + " .");
                    DisplayMenuPrompt("Menu: Light Alarm");
                    return sensorsToMonitor;
                }
                else
                {
                    Console.WriteLine("You have selected an incorrect correct option. Please try again");
                    sensorsToMonitor = Console.ReadLine();
                    Console.WriteLine("You have selected a correct response. The response you selected is " + sensorsToMonitor + " sensor(s) to monitor."); DisplayMenuPrompt("Menu: Light Alarm");
                    ContinuePrompt();
                    return sensorsToMonitor;
                }
            }
        }


        static string LightAlarmShowrangeType()
        {
            string rangeType; 
            DisplayScreenHeader("Sensors to Monitor");
            Console.Write("Select the desired rangetype:");
            Console.Write("[minimum, maximum]");
            rangeType = Console.ReadLine();
            if (rangeType.ToLower() == "minimum" || rangeType.ToLower() == "maximum")
            {
                Console.WriteLine("You have selected a correct option. The option you selected is " + rangeType + " .");
                DisplayMenuPrompt("Menu: Light Alarm");
                return rangeType;
            }
            else
            {
                Console.WriteLine("You have selected an incorrect correct option. Please try again");
                rangeType = Console.ReadLine();
                if (rangeType.ToLower() == "minimum" || rangeType.ToLower() == "maximum")
                {
                    Console.WriteLine("You have selected a correct option. The option you selected is " + rangeType + " .");
                    DisplayMenuPrompt("Menu: Light Alarm");
                    return rangeType;
                }
                else
                {
                    Console.WriteLine("You have selected an incorrect correct option. Please try again");
                    rangeType = Console.ReadLine();
                    Console.WriteLine("You have selected a correct option. The option you selected is " + rangeType + " .");
                    DisplayMenuPrompt("Menu: Light Alarm");
                    ContinuePrompt();
                    return rangeType;
                }
            }
        }
        #endregion

        #region Everything
        //
        //Talent Show Menu
        //
        static void TalentShowMenu(Finch myFinch)
        {
            bool quitTalentShow = false;
            string menuChoice;
            Console.CursorVisible = true;
            do
            {
                DisplayScreenHeader("Menu: Talent Show");
                //
                //user menu choise
                //                
                Console.WriteLine("\ta) Light and Sound");
                Console.WriteLine("\tb) Song Player");
                Console.WriteLine("\tc) Dance");
                Console.WriteLine("\td) Mixing It Up");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();
                switch (menuChoice)
                {
                    case "a":
                        LightAndSound(myFinch);
                        break;
                    case "b":
                        MusicPlayer(myFinch);
                        break;
                    case "c":
                        FinchDance(myFinch);
                        break;
                    case "d":
                        MixingItUp(myFinch);
                        break;
                    case "q":
                        quitTalentShow = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Enter a letter to go to the corresponding menu.");
                        ContinuePrompt();
                        break;
                }

            } while (!quitTalentShow);
        }
        //
        //Light and Sound Talent Show
        //
        static void LightAndSound(Finch finchRobot)
        {
            Console.CursorVisible = false;
            DisplayScreenHeader("Light and Sound");
            Console.WriteLine("The Finch robot will show you a light show");
            ContinuePrompt();
            finchRobot.setLED(0, 0, 255);
            finchRobot.noteOn(10);
            finchRobot.wait(2000);
            finchRobot.noteOff();
            finchRobot.setLED(0, 255, 0);
            finchRobot.noteOn(500);
            finchRobot.wait(2000);
            finchRobot.noteOff();
            finchRobot.setLED(255, 0, 0);
            finchRobot.noteOn(1000);
            finchRobot.wait(000);
            finchRobot.noteOff();
            finchRobot.setLED(0, 0, 0);
            ContinuePrompt();
            DisplayMenuPrompt("Talent Show Menu");
        }
        //
        //Music
        //
        static void MusicPlayer(Finch finchRobot)
        {
            Console.CursorVisible = false;
            DisplayScreenHeader("Light and Sound");
            Console.WriteLine("The Finch robot will now play you Twinkle Twinkle Little Star");
            ContinuePrompt();
            finchRobot.noteOn(800);
            finchRobot.wait(500);
            finchRobot.noteOff();
            finchRobot.wait(300);
            finchRobot.noteOn(800);
            finchRobot.wait(500);
            finchRobot.noteOff();
            finchRobot.wait(300);
            finchRobot.noteOn(600);
            finchRobot.wait(500);
            finchRobot.noteOff();
            finchRobot.wait(300);
            finchRobot.noteOn(600);
            finchRobot.wait(500);
            finchRobot.noteOff();
            finchRobot.wait(300);
            finchRobot.noteOn(660);
            finchRobot.wait(500);
            finchRobot.noteOff();
            finchRobot.wait(300);
            finchRobot.noteOn(660);
            finchRobot.wait(500);
            finchRobot.noteOff();
            finchRobot.wait(300);
            finchRobot.noteOn(600);
            finchRobot.wait(1000);
            finchRobot.noteOff();
            finchRobot.wait(300);
            finchRobot.noteOn(500);
            finchRobot.wait(500);
            finchRobot.noteOff();
            finchRobot.wait(300);
            finchRobot.noteOn(500);
            finchRobot.wait(500);
            finchRobot.noteOff();
            finchRobot.wait(300);
            finchRobot.noteOn(1000);
            finchRobot.wait(500);
            finchRobot.noteOff();
            finchRobot.wait(300);
            finchRobot.noteOn(1000);
            finchRobot.wait(500);
            finchRobot.noteOff();
            finchRobot.wait(300);
            finchRobot.noteOn(900);
            finchRobot.wait(500);
            finchRobot.noteOff();
            finchRobot.wait(300);
            finchRobot.noteOn(900);
            finchRobot.wait(500);
            finchRobot.noteOff();
            finchRobot.wait(300);
            finchRobot.noteOn(800);
            finchRobot.wait(1000);
            finchRobot.noteOff();
            finchRobot.wait(300);
            finchRobot.noteOff();
            ContinuePrompt();
            DisplayMenuPrompt("Talent Show Menu");
        }
        //
        //
        //
        //
        //
        //Dance Talent Show
        //
        static void FinchDance(Finch finchRobot)
        {
            Console.CursorVisible = false;
            DisplayScreenHeader("Light and Sound");
            Console.WriteLine("The Finch robot will show you a dance move and move in a square");
            ContinuePrompt();
            for (int loop = 0; loop < 8; loop++)
            {
                finchRobot.setMotors(255, 255);
                finchRobot.wait(1000);
                finchRobot.setMotors(100, 0);
                finchRobot.wait(1000);
                finchRobot.setMotors(0, 0);
            }
            for (int looptwo = 0; looptwo < 4; looptwo++)
            {
                finchRobot.setMotors(100, 255);
                finchRobot.wait(1000);
                finchRobot.setMotors(-255, -100);
                finchRobot.wait(1000);
                finchRobot.setMotors(0, 0);
            }
            int squareSize;
            string input;
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("What is your name user?");
            Console.WriteLine();
            Console.WriteLine();
            string userResponse;
            userResponse = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("It is good to meet you " + userResponse + ".");
            Console.WriteLine();
            Console.WriteLine("How big would do you think the square that was made by the robot would be in square feet?");
            Console.WriteLine();
            Console.WriteLine("Enter Choice:");
            input = Console.ReadLine();
            if (Int32.TryParse(input, out squareSize))
            {
                Console.WriteLine();
                Console.WriteLine(userResponse + " you guessed " + squareSize + "and it was actually around one foot squared. Good estimate!");
            }
        }
        //
        //
        //Mixingitup
        //
        //
        static void MixingItUp(Finch finchRobot)
        {
            Console.CursorVisible = false;
            DisplayScreenHeader("Light and Sound");
            Console.WriteLine("The Finch robot will show you a pattern with light, sound, and movement");
            ContinuePrompt();
            for (int looptwo = 0; looptwo < 8; looptwo++)
            {
                finchRobot.setMotors(100, 255);
                finchRobot.setLED(0, 255, 0);
                finchRobot.noteOn(50);
                finchRobot.wait(1000);
                finchRobot.noteOff();
                finchRobot.noteOn(500);
                finchRobot.setMotors(-255, -100);
                finchRobot.setLED(255, 0, 0);
                finchRobot.wait(1000);
                finchRobot.noteOff();
                finchRobot.setMotors(0, 0);
                finchRobot.setLED(0, 0, 0);
            }

        }
        //Data Recorder
        //
        //
        //
        //
        //
        static void DataRecorderMenu(Finch finch)
        {
            double[] temperatures = null;
            double[] fahrenheitTemperatures = null;
            double[] lightLevelRight = null;
            double[] lightLevelLeft = null;
            int amountOfDataPoints = 0;
            double dataPointFrequency = 0;
            bool quitMenu = false;
            string menuChoice;
            Console.CursorVisible = true;
            do
            {
                DisplayScreenHeader("Menu: Data Recorder");
                //
                //user menu choise
                //                
                Console.WriteLine("\ta) Data Point Ammount");
                Console.WriteLine("\tb) Data Point Frequency");
                Console.WriteLine("\tc) Get Data");
                Console.WriteLine("\td) Display Data");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();
                switch (menuChoice)
                {
                    case "a":
                        amountOfDataPoints = DataRecorderGetAmountOfDataPoints();
                        break;
                    case "b":
                        dataPointFrequency = DataRecorderGetDataPointFrequency();
                        break;
                    case "c":
                        temperatures = DataRecorderGetData(amountOfDataPoints, dataPointFrequency, finch);
                        fahrenheitTemperatures = DataRecorderGetFahrenheit(amountOfDataPoints, dataPointFrequency, finch);
                        lightLevelRight = DataRecorderGetDataLightRight(amountOfDataPoints, dataPointFrequency, finch);
                        lightLevelLeft = DataRecorderGetDataLightLeft(amountOfDataPoints, dataPointFrequency, finch);
                        break;
                    case "d":
                        DataRecorderShowData(temperatures);
                        DataRecorderShowDataFahrenheit(fahrenheitTemperatures);
                        DataRecorderShowDataLightsRight(lightLevelRight);
                        DataRecorderShowDataLightLeft(lightLevelLeft);
                        break;
                    case "q":
                        quitMenu = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Enter a letter to go to the corresponding menu.");
                        ContinuePrompt();
                        break;
                }

            } while (!quitMenu);
        }
        //
        //Light Get
        //
        static double[] DataRecorderGetDataLightRight(int amountOfDataPoints, double dataPointFrequency, Finch finchRobot)
        {
            double[] lightLevelRight = new double[amountOfDataPoints];
            DisplayScreenHeader("Get Data");
            Console.WriteLine($"Amount of data points provided: {amountOfDataPoints}");
            Console.WriteLine();
            Console.WriteLine($"Data Point Frequency Provided provided: {dataPointFrequency}");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("The Finch Robot is ready to record data based on the given parameters.");
            ContinuePrompt();

            for (int index = 0; index < amountOfDataPoints; index++)
            {
                lightLevelRight[index] = finchRobot.getRightLightSensor();
                Console.WriteLine($"\tReading {index + 1}: {lightLevelRight[index].ToString("n1")}" + " Right light level");
                int waitTimeSeconds = (int)(dataPointFrequency * 1000);
                finchRobot.wait(waitTimeSeconds);
            }
            ContinuePrompt();
            return lightLevelRight;
        }
        static double[] DataRecorderGetDataLightLeft(int amountOfDataPoints, double dataPointFrequency, Finch finchRobot)
        {
            double[] lightLevelLeft = new double[amountOfDataPoints];
            DisplayScreenHeader("Get Data");
            Console.WriteLine($"Amount of data points provided: {amountOfDataPoints}");
            Console.WriteLine();
            Console.WriteLine($"Data Point Frequency Provided provided: {dataPointFrequency}");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("The Finch Robot is ready to record data based on the given parameters.");
            ContinuePrompt();

            for (int index = 0; index < amountOfDataPoints; index++)
            {
                lightLevelLeft[index] = finchRobot.getLeftLightSensor();
                Console.WriteLine($"\tReading {index + 1}: {lightLevelLeft[index].ToString("n1")}" + " Left light level");
                int waitTimeSeconds = (int)(dataPointFrequency * 1000);
                finchRobot.wait(waitTimeSeconds);
            }
            ContinuePrompt();
            return lightLevelLeft;
        }
        //
        //Light Display
        //Right
        static void DataRecorderShowDataLightsRight(double[] lightLevelRight)
        {
            DisplayScreenHeader("Display Data");
            DataRecorderTableLightRight(lightLevelRight);
            ContinuePrompt();
        }
        static void DataRecorderTableLightRight(double[] lightLevelRight)
        {
            {
                //
                //Table Headers
                //
                Console.WriteLine(
                    "Recording Number".PadLeft(20) +
                    "Light Level.".PadLeft(20)
                    );
                Console.WriteLine(
                    "----------------".PadLeft(20) +
                    "----------------".PadLeft(20)
                    );
                //
                //table data
                //
                for (int index = 0; index < lightLevelRight.Length; index++)
                {
                    Console.WriteLine(
                    (index + 1).ToString().PadLeft(20) +
                    lightLevelRight[index].ToString("n1").PadLeft(20) + " light level right"
                    );
                }
            }
        }
        //
        //Light Display
        //Left
        static void DataRecorderShowDataLightLeft(double[] lightLevelLeft)
        {
            DisplayScreenHeader("Display Data");
            DataRecorderTableLightLeft(lightLevelLeft);
            ContinuePrompt();
        }
        static void DataRecorderTableLightLeft(double[] lightLevelLeft)
        {
            {
                //
                //Table Headers
                //
                Console.WriteLine(
                    "Recording Number".PadLeft(20) +
                    "Light Level.".PadLeft(20)
                    );
                Console.WriteLine(
                    "----------------".PadLeft(20) +
                    "----------------".PadLeft(20)
                    );
                //
                //table data
                //
                for (int index = 0; index < lightLevelLeft.Length; index++)
                {
                    Console.WriteLine(
                    (index + 1).ToString().PadLeft(20) +
                    lightLevelLeft[index].ToString("n1").PadLeft(20) + " light level left"
                    );
                }
            }
        }
        //
        //Celcius Display
        //
        static void DataRecorderShowData(double[] temperatures)
        {
            DisplayScreenHeader("Display Data");
            DataRecorderTable(temperatures);
            ContinuePrompt();
        }
        static void DataRecorderTable(double[] temperatures)
        {
            {
                //
                //Table Headers
                //
                Console.WriteLine(
                    "Recording Number".PadLeft(20) +
                    "Temp.".PadLeft(20)
                    );
                Console.WriteLine(
                    "----------------".PadLeft(20) +
                    "----------------".PadLeft(20)
                    );
                //
                //table data
                //
                for (int index = 0; index < temperatures.Length; index++)
                {
                    Console.WriteLine(
                    (index + 1).ToString().PadLeft(20) +
                    temperatures[index].ToString("n1").PadLeft(20) + " degrees celcius"
                    );
                }
            }
        }

        //
        //fahrenheit Display
        //
        static void DataRecorderShowDataFahrenheit(double[] fahrenheitTemperatures)
        {
            DisplayScreenHeader("Display Data");
            DataRecorderTableFahrenheit(fahrenheitTemperatures);
            ContinuePrompt();
        }
        static void DataRecorderTableFahrenheit(double[] fahrenheitTemperatures)
        {
            {
                //
                //Table Headers
                //
                Console.WriteLine(
                    "Recording Number".PadLeft(20) +
                    "Temp.".PadLeft(20)
                    );
                Console.WriteLine(
                    "----------------".PadLeft(20) +
                    "----------------".PadLeft(20)
                    );
                //
                //table data
                //
                for (int index = 0; index < fahrenheitTemperatures.Length; index++)
                {
                    Console.WriteLine(
                    (index + 1).ToString().PadLeft(20) +
                    fahrenheitTemperatures[index].ToString("n1").PadLeft(20) + " degrees fahrenheit"
                    );
                }
            }
        }
        //
        //CelciusTemperatures
        //
        static double[] DataRecorderGetData(int amountOfDataPoints, double dataPointFrequency, Finch finchRobot)
        {
            double[] temperatures = new double[amountOfDataPoints];
            DisplayScreenHeader("Get Data");
            Console.WriteLine($"Amount of data points provided: {amountOfDataPoints}");
            Console.WriteLine();
            Console.WriteLine($"Data Point Frequency Provided provided: {dataPointFrequency}");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("The Finch Robot is ready to record data based on the given parameters.");
            ContinuePrompt(); 

            for (int index = 0; index < amountOfDataPoints; index++)
            {
                temperatures[index] = finchRobot.getTemperature();
                Console.WriteLine($"\tReading {index + 1}: {temperatures[index].ToString("n1")}" + " degrees celcius");
                int waitTimeSeconds = (int)(dataPointFrequency * 1000);
                finchRobot.wait(waitTimeSeconds);
            }
            ContinuePrompt();
            return temperatures;
        }
        //
        //fahrenheitTemperatures
        //
        //
        //
        //
        //
        //
        static double[] DataRecorderGetFahrenheit(int amountOfDataPoints, double dataPointFrequency, Finch finchRobot)
        {
            double[] fahrenheitTemperatures = new double[amountOfDataPoints];
            DisplayScreenHeader("Get Data");
            Console.WriteLine($"Amount of data points provided: {amountOfDataPoints}");
            Console.WriteLine();
            Console.WriteLine($"Data Point Frequency Provided provided: {dataPointFrequency}");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("The Finch Robot is ready to record data based on the given parameters.");
            ContinuePrompt();

            for (int index = 0; index < amountOfDataPoints; index++)
            {
                double inputTemp;
                double finalTemp;
                inputTemp = Convert.ToDouble(finchRobot.getTemperature());
                finalTemp = (inputTemp * 1.8) + 32;
                fahrenheitTemperatures[index] = finalTemp;
                Console.WriteLine($"\tReading {index + 1}: {fahrenheitTemperatures[index].ToString("n1")}" + " degrees fahrenheit");
                int waitTimeSeconds = (int)(dataPointFrequency * 1000);
                finchRobot.wait(waitTimeSeconds);
            }
            ContinuePrompt();
            return fahrenheitTemperatures;
        }
        //
        //Frequency of Data Points
        //
        static double DataRecorderGetDataPointFrequency()
        {
            double frequencyOfData;
            string userResponse;
            //
            //Validate User Input
            //
            do
            {
                DisplayScreenHeader("Data Points Frequency");
                Console.Write("Please enter the desired frequency of data points in seconds in numerical form between 0 and 100 and click enter. Enter number here: ");
                userResponse = Console.ReadLine();
                double.TryParse(userResponse, out frequencyOfData);
                if (frequencyOfData >= 0 && frequencyOfData <= 99)
                {
                    Console.WriteLine("You have entered a correct value for the frequency of data points.The number you entered is " + frequencyOfData + " seconds.");
                    ContinuePrompt();
                    return frequencyOfData;
                }
                else
                {
                    Console.WriteLine("You have entered an incorrect response, please retry by entering a number ranging from 0-100 here: ");
                    userResponse = Console.ReadLine();
                    double.TryParse(userResponse, out frequencyOfData);
                    ContinuePrompt();
                }
            }
            while (frequencyOfData >= 0 && frequencyOfData <= 99);
            double.TryParse(userResponse, out frequencyOfData);
            return frequencyOfData;
        }
        //
        //Amount of Data Points
        //
        static int DataRecorderGetAmountOfDataPoints()
        {
            int amountOfDataPoints;
            string userResponse;
            //
            //Validate User Input
            //
            do
            {
                DisplayScreenHeader("Data Points Number");
                Console.Write("Please enter the desired amount of data points in numerical form between 0 and 100 and click enter. Enter number here: ");
                userResponse = Console.ReadLine();
                int.TryParse(userResponse, out amountOfDataPoints);
                if (amountOfDataPoints >= 0 && amountOfDataPoints <= 99)
                {
                    Console.WriteLine("You have entered a correct value for the amount of data points.The number you entered is " + amountOfDataPoints);
                    ContinuePrompt();
                    return amountOfDataPoints;
                }
                else
                {
                    Console.WriteLine("You have entered an incorrect response, please retry by entering a number ranging from 0-100 here: ");
                    userResponse = Console.ReadLine();
                    int.TryParse(userResponse, out amountOfDataPoints);
                    ContinuePrompt();
                }
            }
            while (amountOfDataPoints >= 0 && amountOfDataPoints <= 99);
            int.TryParse(userResponse, out amountOfDataPoints);
            return amountOfDataPoints;
        }

        //Disconnect Finch robot
        //
        //
        //
        //
        //
        //
        static void DisconnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Disconnect Finch Robot");

            Console.WriteLine("\tAbout to disconnect from the Finch robot.");
            ContinuePrompt();

            finchRobot.disConnect();

            Console.WriteLine("\tThe Finch robot is now disconnect.");

            DisplayMenuPrompt("Main Menu");
        }
        //Connect Finch Robot
        //
        static bool ConnectingFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            bool robotConnected;

            DisplayScreenHeader("Connect Finch Robot");
            Console.WriteLine("\tAbout to connect to Finch robot. Please be sure the USB cable is connected to the robot and computer now.");
            ContinuePrompt();

            robotConnected = finchRobot.connect();

            // TODO test connection and provide user feedback - text, lights, sounds

            DisplayMenuPrompt("Main Menu");

            //
            // reset finch robot
            //
            finchRobot.setLED(0, 0, 0);
            finchRobot.noteOff();

            return robotConnected;
        }
        //
        //Opening Screen
        //
        static void OpeningScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tFinch Control");
            Console.WriteLine();

            ContinuePrompt();
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Closing Screen                            *
        /// *****************************************************************
        /// </summary>
        static void EndingDisplay()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using Finch Control! ");
            Console.WriteLine();

            ContinuePrompt();
        }

        //
        //display continue prompt
        //
        static void ContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display menu prompt
        /// </summary>
        static void DisplayMenuPrompt(string menuName)
        {
            Console.WriteLine();
            Console.WriteLine($"\tPress any key to return to the {menuName} Menu.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }
    }
    #endregion


}
#endregion