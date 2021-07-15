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
        #region 入室ページ
        /// <summary>
        /// 入室ページ
        /// </summary>
        [SerializeField]
        private GameObject joinPage;

        /// <summary>
        /// ユーザー名入力
        /// </summary>
        private TMP_InputField nameInput;
        #endregion

        #region チャットページ
        /// <summary>
        /// チャットページ
        /// </summary>
        [SerializeField]
        private GameObject chatPage;

        /// <summary>
        /// チャット表示
        /// </summary>
        private TextMeshProUGUI chatComment;

        /// <summary>
        /// メッセージ入力
        /// </summary>
        private TMP_InputField messageInput;
        #endregion


        #region MagicOnion用
        /// <summary>
        /// 通信キャンセル用トークン
        /// </summary>
        private CancellationTokenSource shutdownCancellation = new CancellationTokenSource();

        /// <summary>
        /// 接続チャンネル
        /// </summary>
        private ChannelBase channel;

        /// <summary>
        /// サーバー呼出し用
        /// </summary>
        private IChatAppHub streamingClient;
        #endregion


        /// <summary>
        /// ルーム名
        /// </summary>
        private string roomName = "ChatAPP";

        /// <summary>
        /// ユーザー名
        /// </summary>
        private string userName = "名無し";

        private void Start()
        {
            // 画面を非表示
            joinPage.SetActive(false);
            chatPage.SetActive(false);


            // 入室ページ設定
            nameInput = joinPage.GetComponentInChildren<TMP_InputField>(true);

            var joinButton = joinPage.GetComponentInChildren<Button>(true);
            joinButton.onClick.AddListener(OnClick_JoinButton);


            // チャットルーム設定
            chatComment = chatPage.GetComponentInChildren<TextMeshProUGUI>(true);
            chatComment.text = "";
            messageInput = chatPage.GetComponentInChildren<TMP_InputField>(true);

            var sendButton = chatPage.GetComponentInChildren<Button>(true);
            sendButton.onClick.AddListener(OnClick_SendButton);

            Init();
        }

        private async void OnDestroy()
        {
            // 通信キャンセル
            shutdownCancellation.Cancel();

            // 切断処理
            if (streamingClient != null) await streamingClient.DisposeAsync();
            if (channel != null) await channel.ShutdownAsync();
        }

        #region ボタン処理
        /// <summary>
        /// 入室
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
        /// メッセージ送信
        /// </summary>
        private async void OnClick_SendButton()
        {
            await streamingClient.SendMessageAsync(userName, messageInput.text);
            messageInput.text = "";
        }
        #endregion

        /// <summary>
        /// 初期処理
        /// </summary>
        private async void Init()
        {
            // サーバーへ接続
            channel = GrpcChannelx.ForAddress("http://localhost:5001");
            streamingClient = await StreamingHubClient.ConnectAsync<IChatAppHub, IChatAppHubReceiver>(channel, this, cancellationToken: shutdownCancellation.Token);
            joinPage.SetActive(true);
        }


        #region MagicOnion　サーバー⇒クライアントの受信

        /// <summary>
        /// 入室通知
        /// </summary>
        /// <param name="userName">ユーザー名</param>
        public void OnJoin(string userName)
        {
            chatComment.text = $"{chatComment.text}{userName}さんが入室しました。\n";
        }

        /// <summary>
        /// 退出通知
        /// </summary>
        /// <param name="userName">ユーザー名</param>
        public void OnLeave(string userName)
        {
            chatComment.text = $"{chatComment.text}{userName}さんが退出しました。\n";
        }

        /// <summary>
        /// メッセージ通知
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="message"></param>
        public void OnSendMessage(string userName, string message)
        {
            chatComment.text = $"{chatComment.text}{userName}：{message}\n";
        }
        #endregion
    }
}