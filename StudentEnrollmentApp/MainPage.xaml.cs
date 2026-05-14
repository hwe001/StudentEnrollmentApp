using StudentEnrollmentApp.Views;

namespace StudentEnrollmentApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnStudentsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new StudentPage());
    }

    private async void OnCoursesClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CoursePage());
    }

    private async void OnEnrollmentsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EnrollmentPage());
    }
}