namespace KumattaAppServer.Hubs
{
    public interface IChatAppHubReceiver
    {
        /// <summary>
        /// �����ʒm
        /// </summary>
        /// <param name="userName">���[�U�[��</param>
        void OnJoin(string userName);

        /// <summary>
        /// �ޏo�ʒm
        /// </summary>
        /// <param name="userName">���[�U�[��</param>
        void OnLeave(string userName);

        /// <summary>
        /// ���b�Z�[�W�ʒm
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="message"></param>
        void OnSendMessage(string userName, string message);
    }
}
