using System.Reflection;
using System.Security.Permissions;

namespace UnitTests
{
    public class CrossThreadTestRunner
    {
        private Exception? lastException;

        public void RunInMTA(ThreadStart userDelegate)
        {
            Run(userDelegate, ApartmentState.MTA);
        }

        public void RunInSTA(ThreadStart userDelegate)
        {
            Run(userDelegate, ApartmentState.STA);
        }

        private void Run(ThreadStart userDelegate, ApartmentState apartmentState)
        {
            lastException = null;

            Thread thread = new Thread(
              delegate()
              {
                  try
                  {
                      userDelegate.Invoke();
                  }
                  catch (Exception e)
                  {
                      lastException = e;
                  }
              });
            thread.SetApartmentState(apartmentState);

            thread.Start();
            thread.Join();

            if (lastException is not null)
                throw lastException;
        }
    }
}