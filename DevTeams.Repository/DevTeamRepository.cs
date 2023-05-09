
public class DevTeamRepository
{
    private readonly DeveloperRepository _devRepo;
    
    public DevTeamRepository(DeveloperRepository devRepo)
    {
        _devRepo = devRepo;
        SeedTeams();
    }
    
    private List<DeveloperTeam> _devTeamDb = new List<DeveloperTeam>();
    private int _count = 0;

    //Create
    public bool AddDeveloperTeam(DeveloperTeam developerTeam)
    {
        if(developerTeam is null)
        {
            return false;
        }
        else
        {
            //increment the count
            _count++;
            //assign the developer ID to _count
            developerTeam.ID = _count;
            //save to the database
            _devTeamDb.Add(developerTeam);

            return true;
        }
    }

    //Read All
    public List<DeveloperTeam> GetDeveloperTeams()
    {
        return _devTeamDb;
    }

    //Read by ID
    public DeveloperTeam GetDeveloperTeam(int id)
    {
        return _devTeamDb.Find(d=>d.ID == id)!;
    }
    //Update
    public bool UpdateDevTeam(int devTeamId, DeveloperTeam newDevTeamData)
    {
        DeveloperTeam oldDevTeamData = GetDeveloperTeam(devTeamId);

        if(oldDevTeamData != null)
        {
            oldDevTeamData.TeamName = newDevTeamData.TeamName;
            
            if(newDevTeamData.Developers.Count() >0)
            {
                oldDevTeamData.Developers = newDevTeamData.Developers;
            }
            
            return true;
        }

        return false;
    }

    //Delete
    public bool DeleteDevTeam(int devTeamId)
    {
        DeveloperTeam oldDevTeamData = GetDeveloperTeam(devTeamId);

        if(oldDevTeamData != null)
        {
            return _devTeamDb.Remove(oldDevTeamData);
        }
        return false;
    }

    // public bool AddDeveloperToTeam(int devTeamId, Developer devToAdd)
    // {
    //     DeveloperTeam = 
    // }

    public bool AddMultipleDevelopers(int devTeamId, List<Developer> developersToAdd)
    {
        DeveloperTeam teamInDb = GetDeveloperTeam(devTeamId);

        if(teamInDb != null)
        {
            teamInDb.Developers.AddRange(developersToAdd);
            return true;
        }

        return false;
    }
    
    public void SeedTeams()
    {
        var team = new DeveloperTeam
        {
            ID = 1,
            TeamName = "Team1"  
        };
        team.Developers.Add(_devRepo.GetDeveloperById(3));
        team.Developers.Add(_devRepo.GetDeveloperById(4));
        var team2 = new DeveloperTeam
        {
            ID = 2,
            TeamName = "Team2"
        };
        team2.Developers.Add(_devRepo.GetDeveloperById(1));
        team2.Developers.Add(_devRepo.GetDeveloperById(2));
        AddDeveloperTeam(team);
        AddDeveloperTeam(team2);

    }
}
