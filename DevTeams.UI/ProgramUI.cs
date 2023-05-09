using static System.Console;

public class ProgramUI
{
    //Globally scoped variable conatiner with the Developer Repository Data
    private DeveloperRepository _dRepo = new DeveloperRepository();
    private DevTeamRepository _dtRepo;
    private bool isRunning = true;

    public ProgramUI()
    {
        _dtRepo = new DevTeamRepository(_dRepo);
        SeedData();
    }
    internal void Run()
    {
        runApplication();
    }

    private void runApplication()
    {
        while (isRunning)
        {
            Clear();
            WriteLine("Welcome to your DevTeam Manager Portal!\n" +
                "How can I assist you today?\n" +
                "1. View all Developers\n" +
                "2. Find Developer by ID\n" +
                "3. Add Developer\n" +
                "4. Update Developer\n" +
                "5. Delete developer\n" +
                "6. View all Developer Teams\n" +
                "7. View Developer Team By ID\n" +
                "8. Add Developer Team\n" +
                "9. Update Developer Team\n" +
                "10. Delete Developer Team\n" +
                "11. Developers with PluralSight\n" +
                "12. Add Multiple Developers to Team\n" +
                "00. Leave Application");

            var userInput = ReadLine();

            switch (userInput)
            {
                case "1":
                    ViewAllDevelopers();
                    break;
                case "2":
                    FindDeveloperById();
                    break;
                case "3":
                    AddDeveloper();
                    break;
                case "4":
                    UpdateDeveloper();
                    break;
                case "5":
                    DeleteDeveloper();
                    break;
                case "6":
                    ViewAllDeveloperTeams();
                    break;
                case "7":
                    ViewDeveloperTeamById();
                    break;
                case "8":
                    AddDeveloperTeam();
                    break;
                case "9":
                    UpdateDeveloperTeam();
                    break;
                case "10":
                    DeleteDeveloperTeam();
                    break;
                case "11":
                    DevelopersWithPluralSight();
                    break;
                case "12":
                    AddMultipleDevelopersToTeam();
                    break;
                case "00":
                    isRunning = false;
                    break;
                default:
                    WriteLine("Invalid Selection.");
                    break;
            }
        }


    }

    private void AddMultipleDevelopersToTeam()
    {
        try{
            Console.Clear();
            ViewAllDeveloperTeams();
            List<DeveloperTeam> dTeam = _dtRepo.GetDeveloperTeams();

            if(dTeam.Count() > 0)
            {
                WriteLine("Select a Dev Team by Id");
                int userInput = int.Parse(ReadLine()!);
                DeveloperTeam team = _dtRepo.GetDeveloperTeam(userInput);

                List<Developer> auxDevsInDb = _dRepo.GetDevelopers();

                List<Developer> devsToAdd = new List<Developer>();

                if(team != null)
                {
                    bool hasFilledPositions = false;

                    while(!hasFilledPositions)
                    {
                        if(auxDevsInDb.Count() > 0)
                        {
                            DisplayDevelopersInDb(auxDevsInDb);
                            WriteLine("Do you want to add a developer? y/n?");
                            string userInput2 = ReadLine()!.ToLower();

                            if(userInput2 == "y")
                            {
                                WriteLine("Input Developer ID");
                                int userInput3 = int.Parse(ReadLine()!);
                                Developer dev = _dRepo.GetDeveloperById(userInput3);
                                if(dev != null)
                                {
                                    devsToAdd.Add(dev);
                                    auxDevsInDb.Remove(dev);
                                }
                                else
                                {
                                    WriteLine("The Developer Doesn't Exist.");
                                    PressAnyKeyToContinue();
                                }
                            }
                            else
                            {
                                hasFilledPositions = true;
                            }
                        }
                        else
                        {
                            WriteLine("There are no Developers in the Database.");
                            PressAnyKeyToContinue();
                            break;
                        }
                    }

                    if(_dtRepo.AddMultipleDevelopers(team.ID, devsToAdd))
                    {
                        WriteLine("Success!");
                    }
                    else
                    {
                        WriteLine("Failure!");
                    }
                }
                else
                {
                    WriteLine("Sorry invalid DevTeamId.");
                }
            }

            PressAnyKeyToContinue();
        }
        catch(Exception ex)
        {
            WriteLine(ex.Message);
            WriteLine("Something went wrong");
        }
    }

    private void DevelopersWithPluralSight()
    {
        foreach (var dev in _dRepo.GetDeveloperWithOutPluralsight())
        {
            WriteLine(dev);
            WriteLine($"Do you wish to delete {dev.FullName}? They do not have access to Plural Sight?\n" +
                    "Y/N");
            var userInput = ReadLine();
            bool isDeleted = false;
            if (userInput!.ToLower() == "y")
            {
                isDeleted = _dRepo.DeleteDeveloper(dev.ID);
                WriteLine("Successfully Deleted Developer");
            } else
            {
                WriteLine("Delete Failed");
            }
        }
        PressAnyKeyToContinue();
    }

    private void DeleteDeveloperTeam()
    {
        Clear();
        ViewAllDeveloperTeams();
        WriteLine("Please enter the ID of the Dev Team you wish to delete!");
        var userInput = int.Parse(ReadLine()!);
        bool isDeleted = _dtRepo.DeleteDevTeam(userInput);
        if (isDeleted)
        {
            WriteLine("Successfully Deleted Dev Team!");
        }
        else
        {
            WriteLine("Unsuccessfully Deleted");
        }
        PressAnyKeyToContinue();
    }

