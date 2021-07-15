using MagicOnion;
using System.Threading.Tasks;

namespace KumattaAppServer.Hubs
{

    public interface IChatAppHub : IStreamingHub<IChatAppHub, IChatAppHubReceiver>
    {
        /// <summary>
        /// �����ʒm
        /// </summary>
        /// <param name="roomName">���[����</param>
        /// <param name="userName">���[�U�[��</param>
        /// <returns></returns>
        Task JoinAsync(string roomName, string userName);

        /// <summary>
        /// �ގ��ʒm
        /// </summary>
        /// <returns></returns>
        Task LeaveAsync();

        /// <summary>
        /// ���b�Z�[�W�ʒm
        /// </summary>
        /// <param name="userName">���[�U�[��</param>
        /// <param name="message">���b�Z�[�W</param>
        /// <returns></returns>
        Task SendMessageAsync(string userName, string message);
    }
}
