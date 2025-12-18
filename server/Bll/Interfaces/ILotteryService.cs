using server.Models;

namespace server.Bll.Interfaces
{
    public interface ILotteryService
    {
        Ticket DoLottery(int giftId);
        List<Ticket> GetWinnersReport();
        decimal GetTotalIncome();

    }
}
