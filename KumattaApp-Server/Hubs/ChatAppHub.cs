using MagicOnion.Server.Hubs;
using System.Threading.Tasks;

namespace KumattaAppServer.Hubs
{
    public class ChatAppHub : StreamingHubBase<IChatAppHub, IChatAppHubReceiver>, IChatAppHub
    {
        /// <summary>
        /// ルーム
        /// </summary>
        private IGroup room;

        /// <summary>
        /// ユーザー名
        /// </summary>
        private string userName;


        /// <summary>
        /// 入室通知
        /// </summary>
        /// <param name="roomName">ルーム名</param>
        /// <param name="userName">ユーザー名</param>
        /// <returns></returns>
        public async Task JoinAsync(string roomName, string userName)
        {
            this.userName = userName;
            room = await Group.AddAsync(roomName);
            Broadcast(room).OnJoin(userName);
        }

        /// <summary>
        /// 退室通知
        /// </summary>
        /// <returns></returns>
        public async Task LeaveAsync()
        {
            await room.RemoveAsync(this.Context);
            Broadcast(room).OnLeave(userName);
        }

        /// <summary>
        /// メッセージ通知
        /// </summary>
        /// <param name="userName">ユーザー名</param>
        /// <param name="message">メッセージ</param>
        /// <returns></returns>
        public async Task SendMessageAsync(string userName, string message)
        {
            Broadcast(room).OnSendMessage(userName, message);
            await Task.CompletedTask;
        }

        /// <summary>
        /// 切断通知
        /// </summary>
        /// <returns></returns>
        protected override ValueTask OnDisconnected()
        {
            BroadcastExceptSelf(room).OnLeave(userName);
            return CompletedTask;
        }
    }
}
