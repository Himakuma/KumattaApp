using Grpc.Core;
using MagicOnion;
using MagicOnion.Client;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KumattaAppServer.Hubs
{
    public class ChatApp : MonoBehaviour, IChatAppHubReceiver
    {
        #region �����y�[�W
        /// <summary>
        /// �����y�[�W
        /// </summary>
        [SerializeField]
        private GameObject joinPage;

        /// <summary>
        /// ���[�U�[������
        /// </summary>
        private TMP_InputField nameInput;
        #endregion

        #region �`���b�g�y�[�W
        /// <summary>
        /// �`���b�g�y�[�W
        /// </summary>
        [SerializeField]
        private GameObject chatPage;

        /// <summary>
        /// �`���b�g�\��
        /// </summary>
        private TextMeshProUGUI chatComment;

        /// <summary>
        /// ���b�Z�[�W����
        /// </summary>
        private TMP_InputField messageInput;
        #endregion


        #region MagicOnion�p
        /// <summary>
        /// �ʐM�L�����Z���p�g�[�N��
        /// </summary>
        private CancellationTokenSource shutdownCancellation = new CancellationTokenSource();

        /// <summary>
        /// �ڑ��`�����l��
        /// </summary>
        private ChannelBase channel;

        /// <summary>
        /// �T�[�o�[�ďo���p
        /// </summary>
        private IChatAppHub streamingClient;
        #endregion


        /// <summary>
        /// ���[����
        /// </summary>
        private string roomName = "ChatAPP";

        /// <summary>
        /// ���[�U�[��
        /// </summary>
        private string userName = "������";

        private void Start()
        {
            // ��ʂ��\��
            joinPage.SetActive(false);
            chatPage.SetActive(false);


            // �����y�[�W�ݒ�
            nameInput = joinPage.GetComponentInChildren<TMP_InputField>(true);

            var joinButton = joinPage.GetComponentInChildren<Button>(true);
            joinButton.onClick.AddListener(OnClick_JoinButton);


            // �`���b�g���[���ݒ�
            chatComment = chatPage.GetComponentInChildren<TextMeshProUGUI>(true);
            chatComment.text = "";
            messageInput = chatPage.GetComponentInChildren<TMP_InputField>(true);

            var sendButton = chatPage.GetComponentInChildren<Button>(true);
            sendButton.onClick.AddListener(OnClick_SendButton);

            Init();
        }

        private async void OnDestroy()
        {
            // �ʐM�L�����Z��
            shutdownCancellation.Cancel();

            // �ؒf����
            if (streamingClient != null) await streamingClient.DisposeAsync();
            if (channel != null) await channel.ShutdownAsync();
        }

        #region �{�^������
        /// <summary>
        /// ����
        /// </summary>
        private async void OnClick_JoinButton()
        {
            if (!string.IsNullOrEmpty(nameInput.text))
            {
                userName = nameInput.text;
            }

            await streamingClient.JoinAsync(roomName, userName);
            joinPage.SetActive(false);
            chatPage.SetActive(true);
        }

        /// <summary>
        /// ���b�Z�[�W���M
        /// </summary>
        private async void OnClick_SendButton()
        {
            await streamingClient.SendMessageAsync(userName, messageInput.text);
            messageInput.text = "";
        }
        #endregion

        /// <summary>
        /// ��������
        /// </summary>
        private async void Init()
        {
            // �T�[�o�[�֐ڑ�
            channel = GrpcChannelx.ForAddress("http://localhost:5001");
            streamingClient = await StreamingHubClient.ConnectAsync<IChatAppHub, IChatAppHubReceiver>(channel, this, cancellationToken: shutdownCancellation.Token);
            joinPage.SetActive(true);
        }


        #region MagicOnion�@�T�[�o�[�˃N���C�A���g�̎�M

        /// <summary>
        /// �����ʒm
        /// </summary>
        /// <param name="userName">���[�U�[��</param>
        public void OnJoin(string userName)
        {
            chatComment.text = $"{chatComment.text}{userName}���񂪓������܂����B\n";
        }

        /// <summary>
        /// �ޏo�ʒm
        /// </summary>
        /// <param name="userName">���[�U�[��</param>
        public void OnLeave(string userName)
        {
            chatComment.text = $"{chatComment.text}{userName}���񂪑ޏo���܂����B\n";
        }

        /// <summary>
        /// ���b�Z�[�W�ʒm
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="message"></param>
        public void OnSendMessage(string userName, string message)
        {
            chatComment.text = $"{chatComment.text}{userName}�F{message}\n";
        }
        #endregion
    }
}