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
    }

    /// <summary>
    /// Metoda k přidaní do skupiny
    /// </summary>
    /// <param name="student"></param>
    public void addToGroup(Student student)
    {
        Students.Add(student);
        RemainingInGroup = RemainingInGroup - 1;
    }
}