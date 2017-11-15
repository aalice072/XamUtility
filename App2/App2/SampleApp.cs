
using App2.Views;
using Xamarin.Forms;

namespace RestClient
{
    public class SampleApp
    {
        public Page GetInitPage()
        {
            // The root page of your application
            var mainPage = new NavigationPage(new Page1())
            {
                Title = "Rest Client"
            };

            return mainPage;
        }
    }
}
