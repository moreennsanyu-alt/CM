using NUnit.Framework;

namespace ClinicManager.E2E.Tests
{
    [TestFixture]
    public class EmptyTests : UITestBase
    {
    
        [Test]
        public void TestMethodName()
        {
            var loginWindow = GetWindow("LoginWindow");
            
            
            
        }

        
        public TWindow GetWindow<TWindow>(string automationId) where TWindow : Window
        {
            var window = WindowFinder.FindWindowById(
                                        Automation,
                                        automationId,
                                        timeout: TimeSpan.FromSeconds(15),
                                        pollInterval: TimeSpan.FromMilliseconds(300));     
            return window;
    }
  
}
