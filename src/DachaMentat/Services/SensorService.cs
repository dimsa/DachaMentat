namespace DachaMentor.Services
{
    public class SensorService
    {
        public async Task<bool> AddIndication(string id, double value)
        {            
            await Task.Delay(500); 
            return await Task.FromResult(true);
        }
    }
}
