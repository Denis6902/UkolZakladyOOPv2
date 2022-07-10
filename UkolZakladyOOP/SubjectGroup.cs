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
    /// <param name="id"></param>
    /// <param name="teacher"></param>
    /// <param name="remainingInGroup"></param>
    public SubjectGroup(int id, Teacher teacher, int remainingInGroup)
    {
        Id = id;
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
    public bool canIJoinGroup()
    {
        return RemainingInGroup > 0;
    }
}