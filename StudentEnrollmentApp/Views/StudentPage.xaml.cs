using StudentEnrollmentApp.Models;
using StudentEnrollmentApp.Services;

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