﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;

namespace Ch8tter
{
    public class SFDCRestApi
    {
        private HttpClient httpClient;

        public void GenerateDummyData()
        {
            ChatterFeedDataSource chatterFeedDataSource = (ChatterFeedDataSource)App.Current.Resources["chatterFeedDataSource"];
            if (chatterFeedDataSource != null)
            {
                ChatterFeedItem feedItem1 = new ChatterFeedItem();
                feedItem1.Id = "sdfkldjskldfsjhkdfshj";
                feedItem1.Content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam";
                feedItem1.AuthorName = "Barack Obama";
                feedItem1.CreatedDate = "2012-10-01T16:24:29.000Z";
                chatterFeedDataSource.Items.Add(feedItem1);

                ChatterFeedItem feedItem2 = new ChatterFeedItem();
                feedItem2.Id = "dsfdfsf32sdfsdfdsf";
                feedItem2.Content = "Test Test Test Test";
                feedItem2.AuthorName = "Harry Houdini";
                feedItem2.CreatedDate = "2012-10-01T16:24:29.000Z";
                chatterFeedDataSource.Items.Add(feedItem2);
            }
        }

        public async void Request(String method, String path)
        {
            httpClient = new HttpClient();

            //get session instance
            SFDCSession session = SFDCSession.Instance;

            //add access token to request header
            httpClient.DefaultRequestHeaders.Add("Authorization","OAuth " + session.AccessToken);

            //build request
            String request = session.RequestUrl + path;

            Debug.WriteLine("HTTP Request: "+request);

            try
            {
                //send the request, get a response
                HttpResponseMessage response = await httpClient.GetAsync(request);
                
                Debug.WriteLine(response.ToString());

                Debug.WriteLine(await response.Content.ReadAsStringAsync());
            }
            catch (HttpRequestException hre)
            {
                Debug.WriteLine(hre.Message);
            }
            
        }
    }
}
