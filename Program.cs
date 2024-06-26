﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using static RemitaGenRRRStatus.WebClientUtil;

namespace RemitaGenRRRStatus
{
 
    public class RemitaGenRRRStatus
    {
        public static string DEMO = "https://demo.remita.net";
        public static string LIVE = "https://login.remita.net";
       
        public static string GENERATE_RRR = "/remita/exapp/api/v1/send/api/echannelsvc/merchant/api/paymentinit";

        public static Headers _header;
        public static List<Header> headers;

        static void Main(string[] args)
        {
            Console.WriteLine("#########################################################");
            Console.WriteLine("## INITIALIZING REMITA GENERATE RRR AND STATUS API ##");
            Console.WriteLine("#########################################################");
            Console.WriteLine(" ");

            Console.WriteLine(" ");
            Console.WriteLine("############################");
            Console.WriteLine("####### GENERATE RRR #######");
            Console.WriteLine("############################");
            Console.WriteLine(" ");
            GenerateRRR();


            Console.WriteLine(" ");
            Console.WriteLine("###################################");
            Console.WriteLine("####### RRR STATUS #######");
            Console.WriteLine("###################################");
            Console.WriteLine(" ");
            CheckRRRStatus();

            Console.ReadLine();
        }

        static void GenerateRRR()
        {
            string merchantId = "2547916";
            string apiKey = "1946";
            string serviceTypeId = "4430731";
            string amount = "200";
            string orderId = generateRequestID();
            string apiHashString = merchantId + serviceTypeId + orderId + amount + apiKey;
            string apiHash = WebClientUtil.SHA512(apiHashString);

            _header = new Headers();
            headers = new List<Header>();
            headers.Add(new Header { header = "Content-Type", value = "application/json" });
            headers.Add(new Header { header = "Authorization", value = "remitaConsumerKey=" + merchantId + ",remitaConsumerToken=" + apiHash });
            _header.headers = headers;

            GenerateRRRRequest generateRRRRequest = new GenerateRRRRequest();
            generateRRRRequest.serviceTypeId = serviceTypeId;
            generateRRRRequest.amount = amount;
            generateRRRRequest.orderId = orderId;
            generateRRRRequest.payerName = "Michelle Alozie";
            generateRRRRequest.payerEmail = "alozie@systemspecs.com.ng";
            generateRRRRequest.payerPhone = "09062067384";
            generateRRRRequest.description = "payment for Donation 3";

            List<CustomField> customFields = new List<CustomField>();
            CustomField customField1 = new CustomField();
            customField1.name = "Customer Bill Account Number";
            customField1.value = "";
            customField1.type = "ALL";
            customFields.Add(customField1);
            generateRRRRequest.customFields = customFields;

            String jsonGenerateRRRRequest = JsonConvert.SerializeObject(generateRRRRequest);
            try
            {
                var response = WebClientUtil.PostResponse(DEMO, GENERATE_RRR, jsonGenerateRRRRequest, _header);
                Console.WriteLine("+++ Gen RRR Response: " + response);
            }
            catch (Exception)
            {
                throw;
            }

        }

        static void CheckRRRStatus()
        {
            string merchantId = "2547916";
            string apiKey = "1946";
            string rrr = "310008256982";
            string apiHash = WebClientUtil.SHA512(rrr + apiKey + merchantId);
            string rrrStatusPath = "/remita/exapp/api/v1/send/api/echannelsvc/" + merchantId + "/" + rrr + "/" + apiHash + "/status.reg";

            _header = new Headers();
            headers = new List<Header>();
            headers.Add(new Header { header = "Content-Type", value = "application/json" });
            headers.Add(new Header { header = "Authorization", value = "remitaConsumerKey="+ merchantId +",remitaConsumerToken="+ apiHash});
            _header.headers = headers;
            
            try
            {
                var response = WebClientUtil.GetResponse(DEMO, rrrStatusPath, _header);
                Console.WriteLine("+++ RRR Status Response: " + response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string generateRequestID()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
