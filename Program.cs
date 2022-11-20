using CompositeLists;
using CompositeLists.Class;
using CompositeLists.Enum;

Community community = new Community()
{
    Id= 1,
    Name = "The Unremarkable Community of Not-Lizard People",
    Description = "This community contains members from all across the flat earth. We're all humans here. Doing ordinary human stuff, as you do. As humans.",
    Members = Community.GetSampleMembers()
};

Person newMember1 = new(7, "Stormfly", 27, Sex.Male);
Person newMember2 = new(7, "Zilla", 27, Sex.Female);

//community.FemaleMembers.Add(newMember1); incompatible type
community.FemaleMembers.Add(newMember2);
community.MaleMembers.Add(newMember1);

Console.WriteLine("All members");
Console.WriteLine(community.Members);

Console.WriteLine("Male members");
Console.WriteLine(community.MaleMembers);

Console.WriteLine("Female members");
Console.WriteLine(community.FemaleMembers);
Console.ReadLine();