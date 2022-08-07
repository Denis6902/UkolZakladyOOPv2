using System.Collections.Generic;

namespace UkolZakladyOOP;

/// <summary>
/// Skupina předmětu
/// </summary>
public class SubjectGroup
{
    /// <summary>
    /// ID skupiny
    /// </summary>
    public int Id;

    /// <summary>
    /// Učitel
    /// </summary>
    public Teacher Teacher;

    /// <summary>
    /// Studenti v dané skupině
    /// </summary>
    public List<Student> Students = new();

    /// <summary>
    /// Kolik je volných míst
    /// </summary>
    private int RemainingInGroup;

    /// <summary>
    /// Seznam všech skupin
    /// </summary>
    public static List<SubjectGroup> SubjectGroups = new();

    /// <summary>
    /// Konstruktor
    /// </summary>
    /// <param name="teacher">Učitel, který učí v dané skupině</param>
    /// <param name="remainingInGroup"></param>
    public SubjectGroup(Teacher teacher, int remainingInGroup)
    {
        Id = SubjectGroups.Count + 1;
        Teacher = teacher;
        RemainingInGroup = remainingInGroup;
        SubjectGroups.Add(this);
    }

    /// <summary>
    /// Metoda k přidaní do skupiny
    /// </summary>
    /// <param name="student">Student, kterého chceme přidat</param>
    public void addToGroup(Student student)
    {
        Students.Add(student); // přidá studenta do skupiny
        RemainingInGroup = RemainingInGroup - 1; // změní počet volných míst
    }

    /// <summary>
    /// Je ve skupině ještě volné místo??
    /// </summary>
    /// <returns>Je/Není volné místo (true/false)</returns>
    public bool canIJoinGroup(int groupNumber)
    {
        return RemainingInGroup > 0 && groupNumber == Id;
    }

    /// <summary>
    /// Vrátí dostupné skupiny, které mají volné místo
    /// </summary>
    /// <returns>List dostupných skupin</returns>
    public static List<SubjectGroup> returnAvailableSubjectGroups()
    {
        return SubjectGroups.FindAll(SG => SG.RemainingInGroup > 0);
    }
}