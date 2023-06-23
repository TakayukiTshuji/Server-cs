using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    static void Main()
    {
        // ローカルホストのIPアドレスとポート番号を設定
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");//IPAdressクラスはIPアドレスを提供する。IPAdress.ParseはIPアドレスを設定する
        int port = 8888;//宛先や送信元を特定するために設定

        // TCPListenerを作成し、クライアントからの接続を待機
        TcpListener listener = new TcpListener(ipAddress, port);//TcpListenerクラスはクライアントからの接続を待っている状態
        listener.Start();
        Console.WriteLine("サーバーが開始されました。クライアントからの接続を待機しています...");

        // クライアントとの接続を受け入れる
        TcpClient client = listener.AcceptTcpClient();//接続を受け入れる
        Console.WriteLine("クライアントが接続されました。");

        // ネットワークストリームを取得
        NetworkStream stream = client.GetStream();

        while (true)
        {
            // クライアントからのメッセージを受信
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);


            Console.WriteLine("クライアント: " + message);

            // クライアントにメッセージを送信
            string response = "サーバーからのメッセージ: " + DateTime.Now.ToString();
            byte[] data = Encoding.UTF8.GetBytes(response);
            stream.Write(data, 0, data.Length);
        }
    }
}
