using StudentEnrollmentApp.Models;
using StudentEnrollmentApp.Services;
using System.Text.RegularExpressions;

namespace StudentEnrollmentApp.Views;

public partial class StudentPage : ContentPage
{
    DatabaseService databaseService;
    SeedDataService seedDataService;
    public StudentPage()
    {
        InitializeComponent();

        databaseService = new DatabaseService();

        seedDataService =
            new SeedDataService(databaseService);

        Loaded += async (s, e) =>
        {
            await seedDataService.SeedData();

            await LoadStudents();
        };
    }

    private bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        string pattern =
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        return Regex.IsMatch(email, pattern);
    }

    private async void OnAddStudentClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(nameEntry.Text))
        {
            await DisplayAlert(
                "Error",
                "Student name required.",
                "OK");

            return;
        }
        if (!IsValidEmail(emailEntry.Text))
        {
            await DisplayAlert(
                "Invalid Email",
                "Please enter a valid email address.",
                "OK");

            return;
        }
        Student student = new Student
        {
            Name = nameEntry.Text,
            Email = emailEntry.Text
        };

        await databaseService.AddStudent(student);

        await DisplayAlert(
            "Success",
            "Student added.",
            "OK");

        nameEntry.Text = "";
        emailEntry.Text = "";

        await LoadStudents();
    }

    private async void OnLoadStudentsClicked(object sender, EventArgs e)
    {
        await LoadStudents();
    }

    private async Task LoadStudents()
    {
        studentsCollectionView.ItemsSource =
            await databaseService.GetStudents();
    }
}