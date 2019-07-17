using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Layer.func
{
    class HttpHelper
    {
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            //直接确认，否则打不开
            return true;
        }


        /// <summary> 
        /// GET
        /// </summary> 
        /// <param name="URL">Get地址 </param> 
        /// <param name="cookie">cookie </param>
        /// <param name="encoding">编码 </param>
        /// <returns> </returns> 
        public static string request(string URL, string method, string encoding, string[] header, string reqdata, bool isReRequest, bool isdebug)
        {
            string getString = "";
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse webResponse = null;
            Stream getStream = null;
            StreamReader streamReader = null;

            try
            {
                //如果是https就直接忽略证书
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(URL);
                httpWebRequest.Method = method;
                //15秒超时
                httpWebRequest.Timeout = 15000;

                //设置http头，因为部分不允许直接add，所以要挨个搞清楚能不能add
                if (header.Length > 0)
                {
                    string split = "";
                    foreach (string item in header)
                    {
                        if (item.Trim() != "" && item.Contains(":"))
                        {
                            split = Regex.Split(item, ": ", RegexOptions.IgnoreCase)[0];
                            switch (split.ToLower())
                            {
                                case "accept":
                                    httpWebRequest.Accept = Regex.Split(item, ": ", RegexOptions.IgnoreCase)[1];
                                    break;
                                case "content-length":
                                    httpWebRequest.ContentLength = reqdata.Length;
                                    break;
                                case "content-type":
                                    httpWebRequest.ContentType = Regex.Split(item, ": ", RegexOptions.IgnoreCase)[1];
                                    break;
                                case "expect":
                                    httpWebRequest.Expect = Regex.Split(item, ": ", RegexOptions.IgnoreCase)[1];
                                    break;
                                case "if-modified-since":
                                    httpWebRequest.IfModifiedSince = Convert.ToDateTime(Regex.Split(item, ": ", RegexOptions.IgnoreCase)[1]);
                                    break;
                                case "referer":
                                    httpWebRequest.Referer = Regex.Split(item, ": ", RegexOptions.IgnoreCase)[1];
                                    break;
                                case "transfer-encoding":
                                    httpWebRequest.TransferEncoding = Regex.Split(item, ": ", RegexOptions.IgnoreCase)[1];
                                    break;
                                case "user-agent":
                                    httpWebRequest.UserAgent = Regex.Split(item, ": ", RegexOptions.IgnoreCase)[1];
                                    break;
                                case "host":
                                    httpWebRequest.Host = Regex.Split(item, ": ", RegexOptions.IgnoreCase)[1];
                                    break;
                                case "connection":
                                    //丢弃connection，设置了会报错
                                    break;
                                default:
                                    httpWebRequest.Headers.Add(item);
                                    break;
                            }
                        }

                    }
                }
                if (method == "POST")
                {
                    byte[] reqdatabyte = Encoding.GetEncoding(encoding).GetBytes(reqdata);
                    getStream = httpWebRequest.GetRequestStream();//创建一个Stream,赋值是写入HttpWebRequest对象提供的一个stream里面
                    getStream.Write(reqdatabyte, 0, reqdatabyte.Length);
                    getStream.Close();
                }

                webResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                getStream = webResponse.GetResponseStream();
                if (webResponse.ContentEncoding.ToLower().Contains("gzip"))
                {
                    getStream = new GZipStream(getStream, CompressionMode.Decompress);
                }
                streamReader = new StreamReader(getStream, Encoding.GetEncoding(encoding));
                getString = streamReader.ReadToEnd();

            }
            catch (WebException ex)
            {

                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    Stream data = null;
                    StreamReader reader = null;
                    try
                    {
                        data = (ex.Response as HttpWebResponse).GetResponseStream();
                        reader = new StreamReader(data);
                        getString = reader.ReadToEnd();
                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                        if (data != null)
                        {
                            data.Close();
                        }
                        if (reader != null)
                        {
                            reader.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //有的网站正常情况下也是返回500、409什么的，得处理这个
                getString = "请求异常";
                //服务器返回500等等错误可能是暂时的，所以暂停下当前线程一会再请求
                if (!isReRequest)
                {
                    Thread.Sleep(2000);
                    getString = request(URL, method, encoding, header, reqdata, true, isdebug);
                }
                if (isdebug)
                {
                    getString = "请求异常：" + ex.Message;
                }

            }
            finally
            {
                //关闭流
                if (webResponse != null)
                {
                    webResponse.Close();
                }
                if (getStream != null)
                {
                    getStream.Close();
                }
                if (streamReader != null)
                {
                    streamReader.Close();
                }

            }
            return getString;
        }
    }
}
