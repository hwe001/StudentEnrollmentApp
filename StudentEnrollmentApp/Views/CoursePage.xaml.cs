using StudentEnrollmentApp.Models;
using StudentEnrollmentApp.Services;

namespace StudentEnrollmentApp.Views;

public partial class CoursePage : ContentPage
{
    DatabaseService databaseService;
    SeedDataService seedDataService;
    public CoursePage()
    {
        InitializeComponent();

        databaseService = new DatabaseService();

        seedDataService =
            new SeedDataService(databaseService);

        Loaded += async (s, e) =>
        {
            await seedDataService.SeedData();

            await LoadCourses();
        };

 
    }

    private async void OnAddCourseClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(courseCodeEntry.Text))
        {
            await DisplayAlert(
                "Error",
                "Course code required.",
                "OK");

            return;
        }

        Course course = new Course
        {
            CourseCode = courseCodeEntry.Text,
            CourseName = courseNameEntry.Text,
            Credits = int.Parse(creditsEntry.Text)
        };

        string result = await databaseService.AddCourse(course);

        await DisplayAlert(
            "Course Status",
            result,
            "OK");

        await LoadCourses();
    }

    private async void OnLoadCoursesClicked(object sender, EventArgs e)
    {
        await LoadCourses();
    }

    private async Task LoadCourses()
    {
        coursesCollectionView.ItemsSource =
            await databaseService.GetCourses();
    }
}