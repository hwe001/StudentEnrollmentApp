using StudentEnrollmentApp.Models;
using StudentEnrollmentApp.Services;

namespace StudentEnrollmentApp.Views;

public partial class EnrollmentPage : ContentPage
{
    DatabaseService databaseService;
    SeedDataService seedDataService;
    public EnrollmentPage()
    {
        InitializeComponent();

        databaseService = new DatabaseService();

        seedDataService =
            new SeedDataService(databaseService);

        Loaded += async (s, e) =>
        {
            await seedDataService.SeedData();

            await LoadEnrollments();
        };
    }


    

    private async void OnEnrollClicked(object sender, EventArgs e)
    {
        Enrollment enrollment = new Enrollment
        {
            StudentId = int.Parse(studentIdEntry.Text),
            CourseCode = courseCodeEntry.Text,
            EnrollmentDate = DateTime.Now
        };

        string result =
            await databaseService.AddEnrollment(enrollment);

        await DisplayAlert(
            "Enrollment Status",
            result,
            "OK");

        await LoadEnrollments();
    }

    private async void OnLoadEnrollmentsClicked(object sender, EventArgs e)
    {
        await LoadEnrollments();
    }

    private async Task LoadEnrollments()
    {
        enrollmentsCollectionView.ItemsSource =
            await databaseService.GetEnrollments();
    }
}