    private void UpdateDeveloperTeam()
    {
        Clear();
        ViewAllDeveloperTeams();
        WriteLine("Please enter ID of DevTeam you wish to update.");
        var userInput = int.Parse(ReadLine()!);

        DeveloperTeam devForm = new DeveloperTeam();

        WriteLine("Please Enter Developer Team's New Name");
        var userInput2 = ReadLine();
        devForm.TeamName = userInput2!;

        bool isUpdated = _dtRepo.UpdateDevTeam(userInput, devForm);
        PressAnyKeyToContinue();
    }

    private void AddDeveloperTeam()
    {
        Clear();
        WriteLine("Please enter the Name for the new Dev Team");
        var userInput = ReadLine();
        DeveloperTeam newDevTeam = new DeveloperTeam(userInput!);
        if (_dtRepo.AddDeveloperTeam(newDevTeam))
        {
            WriteLine("Succesffuly Added Team");
        }
        else
        {
            WriteLine("Dev Team Failed to Add");
        }
        PressAnyKeyToContinue();
    }

    private void ViewDeveloperTeamById()
    {
        Clear();
        WriteLine("Please enter the ID of the team you wish to view.");
        var userInput = int.Parse(ReadLine()!);
        DeveloperTeam display = _dtRepo.GetDeveloperTeam(userInput);
        WriteLine($"{display}");
        PressAnyKeyToContinue();
    }

    private void ViewAllDeveloperTeams()
    {
        Clear();
        foreach (var devteam in _dtRepo.GetDeveloperTeams())
        {
            WriteLine($"{devteam}");
        }
        PressAnyKeyToContinue();
    }

    private void DeleteDeveloper()
    {
        Clear();
        ViewAllDevelopers();
        WriteLine("Please enter the ID of the dev you wish to delete!");
        var userInput = int.Parse(ReadLine()!);
        bool isDeleted = _dRepo.DeleteDeveloper(userInput);
        if (isDeleted)
        {
            WriteLine("Successfully Deleted Dev!");
        }
        else
        {
            WriteLine("Unsuccessfully Deleted");
        }
        PressAnyKeyToContinue();
    }

    private void UpdateDeveloper()
    {
        Clear();
        ViewAllDevelopers();
        WriteLine("Please enter ID of Dev you wish to update.");
        var userInput = int.Parse(ReadLine()!);

        Developer devForm = new Developer();

        WriteLine("Please Enter Developer's First Name");
        var userInput2 = ReadLine();
        devForm.FirstName = userInput2!;

        WriteLine("Please Enter Developer's Last Name");
        var userInput3 = ReadLine();
        devForm.LastName = userInput3!;

        WriteLine("Does Developer have access to pluralsight?\n" +
                    "Y/N");
        var userInput4 = ReadLine();
        if (userInput4!.ToLower() == "Y".ToLower())
        {
            devForm.HasPluralsight = true;
        }
        else
        {
            devForm.HasPluralsight = false;
        }

        bool isUpdated = _dRepo.UpdateDeveloper(userInput, devForm);

        if (isUpdated)
        {
            WriteLine("Successfully Updated Developer!");
        }
        else
        {
            WriteLine("Updating Developer Failed");
        }
        PressAnyKeyToContinue();
    }

    private void AddDeveloper()
    {
        Clear();
        //Think of this as a form
        Developer devForm = new Developer();

        WriteLine("Please Enter Developer's First Name");
        var userInput = ReadLine();
        devForm.FirstName = userInput!;

        WriteLine("Please Enter Developer's Last Name");
        var userInput2 = ReadLine();
        devForm.LastName = userInput2!;

        WriteLine("Does Developer have access to pluralsight?\n" +
                    "Y/N");
        var userInput3 = ReadLine();
        if (userInput3!.ToLower() == "Y".ToLower())
        {
            devForm.HasPluralsight = true;
        }
        else
        {
            devForm.HasPluralsight = false;
        }

        if (_dRepo.AddDeveloper(devForm))
        {
            WriteLine("Success! Developer was Made.");
        }
        else
        {
            WriteLine("Failure! Developer was not made.");
        }
        PressAnyKeyToContinue();
    }

    private void FindDeveloperById()
    {
        Clear();
        Developer devTemp = new Developer();
        //Ask for ID
        WriteLine("Please enter Dev's ID.");
        var userInput = ReadLine();
        devTemp = _dRepo.GetDeveloperById(int.Parse(userInput!));
        WriteLine(devTemp);
        PressAnyKeyToContinue();
    }

    private void ViewAllDevelopers()
    {
        Clear();
        foreach (var dev in _dRepo.GetDevelopers())
        {
            WriteLine(dev);
        }
        PressAnyKeyToContinue();
    }

    public void PressAnyKeyToContinue()
    {
        WriteLine("Press Any Key to Continue");
        ReadKey();
    }

    private void SeedData()
    {
        _dRepo.SeedDevelopers();
    }

    private void DisplayDevelopersInDb(List<Developer> auxDevelopers)
    {
        if(auxDevelopers.Count > 0)
        {
            foreach (Developer dev in auxDevelopers)
            {
                WriteLine(dev);
            }
        }
    }
}
