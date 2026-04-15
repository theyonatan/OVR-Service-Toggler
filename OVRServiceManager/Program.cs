using System.ServiceProcess;

namespace OVRServiceManager
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Attempt to connect to the Oculus VR service
                using (ServiceController ovrService = new ServiceController("OVRService"))
                {
                    if (ovrService == null)
                    {
                        Console.WriteLine("Service not found.");
                        return;
                    }

                    // Check the current status of the service
                    if (ovrService.Status == ServiceControllerStatus.Running)
                    {
                        Console.WriteLine("Service is running, stopping now...");
                        // Stop the service
                        ovrService.Stop();

                        // Wait for the service to stop
                        ovrService.WaitForStatus(ServiceControllerStatus.Stopped);

                        Console.WriteLine("Service has been stopped successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Status: {ovrService.Status}");
                        Console.WriteLine("Treating Service as not running. starting it.");
                        ovrService.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that might have occurred
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            Console.WriteLine("press any key to continue...");
            Console.Read();
        }
    }
}
