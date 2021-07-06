using System;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

public class FeedBack
{
    private static FeedBack _instance;
    public static FeedBack Instance
    {
        get
        {
            if (_instance == null)
                _instance = new FeedBack();

            return _instance;
        }
    }

    //发送状态
    private bool _mailSent;
    private bool _isInit = false;
    private Action _successCallback;
    private Action<string> _failCallback;
    private string _sendEmail;
    private string _passward;
    private string _reciveEmail;

    public void Init(string sendEmail, string passward, string reciveEmail)
    {
        _sendEmail = sendEmail;
        _passward = passward;
        _reciveEmail = reciveEmail;
        _isInit = true;
    }

    //发送邮件
    public void SendMail(string themeName, string msg, Action onSuccess = null, Action<string> onFail = null)
    {
        //没有邮件在发送
        _successCallback = onSuccess;
        _failCallback = onFail;
        if (!_isInit)
        {
            _failCallback?.Invoke("请先调用Init接口!");
            return;
        }
        if (!_mailSent)
        {
            //设置邮件正在发送状态
            _mailSent = true;

            string MailBody = msg;

            SmtpClient mailClient = new SmtpClient("smtp.qq.com");

            //指定 SmtpClient 是否使用安全套接字层 (SSL) 加密连接
            mailClient.Port = 587;
            mailClient.EnableSsl = true;
            mailClient.UseDefaultCredentials = false;

            //Credentials登陆SMTP服务器的身份验证.
            mailClient.Credentials = new NetworkCredential(_sendEmail, _passward) as ICredentialsByHost;
            //test@qq.com发件人地址、test@tom.com收件人地址
            MailMessage message = new MailMessage(new MailAddress(_sendEmail), new MailAddress(_reciveEmail));

            message.Body = MailBody;//邮件内容
            message.BodyEncoding = Encoding.UTF8;//内容格式为UTF8
            message.Subject = themeName;//邮件主题
            message.SubjectEncoding = Encoding.UTF8;//标题格式为UTF8 

            //获取或设置用于验证服务器证书的回调
            ServicePointManager.ServerCertificateValidationCallback =
                delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };

            //发送....                                            
            mailClient.SendAsync(message, _reciveEmail);
            //发送回调
            mailClient.SendCompleted += SendCompletedCallback;
        }
    }

    void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
    {
        if (_mailSent)
        {
            if (!e.Cancelled && e.Error == null)
            {
                //设置邮件发送成功状态true
                OnSendSuccess();
            }
            else if (e.Error != null)
            {
                //发送失败
                OnSendFailed(e.Error.Message);
            }
        }
    }

    void OnSendSuccess()
    {
        if (_mailSent)
        {
            _mailSent = false;
            _successCallback?.Invoke();
        }
    }
    void OnSendFailed(string msg)
    {
        if (_mailSent)
        {
            _mailSent = false;
            _failCallback?.Invoke(msg);
        }
    }
}