using MagicOnion;
using System.Threading.Tasks;

namespace KumattaAppServer.Hubs
{

    public interface IChatAppHub : IStreamingHub<IChatAppHub, IChatAppHubReceiver>
    {
        /// <summary>
        /// 入室通知
        /// </summary>
        /// <param name="roomName">ルーム名</param>
        /// <param name="userName">ユーザー名</param>
        /// <returns></returns>
        Task JoinAsync(string roomName, string userName);

        /// <summary>
        /// 退室通知
        /// </summary>
        /// <returns></returns>
        Task LeaveAsync();

        /// <summary>
        /// メッセージ通知
        /// </summary>
        /// <param name="userName">ユーザー名</param>
        /// <param name="message">メッセージ</param>
        /// <returns></returns>
        Task SendMessageAsync(string userName, string message);
    }
}
