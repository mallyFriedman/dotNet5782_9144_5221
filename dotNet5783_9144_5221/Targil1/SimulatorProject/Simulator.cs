using BlApi;

namespace SimulatorProject
{
    public static class Simulator
    {
        private static BlApi.IBl Bl = BlApi.Factory.Get();
        public static event EventHandler? StopSimulator;
        public static event EventHandler? ProgressChange;
        private static bool toContinue = true;

        public static void Run()
        {
                Thread thread = new Thread(startSimulator);
                thread.Start();
           
        }


        // השלמת עצירת ההדמיה
        public static void startSimulator()//תהליכון גולמי לביצוע ההדמיה;
        {

            try
            {
                while (toContinue)
                {
                    int? orderId = Bl.Order.GetOrderToUpdate();
                    if (orderId == null)
                    {
                        if (StopSimulator != null)
                        {
                            StopSimulator(null, EventArgs.Empty);
                        }
                        break;
                    }
                    BO.Order order = Bl.Order.Get((int)orderId);
                    Random rand = new Random();
                    int seconds = rand.Next(1,6);
                    Details det = new Details(order, seconds);
                    if (ProgressChange != null)
                    {
                        ProgressChange(null, det);
                        Thread.Sleep(seconds * 1000);
                        if (order.ShipDate == DateTime.MinValue)
                        {
                            Bl.Order.UpdateShipping(order.Id);
                        }
                        else
                            Bl.Order.UpdateSupply(order.Id);
                    }
                }
            }
            catch (BlObjectNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (BlCannotChangeTheStatusException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (BlNoOrderToUpdateException ex)
            {
                Console.WriteLine(ex.Message);

            }
        }


        public static void stopSimulator()//volatile עצירת ההדמיה;
        {
            toContinue = false;
            if (StopSimulator != null)
                StopSimulator(null, EventArgs.Empty);
        }
    }

    public class Details : EventArgs
    {
        public BO.Order order;
        public int seconds;
        public Details(BO.Order ord, int sec)
        {
            order = ord;
            seconds = sec;
        }
    }
}