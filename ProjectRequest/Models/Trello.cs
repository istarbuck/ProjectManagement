using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Net;



namespace ProjectRequest.Models
{
    public class Trello
    {
        public void CreateCard()
        {
            //var serializer = new ManateeSerializer();
            //TrelloConfiguration.Serializer = serializer;
            //TrelloConfiguration.Deserializer = serializer;
            //TrelloConfiguration.JsonFactory = new ManateeFactory();
            //TrelloConfiguration.RestClientProvider = new WebApiClientProvider();
        }


    //    var request = (HttpWebRequest)WebRequest.Create("https://api.trello.com/1/cards");

    //    var postData = "idList=58ebcc9d36e943f39afee321";
    //    postData += "name=CardNameTest";
    //        postData += "desc=CardTestDescription";
    //        postData += "key=18a0487d95aaf89f9ff8ff894fcf4972";
    //        postData += "token=c18f7bb57d79a344065116da15bbafabb7a294a73f38e5f8aee4cb119c346341";

    //        var data = Encoding.ASCII.GetBytes(postData);

    //    request.Method = "POST";
    //        request.ContentType = "application/x-www-form-urlencoded";
    //        request.ContentLength = data.Length;

    //        using (var stream = request.GetRequestStream())
    //        {
    //            stream.Write(data, 0, data.Length);
    //            return stream.Read(data, 0, data.Length).ToString();
    //}
    }
}