namespace KumattaAppServer.Hubs
{
    public interface IChatAppHubReceiver
    {
        /// <summary>
        /// 入室通知
        /// </summary>
        /// <param name="userName">ユーザー名</param>
        void OnJoin(string userName);

        /// <summary>
        /// 退出通知
        /// </summary>
        /// <param name="userName">ユーザー名</param>
        void OnLeave(string userName);

        /// <summary>
        /// メッセージ通知
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="message"></param>
        void OnSendMessage(string userName, string message);
    }
}
