
//P.O.C.O -> Plain old cSharp object
//Domain Object
public class Developer
{
    public Developer()
    {
        
    }

    public Developer(string firstName, string lastName, bool hasPluralsight)
    {
        FirstName = firstName;
        LastName = lastName;
        HasPluralsight = hasPluralsight;
    }
    //We Need a Primary Key
    public int ID {get;set;}
    public string FirstName {get;set;} = string.Empty;
    public string LastName {get;set;} = string.Empty;
    public string FullName 
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }
    public bool HasPluralsight {get;set;}

    //Its everytime I do: Developer.ToString()
    public override string ToString()
    {
        var str = $"ID: {ID}\n"+
                $"Full Name: {FullName} \n" +
                $"Has Pluralsight Access: {HasPluralsight}\n" +
                "=================================================\n";

        return str;
    }
}
