
public class DeveloperRepository
{
    public DeveloperRepository()
    {
        SeedDevelopers();
    }
    // we need a variable container that will hold a collection of Developers
    private List<Developer> _developerDb = new List<Developer>();
    //we need to auto increment the developer id
    private int _count = 0;

    //C.R.U.D

    //Create
    public bool AddDeveloper(Developer developer)
    {
        if(developer is null)
        {
            return false;
        }
        else
        {
            //increment the count
            _count++;
            //assign the developer ID to _count
            developer.ID = _count;
            //save to the database
            _developerDb.Add(developer);

            return true;
        }
    }

    //Read All
    public List<Developer> GetDevelopers()
    {
        return _developerDb;
    }

    //Read by Id
    public Developer GetDeveloperById(int id)
    {
        return _developerDb.Find(d=>d.ID == id)!;
    }

    

    //Update
    public bool UpdateDeveloper(int developerId, Developer newDevData)
    {
        Developer oldDevData = GetDeveloperById(developerId);

        if(oldDevData != null)
        {
            oldDevData.FirstName = newDevData.FirstName;
            oldDevData.LastName = newDevData.LastName;
            oldDevData.HasPluralsight = newDevData.HasPluralsight;
            return true;
        }
        return false;
    }

    //Delete
    public bool DeleteDeveloper(int developerId)
    {
        //Check to see if dev exists
        Developer oldDevData = GetDeveloperById(developerId);

        if(oldDevData != null)
        {
            //remove the developer
            return _developerDb.Remove(oldDevData); //.Remove() returns a bool
        }
        //otherwise
        return false;
    }

    //Developers w/o Pluralsight license
    public List<Developer> GetDeveloperWithOutPluralsight()
    {
        //1. We need an empty list
        List<Developer> devsWithOutPS = new List<Developer>();
        //2. Need to loop though the database
        foreach(Developer developer in _developerDb)
        {
            //3. Check to see if the developer doesn't have pluralsight
            if(developer.HasPluralsight == false)
            {
                //4. If true we will add the dev to the database
                devsWithOutPS.Add(developer);
            }
        }
        //5. When all is done we will return...
        return devsWithOutPS;
    }
    public void SeedDevelopers()
    {
        Developer dev1 = new Developer
        {
            FirstName = "Kody",
            LastName = "Terew",
            HasPluralsight = true
        };
        Developer dev2 = new Developer
        {
            FirstName = "Bob",
            LastName = "Billy",
            HasPluralsight = false
        };
        Developer dev3 = new Developer
        {
            FirstName = "Akira",
            LastName = "Kurusu",
            HasPluralsight = true
        };
        Developer dev4 = new Developer
        {
            FirstName = "Goro",
            LastName = "Akechi",
            HasPluralsight = true
        };
        
        AddDeveloper(dev1);
        AddDeveloper(dev2);
        AddDeveloper(dev3);
        AddDeveloper(dev4);
    }
}
